package senser;

import jsonstream.*;
import observer.SimpleObservable;

/**
 * Periodically fetches Aircrafts from PlaneDataServer and saves them in AircraftSentences
 * 
 * @author      Unknown, Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     2.1
 */

public class Senser extends SimpleObservable<AircraftSentence[]> implements Runnable
{
	PlaneDataServer server;
	
	private int sleepTime = 1000; //Time the thread sleeps to avoid spamming the API and getting banned

	public Senser(PlaneDataServer server)
	{
		this.server = server;
	}
	
	public void run()
	{	
		while(true) {
			AircraftSentence[] sentences = AircraftSentenceFactory.createAircraftSentence(server.getPlaneArray());
			//System.out.println("Current Aircrafts in Range: " + sentences.length);
			//AircraftDisplay.display(sentences);
			
			//Observer Pattern
			setChanged();
			notifyObservers(sentences);
			
			try {
				Thread.sleep(sleepTime);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				System.out.println("Interrupted in Senser run()");
				break;
			}
		}
	}
}