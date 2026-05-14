using System.Collections;
using UnityEngine;

public class PlataformaVertical : MonoBehaviour
{

    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distancia = 3f;

    [Header("Efecto Peso")]
    public float bajada = 2f;
    public float velocidadRebote = 2f;

    private Vector3 posicionInicial;

    private float offsetY = 0f;
    private bool rebotando = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimientoY = Mathf.PingPong(Time.time * velocidad, distancia * 2) - distancia;

        transform.position = new Vector3(
            posicionInicial.x,
            posicionInicial.y + movimientoY + offsetY,
            posicionInicial.z
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            foreach (ContactPoint2D punto in collision.contacts)
            {
                if (punto.normal.y < -0.5f)
                {
                    if (!rebotando)
                    {
                        StartCoroutine(EfectoPeso());
                    }
                }
            }
        }
    }

    IEnumerator EfectoPeso()
    {
        rebotando = true;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * velocidadRebote;

            offsetY = Mathf.Lerp(0, -bajada, t);

            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * velocidadRebote;

            offsetY = Mathf.Lerp(-bajada, 0, t);

            yield return null;
        }

        offsetY = 0;
        rebotando = false;
    }
}
