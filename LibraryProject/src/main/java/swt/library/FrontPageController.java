
package swt.library;

import java.io.IOException;
import java.sql.SQLException;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.stage.Stage;

public class FrontPageController {
	@FXML
	private Button btnAdd, btnBorrow, btnDelete, btnReturn;
	@FXML
	private TextField txtName, txtPassword;
	@FXML
	private Label lblFalseLogin, lblLoginName, lblLoginRole;
	@FXML
	private Pane pLoginFields, pLoginDetails;
	@FXML
	private TableView<Book> tblBooks, tblTop;
	@FXML
	private TableColumn<Book, String> colTitle, colAuthor, colISBN, colDescription, colTags, colRating, colTitleTop, colRatingTop;
	@FXML
	private TableColumn<Book, Integer> colYear;
	
	private ObservableList<Book> books = FXCollections.observableArrayList();
	private ObservableList<Book> topBooks = FXCollections.observableArrayList();
	
    public void initialize() throws SQLException{
    	if(DB.getInstance().getLoggedInID() == 0)  {
    		btnAdd.setVisible(false);
    		btnDelete.setVisible(false);
    		btnBorrow.setVisible(false);
    		btnReturn.setVisible(false);
    	}
    	
    	if(DB.getInstance().getLoggedInID() != 0) {
    		pLoginFields.setVisible(false);
			pLoginDetails.setVisible(true);
			lblLoginName.setText("Welcome Back, " + DB.getInstance().getLoggedInName() + "!");
			lblLoginRole.setText("Your Role: " + DB.getInstance().getLoggedInRole());
			lblFalseLogin.setText("");
    	}
    	books.setAll(DB.getInstance().getBookList());
    	initTbl();
    	
    	topBooks.setAll(DB.getInstance().getTopBooks());
    	initTopTbl();
    }
	
	public void openAdd() throws IOException {
		if(DB.getInstance().getLoggedInID() != 0 && DB.getInstance().getLoggedInRole().equals("admin")) {
				Parent root = FXMLLoader.load(getClass().getResource("addBook.fxml"));
				Stage window = (Stage) btnAdd.getScene().getWindow();
				window.setResizable(false);
				window.setScene(new Scene(root));
		} else {
			lblFalseLogin.setText("You must be logged in as admin to add a Book");
		}
	}
	
	public void openBorrow() throws IOException {
		if(DB.getInstance().getLoggedInID() != 0) {
			Parent root = FXMLLoader.load(getClass().getResource("borrowBook.fxml"));
			Stage window = (Stage) btnBorrow.getScene().getWindow();
			window.setResizable(false);
			window.setScene(new Scene(root));
		} else {
			lblFalseLogin.setText("You must be logged in to borrow a Book");
		}
	}
	
	public void openDelete() throws IOException {
		if(DB.getInstance().getLoggedInID() != 0 && DB.getInstance().getLoggedInRole().equals("admin")) {
			Parent root = FXMLLoader.load(getClass().getResource("deleteBook.fxml"));
			Stage window = (Stage) btnDelete.getScene().getWindow();
			window.setResizable(false);
			window.setScene(new Scene(root));
		} else {
			lblFalseLogin.setText("You must be logged in as admin to delete a Book");
		}
	}
	
	public void openReturn() throws IOException {
		if(DB.getInstance().getLoggedInID() != 0) {
			Parent root = FXMLLoader.load(getClass().getResource("returnBook.fxml"));
			Stage window = (Stage) btnReturn.getScene().getWindow();
			window.setResizable(false);
			window.setScene(new Scene(root));
		} else {
			lblFalseLogin.setText("You must be logged in to return a Book");
		}
	}
	
	public void login() throws SQLException {
		String username = txtName.getText().toString();
		if (DB.getInstance().checkLogin(username, txtPassword.getText().toString())) {
			pLoginFields.setVisible(false);
			pLoginDetails.setVisible(true);

			lblLoginName.setText("Welcome Back, " + DB.getInstance().getLoggedInName() + "!");
			lblLoginRole.setText("Your Role: " + DB.getInstance().getLoggedInRole());
			
			if(DB.getInstance().getLoggedInRole().equals("admin")) {
				btnAdd.setVisible(true);
				btnDelete.setVisible(true);
				btnBorrow.setVisible(true);
				btnReturn.setVisible(true);
			}
			if(DB.getInstance().getLoggedInRole().equals("user")) {
				btnBorrow.setVisible(true);
				btnReturn.setVisible(true);
			}
			
		} else {
			lblFalseLogin.setTextFill(Color.rgb(255, 255, 255));
			lblFalseLogin.setFont(Font.font ("Comic Sans", 12));
			lblFalseLogin.setText("Wrong login details, try again.");
		}
	}
	
	public void logout() {
		DB.getInstance().setLoggedInID(0);
		txtName.setText("");
		txtPassword.setText("");
		lblFalseLogin.setText("");
		pLoginDetails.setVisible(false);
		pLoginFields.setVisible(true);
		btnAdd.setVisible(false);
		btnDelete.setVisible(false);
		btnBorrow.setVisible(false);
		btnReturn.setVisible(false);
	}
	
	private void initTbl() {
		colTitle.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		colAuthor.setCellValueFactory(new PropertyValueFactory<Book, String>("author"));
		colISBN.setCellValueFactory(new PropertyValueFactory<Book, String>("isbn"));
		colYear.setCellValueFactory(new PropertyValueFactory<Book, Integer>("year"));
		colDescription.setCellValueFactory(new PropertyValueFactory<Book, String>("description"));
		colTags.setCellValueFactory(new PropertyValueFactory<Book, String>("tags"));
		colRating.setCellValueFactory(new PropertyValueFactory<Book, String>("rating"));
		
		tblBooks.setItems(books);
	}
	
	private void initTopTbl() {
		colTitleTop.setCellValueFactory(new PropertyValueFactory<Book, String>("title"));
		colRatingTop.setCellValueFactory(new PropertyValueFactory<Book, String>("rating"));
		
		tblTop.setItems(topBooks);
	}
}