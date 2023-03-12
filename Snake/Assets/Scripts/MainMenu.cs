using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameObject menuCanvas; //Das Hauptmen� Objekt

    // Start is called before the first frame update
    void Start()
    {
        CreateMenu(); //Erstellt das Hauptmen�
        if (!GlobalParams.hasLoadedOnce) //�berpr�ft ob bereits einmal geladen wurde
        {
            GlobalParams.eventSystem = this.gameObject;

            this.gameObject.AddComponent<SetScene>(); //F�gt dem GameObject an dem das Script h�ngt das Script SetScene hinzu
            GlobalParams.hasLoadedOnce = true; //Setzt den Bool hatEinmalGeladen auf true
        }

        ResetGame(); //Ruft die ResetGame Methode auf
    }

    void ResetGame()
    {
        GlobalParams.score = 0; //Setzt den aktuellen Score auf 0
        GlobalParams.canMove = true; //Erlaubt der Schlange wieder sich zu bewegen
        GlobalParams.direction = 3; //Setzt die Richtung auf rechts

        for (int i = 0; i < GlobalParams.snakePos.Length; i++) //F�r jede m�gliche Position der Schlange
        {
            GlobalParams.snakePos[i] = new Vector2(0, 0); //Setze die Position auf x = 0, y = 0
        }

        for (int i = 0; i < SnakeBody.snakeBods.Length; i++) //F�r jedes K�rperteil der Schlange
        {
            SnakeBody.snakeBods[i] = null; //L�sche das K�rperteil
        }

        SnakeBody.snakeID = 0; //Setze die L�nge der Schlange auf 0

        if (PlayerPrefs.HasKey("Score")) //Wenn es den Score Schl�ssel gibt
        {
            GlobalParams.highScore = PlayerPrefs.GetInt("Score"); //Setze den HighScore auf den gespeicherten High Score
        }
    }

    void StartGame() //Startet das Spiel und schlie�t das Hauptmen�
    {
        this.gameObject.AddComponent<GameStart>(); //F�gt dem GameObject an dem das Script h�ngt das Script GameStart hinzu

        GameObject.Destroy(menuCanvas); //L�scht das Hauptmen� Canvas, damit es nicht mehr angezeigt wird
        Destroy(this.gameObject.GetComponent<MainMenu>());
    }

    void CloseGame()
    {
        Application.Quit(); //Beendet das Spiel
    }

    void CreateMenu()
    {
        menuCanvas = new GameObject("Menu Canvas"); //Erstellt ein neues GameObject mit dem Name "Menu Canvas"
        menuCanvas.AddComponent<Canvas>(); //F�gt dem erstellten GameObject ein neues Canvas hinzu
        menuCanvas.GetComponent<Canvas>().sortingOrder = 2; //Setzt die Sortierreihenfolge auf 2 (damit es �ber dem Hintergrund und den Spielobjekten ist)
        menuCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay; //Legt den Rendermode auf Screen Space Overlay fest
        menuCanvas.layer = 5; //Setzt das GameObject in den UI Bereich

        menuCanvas.AddComponent<CanvasScaler>(); //F�gt einen Canvas Scaler hinzu
        menuCanvas.AddComponent<GraphicRaycaster>(); //F�gt einen Graphic Raycaster hinzu

        AddBackgroundBlur(menuCanvas);

        GameObject JDSnake = new GameObject("JDSnake"); //Erstellt ein neues GameObject mit dem Name "JDSnake"

        JDSnake.AddComponent<RectTransform>(); //F�gt dem GameObject ein RectTransform hinzu
        JDSnake.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Legt die Anker in die untere linke Ecke
        JDSnake.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^
        JDSnake.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height*0.15f); //Legt die Gr��e auf x = Bildschirmbreite und y = 10% Bildschirmbreite fest

        JDSnake.AddComponent<Text>(); //F�gt ein Textkomponent hinzu
        JDSnake.GetComponent<Text>().text = "JD Snake"; //Legt den Text auf "JD Snake" fest
        JDSnake.GetComponent<Text>().font = Resources.Load("Fonts/On My Way Free Version") as Font; //Setzt das Font fest
        JDSnake.GetComponent<Text>().resizeTextForBestFit = true; //Setzt fest, dass die Gr��e des Texts automatisch skaliert wird
        JDSnake.GetComponent<Text>().resizeTextMaxSize = 300; //Setzt die maximale Gr��e des Texts fest
        JDSnake.GetComponent<Text>().alignment = TextAnchor.MiddleCenter; //Setzt den Text auf die Mitte der Breite und H�he fest

        JDSnake.transform.SetParent(menuCanvas.transform); //Macht das GameObject zu einem Kind von Menu Canvas
        JDSnake.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.6f); //Legt die Position des Texts auf 50% Breite und 60% H�he fest

        GameObject startButton = GameObject.Instantiate((GameObject)Resources.Load("startButton")); //Erstellt ein neues GameObject des startButton Prefabs
        startButton.transform.SetParent(menuCanvas.transform); //Macht das GameObject zu einem Kind von Menu Canvas

        startButton.GetComponent<Button>().onClick.AddListener(StartGame); //F�gt ein Click Event hinzu und setzt es auf StartGame

        GameObject quitButton = GameObject.Instantiate((GameObject)Resources.Load("stopButton")); //Erstellt ein neues GameObject des stopButton Prefabs
        quitButton.transform.SetParent(menuCanvas.transform); //Macht das GameObject zu einem Kind von Menu Canvas

        quitButton.GetComponent<Button>().onClick.AddListener(CloseGame); //F�gt ein Click Event hinzu und setzt es auf CloseGame

        quitButton.transform.position = new Vector2(quitButton.transform.position.x, quitButton.transform.position.y - Screen.height * 0.05f); //Legt die Position des Knopfes auf x = wie zuvor und y = zuvor - 5% fest
    }

    void AddBackgroundBlur(GameObject canvas)
    {
        GameObject imageBlur = new GameObject("imageBlur"); //Erstellt ein neues GameObject namens imageBlur

        imageBlur.AddComponent<RectTransform>(); //F�gt ein RectTransform hinzu
        imageBlur.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f); //Legt die Anker in Mitte des Bildes
        imageBlur.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f); //Siehe oben^
        imageBlur.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height); //Legt die Gr��e auf x = Bildschirmbreite und y = Bildschirmbreite fest

        imageBlur.AddComponent<Image>(); //F�gt ein neues Imagekomponent hinzu
        imageBlur.GetComponent<Image>().sprite = Resources.Load<Sprite>("backgroundBlur"); //Teilt dem Image das Bild "backgroundBlur" zu

        imageBlur.transform.SetParent(canvas.transform); //Erkl�rt das GameObject zu einem Kind des gegebenen Canvas
        imageBlur.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //Setzt die Position auf 50% der H�he und der Breite des Bildschirms
    }
}
