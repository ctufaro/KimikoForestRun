using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {

    public Text MyTime;
    public Text BestTime;
    public static bool _resume;
    
    void Awake()
    {

    }    
    
    // Use this for initialization
	void Start () {

        GameObject[] go = GameObject.FindGameObjectsWithTag("Resumed");
        GameObject start = GameObject.Find("Start");
        
        if (_resume)
        {
            MyTime.text = PlayerPrefs.HasKey("CurrentTime") ? PlayerPrefs.GetString("CurrentTime") : "";
            BestTime.text = PlayerPrefs.HasKey("BestTime") ? PlayerPrefs.GetString("BestTime") : "";

        }

        foreach (var g in go)
        {
            g.SetActive(_resume);
        }

        start.SetActive(!_resume);

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Reset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainStart()
    {
        Application.LoadLevel(1);
    }
}
