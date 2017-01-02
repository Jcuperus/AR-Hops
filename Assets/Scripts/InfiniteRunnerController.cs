using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunnerController : MonoBehaviour {
    private bool isRunning = true;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log("rb" + rb);
        StartCoroutine(walkCycle());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator walkCycle() {
        while(isRunning) {
            Debug.Log("add force");
            rb.AddForce(new Vector3(1, 0, 0));
        }

        yield return 0;
    }
}
