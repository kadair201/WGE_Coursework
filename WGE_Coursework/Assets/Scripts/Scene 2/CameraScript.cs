using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public PlayerController2D playerController;
    Rigidbody rb;
    public float lerpAmount;
    public float lerpTime;

	// Use this for initialization
	void Start () {
        playerController = GetComponent<PlayerController2D>();
        //rb = playerController.GetComponent<Rigidbody>();
        playerController._hMoveInput += PlayerMoving;
	}

    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void PlayerMoving(float movement)
    {
        Debug.Log(movement);
        StartCoroutine(LerpPosition(transform.position, new Vector2(transform.position.x + movement, transform.position.y), lerpTime));
    }


    IEnumerator LerpPosition(Vector2 start, Vector2 end, float maxTime)
    {
        float t = 0;
        while (t < maxTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t / maxTime);
            if (t >= maxTime)
            {
                transform.position = end;
            }
            yield return null;
        }
    }

}
