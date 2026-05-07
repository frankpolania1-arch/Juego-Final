using UnityEngine;
using UnityEngine.SceneManagement;

public class Dsuelo : MonoBehaviour
{
    public static bool tocandoSuelo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        tocandoSuelo = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       tocandoSuelo = false;
        
    }

}
