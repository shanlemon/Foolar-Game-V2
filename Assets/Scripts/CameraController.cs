using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject crosshair;
    public bool lockCursor = true;
    public static float mouseSensitivity;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-52, 85);
	public float cameraMoveSpeed = 10f;

	public float skyRotateSpeed;

	public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Transform player;
    public Transform ball;

    public bool isBallFocused = false;
    public float yaw, pitch, tempYaw, tempPitch;


    void Start() {
        mouseSensitivity = 5f;
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

		StartCoroutine(findObjects());
		
	}

	IEnumerator findObjects() {
		yield return new WaitForSeconds(1);

		if (crosshair == null)
			crosshair = GameObject.Find("Crosshair");

		if (ball == null)
			ball = GameObject.Find("Ball").transform;
	}

	private void Update() {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyRotateSpeed);
	}

	void LateUpdate() {


		if (!GameController.settingsOpen && !GameController_Offline.settingsOpen) {
			yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
			pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

			pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);


			if (Input.GetKeyDown(KeyCode.LeftAlt)) {
				tempYaw = yaw;
				tempPitch = pitch;
			} else if (Input.GetKeyUp(KeyCode.LeftAlt)) {
				yaw = yaw - ((yaw % 360) - (tempYaw % 360));
				pitch = tempPitch;
			}

			if (Input.GetKeyDown(KeyCode.B)) {
				if (isBallFocused) {
					yaw = player.transform.rotation.eulerAngles.y;
					pitch = 13f;
				}
				isBallFocused = !isBallFocused;

				if (crosshair == null)
					crosshair = GameObject.Find("Crosshair");
				crosshair.SetActive(!isBallFocused);
			}

			if (isBallFocused) {
				Quaternion lookRotation = Quaternion.LookRotation(ball.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, .4f);
			} else {
				//crosshair.SetActive(true);
				currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
				transform.eulerAngles = currentRotation;

			}




			float step = cameraMoveSpeed * Time.deltaTime;
			Vector3 desiredPos = target.position - transform.forward * distFromTarget;
			transform.position = Vector3.MoveTowards(transform.position, desiredPos, step);

			//transform.position = target.position - transform.forward * distFromTarget;

			Vector3 pos = transform.position;
			pos.y = Mathf.Clamp(transform.position.y, 1, 100);
			transform.position = pos;

			Quaternion rot = transform.localRotation;
			rot.x = Mathf.Clamp(rot.x, -.43f, .64f);
			transform.localRotation = rot;
		}
	}
}   


    
