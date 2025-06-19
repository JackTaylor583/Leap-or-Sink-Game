using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStartDown : MonoBehaviour
{
    // Platform speed
    private float speed = 2.0f;

    private bool shouldMove = false;

    // Waits 3 seconds then sets ShouldMove to true (gives time for game countdown start to finish)
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        shouldMove = true;
    }

    // When shouldMove = true (3 seconds) plays MoveDown
    void Update()
    {
        if (shouldMove)
        {
            MoveDown();
        }
    }

    // Moves starting platform down
    void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
