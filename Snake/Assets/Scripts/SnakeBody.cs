using UnityEngine;
using UnityEngine.UI;

public static class SnakeBody
{
    public static int snakeID = 0; //Speichert die Menge der Körperteile

    public static GameObject[] snakeBods = new GameObject[(GlobalParams.xTiles - 1) * (GlobalParams.yTiles - 1)]; //Speichert jedes Körperteil und die Koordinaten dazu

    public static void CreateBod() //Erstellt ein neues Körper-Objekt
    {
        snakeID++; //Erhöht die Anzahl der Körperteile um 1

        GameObject snakeBod = new GameObject("snakeBody " + snakeID); //Erstellt ein neues GameObject namens "snakeBody " und die Zahl der Körperteile

        snakeBod.AddComponent<RectTransform>(); //Fügt ein neues RectTransform hinzu
        snakeBod.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf unten links
        snakeBod.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^

        snakeBod.AddComponent<Image>(); //Fügt ein neues Bildkomponent hinzu
        snakeBod.GetComponent<Image>().color = GlobalParams.snakeBodCol; //Setzt die Farbe des Bildes auf die in GlobalParams festgelegte Schlangenkörperfarbe
        snakeBod.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(GlobalParams.snakeWidth-GlobalParams.padding, GlobalParams.snakeHeight-GlobalParams.padding); //Stellt sicher, dass die Körperteile so groß sind, wie der Kopf - das Padding sind (damit der Körper etwas kleiner ist)

        snakeBod.AddComponent<BoxCollider2D>(); //Fügt ein BoxCollider für die Kollisionsprüfung hinzu
        snakeBod.GetComponent<BoxCollider2D>().size = new Vector2(GlobalParams.snakeWidth, GlobalParams.snakeHeight); //Setzt die Größe der Kollisionsbox auf die Größe des Kopfes (die Hitbox ist also größer als der Körper)
        snakeBod.GetComponent<BoxCollider2D>().isTrigger = true; //Setzt den isTrigger Parameter auf true (Vermutlich nicht benötigt)

        snakeBod.AddComponent<Rigidbody2D>(); //Fügt ein Rigidbody hinzu (Wird ebenfalls für Kollisionsprüfung gebraucht)
        snakeBod.GetComponent<Rigidbody2D>().gravityScale = 0; //Setzt die Graviation auf 0, sonst würde die Schlange fallen
        snakeBod.GetComponent<Rigidbody2D>().drag = 0; //Setzt zur Sicherheit den Luftwiderstand auf 0
        snakeBod.GetComponent<Rigidbody2D>().angularDrag = 0; //Setzt zur Sicherheit den seitlichen Luftwiderstand auf 0
        snakeBod.GetComponent<Rigidbody2D>().mass = 0; //Setzt zur Sicherheit die Masse auf 0 (Setzt sich auf 0.00001 nach start, weil Unity)

        snakeBod.AddComponent<SnakeCollision>(); //Fügt das SnakeCollision Script dem Körper hinzu

        snakeBod.transform.SetParent(GlobalParams.mainCanvas.transform); //Macht den Körper zu einem Kindsobjekt des mainCanvas
        snakeBod.transform.position = new Vector2(-Screen.width, -Screen.height);

        snakeBods[snakeID] = snakeBod; //Fügt den erstellen Körper zum Array, was die Körperteile zählt hinzu
    }
}