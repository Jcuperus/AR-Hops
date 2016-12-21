using UnityEngine;
using System.Collections;

public class CameraPathControl : MonoBehaviour
{
	public CameraPath[] paths;
	private int index;
	private Vector3 defaultPosition;

	void ChooseIndex ()
	{
		index = paths.Length > 0 ? Random.Range (0, paths.Length) : -1;
		if (index >= 0)
			paths [index].Reset (defaultPosition);
	}

	// Use this for initialization
	void Start ()
	{
		defaultPosition = transform.localPosition;
		ChooseIndex ();
	}
	
	// LateUpdate is called once per frame
	void LateUpdate ()
	{
		if (index >= 0) {
			transform.localPosition = paths [index].MoveAlongPath ();
			if (paths [index].Done ())
				ChooseIndex ();
		}
	}
}
