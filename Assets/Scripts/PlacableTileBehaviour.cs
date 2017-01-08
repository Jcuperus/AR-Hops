using UnityEngine;
using System.Collections;

public class PlacableTileBehaviour : MonoBehaviour {
    private bool isSelected = false;
    private RaycastHit hitInfo = new RaycastHit();
    private LayerMask layerIndex;
    private Renderer[] rendererComponents;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        layerIndex = LayerMask.NameToLayer("Board");
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        setRendering(false);
	}
	
	// Update is called once per frame
	void Update () {
        castRay();
    }

    void castRay() {
        if (isSelected) {
            Transform camera = Camera.main.transform;
            //Debug.DrawRay(camera.position, camera.forward, Color.red, 2, true);
            var layerMask = 1 << layerIndex;
            if (Physics.Raycast(camera.position, camera.forward, out hitInfo, 500, layerMask)) {
                //Set gameobject location
                setRendering(true);
                //transform.GetChild(0).transform.position = roundVector(hitInfo.point);
                gameObject.transform.position = roundVector(hitInfo.point);
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                //Round vectors to snap to grid
            }
            else {
                setRendering(false);
            }
        }
    }

    public void lockTile(bool boolean) {
        setSelected(boolean);
        setRendering(!boolean);
    }

    private Vector3 roundVector(Vector3 vector) {
        int roundFactor = 2;
        return new Vector3(Mathf.Round(vector.x / roundFactor) * roundFactor, Mathf.Round(vector.y / roundFactor) * roundFactor * roundFactor);
    }

    public void setSelected(bool boolean) {
        if (boolean) {
            gameManager.setSelected(gameObject);
        }
        isSelected = boolean;
    }

    public void setRendering(bool boolean) {
        rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer component in rendererComponents) {
            component.enabled = boolean;
        }
    }
}
