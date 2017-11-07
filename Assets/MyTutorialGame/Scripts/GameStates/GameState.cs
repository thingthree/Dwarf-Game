using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameState 
{
    // every current game statistic, serializable for save functionality, pulls from PlayerStateSerial
    public static GameState Current;

    public PlayerStateSerial Player1Serial { get; set; }
    public PlayerStateSerial Player2Serial { get; set; }
    public PlayerStateSerial Player3Serial { get; set; }
    public PlayerStateSerial Player4Serial { get; set; }
    public PlayerStateSerial Player5Serial { get; set; }
    public PlayerStateSerial Player6Serial { get; set; }
    public PlayerStateSerial Player7Serial { get; set; }

    public int BMID { get; set; }
    public string Location { get; set; }
    public string DateTime { get; set; }

    

    public GameState()
    {
        Player1Serial = Player1Controller.player1Serial;
        Player2Serial = Player2Controller.player2Serial;
        Player3Serial = Player3Controller.player3Serial;
        Player4Serial = Player4Controller.player4Serial;
        Player5Serial = Player5Controller.player5Serial;
        Player6Serial = Player6Controller.player6Serial;
        Player7Serial = Player7Controller.player7Serial;

        int bmid = BMID;
        string location = Location;
        string dateTime = DateTime;

    }
}
