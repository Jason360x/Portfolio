using UnityEngine;
using UnityEngine.UI;

public class SetScene : MonoBehaviour
{
    private float width = Screen.width; //Die Breite des Bildschirms
    private float height = Screen.height; //Die Höhe des Bildschirms
    private float widthX; //Die Breite eines Feldes
    private float heightY; //Die Höhe eines Feldes

    private void Start()
    {
        widthX = width / (GlobalParams.xTiles - 1); //Speichert die Breite jedes Feldes
        heightY = height / (GlobalParams.yTiles - 1); //Speichert die Höhe jedes Feldes

        SetWalls(); //Startet die SetWalls() Methode
        SetFloor(); //Startet die SetFloor() Methode
    }

    private void SetWalls()
    {
        GameObject wall = new GameObject("wall"); //Erstellt ein neues "wall" GameObjekt

        wall.AddComponent<RectTransform>(); //Fügt ein RectTransform hinzu
        wall.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf unten links
        wall.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben^
        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(widthX, heightY); //Setzt die größe auf die Höhe und Breite eines normalen Feldes

        wall.AddComponent<Image>(); //Fügt ein Bildkomponent hinzu
        wall.GetComponent<Image>().color = GlobalParams.borderColor; //Setzt die Wandfarbe auf borderColor, in GlobalParams definiert

        GameObject wallX = wall; //Definiert das neue GameObject wallX als wall
        wallX.GetComponent<RectTransform>().sizeDelta = new Vector2(width, heightY); //Ändert die Größe auf die Breite des Bildschirms und die Höhe eines einzelnen Feldes
        GameObject wallX1 = GameObject.Instantiate(wallX, GlobalParams.backCavnas.transform); //Erstellt ein neues Objekt von wallX namens wallX1
        wallX1.transform.position = new Vector2(width * 0.5f, 0); //Positioniert das GameObject in der Mitte unten
        GameObject wallX2 = GameObject.Instantiate(wallX, GlobalParams.backCavnas.transform); //Erstellt ein neues Objekt von wallX namens wallX2
        wallX2.transform.position = new Vector2(width * 0.5f, height); //Positioniert das Gameobjekt in der Mitte oben

        GameObject wallY = wall; //Definiert das neue GameObject wallY als wall
        wallY.GetComponent<RectTransform>().sizeDelta = new Vector2(widthX, height); //Ändert die Größe auf die Breite eines Feldes und die Höhe des Bildschirms
        GameObject wallY1 = GameObject.Instantiate(wallY, GlobalParams.backCavnas.transform); //Erstellt ein neues Objekt von wallY namens wallY1
        wallY1.transform.position = new Vector2(0, height * 0.5f); //Positioniert das GameObject links mittig
        GameObject wallY2 = GameObject.Instantiate(wallY, GlobalParams.backCavnas.transform); //Erstellt ein neues Objekt von wallY namens wallY2
        wallY2.transform.position = new Vector2(width, height * 0.5f); //Positioniert das GameObject rechts mittig

        GameObject.Destroy(wallX); //Zerstört die wallX Vorlage
        GameObject.Destroy(wallY); //Zerstört die wallY Vorlage
        GameObject.Destroy(wall); //Zerstört die wall Vorlage
    }

    private void SetFloor()
    {
        GameObject floor = new GameObject("floor"); //Erstellt ein neues Floor GameObjekt

        floor.AddComponent<RectTransform>(); //Fügt ein RectTransform hinzu
        floor.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0); //Setzt den Anker auf unten links
        floor.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0); //Siehe oben
        floor.GetComponent<RectTransform>().sizeDelta = new Vector2(widthX, heightY); //Setzt die Größe und Breite auf die eines Feldes

        floor.AddComponent<Image>(); //Fügt eine Bild Komponente hinzu
        floor.GetComponent<Image>().color = GlobalParams.checkerColor2; //Setzt die Farbe auf checkerColor2, definiert in GlobalParams

        GameObject[] floors = new GameObject[GlobalParams.board.Length]; //Erstellt ein neues Array der Länge der Felder namens floors

        for (int i = 1; i < GlobalParams.xTiles-1; i++) //Für jedes Spielfeld in der Breite
        {
            for (int j = 1; j < GlobalParams.yTiles-1; j++) //Für jedes Spielfeld in der Höhe
            {
                floors[i] = GameObject.Instantiate(floor, GlobalParams.backCavnas.transform); //Erstellt ein neues floor GameObject

                if (i % 2 != 0 && j % 2 != 0) //Wenn x nicht durch 2 teilbar ist und y auch
                {
                    floors[i].GetComponent<Image>().color = GlobalParams.checkerColor1; //Setzt die Farbe auf checkerColor1, definiert in GlobalParams
                }
                else if (i % 2 == 0 && j % 2 == 0) //Wenn x durch 2 teilbar ist, y aber nicht
                {
                    floors[i].GetComponent<Image>().color = GlobalParams.checkerColor1; //Setzt die Farbe auf checkerColor1, definiert in GlobalParams
                }

                float widXTemp = widthX * i; //Breite der Felder
                float heiYTemp = heightY * j; //Breite der Felder

                floors[i].transform.position = new Vector2(widXTemp, heiYTemp); //Setzt die Position für jedes floorTile
            }
        }

        GameObject.Destroy(floor); //Löscht die Floor Vorlage
    }
}
