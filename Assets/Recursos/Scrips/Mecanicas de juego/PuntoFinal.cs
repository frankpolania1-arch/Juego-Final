using UnityEngine;

public class PuntoFinal : MonoBehaviour
{
    [Header("Pantalla Final")]
    public Canvas pantallaFinal;

    [Header("btnSalir")]
    public GameObject btnSalir;

    [Header("Collider")]
    public Collider2D box;

    void Start()
    {
        pantallaFinal.enabled = false;
        btnSalir.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detenemos la música de fondo
            if (AudioManager.instance != null)
                AudioManager.instance.StopMusica();

            // 🔊 Reproducimos el sonido de victoria
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.sonidoVictoria);

            pantallaFinal.enabled = true;
            btnSalir.SetActive(true);
        }
    }
}