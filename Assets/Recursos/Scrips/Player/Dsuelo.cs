using UnityEngine;

public class Dsuelo : MonoBehaviour
{
    public static bool tocandoSuelo;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Suelo"))
        {
            tocandoSuelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Suelo"))
        {
            tocandoSuelo = false;
        }
    }
}