import jsonstream.*;
import messer.Messer;
import senser.Senser;

public class OOS2Lab1Starter
{	
    private static double latitude = 48.7433;
    private static double longitude = 9.3201;
    private static boolean haveConnection = true;

	public static void main(String[] args)
	{
		String urlString = "https://opensky-network.org/api/states/all";
		PlaneDataServer server;
		
		if(haveConnection)
			server = new PlaneDataServer(urlString, latitude, longitude, 100);
		else
			server = new PlaneDataServer(latitude, longitude, 150);

		Messer messer = new Messer();
		Senser senser = new Senser(server);
		senser.addObserver(messer);
		
		new Thread(server).start();
		new Thread(messer).start();
		new Thread(senser).start();
	}
}