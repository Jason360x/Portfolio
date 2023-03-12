using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject point; //Das GameObjekt, das den Punkt widerspiegelt

    private GameObject scoreText; //Das GameObjekt, das den ScoreText widerspiegelt

    private void Start()
    {
        CreateScoreText(); //Rufe die CreateScoreText Methode

        point = this.gameObject; //Das point GameObjekt entspricht dem Objekt, an das das Script angehängt ist

        point.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalParams.snakeWidth, GlobalParams.snakeHeight); //Setze die Größe des Punktes auf die Größe des Schlangenkörpers
        point.GetComponent<Image>().color = GlobalParams.pointCol; //Setze die Farbe des Punktes auf die festgelegte Farbe
        ChangePointPos(); //Ändere die Position des Punktes
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //Wenn die Kollision mit dem Spieler war
        {
            GlobalParams.score += GlobalParams.increaseScore; //Erhöhe den Score um die festgelegte Zahl
            scoreText.GetComponent<Text>().text = "Score: " + GlobalParams.score + ", High Score: " + GlobalParams.highScore; //Aktualisiere den Score Text

            SnakeBody.CreateBod(); //Erstelle ein neues Körperteil

            ChangePointPos(); //Ändere die Position des Punktes
        }
    }

    private void ChangePointPos()
    {
        bool pointGenerated = false; //Punkt wurde noch nicht generiert

        while (!pointGenerated) //Solange noch kein Punkt generiert wurde
        {
            int yPos = Random.Range(1, GlobalParams.yTiles - 2); //y-Position des Punkts ist zufällig, aus der verfügbaren Höhe
            int xPos = Random.Range(1, GlobalParams.xTiles - 2); //x-Position des Punkts ist zufällig, aus der verfügbaren Breite

            if (CheckIfPosIsFree(xPos, yPos)) //Falls CheckIfPointIsFree true returned und gebe die x- und y-Positionen weiter
            {
                pointGenerated = true; //Punkt wurde generiert

                point.transform.position = GlobalParams.board[xPos, yPos]; //Position des Punktes sind die berechneten Koordinaten
            }
        }
    }

    private bool CheckIfPosIsFree(int xPos, int yPos)
    {
        bool returnBool = true; //Rückgabewert ist true

        for (int i = 0; i < (GlobalParams.xTiles-1) * (GlobalParams.yTiles-1); i++) //Für jedes Feld auf dem Board
        {
            if (GlobalParams.board[xPos, yPos] == GlobalParams.snakePos[i]) //Falls das Feld generierte Feld mit einer Schlangenposition übereinstimmt
            {
                returnBool = false; //Rückgabewert ist falsch

                Debug.Log("Generated point is on snake."); //Gibt eine Meldung aus, dass der generierte Punkt auf der Schlange wäre
            }
        }

        return returnBool; //Gibt returnBool zurück
    }

    private void CreateScoreText()
    {
        scoreText = new GameObject("Score"); //ScoreText ist ein neues GameObjekt mit dem Name "Score"

        scoreText.AddComponent<RectTransform>(); //Fügt dem scoreText ein RectTransform hinzu
        scoreText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf die untere linke Ecke
        scoreText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^
        scoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height); //Setzt die Größe auf die Größe des Bildschirms

        scoreText.AddComponent<Text>(); //Fügt dem scoreText ein Text hinzu
        scoreText.GetComponent<Text>().text = "Score: " + GlobalParams.score + ", High Score: " + GlobalParams.highScore; //Setzt den Text fest
        scoreText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font; //Ändert das Font auf Arial

        float fontMod = 20 * (Screen.width / GlobalParams.refResX); //Dezimalzahl fontMod ist 20 * (Breite des Bildschirms geteilt durch Referenzauflösung), damit die Schrift immer relativ gleich groß ist
        scoreText.GetComponent<Text>().fontSize = (int)fontMod; //Setzt die Größe des Fonts auf fontMod
        scoreText.GetComponent<Text>().color = Color.black; //Setzt die Farbe des Texts auf schwarz

        scoreText.transform.SetParent(GlobalParams.mainCanvas.transform); //Macht den ScoreText zu einem Kind des MainCanvas
        scoreText.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //Setzt die Position des Scoretexts auf oben links
    }
}