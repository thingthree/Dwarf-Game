using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitManager : MonoBehaviour
{
    // Use this for initialization
    public abstract void Attack(int damage, Vector2 position, GameObject damageBox);
    public abstract void Interact(Vector2 position, GameObject genericInteractionBox);
    public abstract void Move(float xMove, float yMove, Rigidbody2D rb2D, Transform transform, Animator animator, UnitStateMono unit);
    
}