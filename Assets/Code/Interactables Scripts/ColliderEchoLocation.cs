using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEchoLocation : MonoBehaviour 
{
	public string tagToSearch;
    public List<SpriteRenderer> shadowRend = new List<SpriteRenderer>();

    int i = 0;
    // Use this for initialization
    void Start () 
	{
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == tagToSearch) 
		{
			shadowRend.Add(col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>());
            shadowRend[i].enabled = false;
            i++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == tagToSearch)
        {
            shadowRend[--i].enabled = true;
            shadowRend.RemoveAt(i);
        }
    }

    void isActive(Collider2D col)
    {
        col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    void isNotActive(Collider2D col)
    {
        col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
