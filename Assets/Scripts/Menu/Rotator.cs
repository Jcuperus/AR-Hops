using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	private Vector3 axis;
	public float speed = 1f;

	// Use this for initialization
	void Start ()
	{
		transform.rotation = new Quaternion (Random.value * 2f - 1f, Random.value * 2f - 1f, Random.value * 2f - 1f, Random.value * 2f - 1f);
		axis = new Vector3 (Random.value * 2f - 1f, Random.value * 2f - 1f, Random.value * 2f - 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround (transform.position, axis, speed * Time.deltaTime);
	}
}
