using UnityEngine;
using System.Collections;

struct CameraPathStep
{
	public Vector3 speedStep;
	public float lengthStep;
}

[System.Serializable]
public class CameraPath
{
	public bool enabled = true;
	public float speed;
	public Vector3[] pathPoints;
	private CameraPathStep[] speedSteps;

	private float progress = 0f;
	private int index = 0;
	private bool done = false;
	private Vector3 internalTracker;

	public bool Done ()
	{
		return done;
	}

	public Vector3 Reset (Vector3 defaultPosition)
	{
		progress = 0f;
		index = 0;
		speedSteps = new CameraPathStep[pathPoints.Length - 1];
		for (int i = 1; i < pathPoints.Length; i++) {
			speedSteps [i - 1].speedStep = pathPoints [i] - pathPoints [i - 1];
			speedSteps [i - 1].lengthStep = speedSteps [i - 1].speedStep.magnitude;
			speedSteps [i - 1].speedStep.Normalize ();
		}
		done = false;
		internalTracker = (pathPoints.Length > 0) && enabled ? pathPoints [0] : defaultPosition;
		return internalTracker;
	}

	public Vector3 MoveAlongPath ()
	{
		if (!enabled)
			return internalTracker;
		else if (index >= speedSteps.Length) {
			done = true;
			return internalTracker;
		} else {
			progress += Time.deltaTime * speed;

			internalTracker = Vector3.MoveTowards (internalTracker, pathPoints [index + 1], Time.deltaTime * speed);

			while ((index < speedSteps.Length) && (progress >= speedSteps [index].lengthStep)) {
				progress -= speedSteps [index].lengthStep;
				index++;
				internalTracker = pathPoints [index];
			}
			return internalTracker;
		}
	}
}
