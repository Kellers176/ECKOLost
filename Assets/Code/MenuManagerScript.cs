using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MenuManagerScript: MonoBehaviour {

	//The speach recognizer
	KeywordRecognizer recognizer;
	//The list of keywords
	public string[] keywords = new string[]{"down", "up", "left", "right"};
	//The confidence. Better diction needed for higher levels
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;

	//The string I output the word to
	public string word;

	// Use this for initialization
	void Start () 
	{
		//This inits the listener
		if (keywords != null) 
		{
			recognizer = new KeywordRecognizer (keywords, confidence);
			//Not sure what this does
			recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
			recognizer.Start ();
		}
	}

	void OnApplicationQuit()
	{
		if (recognizer != null && recognizer.IsRunning)
		{
			recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
			recognizer.Stop();
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{
		switch (word)
		{
		case "new game":
			SceneManager.LoadScene ("Level1");
			break;
		case "exit":
			Application.Quit ();
			break;
		case "load level":
			SceneManager.LoadScene ("MenuLevels");
			break;
		case "back":
			SceneManager.LoadScene ("Menu1");
			break;
		case "start":
			SceneManager.LoadScene ("Menu1");
			break;
		case "level 1":
			SceneManager.LoadScene ("Level1");
			break;
		case "level 2":
			SceneManager.LoadScene ("Level2");
			break;
		case "level 3":
			SceneManager.LoadScene ("Level3");
			break;
		case "level 4":
			SceneManager.LoadScene ("Level4");
			break;
		case "level 5":
			SceneManager.LoadScene ("Level5");
			break;
		case "level 6":
			SceneManager.LoadScene ("Level6");
			break;
		case "level 7":
			SceneManager.LoadScene ("Level7");
			break;
		case "level 8":
			SceneManager.LoadScene ("Level8");
			break;
		case "level 9":
			SceneManager.LoadScene ("Level9");
			break;
		case "level 10":
			SceneManager.LoadScene ("Level10");
			break;
		default:           
			break;
		}
	}

	//Fires when a recognized string is heard
	void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		word = args.text;
		Debug.Log (word);
	}

	public void stopListening()
	{
		recognizer.Stop();
	}

	public void startListening()
	{
		recognizer.Start();
	}
}
