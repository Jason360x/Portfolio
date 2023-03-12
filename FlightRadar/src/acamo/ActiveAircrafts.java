package acamo;

import java.util.ArrayList;

import java.util.HashMap;

import messer.BasicAircraft;
import observer.Observable;
import observer.Observer;

/**
 * Observes Messer, saves the BasicAircrafts in a HashMap
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     1.0
 *
 */

public class ActiveAircrafts implements ActiveAircraftsInterface, Observer<BasicAircraft[]> {
	private HashMap<String, BasicAircraft> activeAircrafts = new HashMap<>();
	
	//Constructors
	public ActiveAircrafts() {
	}
	public ActiveAircrafts(BasicAircraft[] arBA) {
		storeAll(arBA);
	}
	
	public void storeAll(BasicAircraft[] arBA) {
		for(BasicAircraft ba : arBA)
			store(ba.getIcao(), ba);
	}
	
	@Override
	public void store(String icao, BasicAircraft ac) {
		activeAircrafts.put(icao, ac);
	}

	@Override
	public void clear() {
		activeAircrafts.clear();
	}

	@Override
	public BasicAircraft retrieve(String icao) {
		return activeAircrafts.get(icao);
	}

	@Override
	public ArrayList<BasicAircraft> values() {
		return new ArrayList<BasicAircraft>(activeAircrafts.values());
	}
	
	@Override
	public void update(Observable<BasicAircraft[]> observable, BasicAircraft[] newValue) {
		clear();
		storeAll(newValue);
	}

}
