using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Puerta2")]
    public GameObject puerta2;

    [Header("Puerta3")]
    public GameObject puerta3;

    [Header("CheckP")]
    public BoxCollider2D check;

    [Header("Puerta")]
    public Puerta puerta;

    [Header("PuertaBloque")]
    public GameObject puertaBloque;

    [Header("ServiciosJugador")]
    public GameObject serviciosJugador;

    public bool nivel2 = false;
    void Awake()
    {
        puerta2.SetActive(false);
        puerta3.SetActive(false);
        puerta.GetComponent<Puerta>();
        serviciosJugador.GetComponent<GameObject>();
    }

    public void OnCheckTriggered(Collider2D other)
    {

        if (nivel2)
        {
            puerta3.SetActive(true);
        }
        puerta.puntos = 0;
        serviciosJugador.GetComponent<ServiciosJugador>().PuntoInicio();    
        Debug.Log("Puntos de la puerta: " + puerta.puntos);

        puerta2.SetActive(true);
    }
}