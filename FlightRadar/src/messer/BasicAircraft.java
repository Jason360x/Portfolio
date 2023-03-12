package messer;

import java.util.ArrayList;
import java.util.Date;

/**
 * Saves an aircraft with all of it's important attributes
 * 
 * @author      Unknown, Jason Patrick Duffy jaduit00@hs-esslingen.de
 * @version     1.6
 */

public class BasicAircraft {
	private String icao;
	private String operator;
	private Date posTime;
	private Coordinate coordinate;
	private Double speed;
	private Double trak;
	private Integer altitude;

	
	public BasicAircraft (String icao, String operator, Date posTime, Coordinate coordinate, double speed, double trak, Integer altitude) {
		this.icao = icao;
		this.operator = operator;
		this.posTime = posTime;
		this.coordinate = coordinate;
		this.speed = speed;
		this.trak = trak;
		this.altitude = altitude;
	}

	public String getIcao() {
		return icao;
	}
	
	public String getOperator() {
		return operator;
	}

	public Date getPosTime() {
		return posTime;
	}

	public Coordinate getCoordinate() {
		return coordinate;
	}

	public double getSpeed() {
		return speed;
	}

	public double getTrak() {
		return trak;
	}
	
	public Integer getAltitude() {
		return altitude;
	}
	
	public static ArrayList<String> getAttributesNames()
	{
		ArrayList<String> attributes = new ArrayList<String>();

		attributes.add("icao");
		attributes.add("operator");
		attributes.add("posTime");
		attributes.add("coordinate");
		attributes.add("speed");
		attributes.add("trak");
		attributes.add("altitude");
		
		return attributes;
	}

	public static ArrayList<Object> getAttributesValues(BasicAircraft ac)
	{
		ArrayList<Object> attributes = new ArrayList<Object>();

		//If the given Aircraft is invalid, return empty strings
		if(ac == null) {
			attributes.add("");
			attributes.add("");
			attributes.add("");
			attributes.add("");
			attributes.add("");
			attributes.add("");
			attributes.add("");
		}
		else {
			attributes.add(ac.getIcao());
			attributes.add(ac.getOperator());
			attributes.add(ac.getPosTime());
			attributes.add(ac.getCoordinate());
			attributes.add(ac.getSpeed());
			attributes.add(ac.getTrak());
			attributes.add(ac.getAltitude());
		}

		return attributes;
	}

	@Override
	public String toString() {
		return "BasicAircraft [icao=" + icao + ", operator=" + operator + ", posTime=" + posTime
				+ ", " + coordinate + coordinate + ", speed=" + speed + ", trak =" + trak + "]";
	}
}