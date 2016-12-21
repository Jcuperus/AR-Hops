using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneHelper : MonoBehaviour {
    private Text textReference;

    private void Start()
    {
        textReference = GetComponentInChildren<Text>();
    }

    public void switchScene(string sceneName)
    {
        StartCoroutine(loadScene(sceneName));
    }

    private IEnumerator loadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        while (!ao.isDone)
        {
            if (textReference != null)
            {
                textReference.text = "Loading " + ao.progress + "%";
                Debug.Log(textReference.text);
            }
            yield return 0;
        }
    }
}
