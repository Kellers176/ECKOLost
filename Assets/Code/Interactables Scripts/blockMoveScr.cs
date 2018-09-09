using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class blockMoveScr : MonoBehaviour {

	GameObject player;
	float xDirection, yDirection;
	int xDestination, yDestination;
	Tilemap grid;
	bool objectCollide;
	bool inHole;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("Player");
		grid = player.GetComponent<playerMoveScr> ().grid;

	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void FixedUpdate()
	{
		checkObjectCollision ();
		checkHoleCollision ();

		if (Vector3.Distance(gameObject.transform.position, grid.GetCellCenterWorld(new Vector3Int(xDestination, yDestination, 0))) < 0.01)
		{
			stopBox ();
		}
	}

	void pushBox()
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(player.GetComponent<playerMoveScr>().xDirection*1, player.GetComponent<playerMoveScr>().yDirection*1, 0);
		xDestination = Mathf.FloorToInt(gameObject.transform.position.x) + Mathf.FloorToInt(1 * xDirection);
		yDestination = Mathf.FloorToInt(gameObject.transform.position.y) + Mathf.FloorToInt(1 * yDirection);
	}

	public void stopBox()
	{
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		xDestination = Mathf.FloorToInt(gameObject.transform.position.x);
		yDestination = Mathf.FloorToInt(gameObject.transform.position.y);
		gameObject.transform.position = new Vector2 (xDestination+0.5f, yDestination+0.5f);
	}

	void checkObjectCollision()
	{
		if (objectCollide == true) 
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-xDirection * 1, -yDirection * 1);
			xDestination =  Mathf.FloorToInt(gameObject.transform.position.x);
			yDestination =  Mathf.FloorToInt(gameObject.transform.position.y);
		}
	}

	void checkHoleCollision ()
	{
		if (inHole == true) 
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			if (player.GetComponent<playerMoveScr>().pushing == true)
			{
				xDirection = player.GetComponent<playerMoveScr> ().xDirection;
				yDirection = player.GetComponent<playerMoveScr> ().yDirection;
				pushBox ();
			}
		}
		if (col.gameObject.layer == 9) 
		{
			objectCollide = true;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.layer == 12 && gameObject.transform.position == col.transform.position) 
		{
			inHole = true;
			col.gameObject.SetActive (false);
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
