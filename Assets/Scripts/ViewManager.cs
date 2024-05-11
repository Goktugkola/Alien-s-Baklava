using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "ViewManager")]
public class ViewManager : ScriptableObject
{
    [Header("Scene")]
    [SerializeField]public string scene ;
    public void PlayScene()
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(scene);
    }    
}
