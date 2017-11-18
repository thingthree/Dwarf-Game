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
    public GameObject[] misc;

    public GameObject[] uiFrame;
    public GameObject[] actionBox;

    public Canvas canvas;
    public Text menuText;
    public RectTransform rectTransform;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    private int menuLogicStart;
    private int menuLogicEnd;
    private int menuLogicLength;
    private int menuLogicSelection;
    private bool makingSelection;

    // declaration of arrays needed to use Board Manager ID System

    public static string[] xOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidXOffSet.txt");
    public static float[] xOffSet = xOffSetString.Select(x => float.Parse(x)).ToArray();
    public static string[] yOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidYOffSet.txt");
    public static float[] yOffSet = yOffSetString.Select(x => float.Parse(x)).ToArray();
    public static string[] textStartString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidArrayStart.txt");
    public static int[] textStart = textStartString.Select(x => int.Parse(x)).ToArray();
    public static string[] textEndString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidArrayEnd.txt");
    public static int[] textEnd = textEndString.Select(x => int.Parse(x)).ToArray();
    public static string[] BMIDRedirectStartString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidRedirectStart.txt");
    public static int[] BMIDRedirectStart = BMIDRedirectStartString.Select(x => int.Parse(x)).ToArray();
    public static string[] BMIDRedirectEndString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidRedirectEnd.txt");
    public static int[] BMIDRedirectEnd = BMIDRedirectEndString.Select(x => int.Parse(x)).ToArray();
    public static string[] BMIDRedirectString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\BMIDRedirect.txt");
    public static int[] BMIDRedirect = BMIDRedirectString.Select(x => int.Parse(x)).ToArray();
    public static string[] text = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\gametext.txt");
    
    
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
        float x = xOffSet[bmid];
        float y = yOffSet[bmid];
        int menuTextStart = textStart[bmid];
        int menuTextEnd = textEnd[bmid];
        int menuLogicStart = BMIDRedirectStart[bmid];
        int menuLogicEnd = BMIDRedirectEnd[bmid];
        canvas.GetComponent<Canvas>();
        menuText.GetComponent<Text>();
        rectTransform.GetComponent<RectTransform>();
        canvas.worldCamera = Camera.main;
        Debug.Log(bmid);

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
            if (bmid <= BMID.Length - misc.Length - players.Length- 1)
            // making a menu or map
            {                    
                if (bmid <= BMID.Length - misc.Length - menuSet.Length - players.Length- 1)
                // making map
                {
                    Debug.Log("Method: BoardManager.BoardController - Creating a mapset");
                    GameObject toInstantiate = BMID[bmid];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    return;
                }

                else
                // making a menu
                {
                    if (GameManager.inMenu == false)
                    // creating new menu
                    {
                        Debug.Log("Method: BoardManager.BoardController - Creating a menu");
                        GameManager.inMenu = true;
                        makingSelection = true;
                        GameObject toInstantiate = Instantiate(BMID[bmid], new Vector3(Camera.main.transform.position.x + xOffSet[bmid], Camera.main.transform.position.y + yOffSet[bmid], 0f), Quaternion.identity) as GameObject;
                        Canvas instance = Instantiate(canvas, new Vector3(0, 0, 0f), Quaternion.identity);

                        List<string> menuTextList = new List<string>();
                        for (int i = menuTextStart; i <= menuTextEnd; i++)
                        {
                            menuTextList.Add(text[i]);
                        }
                        string[] menuTextArray = menuTextList.GetRange(0, (menuTextEnd - menuTextStart + 1)).ToArray();
                        menuText.text = MenuTextController("\n", menuTextArray);
                        menuText.rectTransform.transform.position = new Vector3(menuText.rectTransform.transform.position.x + xOffSet[bmid], menuText.transform.position.y + yOffSet[bmid], 0f);
                        menuLogicLength = menuLogicEnd - menuLogicStart + 1;
                        menuLogicSelection = 0;
                        BoardController(bmid);
                        return;
                    }
                    else if (GameManager.inMenu == true && makingSelection == true)
                    // making selection
                    {
                        List<int> menuLogicList = new List<int>();

                        for (int i = menuLogicStart; i <= menuLogicEnd; i++)
                        {
                            menuLogicList.Add(BMIDRedirect[i]);
                        }
                        int[] menuLogicArray = menuLogicList.GetRange(0, (menuLogicEnd - menuLogicStart + 1)).ToArray();
                        Debug.Log("Method: BoardManager.BoardController - Making a Selection");
                        StartCoroutine(MenuLogicController(makingSelection, menuLogicArray, menuLogicSelection, bmid));
                        return;
                    }

                    else if (GameManager.inMenu == true && makingSelection == false)
                    {
                        GameManager.inMenu = false;
                        BoardController(menuLogicStart + menuLogicSelection);
                    }
                }
            }

            else
            // misc bmid
            {
                Debug.Log("Method: BoardManager.BoardController - Not Implemented");
                GameObject toInstantiate = BMID[bmid];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                return;
            }                          
        } 

        else if (GameManager.loadedGame == false)
        // new game
        {
            GameObject toInstantiate = mapSet[bmid];
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

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
                BoardController(BMID.Length-misc.Length-players.Length-menuSet.Length);
                return;
            }

            else
            // first launch
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

    private IEnumerator MenuLogicController(bool wait, int[] myIntArray, int selection, int bmid)
    {
        while (wait == true)
        {
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selection < myIntArray.Length - 1)
                {
                    selection++;
                    Debug.Log("Coroutine: MenuLogicController - Increased Selection");
                    Debug.Log("Coroutine: MenuLogicController - Selection = " + selection);
                }
                else
                {
                    Debug.Log("Coroutine: MenuLogicController - Array index out of range: end of array.");
                    Debug.Log("Coroutine: MenuLogicController - Selection = " + selection);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selection > 0)
                {
                    selection--;
                    Debug.Log("Coroutine: MenuLogicController - Decreased Selection");
                    Debug.Log("Coroutine: MenuLogicController - Selection = " + selection);
                }
                else
                {
                    Debug.Log("Coroutine: MenuLogicController - Array index out of range: start of array.");
                    Debug.Log("Coroutine: MenuLogicController - Selection = " + selection);
                }
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Coroutine: MenuLogicController - Final selection = " + selection);
                menuText.rectTransform.transform.position = new Vector3(menuText.rectTransform.transform.position.x - xOffSet[bmid], menuText.transform.position.y - yOffSet[bmid], 0f);
                BoardController(myIntArray[selection]);
                wait = false;
            }
            yield return null;
        }

        Debug.Log(menuLogicSelection);
        Debug.Log("Coroutine: MenuLogicController - Complete");
    }

    private string MenuTextController(string separator, string[] strings)
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
}




