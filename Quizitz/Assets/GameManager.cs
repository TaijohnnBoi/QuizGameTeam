using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SimonSaysEnemy simonSaysEnemy;

    public void TriggerSimonSays()
    {
        simonSaysEnemy.ActivateEnemy(new string[] { "Pink", "Blue", "Green" });
    }

    void Update()
    {
        if (simonSaysEnemy.IsEnemyActive())
        {
            // Pause game logic while the enemy is active
        }
        else
        {
            // Resume normal gameplay
        }
    }
}
