package messer;

import java.util.Date;

import org.json.JSONArray;
import org.json.JSONException;

import senser.AircraftSentence;

/**
 * Creates a BasicAircraft from a given AircraftSentence and returns it, same with arrays of each
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     1.0
 */

public class AircraftFactory {
	public static BasicAircraft createBasicAircraft(AircraftSentence as) {
		JSONArray thisAircraft = as.getAircraft();
		
		//Values for BasicAircraft
		String icao;
		String operator;
		Date posTime;
		Coordinate coordinate;
		Double speed;
		Double trak;
		Integer altitude;
		
		icao = thisAircraft.getString(0);
		operator = thisAircraft.getString(1);
		
		try {
			posTime = new Date(thisAircraft.getLong(3) * 1000); //API returns seconds but Java expects ms, so multiplying by 1000, can be null
		}catch(JSONException e) { //posTime was null
			System.out.println("posTime of plane " + icao + " was null. Exception: " + e.getMessage());
			posTime = new Date(0);
		}
		
		try {
			coordinate = new Coordinate(thisAircraft.getDouble(6), thisAircraft.getDouble(5)); //Can be null
		}catch(JSONException e) { //Latitude and/or longitude was null
			System.out.println("Latitude and/or longitude of plane " + icao + " was null. Exception: " + e.getMessage());
			coordinate = new Coordinate(0, 0);
		}
		
		try {
			altitude = (int)thisAircraft.getDouble(7); //Cuts off decimals, can be null
		}catch(JSONException e) { //Altitude was null
			System.out.println("Altitude of plane " + icao + " was null. Exception: " + e.getMessage());
			altitude = 0;
		}
		
		try {
			speed = thisAircraft.getDouble(9); //Can be null
		}catch(JSONException e) { //Speed was null
			System.out.println("Speed of plane " + icao + " was null. Exception: " + e.getMessage());
			speed = 0.0;
		}
		
		try {
			trak = thisAircraft.getDouble(10); //Can be null
		}catch(JSONException e) { //Trak was null
			System.out.println("Trak of plane " + icao + " was null. Exception: " + e.getMessage());
			trak = 0.0;
		}
		
		return new BasicAircraft(icao, operator, posTime, coordinate, speed, trak, altitude);
	}
	
	public static BasicAircraft[] createBasicAircraft(AircraftSentence[] as) {
		BasicAircraft[] ba = new BasicAircraft[as.length];
		
		for(int i = 0; i < as.length ; i++) {	
			ba[i] = createBasicAircraft(as[i]);
		}
		
		return ba;
	}
}
