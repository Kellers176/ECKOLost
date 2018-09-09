using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BiteBlock : MonoBehaviour
{
    public bool canBite;
    float xDirection, yDirection;
    int xDestination, yDestination;
    public bool objectCollide;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        
    }
		
    void biteBox()
    {
		objectCollide = false;
		gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.GetComponent<playerMoveScr>().biting == true)
            {
                xDirection = col.GetComponent<playerMoveScr>().xDirection;
                yDirection = col.GetComponent<playerMoveScr>().yDirection;
				canBite = false;
                biteBox();
            }
        }
        if (col.gameObject.layer == 9)
        {
            objectCollide = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            objectCollide = false;
        }
    }
}


