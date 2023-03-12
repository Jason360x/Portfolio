package swt.library;

import java.util.Comparator;

public class BookComparator implements Comparator<Book> {
	@Override
	public int compare(Book book1, Book book2) {
		if(book1.getRating() < book2.getRating()) return 1;
		else if (book1.getRating() == book2.getRating()) return 0;
		else return -1;
	}
}