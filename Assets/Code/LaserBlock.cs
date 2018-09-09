using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserBlock : MonoBehaviour {

    private LineRenderer lineRenderer;
    public Transform laserHit;
    RaycastHit2D hit;


    // Use this for initialization
    void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        hit = Physics2D.Raycast(transform.position, transform.up);
        Debug.DrawLine(transform.position, hit.point);
        laserHit.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserHit.position);
        lineRenderer.enabled = true;
        if(hit.collider.gameObject.tag == "Player")
        {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        }
    }



}
