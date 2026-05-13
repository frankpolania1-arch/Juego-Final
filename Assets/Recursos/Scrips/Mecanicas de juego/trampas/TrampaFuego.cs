using System.Collections;
using Unity.VisualScripting;
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

            // Activa fuego
            animator.SetBool("fuego", true);

            yield return new WaitForSeconds(0.5f);

            // Activa disparo
            animator.SetBool("disparo", true);

            yield return new WaitForSeconds(tiempoDisparo);

            // Apaga animaciones
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
                Debug.Log("Quemado manito by stay");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (animator.GetBool("disparo"))
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Quemado manito");
            }
   
        }
        
    }

}