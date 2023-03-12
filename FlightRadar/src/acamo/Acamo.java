package acamo;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.CompletableFuture;

import de.saring.leafletmap.*;
import javafx.application.Application;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Worker;
import javafx.event.ActionEvent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.*;
import javafx.stage.Stage;
import jsonstream.PlaneDataServer;
import messer.BasicAircraft;
import messer.Messer;
import observer.Observable;
import observer.Observer;
import senser.Senser;

/**
 * Creates the UI to display the aircrafts and starts everything up correctly
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     2.0
 */

public class Acamo extends Application implements Observer<BasicAircraft[]> {
	//Aircraft Tracking start, only starts tracking after the UI is loaded
	String urlString = "https://opensky-network.org/api/states/all";
	PlaneDataServer server;
	
	private static double latitude = 48.7433;
    private static double longitude = 9.3201;
    private static int radius = 150;
    private static boolean haveConnection = true;
    
    private ObservableList<BasicAircraft> obsList_basicAircraft = FXCollections.observableArrayList();
    private TableView<BasicAircraft> table_basicAircraft;
    private BasicAircraft selectedAircraft;
    private Scene mainScene;
    private ActiveAircrafts activeAircrafts;
    
    private CompletableFuture<Worker.State> mapLoadState;
    private ArrayList<Marker> mapMarkers = new ArrayList<>();
    private Marker homeMarker;
    
    private Thread serverThread;
    private Thread senserThread;
    private Thread messerThread;

	public static void main(String[] args)
	{
		launch(args);
	}

	@Override
	public void start(Stage primaryStage) throws Exception {	
		mainScene = new Scene(initUI(), 1400, 450);
		setSelectedAircraftText(); //Sets the detailed aircraft information to empty
		primaryStage.setTitle("Acamo"); // Set the stage title
		primaryStage.setResizable(false); //Disables resizing of the window
		primaryStage.setScene(mainScene); // Place the scene in the stage
		primaryStage.show(); // Display the stage
		
		startServer();
	}
	
	//Starts all threads and establishes the server connection
	private void startServer() {
		if(haveConnection)
			server = new PlaneDataServer(urlString, latitude, longitude, radius);
		else
			server = new PlaneDataServer(latitude, longitude, 100);
		
		serverThread = new Thread(server);
		serverThread.start();
		
		Senser senser = new Senser(server);
		senserThread = new Thread(senser);
		senserThread.start();
		
		Messer messer = new Messer();
		senser.addObserver(messer);
		messerThread = new Thread(messer);
		messerThread.start();
		
		activeAircrafts = new ActiveAircrafts();
		messer.addObserver(activeAircrafts);
		messer.addObserver(this);
	}
	
	//Creates a LeafletMapView and returns it correctly configured
	private LeafletMapView createMap() {
		LeafletMapView map = new LeafletMapView();
		map.setId("map");
		
		List<MapLayer> conf = new LinkedList<>();
		conf.add(MapLayer.OPENSTREETMAP);
		mapLoadState = map.displayMap(new MapConfig(conf, new ZoomControlConfig(), new ScaleControlConfig(), new LatLong(latitude, longitude)));
		mapLoadState.whenComplete((state, throwable) -> {
			map.onMapClick(event -> mapClick(event));
			
			createCustomMarkers(map);
			
			LatLong home = new LatLong(latitude, longitude);
			homeMarker = new Marker(home, "homeMarker", "homeMarker", 0);
			map.addCustomMarker("homeMarker", "icons/basestationlarge.png");
			map.addMarker(homeMarker);
		});
		
		return map;
	}
	
	//Gets called when the user clicks anywhere on the map, changes the position of home to the click location
	private void mapClick(LatLong event) {
		longitude = event.getLongitude();
		latitude = event.getLatitude();
		
		changeHome();
	}
	
	//Places all currently active aircrafts on the map
	private void placePlanesOnMap() {
		//Waits until the map is initialized
		mapLoadState.whenComplete((state, throwable) -> {
			//Needed for JavaFX and updates
			Platform.runLater(new Runnable(){
	            @Override
	            public void run() {
	    			LeafletMapView map = (LeafletMapView)mainScene.lookup("#map");
	    			clearMarkers(map);
	    			
	    			for(BasicAircraft ba : activeAircrafts.values()) {
	    				LatLong baLatLong = new LatLong(ba.getCoordinate().getLatitude(), ba.getCoordinate().getLongitude());
	    				Marker baMarker = new Marker(baLatLong, ba.getIcao(), "plane" + getPlaneMarker(ba), ba.getAltitude());
	    				map.addMarker(baMarker);
	    				mapMarkers.add(baMarker);
	    			}
	            }
	        });
		});
	}
	
