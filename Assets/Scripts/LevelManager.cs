using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Text scoreText;
    public CharacterController2DDriver kimiko;
    private float score;
    
    // Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        kimiko = go.GetComponent<CharacterController2DDriver>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (kimiko)
        {
            if (!kimiko.forceStop)
            {
                score += Time.deltaTime;
                scoreText.text = score.ToString("0");
            }
        }
	}
}
