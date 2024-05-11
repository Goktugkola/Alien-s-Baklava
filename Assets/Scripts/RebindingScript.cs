using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RebindingScript : MonoBehaviour
{
    // Reference to your Input Action Asset
    public InputActionAsset inputActions;

    // Specific action to rebind (e.g., "Move")
    public InputActionReference actionToRebind;

    // UI elements for displaying current binding and rebinding state
    public Text currentBindingText;
    public GameObject waitingForInputObject;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void StartRebinding()
    {
        // Disable current binding
        actionToRebind.action.Disable();

        // Start rebinding process
        rebindingOperation = actionToRebind.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();

        // Show UI for waiting for input
        waitingForInputObject.SetActive(true);
    }

    private void RebindComplete()
    {
        // Update UI to display new binding
        currentBindingText.text = actionToRebind.action.GetBindingDisplayString();

        // Enable the action with the new binding
        actionToRebind.action.Enable();

        // Hide waiting UI
        waitingForInputObject.SetActive(false);

        // Dispose of rebinding operation
        rebindingOperation.Dispose();
    }
}