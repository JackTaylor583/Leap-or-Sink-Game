using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    // Speed
    public static float globalSpeed = 2f;
    private float difficultytTimeIncrease = 15f;
    private float difficultyjump = 0.1f;

    void Start() // Start is called before the first frame update
    {
        StartCoroutine(IncreaseSpeedOverTime());

        if (GameManager.gameOver == true) // Resets speed back to default when game over
        {
            globalSpeed = 2f;
            UnityEngine.Debug.Log("Reset Speed");
        }
    }

    void Update() // Update is called once per frame
    {
        transform.Translate(Vector3.down * Time.deltaTime * globalSpeed); // Moves object down based on the float speed   
    }

    IEnumerator IncreaseSpeedOverTime() // Increases speed of platforms every set seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultytTimeIncrease);
            UnityEngine.Debug.Log("Speed increased! New speed: " + MoveDown.globalSpeed);
            globalSpeed += difficultyjump;
        }
    }
}
