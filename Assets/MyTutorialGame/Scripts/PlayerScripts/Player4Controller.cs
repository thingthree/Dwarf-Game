﻿using UnityEngine;

using System.Collections.Generic;

using UnityEngine.SceneManagement;

using System;

using System.IO;

using Random = UnityEngine.Random;

using System.Linq;



public class Player4Controller : MonoBehaviour

{



    // declares variables for components to be referenced 

    private Animator animator;

    private Rigidbody2D rb2D;

    private Vector2 playerPosition;

    private BoxCollider2D boxCollider2D;

    private SpriteRenderer spriteRenderer;

    public static PlayerStateSerial player4Serial;

    public static PlayerStateMono player4;



    public GameObject[] damageBox;

    public GameObject[] genericInteractionBox;



    public bool canMove = true;

    private int moveCount = 0;

    private int recentDirectionChange = 0;

    int facingHorizontal = 0;



    void Start()

    {

        animator = GetComponent<Animator>();

        rb2D = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        player4 = GetComponent<PlayerStateMono>();

        boxCollider2D = GetComponent<BoxCollider2D>();

        if (GameManager.loadedGame == true)

        {

            player4Serial = new PlayerStateSerial

                (GameState.Current.Player4Serial.xPos,

                 GameState.Current.Player4Serial.yPos,

                 GameState.Current.Player4Serial.facing,

                 GameState.Current.Player4Serial.active);



        }

        if (GameManager.loadedGame == false)

        {

            player4Serial = new PlayerStateSerial(0f, 0f, 0, true);

        }

        player4.Memento = player4Serial;

        player4 = player4.Revert(player4);



    }





    private void Update()

    {

        if (GameManager.inMenu == true || canMove == false)

            return;

        else

        {



            // cycles through players using the R and L keys



            if (GameManager.playerCycle == 4)

                Player4Manual();

            animator.SetInteger("Direction", player4.Facing);

            if (player4.Facing == 2)

                spriteRenderer.flipX = true;

            else

                spriteRenderer.flipX = false;

            player4.XPos = gameObject.transform.position.x;

            player4.YPos = gameObject.transform.position.y;

            player4.Active = true;

            player4Serial.xPos = player4.XPos;

            player4Serial.yPos = player4.YPos;

            player4Serial.facing = player4.Facing;

            player4Serial.active = player4.Active;

            //Debug.Log(string.Format("Facing = {0}", facing.Facing));

        }

    }



    private void Player4Auto()

    {



    }



    private void Player4Manual()

