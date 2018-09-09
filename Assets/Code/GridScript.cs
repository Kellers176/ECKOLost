using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	public int height, width;
	float x, y;
	float isoX, isoY;
	//int spacing;
	public GameObject backgroundObject;
	public GameObject somethingObject;
	public List<Vector2> somethingPositions;
	List<GameObject> grid;
	// Use this for initialization
	void Start () 
	{
		grid = new List<GameObject>();

		for (int i = 0; i < height*width; i++)
		{
			//adds element to list
			grid.Add (backgroundObject);
			//x and y cartesian coordinates
			x = i % width;
			y = i / width;
			//above coordinates as isometric coordinates
			isoX = (x - y) / 2.0f;
			isoY = (y + x) / 2.0f;
			//creats an instance of the gameobject
			grid[i] = Instantiate (grid [i], new Vector2 (isoX, isoY), Quaternion.identity) as GameObject;
		}

		if (somethingObject != null) 
		{
			placeSomething ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		
	void placeSomething()
	{
		for (int i = 0; i < somethingPositions.Count; i++) 
		{
			//Instantiate(somethingObject, grid[(int)somethingPositions[i].y * width + (int)somethingPositions[i].x].transform.position, Quaternion.identity);
			grid[(int)somethingPositions[i].y * width + (int)somethingPositions[i].x] = Instantiate(somethingObject, grid[(int)somethingPositions[i].y * width + (int)somethingPositions[i].x].transform.position, Quaternion.identity) as GameObject;
		}

		for (int i = 0; i < height * width; i++) 
		{
			Debug.Log (grid[i].transform.position.x + " " + grid[i].transform.position.y);
		}
	}
}
