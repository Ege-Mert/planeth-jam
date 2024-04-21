using UnityEngine;

public class QuickRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Assign this in the Unity Editor to the desired respawn point
    public LayerMask playerLayer; // Assign the player layer in the Unity Editor

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private bool IsPlayer(GameObject obj)
    {
        // Check if the GameObject's layer matches the player layer
        return playerLayer == (playerLayer | (1 << obj.layer));
    }

    private void RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPoint.position; // Move the player to the respawn point
        // Optionally, you can add more logic here, such as resetting health, ammo, etc.
    }
}
