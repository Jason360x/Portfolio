package swt.library;

import java.io.IOException;
import java.sql.SQLException;
import java.util.List;

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
	
	/**
	 * Shows the user and his role and initializes all graphical elements
	 */
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
	
	/**
	 * Opens the Front Page
	 * @throws IOException
	 */
	public void openFrontPage() throws IOException {
		Parent root = FXMLLoader.load(getClass().getResource("frontPage.fxml"));
		Stage window = (Stage) btnFrontPage.getScene().getWindow();
		window.setResizable(false);
		window.setScene(new Scene(root));
	}
	
	/**
	 * Filters books that match the user's input and displays them
	 */
	public void SearchBook() {
		UpdateTable();
		
		ObservableList<Book> searchRemoveBookList = FXCollections.observableArrayList();
		
		for(Book bk : availablBookList) {
			
			if(cbSearchFromYear.getSelectionModel().getSelectedItem() != null) {
				if(bk.getYear() < cbSearchFromYear.getSelectionModel().getSelectedItem()) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
			
			if(cbSearchToYear.getSelectionModel().getSelectedItem() != null) {
				if(bk.getYear() > cbSearchToYear.getSelectionModel().getSelectedItem()) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
			
			if(!txtSearchTitle.getText().equals("")) {
				if(!bk.getTitle().contains(txtSearchTitle.getText())) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
			
			if(!txtSearchAuthor.getText().equals("")) {
				if(!bk.getAuthor().contains(txtSearchAuthor.getText())) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
			
			if(!txtSearchISBN.getText().equals("")) { 
				if(!bk.getIsbn().contains(txtSearchISBN.getText())) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}	
			
			if(cbSearchTag.getSelectionModel().getSelectedItem() != null) {
				if(!bk.getTags().contains(cbSearchTag.getSelectionModel().getSelectedItem())) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
			
			if(cbSearchRating.getSelectionModel().getSelectedItem() != null) {
				if(bk.getRating() < cbSearchRating.getSelectionModel().getSelectedItem()) {
					searchRemoveBookList.add(bk);
					continue;
				}
			}
		}
		
		availablBookList.removeAll(searchRemoveBookList);
	}
	
	/**
	 * Sets the search elements to the default value and displays all currently available books
	 */
	public void ResetSearch() {
		UpdateTable();
		cbSearchFromYear.setValue(null);
		cbSearchToYear.setValue(null);
		txtSearchAuthor.setText("");
		txtSearchTitle.setText("");
		txtSearchISBN.setText("");
		cbSearchTag.setValue(null);
		cbSearchRating.setValue(null);
	}
	
	/**
	 * The books which are selected will be borrowed for the given Time
	 */
	public void BorrowBook(){
		if(cbBorrowtime.getSelectionModel().getSelectedItem() != null && !cbBorrowtime.getSelectionModel().getSelectedItem().equals("")) {
			int borrowWeeks = Integer.parseInt(cbBorrowtime.getSelectionModel().getSelectedItem().split("")[0]);

	        
	        for(Book bk: tvSelectedBooks.getItems()) {	
				try {
					DB.getInstance().borrowBook(bk.getBookID(), DB.getInstance().getLoggedInID(), borrowWeeks);
				} catch (SQLException e) {
					System.out.println("Failed to borrow book with ID: " + bk.getBookID());
					System.out.println(e.getMessage());
				}
	        }
	        tvSelectedBooks.getItems().clear();
			ResetSearch();
		}else {
			System.out.println("Not a valid borrowtime");
		}
	}
	
	/**
	 * Deletes the current table and fetches all books that are currently available from the database again 
	 */
	private void UpdateTable() {
		try {
			availablBookList.clear();
			
			for(Book bk : DB.getInstance().getBookList()){
				
				if(DB.getInstance().bookAvailable(bk.getBookID())) {
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
	
	/**
	 * Fills the search elements with the necessary data
	 * @throws SQLException
	 */
	private void initItems() throws SQLException {

		List<Integer> yearRange = DB.getInstance().yearRangeList();
		
		cbSearchFromYear.getItems().addAll(yearRange);
		cbSearchToYear.getItems().addAll(yearRange);
		cbSearchRating.getItems().addAll(null, 1, 2, 3, 4, 5);
		cbSearchTag.getItems().add("");
		cbSearchTag.getItems().addAll(DB.getInstance().getTags());
		cbBorrowtime.getItems().addAll("", "4 Weeks", "5 Weeks", "6 Weeks", "7 Weeks", "8 Weeks");
	}
	
	/**
	 * Initializes the table
	 */
	private void initTable() {
		tvcSelectedTitle.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		tvcSelectedAuthor.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		tvcSelectedISBN.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		tvcSelectedDescription.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		tvcSelectedTags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		tvcSelectedYear.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		tvcSelectedRating.setCellValueFactory(new PropertyValueFactory<Book, Float>("Rating"));
		tvcAvailableTitle.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		tvcAvailableAuthor.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		tvcAvailableISBN.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		tvcAvailableDescription.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		tvcAvailableTags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		tvcAvailableYear.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		tvcAvailableRating.setCellValueFactory(new PropertyValueFactory<Book, Float>("Rating"));
		tvcAvailableSelect.setCellValueFactory(new PropertyValueFactory<Book, CheckBox>("select"));
		
		tvcAvailableSelect.setStyle("-fx-alignment: BASELINE-CENTER;");
		
		tvAvailableBooks.setItems(availablBookList);
	}
}
