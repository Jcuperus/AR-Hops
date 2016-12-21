using UnityEngine;
using System.Collections;
using Vuforia;
using System;

public class PlacableTileEventHandler : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour mTrackableBehaviour;
    private PlacableTileBehaviour tileBehaviour;

    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        tileBehaviour = GetComponent<PlacableTileBehaviour>();
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
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) 
        {
            OnTrackingFound();
        }
        else {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound() {
        //Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        //// Enable rendering:
        //foreach (Renderer component in rendererComponents) {
        //    component.enabled = true;
        //}

        tileBehaviour.setSelected(true);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost() {
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }
}
