using UnityEngine;
using System.Collections;

public class CameraPanRight : MonoBehaviour {

	// Use this for initialization
	void Start () {

        float origX = this.transform.position.x;
        float origY = this.transform.position.y;
        float origZ = this.transform.position.z;
        StartCoroutine(PanRight(this.transform, origX, origY, origZ, 75f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator PanRight(Transform tran, float origX, float origY, float origZ, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            tran.localPosition = new Vector3(Mathf.Lerp(origX, 500f, t), Mathf.Lerp(origY, origY, t), Mathf.Lerp(origZ, origZ, t));
            yield return null;
        }
    }
}
