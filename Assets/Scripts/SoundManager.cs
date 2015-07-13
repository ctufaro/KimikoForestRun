using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;
    private static AudioSource music;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            music = this.GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void ToggleAudio(bool on)
    {
        music.mute = on;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
