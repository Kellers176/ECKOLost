using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {
    //set this to the door sprite locked gameobject
    public GameObject lockedDoor;
    //set this to the unlocked sprite gameobject
    public GameObject unlockedDoor;
    //public GameObject switchGrateThing;
    public Sprite switchOff;
    public Sprite switchOn;
    SpriteRenderer mRenderer;
    // Use this for initialization
    void Start () {
        mRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
       if(col.tag != "echo"  && (col.tag == "Player" || col.tag == "Box") && col.transform.position == transform.position)
        {
            mRenderer.sprite = switchOn;
            lockedDoor.SetActive(false);
            unlockedDoor.SetActive(true);
        }
        else if (col.tag != "echo")
        {
            mRenderer.sprite = switchOff;
            lockedDoor.SetActive(true);
            unlockedDoor.SetActive(false);
        }
       
    }

    //void OnTriggerExit2D(Collider2D collision)
    //{
        
    //    mRenderer.sprite = switchOff;
    //    lockedDoor.SetActive(true);
    //    unlockedDoor.SetActive(false);
    //}
}
