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
    private int columns = 200;
    private int rows = 200;
    // BMID Prefab Arrays
    public GameObject[] BMID;
    public GameObject[] mapSet;
    public GameObject[] menuSet;
    public GameObject[] players;
    public GameObject[] misc;
    public GameObject[] logic;
    // General Prefab Arrays
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] uiFrame;
    public GameObject[] actionBox;
    // Components
    public Canvas canvas;
    public Text menuText;
    public RectTransform rectTransform;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    // Menu Fields
    private int menuLogicSelection;
    // BMID References
    public static string[] xOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidXOffSet.txt");
    public static float[] xOffSet = xOffSetString.Select(x => float.Parse(x)).ToArray();
    public static string[] yOffSetString = File.ReadAllLines(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidYOffSet.txt");
    public static float[] yOffSet = yOffSetString.Select(x => float.Parse(x)).ToArray();
    public static string[] textStartString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidTextStart.txt");
    public static int[] textStart = textStartString.Select(x => int.Parse(x)).ToArray();
    public static string[] textEndString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidTextEnd.txt");
    public static int[] textEnd = textEndString.Select(x => int.Parse(x)).ToArray();
    public static string[] redirectStartString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidRedirectStart.txt");
    public static int[] redirectStart = redirectStartString.Select(x => int.Parse(x)).ToArray();
    public static string[] redirectEndString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidRedirectEnd.txt");
    public static int[] redirectEnd = redirectEndString.Select(x => int.Parse(x)).ToArray();
    public static string[] redirectString = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\redirect.txt");
    public static int[] redirect = redirectString.Select(x => int.Parse(x)).ToArray();
    public static string[] text = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\text.txt");
    public static string[] bmidReference = File.ReadAllLines(@"c:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References\bmidReferenceList.txt");

    // Component References
    private void Start()
    {
        canvas.GetComponent<Canvas>();
        menuText.GetComponent<Text>();
        rectTransform.GetComponent<RectTransform>();
        canvas.worldCamera = Camera.main;
    }
    // Board Instantiation System
    public void BoardController(int bmid)
    {
        Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " BC Assignment");
        BC current = new BC(bmid, xOffSet, yOffSet, textStart, textEnd, redirectStart, redirectEnd, text, redirect);
        
        if (GameManager.initiatedGame == true)
        {
            // place a mapset
            if (current.BMID < BMID.Length - logic.Length - misc.Length - players.Length - menuSet.Length)
            {
                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + "- instantiating a mapset");
                GameObject toInstantiate = BMID[current.BMID];
                GameObject instance = Instantiate(toInstantiate, new Vector3(current.XOffSet, current.YOffSet, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                return;
            }
            // place an object attached to menu text
            else if (current.BMID < BMID.Length - logic.Length - misc.Length - players.Length)
            {

                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - instantiating a menu");
                GameManager.inMenu = true;
                menuLogicSelection = 0;

                Debug.Log("Method: BoardManager.BoardConteroller -" + bmidReference[bmid] + "(" + bmid + ")" + " - setting new menu text");
                menuText.text = TextBuilder("\n", current.TextStart, current.TextEnd, current.Text);

                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - instantiating Canvas, setting RectTransform ");
                GameObject toInstantiate = Instantiate(BMID[current.BMID], new Vector3(Camera.main.transform.position.x + current.XOffSet, Camera.main.transform.position.y + current.YOffSet, 0f), Quaternion.identity) as GameObject;
                Canvas instance = Instantiate(canvas, new Vector3(0, 0, 0f), Quaternion.identity);
                menuText.rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
                menuText.rectTransform.transform.localPosition = new Vector3(menuText.rectTransform.transform.position.x + current.XOffSet, menuText.transform.position.y + current.YOffSet, menuText.rectTransform.transform.position.z);               

                if (current.RedirectStart != -1)
                {
                    Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Making a Selection");
                    int[] redirectArray = BMIDArrayFormat(current.RedirectStart, current.RedirectEnd, current.Redirect);
                    StartCoroutine(MenuSelector(GameManager.inMenu, redirectArray, menuLogicSelection, current));
                }
                return;
            }
            // player
            else if (current.BMID < BMID.Length - logic.Length - misc.Length)
            {
                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Placing Player");
                GameObject toInstantiate = BMID[bmid];
                GameObject instance = Instantiate(toInstantiate, new Vector3(current.XOffSet, current.YOffSet, 0f), Quaternion.identity) as GameObject;
                return;
            }
            // misc
            else if (current.BMID < BMID.Length - logic.Length)
            {
                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Placing Misc. Object");
                GameObject toInstantiate = BMID[bmid];
                GameObject instance = Instantiate(toInstantiate, new Vector3(current.XOffSet, current.YOffSet, 0f), Quaternion.identity) as GameObject;
                return;
            }
            // logic
            else if (current.BMID < BMID.Length)
            {
                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Calling Logic Controller");
                int lmid = current.BMID - BMID.Length + logic.Length;
                GameObject toInstantiate = BMID[bmid];
                GameObject instance = Instantiate(toInstantiate, new Vector3(current.XOffSet, current.YOffSet, 0f), Quaternion.identity) as GameObject;
                StartCoroutine(GameManager.logicScript.LogicController(lmid));
                return;
            }
        }
       
        // launch game
        else if (GameManager.initiatedGame == false)
        {
            Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Initializing Game");
            GameObject toInstantiate = mapSet[bmid];
            GameObject instance = Instantiate(toInstantiate, new Vector3(current.XOffSet, current.YOffSet, 0f), Quaternion.identity) as GameObject;
            GameManager.initiatedGame = true;
            if (File.Exists(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd"))
            {
                Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Save File Exists");
                BoardController(BMID.Length - logic.Length - misc.Length - players.Length - menuSet.Length);
            }
            else
            {   Debug.Log("Method: BoardManager.BoardController -" + bmidReference[bmid] + "(" + bmid + ")" + " - Save File DNE");
                BoardController(BMID.Length - logic.Length - misc.Length - players.Length - menuSet.Length + 2);
            }
        }
        return;
    }
    // Format Logic Arrays for use
    private int[] BMIDArrayFormat(int BMIDArrayStart, int BMIDArrayEnd, int[] reference)
    {
        List<int> redirectList = new List<int>();
        for (int i = BMIDArrayStart; i <= BMIDArrayEnd; i++)
        {
            redirectList.Add(reference[i]);
        }
        {

        }
        int[] BMIDArray = redirectList.GetRange(0, (BMIDArrayEnd - BMIDArrayStart + 1)).ToArray();
        return BMIDArray;
    }
    // Execute next BMID based on player input
    private IEnumerator MenuSelector(bool wait, int[] myIntArray, int selection, BC current)
    {   
        while (wait == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selection < myIntArray.Length - 1)
                {
                    selection++;
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + " Increased Selection");
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + " Selection = " + selection);
                }

                else
                {
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + " Array index out of range: end of array.");
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + " Selection = " + selection);
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selection > 0)
                {
                    selection--;
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + " Decreased Selection");
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + "  Selection = " + selection);
                }

                else
                {
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + "  - Array index out of range: start of array.");
                    Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + "  - Selection = " + selection);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
               
                Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + "  - Final selection = " + selection);
                GameManager.inMenu = false;
                BoardController(myIntArray[selection]);
                wait = false;
                GameManager.inMenu = false;
            }
            yield return null;
        }
        Debug.Log(menuLogicSelection);
        Debug.Log("Coroutine: MenuSelector - " + bmidReference[current.BMID] + "(" + current.BMID + ")" + "  - Complete");
    }
    private string TextBuilder(string separator, int textStart, int textEnd, string[] reference)
    {
        List<string> textList = new List<string>();
        for (int i = textStart; i <= textEnd; i++)
        {
            textList.Add(reference[i]);
        }
        string[] textArray = textList.GetRange(0, (textEnd - textStart + 1)).ToArray();

        string result = "";
        for (int i = 0; i < textArray.Length; i++)
        {
            if (i > 0)
                result += separator;
                result += textArray[i];
        }
        return result;
    }
}
public class BC
{
    public int BMID { get; set; }
    public float XOffSet { get; set; }
    public float YOffSet { get; set; }
    public int TextStart { get; set; }
    public int TextEnd { get; set; }
    public int RedirectStart { get; set; }
    public int RedirectEnd { get; set; }
    public string[] Text { get; set;}
    public int[] Redirect { get; set;}

    public BC(int bmid, float[] xOffSet, float[] yOffSet, int[] textStart, int[] textEnd, int[] redirectStart, int[] redirectEnd, string[] text, int[] redirect)
    {
        BMID = bmid;
        XOffSet = xOffSet[bmid];
        YOffSet = yOffSet[bmid];
        TextStart = textStart[bmid];
        TextEnd = textEnd[bmid];
        RedirectStart = redirectStart[bmid];
        RedirectEnd = redirectEnd[bmid];
        Text = text;
        Redirect = redirect;
    }
}