	//Clears all plane markers off the map
	private void clearMarkers(LeafletMapView map) {
		for(Marker mrk : mapMarkers) {
			map.removeMarker(mrk);
		}
	}
	
	//Returns the number for the marker dependend on the trak of the plane
	private String getPlaneMarker(BasicAircraft ba) {
		int trak = (int)(ba.getTrak()/15);
		if(trak < 0) //Fixes that the sample data sometimes has negative trak (which should never happen in real data)
			trak = 24 - trak;
		
		if(trak < 10)
			return "0" + trak;
		else
			return "" + trak;
	}
	
	//Creates 25 markers for the different directions of the planes
	private void createCustomMarkers(LeafletMapView map) {
		for(int i = 0; i < 25; i++) {
			String numString;
			if(i < 10)
				numString = "0" + i;
			else
				numString = "" + i;
			
			map.addCustomMarker("plane" + numString, "icons/plane" + numString + ".png");
		}
	}
	
	//Gets called when the "Apply" button is clicked, changes the value of latitude and longitude to the ones inputted in the textfields
	private void refreshLatLong(ActionEvent event) {
		try {
			double newLongitude = Double.parseDouble(((TextField)mainScene.lookup("#txtFld_longitude")).getText());
			double newLatitude = Double.parseDouble(((TextField)mainScene.lookup("#txtFld_latitude")).getText());
			
			longitude = newLongitude;
			latitude = newLatitude;
			
			changeHome();
		} catch(NumberFormatException e) {
			System.out.println("Could not parse given longitude or latitude! " + e.getMessage());
		}
	}
	
	//Changes the position of the home, restarts all threads
	private void changeHome() {
		LeafletMapView map = (LeafletMapView)mainScene.lookup("#map");
		LatLong newHome = new LatLong(latitude, longitude);
		homeMarker.move(newHome);
		map.setView(newHome, 10);
		
		server.stop();
		senserThread.interrupt();
		messerThread.interrupt();
		startServer();
		
		((TextField)mainScene.lookup("#txtFld_longitude")).setText(Double.toString(longitude));
		((TextField)mainScene.lookup("#txtFld_latitude")).setText(Double.toString(latitude));
	}
	
	//Initializes all the UI elements and returns them in an HBox
	private HBox initUI() {
		//HBox containing all elements
		HBox hBox_root = new HBox();
		hBox_root.setSpacing(10);
		
		//Map stuff
		VBox vBox_map = new VBox();
		LeafletMapView map = createMap();
		vBox_map.getChildren().add(map);
		vBox_map.setPrefWidth(400);
		
		HBox hBox_latitude = new HBox();
		Label label_latitude = new Label();
		label_latitude.setText("Latitude:\t\t");
		label_latitude.setStyle("-fx-font: 16 arial;");
		TextField txtFld_latitude = new TextField();
		txtFld_latitude.setId("txtFld_latitude");
		txtFld_latitude.setText(Double.toString(latitude));
		hBox_latitude.getChildren().add(label_latitude);
		hBox_latitude.getChildren().add(txtFld_latitude);
		vBox_map.getChildren().add(hBox_latitude);
		
		HBox hBox_longitude = new HBox();
		Label label_longitude = new Label();
		label_longitude.setText("Longitude:\t");
		label_longitude.setStyle("-fx-font: 16 arial;");
		TextField txtFld_longitude = new TextField();
		txtFld_longitude.setId("txtFld_longitude");
		txtFld_longitude.setText(Double.toString(longitude));
		hBox_longitude.getChildren().add(label_longitude);
		hBox_longitude.getChildren().add(txtFld_longitude);
		vBox_map.getChildren().add(hBox_longitude);
		
		Button btn_changeCoord = new Button();
		btn_changeCoord.setText("Apply");
		btn_changeCoord.setOnAction(event -> refreshLatLong(event));
		vBox_map.getChildren().add(btn_changeCoord);
		
		hBox_root.getChildren().add(vBox_map);
		
		//Table stuff
		VBox vBox_table = new VBox();
		Label label_activeAircrafts = new Label();
		label_activeAircrafts.setText("Active Aircrafts");
		label_activeAircrafts.setStyle("-fx-font: 20 arial;");
		label_activeAircrafts.setPrefHeight(35);
		table_basicAircraft = initTable();
		vBox_table.getChildren().addAll(label_activeAircrafts, table_basicAircraft);
		hBox_root.getChildren().add(vBox_table);
		
		//Selected Aircraft stuff
		VBox vBox_selectedAircraft = new VBox();
		Label label_selectedAircraft = new Label();
		label_selectedAircraft.setText("Selected Aircraft:");
		label_selectedAircraft.setStyle("-fx-font: 20 arial;");
		label_selectedAircraft.setPrefHeight(35);
		vBox_selectedAircraft.getChildren().add(label_selectedAircraft);
		Label label = new Label();
		label.setId("infoLabel");
		label.setStyle("-fx-font: 16 arial;");
		vBox_selectedAircraft.getChildren().add(label);
		hBox_root.getChildren().add(vBox_selectedAircraft);
		
		return hBox_root;
	}
	
