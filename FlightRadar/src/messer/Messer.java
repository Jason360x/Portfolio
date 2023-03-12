package messer;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.concurrent.ArrayBlockingQueue;

import observer.Observable;
import observer.Observer;
import observer.SimpleObservable;
import senser.AircraftDisplay;
import senser.AircraftSentence;

/**
 * Observes Senser, saves the changed value in the buffer and displays it
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     1.2
 */

public class Messer extends SimpleObservable<BasicAircraft[]> implements Runnable, Observer<AircraftSentence[]> {
	private final ArrayBlockingQueue<AircraftSentence[]> buffer = new ArrayBlockingQueue<AircraftSentence[]>(1); //Buffer size is 1
	
	public void run()
	{
		//Current time to make the output more readable
		DateTimeFormatter dtf = DateTimeFormatter.ofPattern("dd/MM/yyy HH:mm:ss");
		while(true) {	
			try {
				AircraftSentence[] as = buffer.take();
				
				BasicAircraft[] ba = AircraftFactory.createBasicAircraft(as);
				//Observer Pattern
				setChanged();
				notifyObservers(ba);
				
				LocalDateTime now = LocalDateTime.now();  
				System.out.println("\nCurrent Time: " + dtf.format(now));
				System.out.println("Current aircrafts in range: " + ba.length);
				
				AircraftDisplay.display(ba);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				System.out.println("Interrupted in Messer run()");
				break;
			}
		}	
	}

	@Override
	public void update(Observable<AircraftSentence[]> observable, AircraftSentence[] newValue) {
		// TODO Auto-generated method stub
		try {
			buffer.put(newValue);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			System.out.println("Interrupted in Messer update(...)");
		}
	}
}
