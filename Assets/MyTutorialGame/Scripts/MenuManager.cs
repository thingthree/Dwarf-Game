using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static int[] menuSelections;
    private int menuLevel = 1;
    private int menuSelector = 1;
    private Text text;
    private string[] textArray;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && menuSelector > 1 )
            menuSelector--;
        if (Input.GetKeyDown(KeyCode.S))
            menuSelector++;
        if (Input.GetKeyDown(KeyCode.A))
            menuLevel++;
        if (Input.GetKeyDown(KeyCode.B))
            menuLevel--;
    }
    //private int[] SetMenuText(int mmid) // menu manager ID
    //{

        
    //}
    
}

