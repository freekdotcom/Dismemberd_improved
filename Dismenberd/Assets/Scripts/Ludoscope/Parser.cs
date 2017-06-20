using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using UnityEngine;

public class Parser : MonoBehaviour
{
    private string file = "C:\\Users\\TheCore\\Desktop\\Dismemberd_improved\\puzzle.xpr";

    private string fileData;
    private int x;
    private int y;

	// Use this for initialization
	void Start ()
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

        for (int i=0; i < x*y; i++)
        {
            var t = data[3 + i].Split(':');
            var y2 = i%y;
            var x2 = i/y;
            map[x2, y2] = t[1];
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
