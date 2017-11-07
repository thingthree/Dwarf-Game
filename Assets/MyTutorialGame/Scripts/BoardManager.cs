using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;

public class BoardManager : MonoBehaviour

{
    /* The board manager class is used to instantiate prefabs of game objects and game object arrays.
     * The board manager also holds a new game method, NewGame, which defines GameState.Current
     * when there is no saves.load[0] (default start gamestate for new game) to pull from.
     */

    /* The board manager also has the added advantage of creating a quick prefab with the gridpositions function.
     * To use the board manager as a map creation tool, set GameManager.loadedGame to true manually to avoid loading
     * an instance of the actual game.*/
     

    // declaration of prefab arrays and variables needed to create the grid and instantiate game objects
    private int columns;
    private int rows;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] mapSet;
    public GameObject[] ui;
    public GameObject[] uiFrame;
    public GameObject[] players;



    /* note: when adding to the bmid text files, add from the 
     beginning of the current ''type'' of objects to be instantiated 
     so bmid locations can be tracked using their distance from myArray[].length
     e.g. the first mapset added doesn't have a bmid of 1, it has a bmid of mapSet.length
     as mapsets are the first type of bmid, then mapSet.length - 1 and so on*/

    // declaration of arrays needed to use board manager ID system

    public static string[] columnsString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidColumns.txt");
    public static int[] columnsInt = columnsString.Select(x => int.Parse(x)).ToArray();
    public static string[] rowsString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidRows.txt");
    public static int[] rowsInt = rowsString.Select(x => int.Parse(x)).ToArray();
    public static string[] xString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidX.txt");
    public static int[] xInt = xString.Select(x => int.Parse(x)).ToArray();
    public static string[] yString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidY.txt");
    public static int[] yInt = yString.Select(x => int.Parse(x)).ToArray();



    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    public void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = -1; x < columns - 1; x++)
        {
            for (int y = -1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3((x), (y), 0f));
            }
        }
    }

    public void BoardController(int bmid)
    {

        ////uncomment here to use BoardController for quick prefab creation 

        //boardHolder = new GameObject("Board").transform;
        //if (bmid == 0)
        //{
        //  columns = columnsInt[bmid];
        //  rows = rowsInt[bmid];
        //    
        //  for (int x = -1; x < columns + 1; x++)
        //    {
        //        for (int y = -1; y < rows + 1; y++)
        //        {
        //            if (bmid == 0)
        //            {
        //                GameObject toInstantiate = ui[bmid];

        //                if (y == rows)
        //                {
        //                    toInstantiate = uiFrame[0];
        //                    GameObject instance =
        //                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        //                    instance.transform.SetParent(boardHolder);
        //                }
        //                if (y == -1)
        //                {
        //                    toInstantiate = uiFrame[1];

        //                    GameObject instance =
        //                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        //                    instance.transform.SetParent(boardHolder);
        //                }
        //                if (x == -1)
        //                {
        //                    toInstantiate = uiFrame[2];

        //                    GameObject instance =
        //                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        //                    instance.transform.SetParent(boardHolder);
        //                }
        //                if (x == columns)
        //                {
        //                    toInstantiate = uiFrame[3];

        //                    GameObject instance =
        //                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        //                    instance.transform.SetParent(boardHolder);
        //                }
        //            }
        //        }
        //    }
        //}

        // If the game has already been loaded, BoardController functions using bmid's to instantiate prefabs

        if (GameManager.loadedGame == true)
        {

            if (bmid != 0)
            {
                int x = xInt[bmid];
                int y = yInt[bmid];
                GameObject toInstantiate = mapSet[bmid];
                GameObject instance =
                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        } 

        // If the game has not already been loaded, BoardController instantiates bmid 0, (main menu),
        // and places the players on the map, if this is the first time the game has ever been played
        // there will be no save file, so BoardController will place players in their starting locations
        // and create a save file

        /* under construction*/

        else if (GameManager.loadedGame == false)
        {
            // places map

            int x = xInt[bmid];
            int y = yInt[bmid];
            GameObject toInstantiate = mapSet[bmid];
            GameObject instance =
            Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);

            // if it's a new game, but we have a saveStates[0], aka new game save state

            if (File.Exists( @"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References" + "/savedGames.gd"))
            {
                Saves.Load();
                GameState.Current = Saves.saveStates[0];
                GameManager.loadedGame = true;
                GameObject player1 = Instantiate(players[1], new Vector3(GameState.Current.Player1Serial.xPos, GameState.Current.Player1Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player2 = Instantiate(players[2], new Vector3(GameState.Current.Player2Serial.xPos, GameState.Current.Player2Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player3 = Instantiate(players[3], new Vector3(GameState.Current.Player3Serial.xPos, GameState.Current.Player3Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player4 = Instantiate(players[4], new Vector3(GameState.Current.Player4Serial.xPos, GameState.Current.Player4Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player5 = Instantiate(players[5], new Vector3(GameState.Current.Player5Serial.xPos, GameState.Current.Player5Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player6 = Instantiate(players[6], new Vector3(GameState.Current.Player6Serial.xPos, GameState.Current.Player6Serial.yPos, 0f), Quaternion.identity) as GameObject;
                GameObject player7 = Instantiate(players[7], new Vector3(GameState.Current.Player7Serial.xPos, GameState.Current.Player7Serial.yPos, 0f), Quaternion.identity) as GameObject;               
            }

            // if it's a new game, and we have no saveStates[0], aka first launch

            else
            {
                GameObject player1 = Instantiate(players[1], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player2 = Instantiate(players[2], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player3 = Instantiate(players[3], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player4 = Instantiate(players[4], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player5 = Instantiate(players[5], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player6 = Instantiate(players[6], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameObject player7 = Instantiate(players[7], new Vector3(0, 0), Quaternion.identity) as GameObject;
                GameState.Current = NewGame();
            }

        }
        return;
    }

    // if it's the first launch, we don't have a gamestate, so we have to make a default gamestate
    // player states are set in player controllers individually

        /* under construction */

    private GameState NewGame()
    {
        PlayerStateSerial player1Serial = Player1Controller.player1Serial;
        PlayerStateSerial player2Serial = Player2Controller.player2Serial;
        PlayerStateSerial player3Serial = Player3Controller.player3Serial;
        PlayerStateSerial player4Serial = Player4Controller.player4Serial;
        PlayerStateSerial player5Serial = Player5Controller.player5Serial;
        PlayerStateSerial player6Serial = Player6Controller.player6Serial;
        PlayerStateSerial player7Serial = Player7Controller.player7Serial;

        GameState game = new GameState
        {
            Player1Serial = player1Serial,
            Player2Serial = player2Serial,
            Player3Serial = player3Serial,
            Player4Serial = player4Serial,
            Player5Serial = player5Serial, 
            Player6Serial = player6Serial,
            Player7Serial = player7Serial
        };
        return game;
    }

    /* not currently in use, but will be useful for rng prefab creation with BoardController in future

    public class Count
    {
        public int minimum;             
        public int maximum;             

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    */
}




