using UnityEngine;

public class SensorCheck : MonoBehaviour
{
    public GameManager gameManager;

    [Header("player")]
    public ServiciosJugador player;

    [Header("Animator")]
    public Animator animator;

    float nivel2X;
    float nivel2Y;

    float puertabloque2X;
    float puertabloque2Y;

    float puertabloqueN32X;
    float puertabloqueN32Y;

    private void Awake()
    {
        gameManager.puerta3.SetActive(false);
        GameObject objetoNivel2 = GameObject.FindGameObjectWithTag("nivel2");

        if (objetoNivel2 != null)
        {
            nivel2X = objetoNivel2.transform.position.x;
            nivel2Y = objetoNivel2.transform.position.y;
        }

        GameObject BloquePuerta = GameObject.FindGameObjectWithTag("PuertaN2");
        if (BloquePuerta != null)
        {
            puertabloque2X = BloquePuerta.transform.position.x;
            puertabloque2Y = BloquePuerta.transform.position.y;
        }

        GameObject BloquePuertaN3 = GameObject.FindGameObjectWithTag("PuertaN3");
        if (BloquePuertaN3 != null)
        {
            puertabloqueN32X = BloquePuertaN3.transform.position.x;
            puertabloqueN32Y = BloquePuertaN3.transform.position.y;
        }

        animator.SetBool("check", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animator.GetBool("check"))
            return;

        Cajas[] todasLasCajas = FindObjectsByType<Cajas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Cajas caja in todasLasCajas)
        {
            caja.gameObject.SetActive(true);
            caja.anim.SetBool("cambio", false);
        }

        // --- Sonido de checkpoint (solo si ha pasado al menos 1 segundo desde que se cargó el nivel) ---
        bool esCheckpoint = gameObject.CompareTag("FinNivel1") || gameObject.CompareTag("check");
        if (esCheckpoint && Time.timeSinceLevelLoad > 1f)
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.checkpoint);
        }

        if (gameObject.CompareTag("FinNivel1") && other.CompareTag("Player"))
        {
            animator.SetBool("check", true);
            player.Tp(nivel2X, nivel2Y, "nivel2");
        }

        if (gameObject.CompareTag("check") && other.CompareTag("Player"))
        {
            gameManager.OnCheckTriggered(other);
            animator.SetBool("check", true);

            if (gameObject.name == "nivel2")
            {
                gameManager.puertaBloque.gameObject.SetActive(true);
                gameManager.puertaBloque.gameObject.transform.position = new Vector2(puertabloque2X, puertabloque2Y);
            }
            if (gameObject.name == "Check2")
            {
                gameManager.puerta3.SetActive(true);
            }
            if (gameObject.name == "Check3")
            {
                gameManager.puertaBloque.gameObject.SetActive(true);
                gameManager.puertaBloque.gameObject.transform.position = new Vector2(puertabloqueN32X, puertabloqueN32Y);
            }
        }
    }
}