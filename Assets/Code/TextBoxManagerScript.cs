using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBoxManagerScript : MonoBehaviour
{
    public Text dialogueT;
    public bool talking = false;
    bool done = false;
    int currentTextNum;
    speechScr newScript;
    bool myContinue = false;
    string word;

    public string[] Echo_1;
	public AudioClip[] Echo_1_Voice;
    public string[] Echo_2;
	public AudioClip[] Echo_2_Voice;
    public string[] Echo_3;
	public AudioClip[] Echo_3_Voice;
    public string[] Echo_4;
	public AudioClip[] Echo_4_Voice;
    public string[] Echo_5;
	public AudioClip[] Echo_5_Voice;

	AudioSource audioSource;

    public string[] tempArray;
	public AudioClip[] tempArrayVoice;
    Scene currentScene;

    public Image[] textPanel;
    bool canSwitch = true;
    bool canBox = true;
    bool canPush = true;
    bool canBite = true;
    bool canBox2 = true;
    bool canPush2 = true;
    bool canDanger = true;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        newScript = GameObject.Find("SpeechTest").GetComponent<speechScr>();
		audioSource = GetComponent<AudioSource> ();
        foreach (Image img in textPanel)
        {
            img.enabled = false;
        }
        dialogueT.enabled = false;

        audioSource.PlayDelayed(44100);
    }

    void Update()
    {
        if (newScript.getWord() == "up" && canSwitch == true && currentScene.name == "Level1")
        {
            InputDialogue("EchoPart2");
            canSwitch = false;
        }
        if (word == "box" && canBox == true && currentScene.name == "Level2")
        {
            InputDialogue("EchoPart2");
            canBox = false;
        }
        if (word == "push" && canPush == true && currentScene.name == "Level2")
        {
            InputDialogue("EchoPart3");
            canPush = false;
        }
        if (word == "bite" && canBite == true && currentScene.name == "Level2")
        {
            InputDialogue("EchoPart4");
            canBite = false;
        }
        if (word == "box" && canBox2 == true && currentScene.name == "Level3")
        {
            InputDialogue("EchoPart2");
            canBox2 = false;
        }
        if (word == "push" && canPush2 == true && currentScene.name == "Level3")
        {
            InputDialogue("EchoPart3");
            canPush2 = false;
        }        
    }

    public void InputDialogue(string name)
    {
        if (name == "EchoPart1")
        {
            tempArray = Echo_1;
			tempArrayVoice = Echo_1_Voice;
        }
        //must finish the first array BEFORE going to the next statement
        if(name == "EchoPart2")
        {
            tempArray = Echo_2;
			tempArrayVoice = Echo_2_Voice;
        }
        if (name == "EchoPart3")
        {
            tempArray = Echo_3;
			tempArrayVoice = Echo_3_Voice;
        }
        if (name == "EchoPart4")
        {
            tempArray = Echo_4;
			tempArrayVoice = Echo_4_Voice;
        }
        if (name == "EchoPart5")
        {
            tempArray = Echo_5;
			tempArrayVoice = Echo_5_Voice;
        }
        StartDialogue();
    }

	public void GoToNextLine()
	{
		if (talking)
		{
			Debug.Log ("IM TALKING");
			if (!done)
			{
				Next();
			}
			else
			{
				Debug.Log ("DONE TALKING");
				CloseDialogue();
			}
		}
	}

    void StartDialogue()
    {
        foreach (Image img in textPanel)
        {
            img.enabled = true;
        }
		audioSource.clip = tempArrayVoice[0];
		audioSource.volume = 1f;
		audioSource.Play();
        talking = true;
        done = false;
        dialogueT.enabled = true;
        dialogueT.text = tempArray[0];
        currentTextNum++;
    }

    void Next()
    {
        if (currentTextNum == tempArray.Length)
        {
            done = true;
            CloseDialogue();
        }
        else
        {
			audioSource.clip = tempArrayVoice[currentTextNum];
			audioSource.volume = 1f;
			audioSource.Play();
            dialogueT.text = tempArray[currentTextNum];
            currentTextNum++;
        }
    }

    public void CloseDialogue()
    {
        foreach (Image img in textPanel)
        {
            img.enabled = false;
        }
        dialogueT.enabled = false;
        talking = false;
        currentTextNum = 0;
    }

    public void ChangeContinue(bool thisContinue)
    {
        myContinue = thisContinue;
    }
    public void setWord(string myword)
    {
        word = myword;
    }

}