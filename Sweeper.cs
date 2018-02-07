using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{ 
    private GameObject[] toDestroy;

    public void DestroyObjects(string[] tags)
    {
        foreach (string tag in tags)
        {
            toDestroy = GameObject.FindGameObjectsWithTag(tag);

            for (var i = 0; i < toDestroy.Length; i++)
            {
                Destroy(toDestroy[i]);
            }
        }
    }
}
