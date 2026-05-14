using System.Collections;
using UnityEngine;

public class TrampaFuego : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Tiempos")]
    public float tiempoEspera = 3f;
    public float tiempoDisparo = 1f;

    void Start()
    {
        StartCoroutine(CicloFuego());
    }

    IEnumerator CicloFuego()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEspera);

            animator.SetBool("fuego", true);

            yield return new WaitForSeconds(0.5f);

            animator.SetBool("disparo", true);

            yield return new WaitForSeconds(tiempoDisparo);

            animator.SetBool("disparo", false);
            animator.SetBool("fuego", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (animator.GetBool("disparo"))
            {
                Servicios servicios = other.GetComponent<Servicios>();

                if (servicios != null)
                {
                    servicios.Muerte();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (animator.GetBool("disparo"))
        {
            if (collision.CompareTag("Player"))
            {
                Servicios servicios = collision.GetComponent<Servicios>();

                if (servicios != null)
                {
                    servicios.Muerte();
                }
            }
        }
    }
}