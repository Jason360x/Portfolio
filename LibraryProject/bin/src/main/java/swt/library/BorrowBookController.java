package swt.library;

import java.io.IOException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.control.ComboBox;
import javafx.stage.Stage;

public class BorrowBookController {

	
	@FXML
	private Button btnFrontPage;
	@FXML
	private Label lblLoginDetails;
	@FXML
	private TextField txtSearchTitle, txtSearchAuthor, txtSearchISBN;
	@FXML
	private ComboBox<String> cbSearchTag, cbBorrowtime;
	@FXML
	private ComboBox<Integer> cbSearchFromYear, cbSearchToYear, cbSearchRating;
	@FXML
	private TableView<Book> tvSelectedBooks, tvAvailableBooks;
	@FXML
	private TableColumn<Book, String> tvcSelectedTitle, tvcSelectedAuthor, tvcSelectedISBN, tvcSelectedDescription, tvcSelectedTags, tvcAvailableTitle, tvcAvailableAuthor, tvcAvailableISBN, tvcAvailableDescription, tvcAvailableTags;
	@FXML
	private TableColumn<Book, Integer> tvcSelectedYear, tvcAvailableYear;
	@FXML
	private TableColumn<Book, Float> tvcSelectedRating, tvcAvailableRating;
	@FXML
	private TableColumn<Book, CheckBox> tvcAvailableSelect;
	
	private ObservableList<Book> availablBookList = FXCollections.observableArrayList();
	
	//All books which are not borrowed at the moment will be shown in the Table and the search-items gets necessary data
	public void initialize(){
		lblLoginDetails.setText("Logged in as: " + DB.getInstance().getLoggedInName() + " in Role: " + DB.getInstance().getLoggedInRole());
		
		try {
			initTable();
			UpdateTable();
			initItems();
		} catch (SQLException e) {
			System.out.println("Failed init Items!");
			System.out.println(e.getMessage());
		}
	}
	
	//Opens the Front Page
	public void openFrontPage() throws IOException {
		Parent root = FXMLLoader.load(getClass().getResource("frontPage.fxml"));
		Stage window = (Stage) btnFrontPage.getScene().getWindow();
		window.setResizable(false);
		window.setScene(new Scene(root));
	}
	
	//Creats a SQL Statement based on the given search input and shows the books which are matching
	public void SearchBook() {
		try {
			String sql = "SELECT bookID FROM books WHERE bookID NOT IN (SELECT bookID FROM borrow WHERE returnDate = '')";
			String addSql = "";
			if(cbSearchFromYear.getSelectionModel().getSelectedItem() != null) {
				addSql += " AND year >= " + cbSearchFromYear.getSelectionModel().getSelectedItem();
			}
			if(cbSearchToYear.getSelectionModel().getSelectedItem() != null) {
				addSql += " AND year <= " + cbSearchToYear.getSelectionModel().getSelectedItem();
			}
			if(!txtSearchTitle.getText().equals("")) {
				addSql += " AND title = '" + txtSearchTitle.getText() + "'";
			}
			if(!txtSearchAuthor.getText().equals("")) {
				addSql += " AND author = '" + txtSearchAuthor.getText() + "'";
			}
			if(!txtSearchISBN.getText().equals("")) { 
				addSql += " AND isbn = '" + txtSearchISBN.getText() + "'";
			}	
			if(cbSearchTag.getSelectionModel().getSelectedItem() != null) {
				addSql += " AND tags LIKE '%" + cbSearchTag.getSelectionModel().getSelectedItem() + "%'";
			}
			if(cbSearchRating.getSelectionModel().getSelectedItem() != null) {
				addSql += " AND bookID = (SELECT bookID FROM rating WHERE rating >" + cbSearchRating.getSelectionModel().getSelectedItem() + ")";
			}
			
			sql += addSql + "OR NOT EXISTS (SELECT bookID FROM borrow WHERE returnDate = '')" + addSql;
			
			ResultSet rs = DB.getInstance().execute(sql);
			availablBookList.clear();
			
			do {
				for(Book bk : DB.getInstance().getBookList()){
					if(rs.getInt("bookID") == bk.getBookID()) {
						availablBookList.add(bk);
					}
				}
				rs.next();
			} while (rs.next());
		} catch (SQLException e) {
			System.out.println("Search for book failed!");
			System.out.println(e.getMessage());
		}
	}
	
