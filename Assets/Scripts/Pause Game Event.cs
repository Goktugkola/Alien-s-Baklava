using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Pause Game Event")]
public class PauseGameEvent : ScriptableObject
{
    private System.Action onEventRaised;

    public void Raise()
    {
        if (onEventRaised != null)
        {
            onEventRaised.Invoke();
        }
    }

    public void RegisterListener(System.Action listener)
    {
        onEventRaised += listener;
    }

    public void UnregisterListener(System.Action listener)
    {
        onEventRaised -= listener;
    }
}