using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour {

	public GameObject[] objs;

	void Start () {
		foreach (GameObject obj in objs) {
			obj.SetActive(false);

		}
	}
	
	
}
