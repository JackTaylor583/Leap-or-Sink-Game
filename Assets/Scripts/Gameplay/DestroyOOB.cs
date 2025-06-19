using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    private float lowerBound = -1.0f;

    void Update() // Update is called once per frame
    {
        if (transform.position.y <  lowerBound) // Destroys objects going below lowerbound float
        {
            Destroy(gameObject);
        }
    }
}
