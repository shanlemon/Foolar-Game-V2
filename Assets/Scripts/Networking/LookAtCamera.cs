using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	public new Transform camera;
	void Start() {
		camera = Camera.main.transform;
	}


	// Update is called once per frame
	void LateUpdate () {
		if(camera == null) {
			camera = Camera.main.transform;
			return;
		}
		
		transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
	}
}
