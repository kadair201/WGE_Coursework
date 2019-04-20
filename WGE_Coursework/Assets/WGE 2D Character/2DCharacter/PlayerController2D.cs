using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {
    
    public delegate void JumpInput();
    public delegate void JumpReleaseInput();
    public delegate void JumpPressedInput();
    public delegate void HorizontalMoveInput(float x);
    public delegate void DashPressedInput(Vector2 direction);
    public delegate void CameraZoom(GameObject subjectOfFocus);

    public event JumpInput _jumpInput;
    public event JumpReleaseInput _jumpReleaseInput;
    public event HorizontalMoveInput _hMoveInput;
    public event JumpPressedInput _jumpPressedInput;
    public event DashPressedInput _dashPressedInput;
    public event CameraZoom _cameraZoom;

    public bool canMove = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            float hMove = Input.GetAxis("Horizontal");
            float vMove = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                _jumpPressedInput();
            }
            if (Input.GetButton("Jump"))
            {
                _jumpInput();
            }
            if (Input.GetButtonUp("Jump"))
            {
                _jumpReleaseInput();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _dashPressedInput(new Vector2(hMove, vMove));
            }

            _hMoveInput(hMove);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "NPC")
        {
            _cameraZoom(GameObject.Find("NPC"));
            GameObject.Find("XMLObject").GetComponent<DialogueScript>().BeginDialogue();
        }
    }
}
