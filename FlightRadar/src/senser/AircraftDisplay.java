package senser;

import messer.BasicAircraft;

/**
 * Display a given AircraftSentence, array of AircraftSentence, BasicAicraft or array of BasicAircraft
 * 
 * @author      Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     2.0
 */

public class AircraftDisplay {
	
	/**
	 * Prints the string of a given AircraftSentence
	 * 
	 * @param sent AircraftSentence that should be printed
	 */
	public static void display(AircraftSentence sent) {
		System.out.println(sent.getAircraft());
	}
	
	/**
	 * Prints the string of a given array of AircraftSentence
	 * @param sent Array of AircraftSentence that should be printed
	 */
	public static void display(AircraftSentence[] sent) {
		for(AircraftSentence sents : sent)
			System.out.println(sents.getAircraft() + ",");
	}
	
	/**
	 * Prints the given BasicAircraft in the expected format
	 * @param sent A single BasicAircraft that should be printed
	 */
	public static void display(BasicAircraft sent) {
		System.out.println(sent); //Calls sent.toString()
	}
	
	/**
	 * Prints the given array of BasicAircraft in the expected format
	 * @param sent An array of BasicAircraft that should be printed
	 */
	public static void display(BasicAircraft[] sent) {
		for(BasicAircraft ba : sent) {
			display(ba);
		}
	}
}
