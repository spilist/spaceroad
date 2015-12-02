using UnityEngine;
using System.Collections;

public class awake : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();

	
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.forward * 1f;
	}
}
