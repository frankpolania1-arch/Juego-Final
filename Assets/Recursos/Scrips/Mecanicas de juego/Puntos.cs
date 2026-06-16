using UnityEngine;

public class Puntos : MonoBehaviour
{
    [Header("ServiciosJugador")]
    public ServiciosJugador serviciosJugador;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.recogerPunto);

        serviciosJugador.puntos += 5;
        serviciosJugador.puntosParaVidaExtra += 5;

        if (serviciosJugador.TXTpuntos != null)
            serviciosJugador.TXTpuntos.text = serviciosJugador.puntos.ToString();

        if (serviciosJugador.puntosParaVidaExtra >= 50)
        {
            serviciosJugador.puntosParaVidaExtra -= 50; // conserva sobrantes
            serviciosJugador.GenerarCorazon();

            Debug.Log("Vida extra obtenida");
        }

        Destroy(gameObject);
    }
}