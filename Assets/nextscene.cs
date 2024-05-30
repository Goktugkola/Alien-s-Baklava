using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class VideoEndSceneChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer component in the inspector
    public string sceneName; // Set the name of the scene you want to load in the inspector

    void Start()
    {
        if (videoPlayer != null)
        {
            // Subscribe to the loopPointReached event
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer not assigned.");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
