package swt.library;

import javafx.scene.control.CheckBox;

public class Book {
	private String title, author, isbn, description, tags;
	private int bookID, year;
	private float rating;
	private CheckBox select;
	
	public Book(int bookID, String title, String author, String isbn, int year, String description, String tags, float rating) {
		this.bookID = bookID;
		this.title = title;
		this.author = author;
		this.isbn = isbn;
		this.year = year;
		this.description = description;
		this.tags = tags;
		this.rating = rating;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getAuthor() {
		return author;
	}

	public void setAuthor(String author) {
		this.author = author;
	}

	public String getIsbn() {
		return isbn;
	}

	public void setIsbn(String isbn) {
		this.isbn = isbn;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public String getTags() {
		return tags;
	}

	public void setTags(String tags) {
		this.tags = tags;
	}

	public int getBookID() {
		return bookID;
	}

	public void setBookID(int bookID) {
		this.bookID = bookID;
	}

	public int getYear() {
		return year;
	}

	public void setYear(int year) {
		this.year = year;
	}
	
	public float getRating() {
		return rating;
	}

	public void setRating(float rating) {
		this.rating = rating;
	}
	
	@SuppressWarnings("exports")
	public CheckBox getSelect() {
		return select;
	}
	
	@SuppressWarnings("exports")
	public void setSelect(CheckBox select) {
		this.select = select;
	}
	
	public String toString() {
		return "bookID: " + bookID + "\nTitle: " + title + "\nAuthor: " + author + "\nISBN: " + isbn + "\nYear: " + year + "\nDescription: " + description + "\nTags: " + tags + "\nRating: " + rating;
	}	
}
