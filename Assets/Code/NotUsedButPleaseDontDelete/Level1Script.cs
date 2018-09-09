using UnityEngine.UI;
using UnityEngine;
using System.Collections;


public class Level1Script : MonoBehaviour
{

    public Text[] myText;
    public GameObject panel;
    Text currentText;
    int index = 0;
    public int pauseScript;
    bool canContinue = false;

    speechScr newScript;

    // Use this for initialization
    void Start()
    {
        newScript = GameObject.Find("SpeechTest").GetComponent<speechScr>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (canContinue == false)
        {
            if (col.tag == "Player" && myText.Length != 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (myText.Length != 0)
                    {
                        index++;
                        currentText.enabled = false;
                    }
                }

                if (!(index >= pauseScript))
                {
                    panel.SetActive(true);
                    currentText = myText[index];
                    currentText.enabled = true;
                }
                else
                {
                    panel.SetActive(false);
                    if (myText[index] != null)
                    {
                        currentText.enabled = false;
                    }
                }

                if (newScript.getWord() == "up")
                {
                    canContinue = true;
                }
            }
        }
        else
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
        if (myText.Length != 0)
        {

            panel.SetActive(false);
            currentText.enabled = false;
            index = 0;
        }
    }

}

