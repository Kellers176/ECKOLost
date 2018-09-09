using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueScript : MonoBehaviour {

    public Canvas textCanvas;
    public string myDialogue;
    TextBoxManagerScript tbms;

    void Start()
    {
        tbms = textCanvas.GetComponent<TextBoxManagerScript>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            tbms.InputDialogue(myDialogue);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //if (col.tag == "Player")
        //{
        //    tbms.CloseDialogue();
       // }
    }
}
