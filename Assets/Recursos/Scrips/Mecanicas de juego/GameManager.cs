using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Puerta2")]
    public GameObject puerta2;

    [Header("CheckP")]
    public BoxCollider2D check;

    [Header("Puerta")]
    public Puerta puerta;

    [Header("ServiciosJugador")]
    public GameObject serviciosJugador;

    void Awake()
    {
        puerta2.SetActive(false);
        puerta.GetComponent<Puerta>();
        serviciosJugador.GetComponent<GameObject>();
    }

    public void OnCheckTriggered(Collider2D other)
    {
        Debug.Log("¡El GameManager se enteró de que algo entró al check!: " + other.name);
        puerta.puntos = 0;
        serviciosJugador.GetComponent<ServiciosJugador>().PuntoInicio();    
        Debug.Log("Puntos de la puerta: " + puerta.puntos);

        puerta2.SetActive(true);
    }
}