using UnityEngine;
using UnityEngine.UI;

public static class GlobalParams
{
    //Block der bearbeitbaren Werte startet hier----------------------------------------------------------------------
    public static int increaseScore = 10; //Die Höhe, um die der Score beim Aufsammeln von Punkten erhöht wird

    public static float speedQuot = 5f; //Die Geschwindigkeit - ist 1 / speedQuot

    public static int yTiles = 16 + 1; //Verfügbare Felder + 1 (bei 17 sind also 16 verfügbar)
    public static int xTiles = 16 + 1; //Verfügbare Felder + 1 (bei 17 sind also 16 verfügbar)

    public static float padding = 10.0f; //Der Abstand des Kopfes, Körpers und der Punkte zum Rand

    public static Color snakeCol = new Color32(231, 76, 60, 255); //Farbe des Kopfes - Standard: rot
    public static Color snakeBodCol = new Color32(189, 195, 199, 255); //Farbe des Körpers - Standard: grau
    public static Color pointCol = new Color32(46, 204, 113, 255); //Farbe des Punkts - Standard: grün
    //Block der bearbeitbaren Werte endet hier-------------------------------------------------------------------------

    //Beginn Block der statischen Werte, die NICHT bearbeitet werden dürfen
    public static GameObject mainCanvas; //Der Canvas, auf dem das gesamte UI abgebildet wird
    public static GameObject backCavnas; //Der Canvas für den Hintergrund

    public static GameObject snakeHead; //Das Objekt das den Kopf der Schlange steuert
    public static GameObject point; //Der Punkt, den man einsammeln kann

    public static GameObject eventSystem; //Das Objekt, das das Eventsystem widerspiegelt

    public static bool hasLoadedOnce = false; //Wurde das Programm bereits einmal geladen? Anfangs false

    public static int score = 0; //Speichert den Score
    public static int highScore = 0; //Speichert den Highscore

    public static bool canMove = true; //Ob sich der Spieler bewegen kann

    public static Vector2[,] board = new Vector2[xTiles, yTiles]; //Erstellt ein 2D-Array namens board, was auf x so lange wie die möglichen xTiles und auf y so lange wie die möglichen yTiles lang ist

    public static int refResX = 800; //Die Breite der Testauflösung
    public static int refResY = 800; //Die Höhe des Testauflösung

    public static int currentY = 0; //Die aktuelle Y Position des Kopfes
    public static int currentX = 0; //Die aktuelle X Position des Kopfes
    public static int direction = 3; //0 = oben, 1 = links, 2 = unten, 3 = rechts

    public static float snakeWidth = 0; //Breite der Elemente (Kopf, Körper und Punkte)
    public static float snakeHeight = 0; //Höhe der Elemente (Kopf, Körper und Punkte)

    public static Color borderColor = new Color32(44, 62, 80, 255); //Die Farbe des Randes
    public static Color checkerColor1 = new Color32(52, 152, 219, 255); //Die Farbe des Schachbretts 1
    public static Color checkerColor2 = new Color32(41, 128, 185, 255); //Die Farbe des Schachbretts 2

    public static Vector2[] snakePos = new Vector2[(xTiles-1)*(yTiles-1)]; //Speichert die Koordinaten der Teile der Schlange. 0 ist der Kopf, 1 ist der 1. Teil des Körpers, usw.

    static GlobalParams()
    {
        if (PlayerPrefs.HasKey("Score")) //Wenn ein Highscore gespeichert ist
        {
            highScore = PlayerPrefs.GetInt("Score"); //Setzt den Highscore der Klasse auf den gespeicherten Highscore
        }

        SetBoard(); //Definiert die Spielfelder

        mainCanvas = GameObject.Find("Main Canvas"); //Sucht den Main Canvas
        backCavnas = GameObject.Find("Background Canvas"); //Sucht den Background Canvas

        snakeWidth = ((float)Screen.width / xTiles) - padding; //Setzt die Breite der Schlange auf die Breite eines Tiles - das Padding fest
        snakeHeight = ((float)Screen.height / yTiles) - padding; //Setzt die Höhe der Schlange auf die Höhe eines Tiles - das Padding fest

        Debug.Log("Render resolution is " + Screen.width + "x" + Screen.height); //Gibt die Render Auflösung aus
    }

    public static void SetBoard()
    {
        float widY = (float)Screen.width / (GlobalParams.xTiles - 1); //Mögliche Breite des Feldes (-1 damit ein Rand entsteht)
        float heiX = (float)Screen.height / (GlobalParams.yTiles - 1); //Mögliche Höhe des Feldes (-1 damit ein Rand entsteht)

        for (int i = 0; i < GlobalParams.xTiles; i++) //Für jedes Spielfeld in der Breits
        {
            for (int j = 0; j < GlobalParams.yTiles; j++) //Für jedes Spielfeld in der Höhe
            {
                float widXTemp = widY * i; //Breite der Felder
                float heiYTemp = heiX * j; //Breite der Felder

                board[i, j] = new Vector2(widXTemp, heiYTemp); //Speichert für jedes X und Y jeweils die richtigen Koordinaten
            }
        }
    }
}