using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int MinimumPlayers = 2;
    // Update is called once per frame
    void Update()
    {

        if(GameObject.FindGameObjectsWithTag("Player").Length < MinimumPlayers)
        {

           // Time.timeScale = 0;

        }
        else
        {
          //  Time.timeScale = 1;

        }

    }
}
