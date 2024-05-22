using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;

    private void OnEnable()
    {
        playerDeathEvent.RegisterListener(OnPlayerDeath);
    }

    private void OnDisable()
    {
        playerDeathEvent.UnregisterListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        // Stop time
        Time.timeScale = 0f;

        // Load Game Over scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOver",LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
