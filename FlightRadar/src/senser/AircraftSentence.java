package senser;

import org.json.JSONArray;

/**
 * Saves a single AircraftSentence
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     2.0
 */

public class AircraftSentence {
	protected JSONArray aircraft;
	
	public AircraftSentence(JSONArray sentence) {
		aircraft = sentence;
	}
	
	public JSONArray getAircraft() {
		return aircraft;
	}
}