	//Initializes the table with the correct columns
	private TableView<BasicAircraft> initTable() {
		TableView<BasicAircraft> tableView = new TableView<BasicAircraft>();
		
		//Creates all the table columns and links them to their BasicAircraft objects
		for (String at : BasicAircraft.getAttributesNames()) {
			TableColumn<BasicAircraft, String> tableColumn = new TableColumn<BasicAircraft, String>(at);
			tableColumn.setCellValueFactory(new PropertyValueFactory<BasicAircraft, String>(at));
			tableView.getColumns().add(tableColumn);
		}
		
		//When the selected property is changed, save the selected aircraft in selectedAircraft and change the displayed information
		tableView.getSelectionModel().selectedIndexProperty().addListener((e) -> {
			selectedAircraft = table_basicAircraft.getSelectionModel().getSelectedItem();
			setSelectedAircraftText();
		});
		
		tableView.setItems(obsList_basicAircraft);
		
		return tableView;
	}
	
	//Changes the information that is displayed about the current aircraft
	private void setSelectedAircraftText() {	
		ArrayList<String> attribNames = BasicAircraft.getAttributesNames();
		ArrayList<Object> attribValues = BasicAircraft.getAttributesValues(selectedAircraft);
		
		//Fixes exception when changing the label on update (not necessary if the label is not changed on update)
		Platform.runLater(new Runnable(){
            @Override
            public void run() {
        		Label label = (Label)mainScene.lookup("#infoLabel");
        		label.setText(attribNames.get(0) + ": " + attribValues.get(0) + "\n");
        		for(int i = 1; i < attribNames.size(); i++) {
        			label.setText(label.getText() + attribNames.get(i) + ": " + attribValues.get(i) + "\n");
        		}
            }
        });
	}

	//Refresh displayed information when new information comes in
	//Keep selected row (or aircraft) selected and refresh information about selected aircraft
	@Override
	public void update(Observable<BasicAircraft[]> observable, BasicAircraft[] newValue) {
		int currentIndex = table_basicAircraft.getSelectionModel().getSelectedIndex();
		BasicAircraft currentAircraft = table_basicAircraft.getSelectionModel().getSelectedItem();
		
		obsList_basicAircraft.clear();
		obsList_basicAircraft.addAll(activeAircrafts.values());
		
		//Selects the table row again
		selectTableRow(currentIndex, currentAircraft);
		
		//Refreshes the information of the selected aircraft
		setSelectedAircraftText();
		
		placePlanesOnMap();
	}
	
	//Select the row of the given aircraft or keep selected row selected
	private void selectTableRow(int index, BasicAircraft ba) {
		//If the order of the planes has changed, tries to find the new index of the selected plane
		int newIndex = findIcao(ba);
		
		if(newIndex != -1) {
			table_basicAircraft.getSelectionModel().select(newIndex);
			return;
		}
		
		table_basicAircraft.getSelectionModel().select(index);
	}
	
	//Return the index of the given aircraft in the list of aircrafts
	private int findIcao(BasicAircraft ba) {
		if(ba == null)
			return -1;
		
		for(int i = 0; i < obsList_basicAircraft.size(); i++) {
			if(obsList_basicAircraft.get(i).getIcao().contentEquals(ba.getIcao())) {
				return i;
			}
		}
		
		return -1;
	}
}
