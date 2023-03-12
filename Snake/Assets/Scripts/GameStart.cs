using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        GlobalParams.snakeHead = GameObject.Instantiate((GameObject)Resources.Load("snake"), GlobalParams.mainCanvas.transform); //Erstellt ein neues Objekt des "snake" Prefabs
        GlobalParams.point = GameObject.Instantiate((GameObject)Resources.Load("point"), GlobalParams.mainCanvas.transform); //Erstellt ein neues Objekt des "point" Prefabs
    }
}
