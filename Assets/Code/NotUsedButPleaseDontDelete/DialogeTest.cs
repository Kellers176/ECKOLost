using UnityEngine.UI;
using UnityEngine;
using System.Collections;


public class DialogeTest : MonoBehaviour
{

    public Text[] myText;
    public GameObject panel;
    Text currentText;
    int index = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && (myText.Length != 0))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (myText.Length != 0)
                {
                    index++;
                    currentText.enabled = false;
                }
            }

            if (!(index >= myText.Length))
            {
                panel.SetActive(true);
                currentText = myText[index];
                currentText.enabled = true;
            }
            else
            {
                panel.SetActive(false);
                currentText.enabled = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && (myText.Length != 0))
        {

            panel.SetActive(false);
            currentText.enabled = false;
            index = 0;
        }
    }

}

