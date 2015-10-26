using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public float DegreesPerSecond = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentEuler = transform.eulerAngles;
		currentEuler.y += DegreesPerSecond * Time.deltaTime;
		transform.eulerAngles = currentEuler;
	}
}
