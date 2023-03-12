module swt.library {
    requires javafx.controls;
    requires javafx.fxml;
    requires javafx.graphics;
	requires javafx.base;
	requires java.sql;

    opens swt.library to javafx.fxml;
    exports swt.library;
}