using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

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