	//The books which are selected will be borrowed for the given Time
	public void BorrowBook(){
		if(cbBorrowtime.getSelectionModel().getSelectedItem() != null && !cbBorrowtime.getSelectionModel().getSelectedItem().equals("")) {
			int borrowWeeks = Integer.parseInt(cbBorrowtime.getSelectionModel().getSelectedItem().split("")[0]);
			Calendar cal = Calendar.getInstance();
	        SimpleDateFormat formatter = new SimpleDateFormat("dd.MM.yyy");
	        
	        Date currentTime = cal.getTime();
	        cal.add(Calendar.WEEK_OF_YEAR, borrowWeeks);
	        Date returnTime = cal.getTime();
	        
	        for(Book bk: tvSelectedBooks.getItems()) {	
				try {
					PreparedStatement pstmt = DB.getInstance().conn.prepareStatement("INSERT INTO borrow(bookID, userID, 'from', 'to', returnDate) VALUES(?,?,?,?,?)");
					pstmt.setInt(1, bk.getBookID());
					pstmt.setInt(2, DB.getInstance().getLoggedInID());
					pstmt.setString(3, formatter.format(currentTime));
					pstmt.setString(4, formatter.format(returnTime));
					pstmt.setString(5, "");
					pstmt.executeUpdate();
				} catch (SQLException e) {
					System.out.println("Failed to borrow book with ID: " + bk.getBookID());
					System.out.println(e.getMessage());
				}
	        }
	        tvSelectedBooks.getItems().clear();
			UpdateTable();
		}else {
			System.out.println("Not a valid borrowtime");
		}
	}
	
	//clears the Table and shows all books which are currently not borrowed
	private void UpdateTable() {
		try {
			availablBookList.clear();
			
			for(Book bk : DB.getInstance().getBookList()){
				PreparedStatement pstmt = DB.getInstance().conn.prepareStatement("SELECT returnDate FROM borrow WHERE bookID = ?");
				pstmt.setInt(1, bk.getBookID());
				
				ResultSet rs = pstmt.executeQuery();
				
				if(!rs.next() || (rs.getString("returnDate") != null && !rs.getString("returnDate").equals(null) && !rs.getString("returnDate").equals(""))) {
					availablBookList.add(bk);
				}
				
				CheckBox select = new CheckBox();
				select.selectedProperty().addListener(new ChangeListener<Boolean>() {
					@Override
					public void changed(ObservableValue<? extends Boolean> observable, Boolean oldValue, Boolean newValue) {
						if(select.isSelected()) {
							tvSelectedBooks.getItems().add(bk);
						}else{
							tvSelectedBooks.getItems().remove(bk);
						}
					}
				});
				bk.setSelect(select);
			}
		} catch (SQLException e) {
			System.out.println("Failed to Update Table");
			System.out.println(e.getMessage());
		}
	}
	
	//The search elements gets necassary data
	private void initItems() throws SQLException {
		ResultSet rs = DB.getInstance().execute("SELECT MIN(year), MAX(year) FROM books");
			
		for (int i = rs.getInt(1); i < rs.getInt(2) + 1; i++) {
			cbSearchFromYear.getItems().add(i);
			cbSearchToYear.getItems().add(i);
		}
		
		cbSearchRating.getItems().addAll(1, 2, 3, 4, 5);
		cbSearchTag.getItems().add("");
		cbSearchTag.getItems().addAll(DB.getInstance().getTags());
		cbBorrowtime.getItems().addAll("", "4 Weeks", "5 Weeks", "6 Weeks", "7 Weeks", "8 Weeks");
	}
	
	//The Table will be Initialize
	private void initTable() {
		tvcSelectedTitle.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		tvcSelectedAuthor.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		tvcSelectedISBN.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		tvcSelectedDescription.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		tvcSelectedTags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		tvcSelectedYear.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		tvcAvailableTitle.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		tvcAvailableAuthor.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		tvcAvailableISBN.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		tvcAvailableDescription.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		tvcAvailableTags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		tvcAvailableYear.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		tvcAvailableSelect.setCellValueFactory(new PropertyValueFactory<Book, CheckBox>("select"));
		
		tvcAvailableSelect.setStyle("-fx-alignment: BASELINE-CENTER;");
		
		tvAvailableBooks.setItems(availablBookList);
	}
}
