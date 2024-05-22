using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Player Death Event")]
public class PlayerDeathEvent : ScriptableObject
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