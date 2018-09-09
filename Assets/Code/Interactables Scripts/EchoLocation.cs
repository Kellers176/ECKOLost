using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoLocation : MonoBehaviour {
    public GameObject circle;

    bool canPush = true;
    public float radius;
	// Use this for initialization
	void Start () 
	{
		circle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartEchoing(string searchTerm)
    {
		circle.GetComponent<ColliderEchoLocation> ().tagToSearch = searchTerm;
        if(canPush)
        {
            circle.SetActive(true);
            canPush = false;
            StartCoroutine(ScaleOverTime(1));
        }
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = circle.transform.localScale;

        Vector3 destinationScale = new Vector3(radius, radius, radius);

        float currentTime = 0.0f;

        do
        {
            circle.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        StartCoroutine(ScaleBackToSmall(1));

    }
    IEnumerator ScaleBackToSmall(float time)
    {
        Vector3 originalScale = circle.transform.localScale;

        Vector3 destinationScale = new Vector3 (0.2f, 0.2f, 0.2f);

        float currentTime = 0.0f;

        do
        {
            circle.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        circle.SetActive(false);
        canPush = true;
    }
}
