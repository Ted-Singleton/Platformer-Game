using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Collectible Start");
        //we make sure the collectible is active
        gameObject.SetActive(true);
        //and reset the collectible gathered flag
        LevelData.collectibleGathered = false;
    }

    //when the player's rigidbody touches the collectible's collider
    private void OnTriggerEnter(Collider collider)
    {
        //if the player collided with the collectible
        if (collider.CompareTag("Player"))
        {
            //set the collectible gathered flag to true
            LevelData.collectibleGathered = true;
            //deactivate the collectible
            gameObject.SetActive(false);
        }
    }
}
