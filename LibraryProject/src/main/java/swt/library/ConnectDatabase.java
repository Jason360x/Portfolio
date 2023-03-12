package swt.library;

import java.sql.*;

public class ConnectDatabase {

	private static ConnectDatabase instance;

	@SuppressWarnings("exports")
	public static Connection conn = null;
	@SuppressWarnings("exports")
	public static Statement stmt = null;
	
	private ConnectDatabase() throws SQLException {
		connect();
	}

	public static ConnectDatabase getInstance() throws SQLException {
		if (instance == null)
			instance = new ConnectDatabase();
		return instance;
	}
	
	@SuppressWarnings("exports")
	public static Connection connect() {
		conn = null;
		try {
			// db parameters
			String url = "jdbc:sqlite:library.db";
			// create a connection to the database
			conn = DriverManager.getConnection(url);

			System.out.println("Connection to SQLite has been established.");

		} catch (SQLException e) {
			System.out.println("Connection to SQLite failed.");
			System.out.println(e.getMessage());
		}
		return conn;
	}
	
	public static void disconnect() throws SQLException {
		try {
			if (stmt != null)
				stmt.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
		try {
			if (conn != null)
				conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	@SuppressWarnings("exports")
	public static Connection getConn() {
		return conn;
	}
}