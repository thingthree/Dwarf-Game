using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MeleeUnitManager : UnitManager
    {
        public override void Attack(int damage, Vector2 position, GameObject damageBox)
        {
            GameObject toInstantiate = damageBox;
            GameObject instance = Instantiate(toInstantiate, new Vector2(position.x + 1, position.y + 1),
            Quaternion.identity) as GameObject;
            Destroy(instance, .1f);
            return;
        }

        public override void Interact(Vector2 position, GameObject genericInteractionBox)
        {
            GameObject toInstantiate = genericInteractionBox;
            GameObject instance = Instantiate(toInstantiate, new Vector2(position.x, position.y),
            Quaternion.identity) as GameObject;
            return;
        }

        public override void Move(float xMove, float yMove, Rigidbody2D rb2D, Transform transform, Animator animator, UnitStateMono unit)
        {
            if (unit.Facing == 0 || unit.Facing == 1)
                rb2D.AddForce(transform.up * yMove);
            if (unit.Facing == 2 || unit.Facing == 3)
                rb2D.AddForce(transform.right * xMove);
        }
    }
