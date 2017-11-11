using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour

{
    /* The "BoardManager" class is used to instantiate objects which can be considered a part of the game board.
     * This includes, but is not limited to: ground layer, players, menu boxes, static text elements, etc...
     * Most of what the end user sees is instantiated here. Most of what the end user doesn't see is handled
     * in other classes.*/


    // declaration of prefab arrays and variables needed to create the grid and instantiate game objects
    private int columns = 200;
    private int rows = 200;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] BMID;
    public GameObject[] mapSet;
    public GameObject[] menuSet;
    public GameObject[] players;
    public GameObject[] uiFrame;
    public Canvas canvas;
    public Text menuText;
    public RectTransform rectTransform;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
   

    // declaration of arrays needed to use Board Manager ID System

    public static string[] xOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidXOffSet.txt");
    public static int[] xOffSetInt = xOffSetString.Select(x => int.Parse(x)).ToArray();
    public static string[] yOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidYOffSet.txt");
    public static int[] yOffSetInt = yOffSetString.Select(x => int.Parse(x)).ToArray();
    public static string[] menuStartString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidMenuArrayStart.txt");
    public static int[] menuStartInt = menuStartString.Select(x => int.Parse(x)).ToArray();
    public static string[] menuEndString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidMenuArrayEnd.txt");
    public static int[] menuEndInt = menuEndString.Select(x => int.Parse(x)).ToArray();
    public static string[] gameText = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\gametext.txt");
    // initializes list on which items will be instantiated

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

    // instantiates objects to the correct transform and sets parent

    public void BoardController(int bmid)
    {

        // declaration of variables which could be used in multiple situations within the board controller

        int x = xOffSetInt[bmid];
        int y = yOffSetInt[bmid];
        int menuStart = menuStartInt[bmid];
        int menuEnd = menuEndInt[bmid];
        canvas.GetComponent<Canvas>();
        menuText.GetComponent<Text>();
        rectTransform.GetComponent<RectTransform>();
        canvas.worldCamera = Camera.main;
        

        // uncomment and set GameManager.loadedGame to true to use Quick Prefab Builder
        //start uncomment for quick prefabs
        //if (bmid == 0)               
        //{
        //    //common coords
        //    //Camera.main.transform.position.x + x, Camera.main.transform.position.y + y, 0f
        //    //x, y, 0f
        //    columns;
        //    rows;

        //    for (int x = -1; x < columns + 1; x++) 

        //    {

        //        for (int y = -1; y < rows + 1; y++)

        //        {

        //            GameObject toInstantiate = ui[bmid];

        //            GameObject instance =

        //            Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

        //            instance.transform.SetParent(boardHolder);

        //            if (y == rows)

        //            {

        //                toInstantiate = uiFrame[4];

        //                instance =

        //                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

        //                instance.transform.SetParent(boardHolder);

        //            }

        //            if (y == -1)

        //            {

        //                toInstantiate = uiFrame[5];

        //                instance =

        //                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

        //                instance.transform.SetParent(boardHolder);

        //            }

        //            if (x == -1)

        //            {

        //                toInstantiate = uiFrame[6];

        //                instance =

        //                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

        //                instance.transform.SetParent(boardHolder);

        //            }

        //            if (x == columns)

        //            {

        //                toInstantiate = uiFrame[7];

        //                instance =

        //                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

        //                instance.transform.SetParent(boardHolder);

        //            }

        //        }

        //    }
        //}                     
        //stop uncomment for quick prefabs

        if (GameManager.loadedGame == true)
        {
            if (bmid != 0)
            {
                // If bmid !=0 it's not a special case, so we're moving on with normal BoardController instantiation.
                // The branching if statements determine if what type of prefabs we're instantiating, so we can
                // give proper instructions to instantiate the objects correctly.

                if (bmid <= BMID.Length - players.Length - 1)
                {
                    if (bmid <= BMID.Length - menuSet.Length - players.Length - 1)
                    {
                        // do something with mapset array
                        GameObject toInstantiate = mapSet[bmid];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                        return;
                    }

                    else
                    {
                        // do something with menus array
                        // Instantiating a menu requires 4 things. The text, it's transform, the menu(if any), and its transform.
                        // We find the text by getting the start and end value of the index in our game text array. 
                        // We set its transform by (under construction)
                        // We set the menu by instantiating its prefab from the BMID array.
                        // We set its transform by adding our x and y offset array at index [bmid] to the objects transform as it's instantiated. 

                       
                        GameManager.inMenu = true;
                        GameObject toInstantiate = Instantiate(BMID[bmid], new Vector3(Camera.main.transform.position.x + xOffSetInt[bmid], Camera.main.transform.position.y + yOffSetInt[bmid], 0f), Quaternion.identity) as GameObject;
                        Canvas instance = Instantiate(canvas, new Vector3(0, 0, 0f), Quaternion.identity);
                        List<string> menuTextList = new List<string>();
                        for (int i = menuStart; i <= menuEnd; i++)
                        {
                            menuTextList.Add(gameText[i]);
                        }
                        string[] menuTextArray = menuTextList.GetRange(0, (menuEnd - menuStart + 1)).ToArray();
                        menuText.text = MenuController("\n", menuTextArray);
                        menuText.rectTransform.transform.Translate(xOffSetInt[bmid], yOffSetInt[bmid], 0f);


                        // finally, we want our menu to 'do things' and react accordingly when we 'do things'
                        // the doing things component is tracked in the game manager
                        // the reacting accordingly occurs by calling another Boarc Controller method with a new BMID

                        return;
                    }
                }

                else
                {
                    //do something with players array
                }
                
            }
        } 

        /* under construction*/

        else if (GameManager.loadedGame == false)
        {
            // places main menu 
            GameObject toInstantiate = mapSet[bmid];
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

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
                BoardController(BMID.Length-players.Length-menuSet.Length);
                return;
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
                return;
            }
            
        }
        return;
    }

    private string MenuController(string separator, string[] strings)
    {
       
        string result = "";

        for (int i = 0; i < strings.Length; i++)
        {
            if (i > 0)

                result += separator;

            result += strings[i];
        }

        return result;

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




