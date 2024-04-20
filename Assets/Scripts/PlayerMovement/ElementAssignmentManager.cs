using System.Collections;
using UnityEngine;

public class ElementAssignmentManager : MonoBehaviour
{
    public enum Element { None, Slime, Stone, Air }
    private enum Mechanic { Sprinting, Dashing, Jumping }

    private Element sprintElement = Element.None;
    private Element dashElement = Element.None;
    private Element jumpElement = Element.None;

    private int assignedCombinations = 0;
    private const int maxCombinations = 3;

    private bool isAssigningElements = false;
    private Mechanic currentMechanic = Mechanic.Sprinting; // Track the current mechanic being assigned


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isAssigningElements)
            {
                StartElementAssignment();
            }
            else
            {
                StopElementAssignment();
            }
        }
    }
        private void StartElementAssignment()
    {
        isAssigningElements = true;
        StartCoroutine(ElementAssignmentRoutine());
    }

    private void StopElementAssignment()
    {
        isAssigningElements = false;
        ResetElementAssignment();
    }

    private IEnumerator ElementAssignmentRoutine()
    {
        while (isAssigningElements && assignedCombinations < maxCombinations)
        {
            yield return null; // Wait for next frame to avoid freezing the game
            ShowElementAssignmentScreen();
        }
    }
    // Called when the player presses the tab key
    public void ShowElementAssignmentScreen()
    {
        // Display UI for element assignment (e.g., prompts and keys)
        // You can use Unity's UI system (Canvas, Text, Buttons, etc.) to create the UI elements.
        // Capture player input to assign elements for the current mechanic
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        {
            // Assign element based on the current mechanic
            Element assignedElement = Element.None;

            if (Input.GetKeyDown(KeyCode.Q))
                assignedElement = Element.Slime;
            else if (Input.GetKeyDown(KeyCode.E))
                assignedElement = Element.Stone;
            else if (Input.GetKeyDown(KeyCode.R))
                assignedElement = Element.Air;
                Debug.Log("It werks");

            AssignElement(assignedElement, currentMechanic);

            // Move to the next mechanic
            currentMechanic = GetNextMechanic(currentMechanic);
        }
    }
    // Method to get the next mechanic in sequence
    private Mechanic GetNextMechanic(Mechanic current)
    {
        switch (current)
        {
            case Mechanic.Sprinting:
                return Mechanic.Jumping;
            case Mechanic.Jumping:
                return Mechanic.Dashing;
            case Mechanic.Dashing:
                return Mechanic.Sprinting; // Go back to sprinting to repeat the cycle
            default:
                return Mechanic.Sprinting;
        }
    }

    // Called when the player assigns an element
    private void AssignElement(Element element, Mechanic mechanic)
    {
        if (assignedCombinations >= maxCombinations)
        {
            Debug.LogWarning("Maximum combinations reached!");
            return;
        }

        // Check if the chosen element is already assigned to another mechanic
        if (IsElementAssigned(element))
        {
            Debug.LogWarning("Element already assigned to a different mechanic!");
            return;
        }

        // Assign the chosen element to the specified mechanic
        switch (mechanic)
        {
            case Mechanic.Sprinting:
                sprintElement = element;
                Debug.Log(element.ToString() + " assigned to Sprinting.");
                break;
            case Mechanic.Dashing:
                dashElement = element;
                Debug.Log(element.ToString() + " assigned to Dashing.");
                break;
            case Mechanic.Jumping:
                jumpElement = element;
                Debug.Log(element.ToString() + " assigned to Jumping.");
                break;
        }

        assignedCombinations++;
        if (assignedCombinations >= maxCombinations)
        {
            StopElementAssignment();
        }
    }

    // Reset assigned elements and combinations
    private void ResetElementAssignment()
    {
        sprintElement = Element.None;
        dashElement = Element.None;
        jumpElement = Element.None;
        assignedCombinations = 0;
    }

    // Check if an element is already assigned to any mechanic
    private bool IsElementAssigned(Element element)
    {
        return sprintElement == element || dashElement == element || jumpElement == element;
    }



    // Apply element effects to mechanics based on player's choices
    private void ApplyElementEffects()
    {
        // Get the player's Rigidbody component (assuming your player has one)
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();

        // Apply effects based on assigned elements
        switch (sprintElement)
        {
            case Element.Slime:
                // Modify sprinting speed or friction
                playerRigidbody.drag = 2f; // Example: Increase drag for slime effect
                break;
            case Element.Stone:
                // Add weight or modify collision behavior
                playerRigidbody.mass = 100f; // Example: Increase mass for stone effect
                break;
            case Element.Air:
                // No specific effect for sprinting
                break;
        }

        switch (dashElement)
        {
            case Element.Slime:
                // No specific effect for dashing
                break;
            case Element.Stone:
                // Modify dash behavior (e.g., disable dashing temporarily)
                // Example: playerCanDash = false;
                break;
            case Element.Air:
                // Modify dash behavior (e.g., increase dash distance)
                // Example: dashDistance *= 1.5f;
                break;
        }

        switch (jumpElement)
        {
            case Element.Slime:
                // Modify jump height or air control
                playerRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse); // Example: Increase jump force
                break;
            case Element.Stone:
                // No specific effect for jumping
                break;
            case Element.Air:
                // Modify jump behavior (e.g., double jump)
                // Example: canDoubleJump = true;
                break;
        }
    }

    // Additional methods for handling UI, input, and gameplay mechanics
    // ...
}
