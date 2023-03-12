package library;

import static org.junit.Assert.*;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Statement;

import org.junit.Assert;
import org.junit.Test;

import swt.library.ConnectDatabase;

public class TestDBConn {

	@Test
	public void testConn() throws Exception {
		try {
			System.out.println("Test database connection.");
			Connection res = ConnectDatabase.connect();
			assertEquals(res != null, true);
			ConnectDatabase.disconnect();
			System.out.println("Database connection test successful.\n");
		} catch (Exception e) {
			System.out.println("Database connection test didn't work.");
			e.printStackTrace();
		}
	}

	@Test
	public void testCreateTables() throws Exception {
		String userTables = "CREATE TABLE test (\n" + "	\"uID\"	INTEGER,\n" + "	\"name\"	TEXT,\n"
				+ "	\"vorname\"	TEXT,\n" + "password TEXT, \n" + "username TEXT, " + "role TEXT" + ");";
		try {
			System.out.println("Create table 'test'.");
			ConnectDatabase.connect();
			Statement stmt = ConnectDatabase.conn.createStatement();
			stmt.execute(userTables);
			PreparedStatement pstmt = ConnectDatabase.conn.prepareStatement("SELECT * FROM test");

			ResultSet rs = pstmt.executeQuery();
			ResultSetMetaData rsmd = rs.getMetaData();

			String column1 = rsmd.getColumnName(1);
			String column2 = rsmd.getColumnName(2);
			String column3 = rsmd.getColumnName(3);
			String column4 = rsmd.getColumnName(4);
			String column5 = rsmd.getColumnName(5);
			String column6 = rsmd.getColumnName(6);

			assertEquals("uID", column1);
			assertEquals("name", column2);
			assertEquals("vorname", column3);
			assertEquals("password", column4);
			assertEquals("username", column5);
			assertEquals("role", column6);
			
			ConnectDatabase.disconnect();
			System.out.println("Table 'test' successfully created.\n");
		} catch (Exception e) {
			System.out.println("Table 'test' couldn't be created.");
			e.printStackTrace();
		}
	}

	@Test
	public void testInsertData() throws Exception {
		String insertUser = "INSERT INTO test" + "(uID,name,vorname,password,username,role)"
				+ "VALUES (99999, 'testName', 'testVorname', 'testPassword', 'testUsername', 'testRole');";
		try {
			System.out.println("Testing insert data into 'test'.");
			ConnectDatabase.connect();
			Statement stmt = ConnectDatabase.conn.createStatement();
			stmt.execute(insertUser);

			PreparedStatement pstmt = ConnectDatabase.conn.prepareStatement("SELECT * FROM test");
			ResultSet rs = pstmt.executeQuery();

			int uID = rs.getInt("uID");
			String name = rs.getString("name");
			String vorname = rs.getString("vorname");
			String password = rs.getString("password");
			String username = rs.getString("username");
			String role = rs.getString("role");

			assertEquals(99999, uID);
			assertEquals("testName", name);
			assertEquals("testVorname", vorname);
			assertEquals("testPassword", password);
			assertEquals("testUsername", username);
			assertEquals("testRole", role);
			
			ConnectDatabase.disconnect();
			System.out.println("Inserting data into 'test' was successful.\n");
		} catch (Exception e) {
			System.out.println("Inserting data into 'test' didn't work.");
			e.printStackTrace();
		}
	}

	@Test
	public void deleteData() throws Exception {
		String deleteTestTable = "DROP TABLE IF EXISTS test;";

		try {
			System.out.println("Testing deletion of data from 'test'.");
			ConnectDatabase.connect();

			Statement stmt = ConnectDatabase.conn.createStatement();
			stmt.execute(deleteTestTable);

			try {
				stmt.executeQuery("SELECT * FROM test;");
				Assert.fail("SELECT should throw");
			} catch (Exception e) {
				Assert.assertTrue(e.getMessage().contains("no such table"));
			}
			ConnectDatabase.disconnect();
			System.out.println("Deleting data from 'test' was successful.\n");

		} catch (Exception e) {
			System.out.println("Deleting data from 'test' didn't work.");
			e.printStackTrace();
		}
	}
}
