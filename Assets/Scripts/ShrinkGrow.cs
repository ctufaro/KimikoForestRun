using UnityEngine;
using System.Collections;

public class ShrinkGrow : MonoBehaviour {

    public float maxSize = 2.0f;
    public float minSize = 0.2f;
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        var range = maxSize - minSize;
        double y = (Mathf.Sin(Time.time * speed) + 1.0) / 2.0 * range + minSize;
        double x = (Mathf.Sin(Time.time * speed) + 1.0) / 2.0 * range + minSize;
        this.transform.localScale = new Vector3((float)x, (float)y, 0);
    }
}
