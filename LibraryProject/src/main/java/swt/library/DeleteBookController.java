package swt.library;

import java.io.IOException;
import java.sql.SQLException;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.stage.Stage;

public class DeleteBookController {
	@FXML
	private Label lblLoginDetails;
	@FXML
	private Label lblDeleteResult;
	@FXML
	private Button btnFrontPage, btnSubmit;
	@FXML
	private TextField txtTitle, txtAuthor, txtYear, txtISBN;
	@FXML
	private TextArea txtDescription;
	
	public void openFrontPage() throws IOException {
		Parent root = FXMLLoader.load(getClass().getResource("frontPage.fxml"));
		Stage window = (Stage) btnFrontPage.getScene().getWindow();
		window.setResizable(false);
		window.setScene(new Scene(root));
	}
	
	public void initialize() {
        lblLoginDetails.setText("Logged in as: " + DB.getInstance().getLoggedInName() + " in Role: " + DB.getInstance().getLoggedInRole());
    }
	
	public void submit() throws NumberFormatException, SQLException  {
		if(DB.getInstance().deleteBooks(txtTitle.getText().toString(), txtAuthor.getText().toString(), txtISBN.getText().toString(), Integer.parseInt(txtYear.getText()), txtDescription.getText().toString())) {
			DB.getInstance().deleteBooks(txtTitle.getText().toString(), txtAuthor.getText().toString(), txtISBN.getText().toString(), Integer.parseInt(txtYear.getText()), txtDescription.getText().toString());
			lblDeleteResult.setText("The Book was successfully deleted!");
		}
		else {
			lblDeleteResult.setText("Book cannot be deleted!");
		}
	}
}