using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerControl : NetworkBehaviour {

	//Player Attributes
	[SyncVar (hook = "CmdOnNameChanged")]
	public string playerName;

	[SyncVar (hook = "OnColorChanged")]
	public Color playerColor;

	[SyncVar(hook = "OnTeamChanged")]
	public int playerTeam;

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
    private bool isGrounded;
    public static float normalGrav = Physics.gravity.y;
    public static float fallMultiplier = normalGrav * 2.5f;
    public static float lowJumpMultiplier = normalGrav * 2f;

	//Animations
	public Animator anim;

	// Use this for initialization
	void Start() {
        tempMovementSpeed = movementSpeed;
		isGrounded = true;

		StartCoroutine(setPosition());
    }

	public IEnumerator setPosition() {
		yield return new WaitForSeconds(1);
		MatchManager mm = FindObjectOfType<MatchManager>();
		if(playerTeam == 0) {
			transform.position = mm.blueSpawnPoints[Random.Range(0, mm.blueSpawnPoints.Length )].transform.position;
		}else if(playerTeam == 1) {
			transform.position = mm.redSpawnPoints[Random.Range(0, mm.redSpawnPoints.Length)].transform.position;
		}
	}

	void FixedUpdate() {

		if (!GameController.settingsOpen) {

			//Movement
			Vector3 xMov = (movementSpeed * transform.right * Time.deltaTime * Input.GetAxisRaw("Horizontal"));
			Vector3 zMov = (movementSpeed * transform.forward * Time.deltaTime * Input.GetAxisRaw("Vertical"));
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
		int rand = (int)Mathf.Floor(Random.Range(0, 4));
		if (rand == 0)
			anim.Play("Die01");
		else if (rand == 1)
			anim.Play("Die02");
		else if (rand == 2)
			anim.Play("Die03");
		else if (rand == 3)
			anim.Play("Die04");
	}

	void Update() {
        if (!isLocalPlayer)
            return;

		if((transform.position.y - 1.2878) <= .1) {
			isGrounded = true;
		}

        if (!GameController.settingsOpen) {

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
        if(collision.gameObject.tag == "Ground") {
            isGrounded = true;
			anim.SetBool("isJumping", false);
		}
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }  

	[Command]
	void CmdOnNameChanged(string value) {
		playerName = value;
		gameObject.name = playerName + " / " + playerTeam;
		//set text
		playerText.text = playerName + " / " + playerTeam;
	}
	void OnTeamChanged(int value) {
		playerTeam = value;
		gameObject.name = playerName + " / " + playerTeam;
		//set text
		playerText.text = playerName + " / " + playerTeam;
	}
	void OnColorChanged(Color value) {
		playerColor = value;
		renderer.material.color = playerColor;
	}
}
