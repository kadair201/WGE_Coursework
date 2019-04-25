using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    public GameObject npc;
    PlayerMovement2D playerMovement;
    PlayerController2D playerController;
    public float xLerpTime;
    public float yLerpTime;
    public float xWiggle;
    public float yWiggle;
    public float zoomSpeed;
    public float camSize;
    //public float speed;
    public bool zooming = false;
    public Camera mainCam;
    public GameObject lerpSubject;
    public DialogueScript dialogueScript;

    // Use this for initialization
    void Start () {
        playerMovement = player.GetComponent<PlayerMovement2D>();
        playerController = player.GetComponent<PlayerController2D>();
        playerMovement._cameraWiggle += CameraShake;
        playerController._cameraZoom += FocusOnSubject;
        lerpSubject = player;
        npc = GameObject.Find("NPC");
	}

    // Update is called once per frame
    void FixedUpdate () {

        float xLerp = Mathf.Lerp(transform.position.x, lerpSubject.transform.position.x, xLerpTime);
        float yLerp = Mathf.Lerp(transform.position.y, lerpSubject.transform.position.y, yLerpTime);
        transform.position = new Vector3(xLerp, yLerp, -12);

        
    }

    private void Update()
    {
        if (!zooming)
        {
            playerController.canMove = true;
        }
        else
        {
            playerController.canMove = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (lerpSubject == npc)
                {
                    lerpSubject = player;
                    dialogueScript.ShowPlayerOptions();
                }
            }
        }
    }

    public void CameraShake()
    {
        StartCoroutine(Wiggle());
    }

    public void FocusOnSubject(GameObject subject)
    {
        zooming = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(Zoom());
        lerpSubject = subject;
        
    }

    public void ZoomOut()
    {
        zooming = false;
        lerpSubject = player;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(StartZoomOut());
    }

    IEnumerator Wiggle()
    {
        transform.Translate(Vector3.left * xWiggle);
        transform.Translate(Vector3.up * yWiggle);
        yield return new WaitForSeconds(0.01f);
        transform.Translate(Vector3.right * xWiggle * 2);
        transform.Translate(Vector3.down * yWiggle);
        yield return new WaitForSeconds(0.01f);
        transform.Translate(Vector3.left * xWiggle);
        transform.Translate(Vector3.up * yWiggle);
    }

    IEnumerator Zoom()
    {
        do
        {
            mainCam.orthographicSize -= camSize;
            yield return new WaitForSeconds(zoomSpeed);
            //speed /= 2;
        } while (mainCam.orthographicSize > 2);
    }

    IEnumerator StartZoomOut()
    {
        do
        {
            mainCam.orthographicSize += camSize;
            yield return new WaitForSeconds(zoomSpeed);
            //speed /= 2;
        } while (mainCam.orthographicSize < 4);
    }
}
