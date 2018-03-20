using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public static bool menuCam = true;

	public Vector3 mainMenuPos;
	public Quaternion mainMenuAngle;

	public Vector3 customMenuPos;
	public Quaternion customMenuAngle;

	public GameObject cam;
	public Animator playerAnim;
	public GameObject player;
	public Quaternion playerMenuRotation;
	public Quaternion playerCustomRotation;



	public GameObject wall;
	public bool rise = true;
	private float stopHeight = 8;
	private float riseSpeed = .5f;

	public void camToMenu() {
		menuCam = true;
		playerAnim.Play("RunAndJump");
		rise = true;
	}

	public void camToCustom() {
		menuCam = false;
		playerAnim.Play("ArmsCrossed");
		rise = false;
	}


	void moveCameraToMain() {
		if (cam != null && player != null) {
			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, mainMenuAngle, .01f);
			cam.transform.position = Vector3.Lerp(cam.transform.position, mainMenuPos, .1f);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerMenuRotation, .05f);
		}
	}

	void moveCameraToCustom() {
		if (cam != null && player != null) {

			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, customMenuAngle, .01f);
			cam.transform.position = Vector3.Lerp(cam.transform.position, customMenuPos, .1f);
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerCustomRotation, .1f);
		}
	}

	private void Update() {

		if (menuCam) {
			moveCameraToMain();
		}else {
			moveCameraToCustom();
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			menuCam = true;
		} else if (Input.GetKeyDown(KeyCode.B)) {
			menuCam = false;
		}
		if (wall != null) {
			if (rise) {
				if (wall.transform.position.y < stopHeight) {
					Vector3 pos = wall.transform.position;
					pos.y += riseSpeed;
					wall.transform.position = pos;

				}
				if (wall.transform.position.y > stopHeight) {
					Vector3 pos = wall.transform.position;
					pos.y = stopHeight;
					wall.transform.position = pos;
				}
			} else {
				if (wall.transform.position.y > -19) {
					Vector3 pos = wall.transform.position;
					pos.y -= riseSpeed;
					wall.transform.position = pos;

				}
				if (wall.transform.position.y < -19) {
					Vector3 pos = wall.transform.position;
					pos.y = -19;
					wall.transform.position = pos;
				}
			}
		}
	}
}
