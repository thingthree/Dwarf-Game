using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

// not serializable so we need to add memento class PlayerStateSerial
// Revert() allows us to get deserialize and get info into PlayerStateSerial
// set the PlayerStateMono's Memento, and then revert to set PlayerStateSerial's 
// other properties
public class PlayerStateMono : MonoBehaviour
{
    public PlayerStateSerial Memento { get; set; }  
    
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int Facing { get; set; }
    public bool Active { get; set; }

    public PlayerStateMono Revert(PlayerStateMono psm)
    {
        this.XPos = this.Memento.xPos;
        this.YPos = this.Memento.yPos;
        this.Facing = this.Memento.facing;
        this.Active = this.Memento.active;
        return psm;
    }
}

