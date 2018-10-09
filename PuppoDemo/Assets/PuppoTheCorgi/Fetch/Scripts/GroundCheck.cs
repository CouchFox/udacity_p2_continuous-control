using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    void OnCollisionStay(Collision collisionInfo) {
        // Debug-draw all contact points and normals
        if (collisionInfo.collider.tag == "body"){
           // Debug.Log("grounded: " + collisionInfo.collider.name);
            transform.parent.GetComponentInChildren<DogAgent>().BodyTouchingGround();
        }
    }


}
