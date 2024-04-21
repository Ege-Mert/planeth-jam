    using System;
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
        
        public WallRunning wallRunningComponent;
        public PlayerMovementDashing pmd;
        public GameObject Player;
        public Rigidbody rb;

        private void Start()
        {
            
            pmd = GetComponent<PlayerMovementDashing>();
            Debug.Log(pmd.jumpForce);
        }

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
                ApplyElementEffects(); // Call ApplyElementEffects() when the maximum combinations are reached
                StopElementAssignment(); // Stop element assignment after applying effects
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
                ApplyElementEffects();
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
            
            Debug.Log("Reseted");
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
                    pmd.sprintSpeed = 8f;
                    wallRunningComponent.enabled = true;
                    break;
                case Element.Stone:
                    pmd.sprintSpeed = 2.1f;
                    wallRunningComponent.enabled = false;
                    break;
                case Element.Air:
                    wallRunningComponent.enabled = false;
                    pmd.sprintSpeed = 14f;
                    break;
            }

            switch (dashElement)
            {
                case Element.Slime:
                    Player.tag = "Tag1";
                    pmd.dashSpeed = 20;
                    break;
                
                case Element.Stone:
                    Player.tag = "Player";
                    pmd.dashSpeed = 10;
                    break;
               
                case Element.Air:
                    Player.tag = "Tag1";
                    pmd.dashSpeed = 30;
                    break;
            }

            switch (jumpElement)
            {
                case Element.Slime:
                    Debug.Log("o");
                    rb.mass = 1f;
                    pmd.jumpForce = 18;
                    break;
                case Element.Stone:
                    Debug.Log("p");
                    // No specific effect for jumping
                    rb.mass = 1f;
                    pmd.jumpForce = 6;
                    
                    break;
                case Element.Air:
                    Debug.Log("u");
                    AzaltYercekimi(0.5f);
                    pmd.jumpForce = 7;
                    break;
            }
        }
        
        public void AzaltYercekimi( float azaltmaMiktari)
        {
            Debug.Log("sex");
          
                Debug.Log("Sex2"); 
                // Karakterin kütlesini azaltarak yerçekimini etkile
                // Karakterin kütlesini azaltarak yerçekimini etkiles
                rb.mass *= azaltmaMiktari; // Karakterin kütlesini azalt
            
            
        }
    }
