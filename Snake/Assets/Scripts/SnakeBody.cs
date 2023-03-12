using UnityEngine;
using UnityEngine.UI;

public static class SnakeBody
{
    public static int snakeID = 0; //Speichert die Menge der K�rperteile

    public static GameObject[] snakeBods = new GameObject[(GlobalParams.xTiles - 1) * (GlobalParams.yTiles - 1)]; //Speichert jedes K�rperteil und die Koordinaten dazu

    public static void CreateBod() //Erstellt ein neues K�rper-Objekt
    {
        snakeID++; //Erh�ht die Anzahl der K�rperteile um 1

        GameObject snakeBod = new GameObject("snakeBody " + snakeID); //Erstellt ein neues GameObject namens "snakeBody " und die Zahl der K�rperteile

        snakeBod.AddComponent<RectTransform>(); //F�gt ein neues RectTransform hinzu
        snakeBod.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf unten links
        snakeBod.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^

        snakeBod.AddComponent<Image>(); //F�gt ein neues Bildkomponent hinzu
        snakeBod.GetComponent<Image>().color = GlobalParams.snakeBodCol; //Setzt die Farbe des Bildes auf die in GlobalParams festgelegte Schlangenk�rperfarbe
        snakeBod.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(GlobalParams.snakeWidth-GlobalParams.padding, GlobalParams.snakeHeight-GlobalParams.padding); //Stellt sicher, dass die K�rperteile so gro� sind, wie der Kopf - das Padding sind (damit der K�rper etwas kleiner ist)

        snakeBod.AddComponent<BoxCollider2D>(); //F�gt ein BoxCollider f�r die Kollisionspr�fung hinzu
        snakeBod.GetComponent<BoxCollider2D>().size = new Vector2(GlobalParams.snakeWidth, GlobalParams.snakeHeight); //Setzt die Gr��e der Kollisionsbox auf die Gr��e des Kopfes (die Hitbox ist also gr��er als der K�rper)
        snakeBod.GetComponent<BoxCollider2D>().isTrigger = true; //Setzt den isTrigger Parameter auf true (Vermutlich nicht ben�tigt)

        snakeBod.AddComponent<Rigidbody2D>(); //F�gt ein Rigidbody hinzu (Wird ebenfalls f�r Kollisionspr�fung gebraucht)
        snakeBod.GetComponent<Rigidbody2D>().gravityScale = 0; //Setzt die Graviation auf 0, sonst w�rde die Schlange fallen
        snakeBod.GetComponent<Rigidbody2D>().drag = 0; //Setzt zur Sicherheit den Luftwiderstand auf 0
        snakeBod.GetComponent<Rigidbody2D>().angularDrag = 0; //Setzt zur Sicherheit den seitlichen Luftwiderstand auf 0
        snakeBod.GetComponent<Rigidbody2D>().mass = 0; //Setzt zur Sicherheit die Masse auf 0 (Setzt sich auf 0.00001 nach start, weil Unity)

        snakeBod.AddComponent<SnakeCollision>(); //F�gt das SnakeCollision Script dem K�rper hinzu

        snakeBod.transform.SetParent(GlobalParams.mainCanvas.transform); //Macht den K�rper zu einem Kindsobjekt des mainCanvas
        snakeBod.transform.position = new Vector2(-Screen.width, -Screen.height);

        snakeBods[snakeID] = snakeBod; //F�gt den erstellen K�rper zum Array, was die K�rperteile z�hlt hinzu
    }
}