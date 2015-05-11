using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {

    public Text MyTime;
    public Text BestTime;
    public static bool _resume;
    public static string _myTime;
    public static string _bestTime;
    
    void Awake()
    {

    }    
    
    // Use this for initialization
	void Start () {

        GameObject go = GameObject.FindGameObjectWithTag("MainTimes");
        
        if (!_resume)
        {
            go.SetActive(_resume);
        }
        else
        {
            go.SetActive(_resume);
            MyTime.text = _myTime;
            BestTime.text = _bestTime;
        }
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
