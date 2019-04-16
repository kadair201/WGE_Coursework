using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    PlayerMovement2D playerMovement;
    public float xLerpTime;
    public float yLerpTime;
    public float xWiggle;

    // Use this for initialization
    void Start () {
        playerMovement = player.GetComponent<PlayerMovement2D>();
        playerMovement._cameraWiggle += CameraShake;
	}

    // Update is called once per frame
    void FixedUpdate () {
        float xLerp = Mathf.Lerp(transform.position.x, player.transform.position.x, xLerpTime);
        float yLerp = Mathf.Lerp(transform.position.y, player.transform.position.y, yLerpTime);
        transform.position = new Vector3(xLerp, yLerp, -12);
    }

    public void CameraShake()
    {
        StartCoroutine(Wiggle());
    }

    IEnumerator Wiggle()
    {
        transform.Translate(Vector3.left * xWiggle);
        yield return new WaitForSeconds(0.05f);
        transform.Translate(Vector3.right * xWiggle * 2);
        yield return new WaitForSeconds(0.05f);
        transform.Translate(Vector3.left * xWiggle);
    }
}
