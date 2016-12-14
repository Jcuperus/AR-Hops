using UnityEngine;
using System.Collections;

public class PlacableTileBehaviour : MonoBehaviour {
    private bool isSelected = false;

    public void setSelected(bool boolean) {
        isSelected = boolean;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (isSelected) {
            gameObject.transform.position = Camera.main.transform.position;
        }
	}
}
