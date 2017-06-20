using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public GameObject wall;
    public GameObject open;
    public GameObject door;
    public GameObject entrance;
    public GameObject lockedDoor;
    public GameObject key;
    public GameObject Finish;

    private string file = "C:\\Users\\TheCore\\Desktop\\Dismemberd_improved\\puzzle.xpr";

    private string fileData;
    private int x;
    private int y;

    private int tileSize = 6;

    // Use this for initialization
    void Start()
    {
        // load data
        fileData = System.IO.File.ReadAllText(file);

        // parse data
        string[] data = fileData.Split(' ');

        if (data[0] != "TILEMAP")
            return;

        x = Convert.ToInt32(data[1]);
        y = Convert.ToInt32(data[2]);

        string[,] map = new string[x, y];

        for (int i = 0; i < x * y; i++)
        {
            var t = data[3 + i].Split(':');
//            var y2 = i % y;
//            var x2 = i / y;
            var y2 = i / x;
            var x2 = i % x;
            map[x2, y2] = t[1];
        }

        for (int x2 = 0; x2 < x; x2++)
        {
            for (int y2 = 0; y2 < y; y2++)
            {
                GameObject tile = getGameObject(map[x2, y2]);
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(x2 * tileSize, 0, y2 * tileSize);
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
            default:
                return new GameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
