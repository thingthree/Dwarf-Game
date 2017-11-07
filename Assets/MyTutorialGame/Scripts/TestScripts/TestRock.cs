using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You can do it Rocky!
public class TestRock : MonoBehaviour
{
    private int health = 100;

    //Triggers on collision with rb2d
    //checks tags and performs operations based on tag of object it collides with
    //temporary home for 'death' function, destroys game object, no animations etc..
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // damage
        if (collision.gameObject.tag == "InstantDamage")
        {
            health -= 33;
        }
        if (health < 1)
        {
            DestroyObject(gameObject);
        }

        //interaction
        if (collision.gameObject.tag == "GenericInteraction")
        {
            Debug.Log("Dis Rock. Who there?");
            Destroy(collision.gameObject);
        }
    }
}