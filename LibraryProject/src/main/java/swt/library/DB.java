package swt.library;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Comparator;
import java.util.Date;
import java.util.List;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;

public class DB {
	static DB instance;
	static ConnectDatabase DBconnection;
	static Connection conn = null;
	int loggedInID = 1;
	ObservableList<Book> bookList = FXCollections.observableArrayList();
	ObservableList<String> tags = FXCollections.observableArrayList ("Biography", "Erotic", "Fantasy", "Horror", "Humor & Satire", "Children/Youth", "Classics", "Cookbook", "War/Political Novel", "Crime/Thriller", "Romance", "Poetry", "Novel/Narrative", "Nonfiction/Fiction", "Science Fiction", "Other");
	ObservableList<Book> books = FXCollections.observableArrayList();
	ObservableList<Book> topBooks = FXCollections.observableArrayList();
	Comparator<Book> topCompare = new BookComparator();
	
	private DB() throws SQLException {
		 DBconnection = ConnectDatabase.getInstance();
		 conn = ConnectDatabase.conn;
		 try {
			 createTables();
			 insertNecessaryData();
			 generateBookList();
		} catch (SQLException e) {
			System.out.println("Creating necessary tables and data for the Database failed!");
			System.out.println(e.getMessage());
		}
	}
	
	public static DB getInstance() {
		if(instance == null) {
			try {
				instance = new DB();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		return instance;
	}
	
	public boolean checkLogin(String username, String password) throws SQLException {
		PreparedStatement pstmt = DB.conn.prepareStatement("SELECT password FROM user WHERE username = ?");
		pstmt.setString(1, username);
		ResultSet rs = pstmt.executeQuery();

		// User was not found
		if (!rs.next())
			return false;
		
		PreparedStatement pstmt2 = DB.conn.prepareStatement("SELECT user.userID FROM user INNER JOIN role on user.roleID = role.roleID WHERE username = ?");
		pstmt2.setString(1, username);
		ResultSet rs2 = pstmt2.executeQuery();
		
		setLoggedInID(rs2.getInt("userID"));
		
		String passwordFromDb = rs.getString("password");

		return passwordFromDb.equals(password);
	}
	
	public void setLoggedInID(int ID) {
		loggedInID = ID;
	}
	
	public int getLoggedInID() {
		return loggedInID;
	}
	
	public String getLoggedInName() {
		try {
			ResultSet rs = execute("SELECT user.username FROM user INNER JOIN role on user.roleID = role.roleID WHERE userID = " + loggedInID);
			return rs.getString("username");
		} catch (Exception e) {
			System.out.println("Could not find a username to the given userID");
			return "username not found";
		}
	}
	
	public String getLoggedInRole() {
		try {
			ResultSet rs = execute("SELECT role.name FROM user INNER JOIN role on user.roleID = role.roleID WHERE userID = " + loggedInID);
			return rs.getString("name");
		} catch (Exception e) {
			System.out.println("Could not find a role to the given userID");
			return "Role not found";
		}
	}
	
	public ObservableList<Book> getBookList() throws SQLException {
		if(bookList.isEmpty()) return bookList;
		bookList.clear();
		generateBookList();
		return bookList;
	}
	
	public ObservableList<String> getTags() {
		return tags;
	}
	
	@SuppressWarnings("exports")
	public ResultSet execute(String SQL) throws SQLException {
		Statement stmt = conn.createStatement();
		ResultSet rs = stmt.executeQuery(SQL);
		return rs;
	}
	
	public void executeUpdate(String SQL) throws SQLException {
		Statement stmt = conn.createStatement();
		stmt.executeUpdate(SQL);
	}
	
	private void generateBookList() throws SQLException {
		ResultSet rs = execute("SELECT * FROM books");
		
		while(rs.next()) {
			ResultSet rsRating = execute("SELECT AVG(rating) FROM rating WHERE bookID = " + rs.getInt("bookID"));
			Book book = new Book(rs.getInt("bookID"), rs.getString("title"), rs.getString("author"), rs.getString("isbn"), rs.getInt("year"), rs.getString("description"), rs.getString("tags"), rsRating.getFloat(1));
			bookList.add(book);
		}
	}
	
	public ObservableList<Book> getTopBooks() throws SQLException {
		topBooks = getBookList();
		topBooks.sort(topCompare);
		
		for(int i = 0; i < topBooks.size(); i++) {
			if(i > 9) topBooks.remove(i);
		}
		return topBooks;
	}
	
	public ArrayList<Book> getBorrowedBooksForUser() throws SQLException {
		ArrayList<Book> borrowedBooks = new ArrayList<>();
		
		PreparedStatement pstmt = conn.prepareStatement("SELECT * FROM borrow WHERE userID = ? AND returnDate = ''");
		pstmt.setInt(1, getLoggedInID());
		ResultSet rs = pstmt.executeQuery();
		
		while(rs.next()) {
			for(Book bk : getBookList()) {
				if(bk.getBookID() == rs.getInt("bookID")) {
					borrowedBooks.add(bk);
					break;
				}
			}
		}
		
		return borrowedBooks;
	}
	
	public void rateBook(int bookID, int rating) throws SQLException {
		PreparedStatement pstmt = conn.prepareStatement("SELECT * FROM rating WHERE bookID = ? AND userID = ?");
		pstmt.setInt(1, bookID);
		pstmt.setInt(2, getLoggedInID());
		
		ResultSet rs = pstmt.executeQuery();
    	
    	//Rating does not already exist
		if(!rs.next()) {
			PreparedStatement pstmt2 = conn.prepareStatement("INSERT INTO rating(bookID, userID, rating) VALUES (?, ?, ?)");
			pstmt2.setInt(1, bookID);
			pstmt2.setInt(2, getLoggedInID());
			pstmt2.setInt(3, rating);
			pstmt2.executeUpdate();
		} else { //Rating already exists
			PreparedStatement pstmt2 = conn.prepareStatement("UPDATE rating SET rating = ? WHERE bookID = ? AND userID = ?");
			pstmt2.setInt(1, rating);
			pstmt2.setInt(2, bookID);
			pstmt2.setInt(3, getLoggedInID());
			pstmt2.executeUpdate();
		}
	}
	
	public void returnBook(Book bk) throws SQLException {
		SimpleDateFormat formatter = new SimpleDateFormat("dd.MM.yyyy");
		String currentDate = formatter.format(Calendar.getInstance().getTime());
		
		PreparedStatement pstmt = conn.prepareStatement("UPDATE borrow SET returnDate = ? WHERE bookID = ? AND userID = ? AND returnDate = ''");
		pstmt.setString(1, currentDate);
		pstmt.setInt(2, bk.getBookID());
		pstmt.setInt(3, getLoggedInID());
		pstmt.executeUpdate();
	}
	
	public boolean deleteBooks(String title, String author, String isbn, Integer year, String description) throws SQLException{
		PreparedStatement pstmt = DB.conn.prepareStatement("SELECT isbn FROM books WHERE year = ?");
		pstmt.setInt(1, year);
		
		ResultSet rs = pstmt.executeQuery();

		// Book was not found
		if (!rs.next())
			return false;

		pstmt = DB.conn.prepareStatement("DELETE FROM books WHERE isbn = ?");
		pstmt.setString(1, isbn);
		pstmt.executeUpdate();

		submitdeleteBooks(title , author, isbn, year, description);
		return true;
	}
	
	public void submitdeleteBooks(String title, String author, String isbn, Integer year, String description) throws SQLException{
		PreparedStatement pstmt = DB.conn.prepareStatement("INSERT INTO deleted_books(title,author,isbn,year,description) VALUES (?,?,?,?,?);");
		pstmt.setString(1, title);
		pstmt.setString(2, author);
		pstmt.setString(3, isbn);
		pstmt.setInt(4, year);
		pstmt.setString(5, description);
		pstmt.executeUpdate();
	}
	
	/**
	 * Checks if the book is borrowed at the moment
	 * @param BookID
	 * @return true if the book is available at the moment false otherwise
	 * @throws SQLException
	 */
	public boolean bookAvailable(int BookID) throws SQLException {
		PreparedStatement pstmt = conn.prepareStatement("SELECT returnDate FROM borrow WHERE bookID = ? AND returnDate = ?");
		pstmt.setInt(1, BookID);
		pstmt.setString(2, "");
		
		ResultSet rs = pstmt.executeQuery();
		
		if(!rs.next() || (rs.getString("returnDate") != null && !rs.getString("returnDate").equals(null) && !rs.getString("returnDate").equals(""))) {
			return true;
		}else {
			return false;
		}
	}
	
	/**
	 * 
	 * @return a Integer list which contains all years between the oldest and the newest book
	 * @throws SQLException
	 */
	public List<Integer> yearRangeList() throws SQLException{
		ResultSet rs = execute("SELECT MIN(year), MAX(year) FROM books");
		
		List<Integer> yearRange = new ArrayList<>();
		yearRange.add(null);
	
		for (int i = rs.getInt(1); i < rs.getInt(2) + 1; i++) {
			yearRange.add(i);
		}
		
		return yearRange;
	}
	
	/**
	 * Borrows the book to the given user for the given period of time
	 * @param bookID
	 * @param userID
	 * @param borrowWeeks (Weeks the book will be borrowed from today)
	 * @throws SQLException
	 */
	public void borrowBook(int bookID, int userID, int borrowWeeks) throws SQLException{
		Calendar cal = Calendar.getInstance();
		SimpleDateFormat formatter = new SimpleDateFormat("dd.MM.yyy");
		
        Date currentTime = cal.getTime();
        cal.add(Calendar.WEEK_OF_YEAR, borrowWeeks);
        Date returnTime = cal.getTime();
        
		PreparedStatement pstmt = conn.prepareStatement("INSERT INTO borrow(bookID, userID, 'from', 'to', returnDate) VALUES(?,?,?,?,?)");
		pstmt.setInt(1, bookID);
		pstmt.setInt(2, userID);
		pstmt.setString(3, formatter.format(currentTime));
		pstmt.setString(4, formatter.format(returnTime));
		pstmt.setString(5, "");
		pstmt.executeUpdate();
	}
	
	private void createTables() throws SQLException {
		executeUpdate("CREATE TABLE IF NOT EXISTS user (\n" + "	\"userID\"	INTEGER,\n" + "	\"firstname\"	TEXT,\n"
				+ "	\"lastname\"	TEXT,\n" + "password TEXT, \n" + "mail TEXT, " + "username TEXT, " + "roleID INTEGER,"
				+ "PRIMARY KEY(\"userID\" AUTOINCREMENT)\n" + ");");
		executeUpdate("CREATE TABLE IF NOT EXISTS books (\n" + "	\"bookID\"	INTEGER,\n" + "	\"title\"	TEXT,\n"
				+ "	\"author\"	TEXT,\n" + "isbn TEXT, \n" + "year INTEGER, " + "description TEXT," + "tags TEXT,"
				+ "PRIMARY KEY(\"bookID\" AUTOINCREMENT)\n" + ");");
		executeUpdate("CREATE TABLE IF NOT EXISTS\"role\" (\r\n"
				+ "	\"roleID\"	INTEGER NOT NULL,\r\n"
				+ "	\"name\"	TEXT,\r\n"
				+ "	PRIMARY KEY(\"roleID\" AUTOINCREMENT)\r\n" + ");");
		executeUpdate("CREATE TABLE IF NOT EXISTS \"borrow\" (\r\n"
				+ "	\"borrowID\"	INTEGER NOT NULL,\r\n"
				+ "	\"bookID\"	INTEGER,\r\n"
				+ "	\"userID\"	INTEGER,\r\n"
				+ "	\"from\"	TEXT,\r\n"
				+ "	\"to\"	TEXT,\r\n"
				+ "	PRIMARY KEY(\"borrowID\" AUTOINCREMENT)\r\n"
				+ ");");
		executeUpdate("CREATE TABLE IF NOT EXISTS \"rating\" (\r\n"
				+ "	\"ratingID\"	INTEGER NOT NULL,\r\n"
				+ "	\"bookID\"	INTEGER,\r\n"
				+ "	\"userID\"	INTEGER,\r\n"
				+ "	\"rating\"	INTEGER,\r\n"
				+ "	PRIMARY KEY(\"ratingID\" AUTOINCREMENT)\r\n"
				+ ");");
	}
	
	private void insertNecessaryData() throws SQLException {
		executeUpdate("INSERT OR IGNORE INTO role(roleID, name) VALUES (1, 'admin'), (2, 'user')");
		executeUpdate("INSERT OR IGNORE INTO user(userID, firstname, lastname, password, mail, username, roleID) VALUES (1, 'Karol', 'Fedurko', 'test', 'kafeit00@hs-esslingen.de', 'admin', 1),"
				+ "(2, 'Max', 'Mustermann', 'MusterPW', 'Max@Mustermann.de', 'Max2003', 2)");
	}
}