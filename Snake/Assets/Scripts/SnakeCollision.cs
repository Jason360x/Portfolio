using UnityEngine;
using UnityEngine.UI;

public class SnakeCollision : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //Prüft ob die Kollision mit dem Spieler war
        {
            GlobalParams.canMove = false; //Setzt den Bool auf falsch, damit die Bewegung endet
            Debug.Log("Crash"); //Zeigt im Log an, dass eine Kollision passierte

            if (PlayerPrefs.HasKey("Score")) //Wenn es bereits den Key Score gibt
            {
                if (PlayerPrefs.GetInt("Score") < GlobalParams.score) //Überprüft ob der aktuelle Score höher als der Highscore ist
                {
                    PlayerPrefs.SetInt("Score", GlobalParams.score); //Speichert den aktuellen Score als Highscore
                }
            }
            else
            {
                PlayerPrefs.SetInt("Score", GlobalParams.score); //Wenn es noch keinen Score gibt, speichert es den aktuellen Score als Highscore
            }

            GameObject GameOverText = new GameObject("GameOver"); //Erstellt ein neues GameObject namens GameOverText

            GameOverText.AddComponent<RectTransform>(); //Fügt ein RectTransform hinzu
            GameOverText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf unten links
            GameOverText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^
            GameOverText.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height); //Legt die Größe auf den kompletten Bildschirm fest

            GameOverText.AddComponent<Text>(); //Fügt ein Textkomponent hinzu
            GameOverText.GetComponent<Text>().text = "Game Over!"; //Legt den Text auf "Game Over!" fest
            GameOverText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font; //Weist dem Text das Arial Font zu
            GameOverText.GetComponent<Text>().fontSize = 50; //Legt die Fontgröße auf 50 fest
            GameOverText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter; //Legt die Position des Textes auf die Mitte des Bildschirms

            GameOverText.transform.SetParent(GlobalParams.mainCanvas.transform); //Macht das GameObject zu einem Kind des Main Canvas
            GameOverText.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //Legt die Position auf die Mitte des Bildschirms fest

            CreateMainMenuButton(); //Erstellt den Button, um zum Hauptmenü zurückzukommen
        }
    }

    void CreateMainMenuButton()
    {
        GameObject mainMenuButton = GameObject.Instantiate(Resources.Load("backToMainButton") as GameObject); //Lädt den MainMenuButton aus den Prefabs

        mainMenuButton.transform.SetParent(GlobalParams.mainCanvas.transform); //Setzt den MainMenuButton als Kind des MainCanvas
        mainMenuButton.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.4f); //Legt die Position auf die Mitte des Bildschirms (40% bei Höhe) fest

        mainMenuButton.GetComponent<Button>().onClick.AddListener(GoBackToMain); //Fügt dem Button einen OnClickListener hinzu, der GoBackToMain ruft, um da Hauptmenü zu öffnen
    }

    void GoBackToMain()
    {
        foreach (Transform child in GlobalParams.mainCanvas.transform) //Für jedes Kindobjekt des Main Canvas
        {
            Destroy(child.gameObject); //Lösche das Kindobjekt
        }

        Destroy(GlobalParams.eventSystem.GetComponent<GameStart>()); //Entfernt das GameStart Script vom Eventsystem
        GlobalParams.eventSystem.AddComponent<MainMenu>(); //Fügt dem Eventsystem das Hauptmenü Script hinzu, damit es geladen wird
    }
}