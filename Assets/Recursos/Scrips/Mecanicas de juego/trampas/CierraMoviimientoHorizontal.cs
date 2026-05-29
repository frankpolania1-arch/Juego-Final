using UnityEngine;

public class CierraMoviimientoHorizontal : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    private BoxCollider2D box;

    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distancia = 3f;
    private float offsetY = 0f;
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }
    void Update()
    {
        float movimientoX =
            Mathf.PingPong(Time.time * velocidad, distancia * 2) - distancia;

        transform.position = new Vector3(
            posicionInicial.x + movimientoX,
            posicionInicial.y + offsetY,
            posicionInicial.z
        );
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
