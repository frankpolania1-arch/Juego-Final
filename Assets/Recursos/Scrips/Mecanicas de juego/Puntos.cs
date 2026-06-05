using UnityEngine;

public class Puntos : MonoBehaviour
{
    [Header("ServiciosJugador")]
    public ServiciosJugador serviciosJugador;

    private int puntosParaVidaExtra = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 🔊 Sonido al recoger un punto
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.recogerPunto);

            serviciosJugador.puntos = serviciosJugador.puntos + 5;
            puntosParaVidaExtra += 5;

            if (puntosParaVidaExtra >= 50)
            {
                serviciosJugador.TXTpuntos.text = serviciosJugador.puntos.ToString();
                puntosParaVidaExtra = 0;
                serviciosJugador.GenerarCorazon();

                Debug.Log("Vida extra obtenida");
                Destroy(gameObject);
                return;
            }

            serviciosJugador.TXTpuntos.text = serviciosJugador.puntos.ToString();
            Destroy(gameObject);
        }
    }
}