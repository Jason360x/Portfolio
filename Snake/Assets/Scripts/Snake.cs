using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private float time = 0; //Eine Dezimalzahl time mit 0 als Startwert
    private bool hasChangedDirection = false; //Wurde die Richtung bereits geändert? Anfangs auf falsch
    private GameObject snake; //ein GameObjekt namens snake

    // Start is called before the first frame update
    private void Start()
    {
        snake = this.gameObject; //Weise dem Snake GameObjekt das GameObjekt, an das das Script hängt, zu

        snake.tag = "Player"; //Setzt das Tag des Schlangen-GameObjekt auf "Player"

        snake.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(GlobalParams.snakeWidth, GlobalParams.snakeHeight); //Setze die Größe des Kopfes auf die festgelegten x- und y-Werte
        snake.GetComponent<Image>().color = GlobalParams.snakeCol; //Legt die Farbe auf die festgelegte Farbe fest

        snake.transform.position = GlobalParams.board[1, GlobalParams.yTiles - 2]; //Setze die Position auf x = 1 und y = maxHöhe
        GlobalParams.currentY = GlobalParams.yTiles - 2; //Aktuelles Y ist maxHöhe
        GlobalParams.currentX = 1; //Aktuelles x ist 1
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Falsch Esc gedrückt wurde
        {
            GoBackToMain(); //Rufe die GoBackToMain Methode
        }

        ChangeDirection(); //Rufe die ChangeDirection Methode

        time += Time.deltaTime; //time ist gleich time + ZeitSeitLetztemFrame

        if (time > ((float)1 / GlobalParams.speedQuot)) //Ist time größer als 1 geteilt durch Geschwindigkeitsquotient
        {
            if (GlobalParams.canMove) //Falls sich die Schlange bewegen kann
            {
                MoveSnake(); //Rufe die MoveSnake Methode
                UpdatePos(); //Rufe die UpdatePos Methode

                time = 0; //Setze die Zeit auf 0
                hasChangedDirection = false; //HatPositionGeändert ist falsch
            }
        }

        for (int i = 1; i < SnakeBody.snakeBods.Length; i++) //Für jedes Körperteil
        {
            if (SnakeBody.snakeBods[i] != null) //Falls das Körperteil nicht null ist
            {
                if (GlobalParams.snakePos[i] != new Vector2(0, 0)) //Falls die Position nicht x=0, y=0 ist
                {
                    SnakeBody.snakeBods[i].transform.position = GlobalParams.snakePos[i]; //Speichere die Position des Körperteils in SnakeBods
                }
            }
        }
    }

    private void GoBackToMain()
    {
        foreach (Transform child in GlobalParams.mainCanvas.transform) //Für jedes Kindobjekt des Main Canvas
        {
            Destroy(child.gameObject); //Lösche das Kindobjekt
        }

        Destroy(GlobalParams.eventSystem.GetComponent<GameStart>()); //Entfernt das GameStart Script vom Eventsystem
        GlobalParams.eventSystem.AddComponent<MainMenu>(); //Fügt dem Eventsystem das Hauptmenü Script hinzu, damit es geladen wird
    }

    private void MoveSnake()
    {
        switch (GlobalParams.direction) //Prüfe den Wert der direction Variable
        {
            case 0: //Ist der Wert 0 -> Oben
                if (GlobalParams.currentY < GlobalParams.yTiles - 2) //Falls y nicht maximum ist
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX, GlobalParams.currentY + 1]; //Bewege die Schlange 1 nach oben
                    GlobalParams.currentY++; //Erhöhe das aktuelle Y um 1
                }
                else //Y ist maximal
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX, 1]; //Setze Position auf y minimum
                    GlobalParams.currentY = 1; //Setze das aktuelle Y auf minimum
                }
                break;
            case 1: //Ist der Wert 1 -> Links
                if (GlobalParams.currentX > 1) //Falls x nicht minimal ist
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX - 1, GlobalParams.currentY]; //Setze die Schlange 1 nach links
                    GlobalParams.currentX--; //Verringere das aktuelle X um 1
                }
                else //X ist minimal
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.xTiles - 2, GlobalParams.currentY]; //Setze die Schlange auf x maximal
                    GlobalParams.currentX = GlobalParams.xTiles - 2; //Das aktuelle X ist maximal
                }
                break;
            case 2: //Ist der Wert 2 -> Unten
                if (GlobalParams.currentY > 1) //Ist Y nicht minmal
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX, GlobalParams.currentY - 1]; //Setze die Schlange 1 nach unten
                    GlobalParams.currentY--; //Verringere das aktuelle Y um 1
                }
                else //Y ist minimal
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX, GlobalParams.yTiles - 2]; //Setze die Schlange auf y maximal
                    GlobalParams.currentY = GlobalParams.yTiles - 2; //Setze das aktuelle Y auf maximal
                }
                break;
            case 3: //Ist der Wert 3 -> Rechts
                if (GlobalParams.currentX < GlobalParams.xTiles - 2) //Ist X nicht maximal
                {
                    snake.transform.position = GlobalParams.board[GlobalParams.currentX + 1, GlobalParams.currentY]; //Setze die Schlange 1 nach rechts
                    GlobalParams.currentX++; //Erhöhe das aktuelle X um 1
                }
                else //X ist maximal
                {
                    snake.transform.position = GlobalParams.board[1, GlobalParams.currentY]; //Bewege die Schlange aufs minimum
                    GlobalParams.currentX = 1; //Setze das aktuelle X auf minimum
                }
                break;
        }
    }

    private void ChangeDirection()
    {
        if (!hasChangedDirection) //Falls die Richtung noch nicht geändert wurde
        {
            if (Input.GetKeyDown("up")) //Prüfe ob nach oben gedrückt wurde
            {
                if (GlobalParams.direction != 2) //Falls die aktuelle Richtung nicht unten ist
                {
                    GlobalParams.direction = 0; //Setze Richtung auf oben
                    hasChangedDirection = true; //Es wurde sich bereits bewegt
                }
            }
            else if (Input.GetKeyDown("left")) //Prüfe ob nach links gedrückt wurde
            {
                if (GlobalParams.direction != 3) //Falls die aktuelle Richtung nicht rechts ist
                {
                    GlobalParams.direction = 1; //Ändere Richtung auf links
                    hasChangedDirection = true; //Es wurde sich bereits bewegt
                }
            }
            else if (Input.GetKeyDown("right")) //Prüfe ob nach rechts gedrückt wurde
            {
                if (GlobalParams.direction != 1) //Prüfe ob aktuelle Richtung nichts links ist
                {
                    GlobalParams.direction = 3; //Setze Richtung auf rechts
                    hasChangedDirection = true; //Es wurde sich bereits bewegt
                }
            }
            else if (Input.GetKeyDown("down")) //Prüfe ob nach unten gedrückt wurde
            {
                if (GlobalParams.direction != 0) //Falls die aktuelle Richtung nicht oben ist
                {
                    GlobalParams.direction = 2; //Setze Richtung nach unten
                    hasChangedDirection = true; //Es wurde sich bereits bewegt
                }
            }
        }
    }

    private void UpdatePos()
    {
        if (GlobalParams.snakePos[0] == null) //Falls die Position des Kopfes null ist (Ist nur 1x true)
        {
            GlobalParams.snakePos[0] = GlobalParams.board[GlobalParams.currentX, GlobalParams.currentY]; //Setze die Position des Kopfes als snakePos[0]
        }

        for (int i = SnakeBody.snakeID; i > 0; i--) //Für jedes Körperteil, rückwärts
        {
            GlobalParams.snakePos[i] = GlobalParams.snakePos[i - 1]; //Setze die Position des Teils auf das vorherige Körperteil
        }

        GlobalParams.snakePos[0] = GlobalParams.board[GlobalParams.currentX, GlobalParams.currentY]; //Setze snakePos[0] auf die Position des Kopfes
    }
}