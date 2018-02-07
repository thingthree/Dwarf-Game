using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

// Monobehavior classes are not serializable, so to pull from serialized
// data we need to use a memento. We do this by including a memento varialbe
// Memento of the PlayerStateSerial class into our non-serializable monobehavior
// class then using a revert method to revert that PlayerStateSerial setting
// the rest of the PlayerStateMono classes variables
public class UnitStateMono : MonoBehaviour
{
    public UnitStateSerial Memento { get; set; }  
    
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int Facing { get; set; }
    public bool Active { get; set; }

    public UnitStateMono Revert(UnitStateMono psm)
    {
        this.XPos = this.Memento.xPos;
        this.YPos = this.Memento.yPos;
        this.Facing = this.Memento.facing;
        this.Active = this.Memento.active;
        return psm;
    }
}

