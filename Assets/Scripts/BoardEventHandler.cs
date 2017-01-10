using UnityEngine;
using System.Collections;
using Vuforia;

public class BoardEventHandler : MonoBehaviour, ITrackableEventHandler {
    private TrackableBehaviour mTrackableBehaviour;
    private GameObject[] tiles;
    private InfiniteRunnerController playerController;

    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        tiles = GameObject.FindGameObjectsWithTag("TileTarget");
        playerController = GameObject.Find("BeepBoop").GetComponent<InfiniteRunnerController>();
        
        if (mTrackableBehaviour) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        print("status changed: " + newStatus);
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

        // Enable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = true;
        }

        //TODO: show placed tiles only
        //Show all tiles
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<PlacableTileBehaviour>().setRendering(true);
        }

        //TODO: suspend game when target lost
        //playerController.setIsRunning(true);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost() {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = false;
        }

        //Hide all tiles
        foreach (GameObject tile in tiles) {
            tile.GetComponent<PlacableTileBehaviour>().setRendering(false);
        }

        //TODO: suspend game when target lost
        //playerController.setIsRunning(false);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }
}
