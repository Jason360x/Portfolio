package swt.library;

import java.io.IOException;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.SelectionMode;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.stage.Stage;

public class AddBookController {

	@FXML
	private Button btnFrontPage;
	@FXML
	private Label lblAddResult;
	@FXML
	private TextField txtAddTitle, txtAddAuthor, txtAddYear, txtAddIsbn;
	@FXML
	private TextArea txtAddDescription;
	@FXML
	private ListView<String> lvAddTags;
	@FXML
	private Label lblLoginDetails;
	
	public void initialize(){
		lblLoginDetails.setText("Logged in as: " + DB.getInstance().getLoggedInName() + " in Role: " + DB.getInstance().getLoggedInRole());
		lvAddTags.getSelectionModel().setSelectionMode(SelectionMode.MULTIPLE);
		lvAddTags.setItems(DB.getInstance().getTags());
	}
	
	public void openFrontPage() throws IOException {
		Parent root = FXMLLoader.load(getClass().getResource("frontPage.fxml"));
		Stage window = (Stage) btnFrontPage.getScene().getWindow();
		window.setResizable(false);
		window.setScene(new Scene(root));
	}
	
	public void addBook() {
		try {
			ObservableList<String> selectedTags = lvAddTags.getSelectionModel().getSelectedItems();
			String selectedTagsString = "";
			
			if(selectedTags.size() > 0) {
				selectedTagsString = selectedTags.get(0);
				
				for (int i = 1; i < selectedTags.size(); i++) {
					selectedTagsString += ";" + selectedTags.get(i); 
				}
			}
			
			if(txtAddYear.getText().matches("\\d{4}")) {
				PreparedStatement pstmt = DB.conn.prepareStatement("INSERT INTO books(title,author,isbn,year,description,tags) VALUES (?,?,?,?,?,?);");
				pstmt.setString(1, txtAddTitle.getText());
				pstmt.setString(2, txtAddAuthor.getText());
				pstmt.setString(3, txtAddIsbn.getText());
				pstmt.setInt(4, Integer.parseInt(txtAddYear.getText()));
				pstmt.setString(5, txtAddDescription.getText());
				pstmt.setString(6, selectedTagsString);
				pstmt.executeUpdate();
				
				lblAddResult.setText("The Book was successfully added!");
				txtAddTitle.setText("");
				txtAddAuthor.setText("");
				txtAddIsbn.setText("");
				txtAddYear.setText("");
				txtAddDescription.setText("");
				lvAddTags.getSelectionModel().clearSelection();
			}
			else
			{
				lblAddResult.setText("Plese enter a valid Year");
			}
		} catch (SQLException e) {
			lblAddResult.setText("A Error accured while adding the Book! Please try again.");
			e.printStackTrace();
		}
	}
}
