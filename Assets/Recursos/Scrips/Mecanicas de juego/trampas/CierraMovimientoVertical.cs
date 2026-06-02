using UnityEngine;

public class CierraMovimientoVertical : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distancia = 3f;

    private Vector3 posicionInicial;
    private float offsetActual = 0f;   // este sí se usa en Update

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimientoY = Mathf.PingPong(Time.time * velocidad, distancia * 2);

        transform.position = new Vector3(
            posicionInicial.x,
            posicionInicial.y + movimientoY + offsetActual,
            posicionInicial.z
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<ServiciosJugador>().Muerte();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<ServiciosJugador>().Muerte();
        }
    }
}