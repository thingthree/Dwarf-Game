using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// serializable so we can save, PlayerStateMono's memento class
public class PlayerStateSerial
{
    public float xPos;
    public float yPos;
    public int facing;
    public bool active;

    public PlayerStateSerial(float xPos, float yPos, int facing, bool active)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.facing = facing;
        this.active = active;
    }
}
