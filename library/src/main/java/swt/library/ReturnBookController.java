package swt.library;

import java.io.IOException;
import java.net.URL;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.ResourceBundle;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.*;
import javafx.scene.*;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;

public class ReturnBookController implements Initializable {

	ObservableList<Book> borrowedBooks = FXCollections.observableArrayList(); //List the JavaFX table uses
	
	ArrayList<Book> returnedBooks = new ArrayList<>(); //List that temporarily stores the books the user wants to return
	HashMap<Integer, Integer> bookRatings = new HashMap<>(); //HashMap that stores the user's ratings and the books before they are submitted
	
	@FXML
	private Button btnFrontPage, btn_saveRating, btn_returnBook, btn_revertChanges, btn_confirmChanges;
	@FXML
	private TableView<Book> table_checkedOutBooks;
	@FXML
	private TableColumn<Book, String> tableCol_title, tableCol_author, tableCol_isbn, tableCol_desc, tableCol_tags;
	@FXML
	private TableColumn<Book, Integer> tableCol_year;
	@FXML
	private TableColumn<Book, Float> tableCol_rating;
	@FXML
	private Slider slider_bookRating;
	@FXML
	private Label label_Greeting;
	
	//Gets called when ReturnBook is opened
	@Override
	public void initialize(URL location, ResourceBundle resources) {	
		try {
			setLoginMessage();
			fillBorrowedBooksList();
			initTable();
			setLoginMessage();
		} catch (SQLException e) {
			System.out.println("Could not fully initialize ReturnBook!");
			e.printStackTrace();
		}
	}
	
	//Fills the borrowedBooks list with all books borrowed by the current user
	private void fillBorrowedBooksList() throws SQLException {
		borrowedBooks.setAll(DB.getInstance().getBorrowedBooksForUser());
	}
	
	//Initializes all table columns and sets the table to read from the borrowedBooks list
	private void initTable() {
		tableCol_title.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		tableCol_author.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		tableCol_isbn.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		tableCol_desc.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		tableCol_tags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		tableCol_year.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		tableCol_rating.setCellValueFactory(new PropertyValueFactory<Book, Float>("rating"));
		
		table_checkedOutBooks.setItems(borrowedBooks);
	}
	
	//Returns the book that is selected in the table
	private Book getSelectedBook() {
		return table_checkedOutBooks.getSelectionModel().getSelectedItem();
	}
	
	//Saves the current rating in a HashMap
	public void saveRating() {
		Book bk = getSelectedBook();
		if(bk != null)
			bookRatings.put(bk.getBookID(), (int)slider_bookRating.getValue());
	}
	
	//Saves the selected book in the returnedBooks ArrayList
	public void returnBook() {
		Book bk = getSelectedBook();
		if(bk != null) {
			if(!returnedBooks.contains(bk))
				returnedBooks.add(bk);
		}
	}
	
	//Submits all ratings saved in bookRatings and all returned books saved in returnedBooks to the DB
	public void submit() {
		//Submit ratings	
		if(!bookRatings.isEmpty()) {
			for(Map.Entry<Integer, Integer> entry : bookRatings.entrySet()) {
			    int bookID = entry.getKey();
			    int ratingValue = entry.getValue();
			    
			    try {
			    	DB.getInstance().rateBook(bookID, ratingValue);
				} catch (SQLException e) {
					// TODO Auto-generated catch block
					System.out.println("Could not submit ratings in ReturnBook!");
					e.printStackTrace();
				}
			}
		}
		
		//Submit returns
		if(!returnedBooks.isEmpty()) {
			try {
				for(Book bk : returnedBooks) {
					DB.getInstance().returnBook(bk);
				}
			} catch (SQLException e) {
				System.out.println("Could not submit returnedBooks in ReturnBook!");
				e.printStackTrace();
			}
		}
		
		//Refresh list of borrowed books
		try {
			fillBorrowedBooksList();
		} catch (SQLException e) {
			System.out.println("Could not fill the borrowed books list in ReturnBook!");
			e.printStackTrace();
		}
		
		clearRatingsAndReturnedLists(); //Clears the ratings and returned book lists after submission
	}
	
	//Clears the returnedBooks array and bookRatings HashMap if the Revert Changes button is clicked
	public void revertChanges() {
		clearRatingsAndReturnedLists();
	}
	
	//Clears the returnedBooks array and bookRatings HashMap
	private void clearRatingsAndReturnedLists() {
		bookRatings.clear();
		returnedBooks.clear();
	}
	
	//Fills "Logged in as USER in role ROLE" label
	private void setLoginMessage() {
		label_Greeting.setText("Logged in as " + DB.getInstance().getLoggedInName() + " in role " + DB.getInstance().getLoggedInRole());
	}
	
	public void openFrontPage() throws IOException {
		Parent root = FXMLLoader.load(getClass().getResource("frontPage.fxml"));
		Stage window = (Stage) btnFrontPage.getScene().getWindow();
		window.setResizable(false);
		window.setScene(new Scene(root));
	}
}