    {

        animator.SetBool("PlayerMove", false);

        float xMove = 0;

        float yMove = 0;

        xMove = (int)(Input.GetAxisRaw("Horizontal"));

        yMove = (int)(Input.GetAxisRaw("Vertical"));

        //string myString = player3StateString[0];

        //string[] lines = {myString, player3StateString[1], player3StateString[2], player3StateString[3] };





        // stopp and attack

        if ((Input.GetKeyDown(KeyCode.X)))

        {

            Vector2 playerPosition = transform.position;

            float x = playerPosition.x;

            float y = playerPosition.y;

            float newX = x + 1f;

            float newY = y + 1f;

            GameObject toInstantiate = damageBox[Random.Range(0, damageBox.Length)];

            GameObject instance = Instantiate(toInstantiate, new Vector2(newX, newY),

            Quaternion.identity) as GameObject;

            Destroy(instance, .1f);

            return;

        }



        if ((Input.GetKeyDown(KeyCode.Z)))

        {

            Vector2 playerPosition = transform.position;

            float x = playerPosition.x;

            float y = playerPosition.y;

            float newX = x;

            float newY = y;

            GameObject toInstantiate = genericInteractionBox[Random.Range(0, genericInteractionBox.Length)];

            GameObject instance = Instantiate(toInstantiate, new Vector2(newX, newY),

            Quaternion.identity) as GameObject;

            return;

        }

        // stopped because holding attack

        if ((Input.GetKey(KeyCode.X)))

        {

            return;

        }



        // not attempting to move

        if (xMove == 0 && yMove == 0)

        {

            moveCount = 0;

            recentDirectionChange = 0;

            return;

        }

        // attempting to move

        if (xMove != 0 || yMove != 0)

        {

            animator.SetBool("PlayerMove", true);

            moveCount++;

            // facing vertically and attempting to move

            if (facingHorizontal == 0)

            {



                // attempting to move, in both axis, after recently facing vertically

                if (recentDirectionChange != 0 && xMove != 0 && yMove != 0)

                {

                    rb2D.AddForce(transform.up * yMove);

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, horizontally, after recently facing vertically

                if (recentDirectionChange != 0 && xMove != 0 && yMove == 0)

                {

                    rb2D.AddForce(transform.right * xMove);

                    if (facingHorizontal < 1)

                        facingHorizontal++;

                    recentDirectionChange = 0;

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    return;

                }

                // attempting to move, vertically, after recently facing vertically

                if (recentDirectionChange != 0 && xMove == 0 && yMove != 0)

                {

                    recentDirectionChange = 0;

                    rb2D.AddForce(transform.up * yMove);

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, in both axis, after recently attempting to move in only one

                if (moveCount != 0 && xMove != 0 && recentDirectionChange == 0)

                {

                    recentDirectionChange++;

                    rb2D.AddForce(transform.right * xMove);

                    if (facingHorizontal < 1)

                        facingHorizontal++;

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, horizontally, after being stationary, facing vertically

                if (moveCount == 0 && xMove != 0)

                {

                    rb2D.AddForce(transform.right * xMove);

                    if (facingHorizontal < 1)

                        facingHorizontal++;

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, vertically, after being stationary, facing vertically

                else

                {

                    rb2D.AddForce(transform.up * yMove);

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

            }

            // facintg horizontally and attempting to move

            if (facingHorizontal == 1)

            {



                // attempting to move, in both axis, after recently facing horizontally

                if (recentDirectionChange != 0 && xMove != 0 && yMove != 0)

                {

                    rb2D.AddForce(transform.right * xMove);

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, horizontally, after recently facing horizontally

                if (recentDirectionChange != 0 && xMove != 0 && yMove == 0)

                {

                    rb2D.AddForce(transform.right * xMove);

                    recentDirectionChange = 0;

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, vertically, after recently facing horizontally

                if (recentDirectionChange != 0 && xMove == 0 && yMove != 0)

                {

                    rb2D.AddForce(transform.up * yMove);

                    if (facingHorizontal > 0)

                        facingHorizontal--;

                    recentDirectionChange = 0;

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, in both axis, after recently attempting to move in only one

                if (moveCount != 0 && yMove != 0 && recentDirectionChange == 0)

                {

                    recentDirectionChange++;

                    rb2D.AddForce(transform.up * yMove);

                    if (facingHorizontal > 0)

                        facingHorizontal--;

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, vertically, after being stationary, facing horizontally

                if (moveCount == 0 && yMove != 0)

                {

                    rb2D.AddForce(transform.up * yMove);

                    if (facingHorizontal > 0)

                        facingHorizontal--;

                    if (yMove > 0)

                    {

                        player4.Facing = 0;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 1;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }

                // attempting to move, horizontally, after being stationary, facing horizontally

                else

                {

                    rb2D.AddForce(transform.right * xMove);

                    if (xMove > 0)

                    {

                        player4.Facing = 3;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                    }

                    else

                    {

                        player4.Facing = 2;

                        //Debug.Log(string.Format("Facing = {0}", facing.Facing));

                        return;

                    }

                }



            }

        }

    }



    private void OnCollisionEnter(Collision collision)

    {

        if (collision.gameObject.tag == "GenericInteraction")

        {

            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());

        }

    }



}