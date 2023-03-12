package senser;

import org.json.JSONArray;

/**
 * Creates an array of AircraftSentence
 * 
 * @author 		Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     2.0
 *
 */

public class AircraftSentenceFactory {
	/**
	 * Creates an array of AircraftSentence objects from a given JSONArray
	 * 
	 * @param planes Input of all airplanes in a JSONArray
	 * @return Array of AircraftSentence with each plane having it's own AircraftSentence object
	 */
	public static AircraftSentence[] createAircraftSentence(JSONArray planes) {
		AircraftSentence[] aircraftSentences = new AircraftSentence[planes.length()];
		
		for(int i = 0; i < planes.length(); i++) {
			aircraftSentences[i] = new AircraftSentence(planes.getJSONArray(i));
		}
		
		return aircraftSentences;
	}
}
