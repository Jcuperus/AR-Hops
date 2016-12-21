using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject selectedTile;
    private GameObject confirmButton;

	void Start () {
        confirmButton = GameObject.Find("ConfirmButton");
        confirmButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setSelected(GameObject tile) {
        selectedTile = tile;
        showConfirmUi(true);
    }

    public void lockTile() {
        if (selectedTile != null) {
            Debug.Log("Locking tile:" + selectedTile);
            selectedTile.GetComponent<PlacableTileBehaviour>().lockTile(false);
            selectedTile = null;
            showConfirmUi(false);
        }
    }

    public GameObject getSelected() {
        return selectedTile;
    }

    public void showConfirmUi(bool isActive) {
        confirmButton.SetActive(isActive);
    }
}
