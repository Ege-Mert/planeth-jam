using UnityEngine;

public class ElementAssignmentManager : MonoBehaviour
{
    public enum Element { None, Slime, Stone, Air }

    private Element sprintElement = Element.None;
    private Element dashElement = Element.None;
    private Element jumpElement = Element.None;

    private int assignedCombinations = 0;
    private const int maxCombinations = 3;

    private enum Mechanic
    {
        Sprinting,
        Dashing,
        Jumping
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            ShowElementAssignmentScreen();
        }
    }
    // Called when the player presses the tab key
    public void ShowElementAssignmentScreen()
    {
        // Display UI for element assignment (e.g., prompts and keys)
        // You can use Unity's UI system (Canvas, Text, Buttons, etc.) to create the UI elements.
        // For example:
        // - Show a panel with text instructions ("Assign elements to mechanics:")
        // - Display keys (Q, E, R) next to each mechanic (sprinting, dashing, jumping)
        

        // Capture player input (Q, E, R) to assign elements
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssignElement(Element.Slime, Mechanic.Sprinting);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AssignElement(Element.Stone, Mechanic.Sprinting);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            AssignElement(Element.Air, Mechanic.Sprinting);
        }
        // Repeat similar logic for dashing and jumping
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssignElement(Element.Slime, Mechanic.Jumping);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AssignElement(Element.Stone, Mechanic.Jumping);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            AssignElement(Element.Air, Mechanic.Jumping);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssignElement(Element.Slime, Mechanic.Dashing);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AssignElement(Element.Stone, Mechanic.Dashing);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            AssignElement(Element.Air, Mechanic.Dashing);
        }
        // You can also update the UI dynamically to show which elements are already assigned.
        // For example, change the color of assigned elements or display checkmarks next to them.
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
                break;
            case Mechanic.Dashing:
                dashElement = element;
                break;
            case Mechanic.Jumping:
                jumpElement = element;
                break;
        }

        assignedCombinations++;
        ApplyElementEffects();
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
