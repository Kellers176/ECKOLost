using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanScript : MonoBehaviour {

	public Transform laserHit;


	RaycastHit2D hit;


	public float speed;
	public float rotateSpeed;
	//float rotateCounter = 0;
	float distance;
	float currRotation;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		hit = Physics2D.Raycast(transform.position + transform.right, transform.right );


		Debug.DrawLine(transform.position, hit.point);
		laserHit.position = hit.point;

		distance = speed * Time.deltaTime;

		if (GameObject.Find("SpeechTest").GetComponent<speechScr>().guardCanRotate == true) 
		{
			gameObject.transform.rotation = Quaternion.Euler (0, 0, currRotation += 90);
			//GameObject.Find ("SpeechTest").GetComponent<speechScr> ().guardCanRotate = false;
			//rotateCounter = 0;
		}
		if(hit.collider.gameObject.tag == "Player")
		{
			//rotateCounter = 0;
			gameObject.transform.position = Vector3.MoveTowards (transform.position, hit.collider.gameObject.transform.position, distance);
			GameObject.Find ("SpeechTest").GetComponent<speechScr> ().stopListening ();
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player" && col.gameObject.layer !=5) 
		{
			if (col.gameObject.GetComponent<playerMoveScr> ().hugging == false)
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			else 
			{
				gameObject.SetActive (false);
			}
		}
	}

	void RotateHuman()
	{
		gameObject.transform.rotation = Quaternion.Euler (0, 0, currRotation += 90);
	}
}
