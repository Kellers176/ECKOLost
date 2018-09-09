using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class speechScr : MonoBehaviour {

	Rigidbody2D camRb;
    public GameObject player;
	//The speach recognizer
	KeywordRecognizer recognizer;
	//The list of keywords
	public string[] keywords = new string[]{"down", "up", "left", "right"};
	//The confidence. Better diction needed for higher levels
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    TextBoxManagerScript myTextManager;

    Vector3Int pos;
	Vector3 posNudge;
    public float speed;
	public int maxDistance = 5;

	float xDirection, yDirection;

    public EchoLocation myEcho;

    //The string I output the word to
    public string word;

    Vector3 position;

	Text text;
	RectTransform controlImageRT;
	bool helpDisplayed = false;
	public bool guardCanRotate = false;

	// Use this for initialization
	void Start () 
	{
        myTextManager = GameObject.Find("EckoUI_AI").GetComponent<TextBoxManagerScript>();
        camRb = GameObject.Find ("Main Camera").GetComponent<Rigidbody2D>();
		//This inits the listener
		if (keywords != null) 
		{
			recognizer = new KeywordRecognizer (keywords, confidence);
			//Not sure what this does
			recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
			recognizer.Start ();
		}
		text = GameObject.Find ("Canvas").GetComponentInChildren<Text> ();

		controlImageRT = GameObject.Find ("Canvas").transform.GetChild (3).GetComponent<RectTransform>();
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
		guardCanRotate = false;

        switch (word)
        {
			case "up":
				setPlayerDirection(0, 1, 0);
                myTextManager.setWord("up");
                break;
            case "down":
				setPlayerDirection(0, -1, 180);
                break;
            case "left":
				setPlayerDirection(-1, 0, 90);
                break;
            case "right":
				setPlayerDirection(1, 0, 270);
                break;
            case "box":
                myEcho.StartEchoing("Box");
                myTextManager.setWord("box");
                word = "";
                break;
			case "push": 
				player.GetComponent<playerMoveScr> ().canPush = true;
                myTextManager.setWord("push");
                word = " ";
				break;
            case "bite":
                player.GetComponent<playerMoveScr>().canBite = true;
                myTextManager.setWord("bite");
                word = " ";
                break;
			case "hug":
				player.GetComponent<playerMoveScr>().canHug = true;
				word = " ";
				break;
			case "echo reset":
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				word = " ";
				break;
			case "echo main menu":
				SceneManager.LoadScene ("Menu1");
				word = " ";
				break;
            case "danger":
                myEcho.StartEchoing("Danger");
                myTextManager.setWord("Danger");
                word = "";
                break;
			case "environment":
				myEcho.StartEchoing("Environment");
				word = "";
				break;
            case "next":
				myTextManager.GoToNextLine();
                word = "";
                break;
			case "echo help":
				controlImageRT.anchoredPosition = Vector3.MoveTowards (controlImageRT.transform.localPosition, new Vector3(65,0,0), 10);
				break;
			case "close":
				controlImageRT.anchoredPosition = Vector3.MoveTowards (controlImageRT.transform.localPosition, new Vector3 (-740, 0, 0), 10);
				break;
            case "echo activate protocol gamma alpha romeo foxtrot":
                stopListening();
                GameObject instance = Instantiate(Resources.Load("Gorf_God")) as GameObject;
                instance.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
                word = "";
                break;
            default:           
                break;
        }
        
		int possibleParse;
		int.TryParse (word, out possibleParse);
		if (possibleParse > 0 && possibleParse <= maxDistance)
		{
			stopListening ();
			player.GetComponent<playerMoveScr> ().movePlayer (possibleParse);
			guardCanRotate = true;
			word = "";
		}
    
		if (player.GetComponent<playerMoveScr> ().canListen == true) 
		{
			startListening ();
		}
    }

	void setPlayerDirection(int newXDirection, int newYDirection, int newAngle)
	{
		if (player.GetComponent<playerMoveScr> ().canListen == true) {
			player.GetComponent<playerMoveScr> ().xDirection = newXDirection;
			player.GetComponent<playerMoveScr> ().yDirection = newYDirection;
			player.GetComponent<playerMoveScr> ().angle = newAngle;		
		} 
		else 
		{
			word = "";
		}
	}

	//Fires when a recognized string is heard
	void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		if (args.text == "echo help") 
		{
			helpDisplayed = true;
			word = args.text;
		}
		if (args.text == "close") 
		{
			helpDisplayed = false;
		}

		if (myTextManager.talking == true && args.text == "next")
			word = args.text;
		else if (myTextManager.talking == false && helpDisplayed == false) 
		{
			word = args.text;
		}

        text.text = "You said: " + word;
	}

	public string getWord()
	{
		return word;
	}

	public void stopListening()
	{
		recognizer.Stop();
	}

	public void startListening()
	{
		if (recognizer.IsRunning == false)
		recognizer.Start();
	}

}
