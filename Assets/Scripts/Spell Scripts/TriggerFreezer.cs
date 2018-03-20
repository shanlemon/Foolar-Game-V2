using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFreezer : MonoBehaviour {

    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    void OnTriggerEnter(Collider other) {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            rigidbodies.Add(rb);
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void OnDestroy() {
        Debug.Log("BEing es");
        foreach (Rigidbody rb in rigidbodies) {
            if(rb != null) {
                rb.constraints = RigidbodyConstraints.None;
            }
            
        }
    }





}
