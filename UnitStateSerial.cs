using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// This is PlayerStateMono's memento class. It is needed for saving a gamestate.
public class UnitStateSerial
{
    public float xPos;
    public float yPos;
    public int facing;
    public bool active;

    public UnitStateSerial(float xPos, float yPos, int facing, bool active)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.facing = facing;
        this.active = active;
    }
}
