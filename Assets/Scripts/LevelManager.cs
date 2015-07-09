using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Text scoreText;
    public Button AudioButton;
    public Sprite AudioOffImage;
    public Sprite AudioOnImage;
    public CharacterController2DDriver kimiko;
    public static Vector3? position = null;
    public static bool AudioOn = true;
    public bool SendToEnd;
    private float score;
    
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        kimiko = go.GetComponent<CharacterController2DDriver>();
        AudioButton.image.overrideSprite = (AudioOn) ? AudioOnImage : AudioOffImage;
        score = 0;

        if (SendToEnd)
        {
            kimiko.transform.position = new Vector3(GameObject.Find("RoundOverZone").transform.position.x-5,GameObject.Find("RoundOverZone").transform.position.y,0);
        }

        if (position != null)
        {
            go.transform.position = position.Value;
        }
    }

    public void ToggleAudioButton()
    {      
        AudioOn = !AudioOn;

        if (AudioButton)
        {
            AudioButton.image.overrideSprite = (AudioOn) ? AudioOnImage : AudioOffImage;
            PlayMusic(AudioOn);
        }
       
    }

    public void PlayMusic(bool on)
    {

    }
    
    public static void CheckPoint()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        position = go.transform.position;
    }
	
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
