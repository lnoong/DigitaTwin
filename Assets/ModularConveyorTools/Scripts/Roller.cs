using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Roller : MonoBehaviour {

	//tangent speed at any point along roller
	public float tangentSpeed;
	//radius of the roller
	public float radius= 0.125f;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
	}
	// Update is called once per frame
	void Update () {
		//rotate the roller
		float angularVelocity = (tangentSpeed) / radius;
		Quaternion newRot = rb.rotation * Quaternion.AngleAxis (angularVelocity * Mathf.Rad2Deg * Time.deltaTime, transform.InverseTransformDirection(transform.right));
		rb.MoveRotation (newRot);
	}
}
