<<<<<<< HEAD
using System;
=======
using System.Threading.Tasks;
>>>>>>> 79718277a544656d64e1422818c9eaa80f361c21
using UnityEngine;

public class SensorCheck : MonoBehaviour
{
    public GameManager gameManager;
    public Animator anim;



    [Header("player")]
    public ServiciosJugador player;

    [Header("Animator")]
    public Animator animator;


    float nivel2X;
    float nivel2Y;

    float puertabloque2X;
    float puertabloque2Y;

    float puertabloqueN32X;
    float puertabloqueN32Y;
    private void Awake()
    {
        gameManager.puerta3.SetActive(false);
        GameObject objetoNivel2 = GameObject.FindGameObjectWithTag("nivel2");

        if (objetoNivel2 != null) 
        {
            nivel2X = objetoNivel2.transform.position.x;
            nivel2Y = objetoNivel2.transform.position.y;
        }

        GameObject BloquePuerta= GameObject.FindGameObjectWithTag("PuertaN2");

        if (BloquePuerta != null)
        {
            puertabloque2X = BloquePuerta.transform.position.x;
            puertabloque2Y = BloquePuerta.transform.position.y;
        }

        GameObject BloquePuertaN3 = GameObject.FindGameObjectWithTag("PuertaN3");

        if (BloquePuertaN3 != null)
        {
            puertabloqueN32X = BloquePuertaN3.transform.position.x;
            puertabloqueN32Y = BloquePuertaN3.transform.position.y;
        }

        animator.SetBool("check", false); 
        Debug.Log("Nivel 2 X: " + puertabloqueN32X);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animator.GetBool("check"))
        {
            return;
        }
        Cajas[] todasLasCajas = FindObjectsByType<Cajas>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Cajas caja in todasLasCajas)
        {
            caja.gameObject.SetActive(true);

            caja.anim.SetBool("cambio", false);
        }
        if (gameObject.tag == "FinNivel1" && other.CompareTag("Player"))
        {
            animator.SetBool("check", true);
            player.Tp(nivel2X, nivel2Y, "nivel2");  
        }

        if (gameObject.tag == "check" && other.CompareTag("Player"))
        {

           
            gameManager.OnCheckTriggered(other);
<<<<<<< HEAD
            animator.SetBool("check", true);

            if (gameObject.name == "nivel2")
            {
                gameManager.puertaBloque.gameObject.SetActive(true);
                gameManager.puertaBloque.gameObject.transform.position = new Vector2(puertabloque2X, puertabloque2Y); 
            }
            if (gameObject.name == "Check2")
            {
                gameManager.puerta3.SetActive(true);
               
            }
            if (gameObject.name == "Check3")
            {
                gameManager.puertaBloque.gameObject.SetActive(true);
                gameManager.puertaBloque.gameObject.transform.position = new Vector2(puertabloqueN32X, puertabloqueN32Y);
            }
=======
            anim.SetBool("check", true);
        }
    }
    private async void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            await Task.Delay(500);
            anim.SetBool("check", false);
>>>>>>> 79718277a544656d64e1422818c9eaa80f361c21
        }
    }
}