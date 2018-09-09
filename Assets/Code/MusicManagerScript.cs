using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerScript : MonoBehaviour {

    public static MusicManagerScript instance = null;

    public static MusicManagerScript Instance
    {
        get
        { return instance; }
    }

    [HideInInspector]

    public AudioSource audioSound;
    public AudioClip[] music;
    public string[] ScenesPlayedAt;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

		audioSound = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;

        audioSound.PlayDelayed(44100);

		audioSound.clip = music[0];
		audioSound.volume = 0.1f;
		audioSound.Play();	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log (scene.buildIndex);
		//play the music that corresponds with the scene
		PlayThisMusic(scene.buildIndex);
	}


	void PlayThisMusic(int index)
	{
		if (MusicManagerScript.instance.audioSound.clip.name != MusicManagerScript.instance.music[index].name)
		{
			audioSound.clip = music[index];
			audioSound.volume = 0.1f;
			audioSound.Play();
		}
	}
}
