using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerControl_Offline : MonoBehaviour {

	//Player Attributes
	public string playerName;
	public Color playerColor;
	public Text playerText;

    //Components
    public Rigidbody rb;
    public Camera cam;
    public CameraController cameraController;
	public new Renderer renderer;

	//movement
	private float tempMovementSpeed;
    public float movementSpeed;
    public float rotationSpeed;

    //jumping
    public float jumpForce;
    private bool jumpRequest;
    public bool isGrounded;
    public static float normalGrav = Physics.gravity.y;
    public static float fallMultiplier = normalGrav * 2.5f;
    public static float lowJumpMultiplier = normalGrav * 2f;

	//Animations
	public Animator anim;

	// Use this for initialization
	void Start() {
        tempMovementSpeed = movementSpeed;
    }

    void FixedUpdate() {
		if (!GameController_Offline.settingsOpen) {
			//Movement
			Vector3 xMov = (movementSpeed * transform.right * Time.deltaTime * Input.GetAxis("Horizontal"));
			Vector3 zMov = (movementSpeed * transform.forward * Time.deltaTime * Input.GetAxis("Vertical"));
			rb.MovePosition(transform.position + xMov + zMov);


			//Gravity 4 better jumping
			if (rb.velocity.y < 0) {
				Physics.gravity = new Vector3(0, fallMultiplier, 0);
			} else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
				Physics.gravity = new Vector3(0, lowJumpMultiplier, 0);
			} else {
				Physics.gravity = new Vector3(0, normalGrav, 0);
			}


			//Jumping
			if (jumpRequest) {
				rb.AddForce((Vector3.up * Time.deltaTime * (jumpForce * 100)), ForceMode.Impulse);
				jumpRequest = false;
			}
		}

	}

	void Die() {
		int rand = (int) Mathf.Floor(Random.Range(0, 4));
		switch(rand) {
			case 0:
				anim.Play("Die01");
				break;
			case 1:
				anim.Play("Die02");
				break;
			case 2:
				anim.Play("Die03");
				break;
			case 3:
				anim.Play("Die04");
				break;
		}

		/*if (rand == 0)
		else if (rand == 1)
		else if (rand == 2)
			
		else if (rand == 3)
		*/
	}

	void Update() {
        if (!GameController_Offline.settingsOpen) {

			//Animations
			anim.SetFloat("BlendX", Input.GetAxis("Horizontal"));
			anim.SetFloat("BlendY", Input.GetAxis("Vertical"));


			//Jumping
			if (Input.GetKeyDown(KeyCode.Space)) {
                if (isGrounded) {
					jumpRequest = true;
					anim.SetBool("isJumping", true);
				} 
			}

			//emotes
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				Die();
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				anim.Play("emote2");
			}
			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				anim.Play("emote3");
			}

			//Change movement when ball is focused
			if (cameraController.isBallFocused) {
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, Space.Self);
            } 

            //Free Look if alt pressed
            if (!Input.GetKey(KeyCode.LeftAlt) && !cameraController.isBallFocused) {
                transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y);
            }

        }
      }


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
			anim.SetBool("isJumping", false);
		}
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }  

	void OnNameChanged(string value) {
		playerName = value;
		gameObject.name = playerName;
		//set text
		playerText.text = playerName;
	}

	void OnColorChanged(Color value) {
		playerColor = value;
		renderer.material.color = playerColor;
	}
}
