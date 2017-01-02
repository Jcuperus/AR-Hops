using UnityEngine;
using System.Collections;
using Vuforia;

public class BoardEventHandler : MonoBehaviour, ITrackableEventHandler {
    private TrackableBehaviour mTrackableBehaviour;
    private GameObject[] tiles;

    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        if (mTrackableBehaviour) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            OnTrackingFound();
        }
        else {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound() {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = true;
        }

        //// Enable colliders:
        //foreach (Collider component in colliderComponents) {
        //    component.enabled = true;
        //}

        //Show all tiles
        foreach (GameObject tile in tiles) {
            tile.GetComponent<PlacableTileBehaviour>().setRendering(true);
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost() {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = false;
        }

        //// Disable colliders:
        //foreach (Collider component in colliderComponents) {
        //    component.enabled = false;
        //}

        //Hide all tiles
        foreach (GameObject tile in tiles) {
            tile.GetComponent<PlacableTileBehaviour>().setRendering(false);
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }
}
