using UnityEngine;

public class SensorCheck : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.OnCheckTriggered(other);
        }
    }
}