using System.Collections;
using UnityEngine;

public class PlataformaVertical : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distancia = 3f;

    [Header("Efecto Peso")]
    public float bajada = 0.4f;
    public float velocidadPeso = 8f;
    public float velocidadRecuperacion = 4f;

    private Vector3 posicionInicial;

    private float offsetActual = 0f;
    private float offsetObjetivo = 0f;

    private bool jugadorEncima = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimientoY =
            Mathf.PingPong(Time.time * velocidad, distancia * 2)
            - distancia;
        float velocidadSuavizado =
            jugadorEncima ?
            velocidadPeso :
            velocidadRecuperacion;

        offsetActual = Mathf.Lerp(
            offsetActual,
            offsetObjetivo,
            Time.deltaTime * velocidadSuavizado
        );
        transform.position = new Vector3(
            posicionInicial.x,
            posicionInicial.y + movimientoY + offsetActual,
            posicionInicial.z
        );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in collision.contacts)
            {
                if (punto.normal.y < -0.5f)
                {
                    jugadorEncima = true;

                    offsetObjetivo = -bajada;

                    collision.transform.SetParent(transform);

                    break;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEncima = true;

            offsetObjetivo = -bajada;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorEncima = false;

            offsetObjetivo = 0f;

            collision.transform.SetParent(null);
        }
    }
}