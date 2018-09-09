using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class playerMoveScr : MonoBehaviour {

	public bool wallCollide;
	public bool boxCollide;
	public GameObject box;
	public Tilemap grid;

	Vector3Int pos;
	Vector3 posNudge;
	public float speed;

	public bool canPush;
	public bool pushing = false;
	public bool canBite;
	public bool biting = false;
	public bool canHug = false;
	public bool hugging = false;
	public float xDirection, yDirection;
	public int xDistance = 0;
	public int yDistance = 0;
	public int xDestination= 0;
	public int yDestination = 0;
	public int angle = 0;
	public bool canListen = false;


	Animator anim;

	// Use this for initialization
	void Start () 
	{
		xDirection = 0;
		yDirection = 1;

		gameObject.transform.position = grid.GetCellCenterWorld(new Vector3Int(0, 0, 0));

        wallCollide = false;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
    }

	void FixedUpdate()
	{
		checkWallCollision ();

		//gameObject.transform.Rotate (Vector3.forward * angle);
		gameObject.transform.rotation = Quaternion.Euler (0, 0, angle);

		if (Vector3.Distance(gameObject.transform.position, grid.GetCellCenterWorld(new Vector3Int(xDestination, yDestination, pos.z))) < 0.01 && pushing == false && biting == false && hugging == false)
		{
			stopPlayer ();
			anim.SetBool ("isMoving", false);
		}

		if (canPush == true) 
		{
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xDirection, yDirection);

			posNudge = new Vector3 (gameObject.transform.position.x + (0.3f * xDirection), gameObject.transform.position.y + (0.3f * yDirection), 0);//Mathf.Floor(player.transform.position);
			canPush = false;
			pushing = true;
		}
       
        if(canBite == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xDirection, yDirection);
            posNudge = new Vector3(gameObject.transform.position.x + (0.3f * xDirection), gameObject.transform.position.y + (0.3f * yDirection), 0);
            canBite = false;
            biting = true;
        }

		if(canHug == true)
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xDirection, yDirection);
			posNudge = new Vector3(gameObject.transform.position.x + (0.3f * xDirection), gameObject.transform.position.y + (0.3f * yDirection), 0);
			canHug = false;
			hugging = true;
		}

        if(biting == true)
        {
            pushMotion();
        }

		if (pushing == true) 
		{
			pushMotion();
		}

		if (hugging == true) 
		{
			pushMotion();
		}
	}

	public void stopPlayer()
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		canListen = true;
	}

	public void updatePlayer(bool pushStatus, float newSpeed, float newXDir, float newYDir, int newXDist, int newYDist, int newXDest, int newYDest)
	{
		canPush = pushStatus;
		speed = newSpeed;
		xDirection = newXDir;
		yDirection = newYDir;
		xDistance = newXDist;
		yDistance = newYDist;
		xDestination = newXDest;
		yDestination = newYDest;
	}

	public void movePlayer(int parseDistance)
	{
		canListen = false;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xDirection * speed, yDirection * speed);
		pos = new Vector3Int (Mathf.FloorToInt(gameObject.transform.position.x), Mathf.FloorToInt(gameObject.transform.position.y), 0);//Mathf.Floor(player.transform.position);
		yDistance = Mathf.FloorToInt(parseDistance * yDirection); 
		xDistance = Mathf.FloorToInt(parseDistance * xDirection); 
		xDestination = pos.x + xDistance;
		yDestination = pos.y + yDistance;
		anim.SetBool ("isMoving", true);
	}

	void pushMotion()
	{
		//Do a little push motion
		if (Vector3.Distance (gameObject.transform.position, posNudge) < 0.01) 
		{
			pos = new Vector3Int (Mathf.FloorToInt(gameObject.transform.position.x), Mathf.FloorToInt(gameObject.transform.position.y), 0);//Mathf.Floor(player.transform.position);
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-xDirection * 1, -yDirection * 1);
			pushing = false;
            biting = false;
			hugging = false;
		}
	}

	void checkWallCollision()
	{
		if (pushing == false && biting == false  && hugging == false && gameObject.GetComponent<Rigidbody2D> ().velocity == Vector2.zero) 
		{
			wallCollide = false;
			boxCollide = false;
		}

		if (gameObject.GetComponent<playerMoveScr>().wallCollide == true && pushing == false && biting == false && hugging == false) 
		{
			pos = new Vector3Int (Mathf.FloorToInt(gameObject.transform.position.x), Mathf.FloorToInt(gameObject.transform.position.y), 0);//Mathf.Floor(player.transform.position);
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-xDirection * speed, -yDirection * speed);
			xDestination =  Mathf.FloorToInt(pos.x);
			yDestination =  Mathf.FloorToInt(pos.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.layer == 9) 
		{
			wallCollide = true;
		}
		if (col.gameObject.tag == "Box") 
		{
			boxCollide = true;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.layer == 12 && Vector2.Distance(gameObject.transform.position, col.transform.position) < 0.01) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
			
		//if (col.gameObject.tag == "textTrigger" && (((gameObject.transform.position.x * Mathf.Abs(xDirection)) == col.transform.position.x || (gameObject.transform.position.y * Mathf.Abs(yDirection)) == col.transform.position.y))) 
		if (col.gameObject.tag == "textTrigger" && yDirection != 0 && Mathf.Abs(gameObject.transform.position.y - col.transform.position.y) < 0.01)
		{
			//your destination is here
			Debug.Log("RELOCATING............");
			pos = new Vector3Int (Mathf.FloorToInt(gameObject.transform.position.x), Mathf.FloorToInt(gameObject.transform.position.y), 0);
			xDestination =  Mathf.FloorToInt(pos.x);
			yDestination =  Mathf.FloorToInt(pos.y);
			Destroy(col.gameObject);
		}

		if (col.gameObject.tag == "textTrigger" && xDirection != 0 && Mathf.Abs(gameObject.transform.position.x - col.transform.position.x) < 0.01)
		{
			//your destination is here
			Debug.Log("RELOCATING............");
			pos = new Vector3Int (Mathf.FloorToInt(gameObject.transform.position.x), Mathf.FloorToInt(gameObject.transform.position.y), 0);
			xDestination =  Mathf.FloorToInt(pos.x);
			yDestination =  Mathf.FloorToInt(pos.y);
			Destroy(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.layer == 9) 
		{
			wallCollide = false;
		}
		if (col.gameObject.tag == "Box") 
		{
			boxCollide = false;
			box = col.gameObject;
		}
	}
}
