using UnityEngine;

using System.Collections.Generic;

using UnityEngine.SceneManagement;

using System;

using System.IO;

using Random = UnityEngine.Random;

using System.Linq;



public class Player1Controller : MonoBehaviour

{
    // declares variables for components to be referenced 
    private Animator animator;
    private Rigidbody2D rb2D;
    private Vector2 playerPosition;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    public static UnitStateSerial player1Serial;
    public static UnitStateMono player1;

    public GameObject damageBox;
    public GameObject genericInteractionBox;

    public bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player1 = GetComponent<UnitStateMono>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (File.Exists(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References" + "/savedGames.gd"))
            player1Serial = new UnitStateSerial
                (GameState.Current.Player1Serial.xPos,
                 GameState.Current.Player1Serial.yPos,
                 GameState.Current.Player1Serial.facing,
                 GameState.Current.Player1Serial.active);

        else
        {
            player1Serial = new UnitStateSerial
                (0,0,0,true);
        }

        player1.Memento = player1Serial;
        player1 = player1.Revert(player1);
    }



    private void Update()
    {
        if (GameManager.inMenu == true || canMove == false)
            return;
        else
        {
            player1.XPos = gameObject.transform.position.x;
            player1.YPos = gameObject.transform.position.y;
            player1.Active = true;
            player1Serial.xPos = player1.XPos;
            player1Serial.yPos = player1.YPos;
            player1Serial.facing = player1.Facing;
            player1Serial.active = player1.Active;

            if (GameManager.playerCycle == 1)
                Player1Manual();
            animator.SetInteger("Direction", player1.Facing);
            if (player1.Facing == 3)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }

    private void Player1Auto()
    {
    }



    private void Player1Manual()
    {
        animator.SetBool("PlayerMove", false);
        float xMove = 0;
        float yMove = 0;
        xMove = (int)(Input.GetAxisRaw("Horizontal"));
        yMove = (int)(Input.GetAxisRaw("Vertical"));
        if ((Input.GetKeyDown(KeyCode.X)))
            GameManager.meleeUnitScript.Attack(10, transform.position, damageBox);
        if ((Input.GetKeyDown(KeyCode.Z)))       
            GameManager.meleeUnitScript.Interact(transform.position, genericInteractionBox);
        if ((Input.GetKey(KeyCode.X)))
            return;
        if (xMove != 0 || yMove != 0)
            GameManager.meleeUnitScript.Move(xMove, yMove, rb2D, transform, animator, player1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GenericInteraction")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}