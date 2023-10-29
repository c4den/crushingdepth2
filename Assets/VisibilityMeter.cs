using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityMeter : MonoBehaviour
{
    /*public float maxVisibility = 100f;  //The maximum visibility value
    public float increaseRate = 5f;    //Rate at which visibility increases
    public float decreaseRate = 5f;    //Rate at which visibility decreases

    public float currentVisibility = 0f;   //Current visibility value
    private PlayerInvisibility playerInvisibility; //Reference to the PlayerInvisibility script
    private EnemySight enemySight; //Reference to the EnemySight script

    //private float increaseTimer = 0f;   //Timer to control visibility increase

    public float CurrentVisibility
    {
        get { return currentVisibility; }
    }

    void Start()
    {
        playerInvisibility = GetComponent<PlayerInvisibility>();
        enemySight = GetComponent<EnemySight>();
    }

    void Update()
    {
        if (playerInvisibility != null && enemySight != null)
        {
            bool isPlayerInvisible = playerInvisibility.IsPlayerInvisible();
            bool enviromentView = enemySight.EnviromentView();

            if (enviromentView && !isPlayerInvisible)
            {
                Debug.Log("Visibility Increased: " + currentVisibility);
                //Increase visibility over time while the player is visible
                currentVisibility += increaseRate * Time.deltaTime;
            }
            
        }

            //Ensure visibility doesn't exceed the maximum
            currentVisibility = Mathf.Min(maxVisibility, currentVisibility);
        }*/
    }

