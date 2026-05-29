using UnityEngine;

public class Cierra : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    private BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<ServiciosJugador>().Muerte();
        }
        else return;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<ServiciosJugador>().Muerte();
        }
        else return;
    }
}
