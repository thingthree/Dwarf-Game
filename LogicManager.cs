using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogicManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}

    public IEnumerator LogicController(int lmid)
    {
        if (lmid == 0)
        {
            string[] tags = new string[] {"Player", "Enemy", "InstantDamage", "GenericInteraction",
            "Type 1 UI", "Logic", "MapSet", "MenuSet", "Canvas"};
            GameManager.sweeperScript.DestroyObjects(tags);
            Saves.Load();
            GameState.Current = Saves.saveStates[0];
            GameObject player1 = Instantiate(GameManager.boardScript.players[1], new Vector3(GameState.Current.Player1Serial.xPos, GameState.Current.Player1Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player2 = Instantiate(GameManager.boardScript.players[2], new Vector3(GameState.Current.Player2Serial.xPos, GameState.Current.Player2Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player3 = Instantiate(GameManager.boardScript.players[3], new Vector3(GameState.Current.Player3Serial.xPos, GameState.Current.Player3Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player4 = Instantiate(GameManager.boardScript.players[4], new Vector3(GameState.Current.Player4Serial.xPos, GameState.Current.Player4Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player5 = Instantiate(GameManager.boardScript.players[5], new Vector3(GameState.Current.Player5Serial.xPos, GameState.Current.Player5Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player6 = Instantiate(GameManager.boardScript.players[6], new Vector3(GameState.Current.Player6Serial.xPos, GameState.Current.Player6Serial.yPos), Quaternion.identity) as GameObject;
            GameObject player7 = Instantiate(GameManager.boardScript.players[7], new Vector3(GameState.Current.Player7Serial.xPos, GameState.Current.Player7Serial.yPos), Quaternion.identity) as GameObject;
            GameManager.boardScript.BoardController(GameState.Current.BMID);
            yield break;
        }
        if (lmid == 1)
        {
            string[] tags = new string[] {"Player", "Enemy", "InstantDamage", "GenericInteraction",
            "Type 1 UI", "Logic", "MapSet", "MenuSet", "Canvas"};
            GameManager.sweeperScript.DestroyObjects(tags);
            if (File.Exists(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd"))
            {
                Debug.Log("Method: LogicManager.LogicController -" + "logic reference list todo" + "(" + lmid + ")" + " - Deleting Current Save File");
                File.Delete(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd");
            }
            Debug.Log("Method: LogicManager.LogicController -" + "logic reference list todo" + "(" + lmid + ")" + " - Creating New Save File");
            GameObject player1 = Instantiate(GameManager.boardScript.players[1], new Vector3(0, 0), Quaternion.identity) as GameObject;
            GameObject player2 = Instantiate(GameManager.boardScript.players[2], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameObject player3 = Instantiate(GameManager.boardScript.players[3], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameObject player4 = Instantiate(GameManager.boardScript.players[4], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameObject player5 = Instantiate(GameManager.boardScript.players[5], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameObject player6 = Instantiate(GameManager.boardScript.players[6], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameObject player7 = Instantiate(GameManager.boardScript.players[7], new Vector3(-20, -20), Quaternion.identity) as GameObject;
            GameState.Current = new GameState();
            GameState.Current.BMID = 1;
            GameManager.boardScript.BoardController(GameState.Current.BMID);
            Debug.Log("Method: LogicManager.LogicController -" + "logic reference list todo" + "(" + lmid + ")" + " - Complete");
            yield break;
        }
        if (lmid == 2)
        {
            GameState.Current.Player1Serial = Player1Controller.player1Serial;
            GameState.Current.Player2Serial = Player2Controller.player2Serial;
            GameState.Current.Player3Serial = Player3Controller.player3Serial;
            GameState.Current.Player4Serial = Player4Controller.player4Serial;
            GameState.Current.Player5Serial = Player5Controller.player5Serial;
            GameState.Current.Player6Serial = Player6Controller.player6Serial;
            GameState.Current.Player7Serial = Player7Controller.player7Serial;
            GameState.Current.BMID = 1;
            GameState.Current.Location = "Not Implemented";
            GameState.Current.DateTime = System.DateTime.Now.ToString();
            Saves.Save();
        }
        yield break;
    }
}
