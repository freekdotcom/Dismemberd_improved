using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using Boo.Lang.Runtime;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public TextAsset file2;

    public GameObject wall;
    public GameObject open;
    public GameObject door;
    public GameObject entrance;
    public GameObject lockedDoor;
    public GameObject key;
    public GameObject Finish;
    public GameObject bones;
    public GameObject pressurePlate;
    public GameObject hardPuzzleNormal;

    private string file = @"C:\Users\siem\Documents\Dismemberd_improved\puzzle.xpr";

    private string fileData;
    private int x;
    private int y;

    private int tileSize = 6;

    // Use this for initialization
    void Start()
    {
        // load data
//        fileData = System.IO.File.ReadAllText(file);
        fileData = file2.text;

        // parse data
        string[] data = fileData.Split(' ');

        if (data[0] != "TILEMAP")
            return;

        x = Convert.ToInt32(data[1]);
        y = Convert.ToInt32(data[2]);

        string[,] map = new string[x, y];

        // Parse file to map data
        for (int i = 0; i < x * y; i++)
        {
            var t = data[3 + i].Split(':');
            var y2 = i / x;
            var x2 = i % x;
            map[x2, y2] = t[1];
        }

        var rooms = getRooms(map);

        // Load in Map
        for (int x2 = 0; x2 < x; x2++)
        {
            for (int y2 = 0; y2 < y; y2++)
            {
                GameObject tile = getGameObject(map[x2, y2]);
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(x2 * tileSize, 0, y2 * tileSize);

                if (tile == door || tile == lockedDoor)
                {
                    if (map[x2, y2 + 1] != "wall")
                    {
                        newTile.transform.Rotate(Vector3.up, 90);
                        newTile.transform.position = newTile.transform.position - new Vector3((tileSize / 2), 0, (tileSize / 2) * -1);
                    }
                }

                if (tile == entrance)
                {
                    if (map[x2, y2 + 1] != "wall")
                    {
                        newTile.transform.Rotate(Vector3.up, -90);
                        newTile.transform.position = newTile.transform.position - new Vector3((tileSize / 2) - 6, 0, (tileSize / 2) * -1);
                    }
                    if (map[x2, y2 - 1] != "wall")
                    {
                        newTile.transform.Rotate(Vector3.up, 90);
                        newTile.transform.position = newTile.transform.position - new Vector3((tileSize / 2), 0, (tileSize / 2) * -1);
                    }
                    if (map[x2 + 1, y2] != "wall")
                    {
                        //newTile.transform.Rotate(Vector3.up,);
                        newTile.transform.position = newTile.transform.position - new Vector3((tileSize / 2) - 3, 0, (tileSize / 2) * -1 + 3);

                    }
                    if (map[x2 - 1, y2] != "wall")
                    {
                        newTile.transform.Rotate(Vector3.up, 180);
                        newTile.transform.position = newTile.transform.position - new Vector3((tileSize / 2) - 3, 0, (tileSize / 2) * -1 - 3);
                    }
                }
            }
        }
    }

    private GameObject getGameObject(string i)
    {
        switch (i)
        {
            case "wall":
                return wall;
            case "open":
                return open;
            case "key":
                return key;
            case "door":
                return door;
            case "entrance":
                return entrance;
            case "ending":
                return Finish;
            case "lockedDoor":
                return lockedDoor;
            case "bone":
                return bones;
            case "pressurePlate":
                return pressurePlate;
            case "hardpuzzle":
                return hardPuzzleNormal;
            default:
                return new GameObject();
        }
    }

    private List<Vector2> getRooms(string[,] map)
    {
        List<Vector2> rooms = new List<Vector2>();

        int roomSize = 5;

        for (int x2 = 0; x2 < x - roomSize; x2++)
        {
            for (int y2 = 0; y2 < y - roomSize; y2++)
            {
                // Check
                bool failed = false;
                for (int i= x2; i < x2 + roomSize; i++)
                    for (int l = y2; l < y2 + roomSize; l++)
                        if (map[i, l] == "wall")
                        {
                            failed = true;
                            break;
                        }

                if (!failed)
                {
                    rooms.Add(new Vector2(x2,y2));
                }
            }
        }

        return rooms;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
