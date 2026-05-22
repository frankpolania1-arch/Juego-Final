using Assets.Recursos.Scrips.Mecanicas_de_juego;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cajas : MonoBehaviour
{
    BoxCollider2D Bx;
    Animator anim;
    Rigidbody2D rb;
    ServiciosJuego Sjuego;
    string mensaje;
    string orden1, orden2, orden3, orden4, orden;
    RanaNPC rana;
    private void Awake()
    {
        rana = FindAnyObjectByType<RanaNPC>();
        Sjuego = Object.FindAnyObjectByType<ServiciosJuego>();
        anim = GetComponent<Animator>();
        Bx = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        orden = Sjuego.orden;
        orden1 = Sjuego.orden1;
        orden2 = Sjuego.orden2;
        orden3 = Sjuego.orden3;
        orden4 = Sjuego.orden4;
       
    }
    void Start()
    {
        gameObject.SetActive(true);
    }


    public async void partesMensaje()
    {
        int random = Random.Range(0, 7);

        if (Sjuego.fragmentosRecogidos == 4)
        {
            Sjuego.fragmentosRecogidos = 0;
        }
        if (random == 1 && Sjuego.msj1)
        {
            anim.SetBool("cambio", true);
            gameObject.transform.localScale = new Vector2(3f, 3f);
            Sjuego.msj1 = false;
            Sjuego.fragmentosRecogidos++;
            rana.fragmentosR++;
            Debug.Log(Sjuego.frases[Sjuego.Mensaje].partes[0].texto);
        }
        else if (random == 2 && Sjuego.msj2)
        {
            anim.SetBool("cambio", true);
            gameObject.transform.localScale = new Vector2(3f, 3f);
            Sjuego.msj2 = false;
            rana.fragmentosR++;
            Sjuego.fragmentosRecogidos++;
            Debug.Log(Sjuego.frases[Sjuego.Mensaje].partes[1].texto);
        }
        else if (random == 3 && Sjuego.msj3)
        {
            anim.SetBool("cambio", true);
            gameObject.transform.localScale = new Vector2(3f, 3f);
            rana.fragmentosR++;
            Sjuego.fragmentosRecogidos++;
            Sjuego.msj3 = false;
            Debug.Log(Sjuego.frases[Sjuego.Mensaje].partes[2].texto);
        }
        else if (random == 4 && Sjuego.msj4)
        {
            anim.SetBool("cambio", true);
            gameObject.transform.localScale = new Vector2(3f, 3f);
            rana.fragmentosR++;
            Sjuego.fragmentosRecogidos++;
            Sjuego.msj4 = false;
            Debug.Log(Sjuego.frases[Sjuego.Mensaje].partes[3].texto);
        }
        else 
        {
            gameObject.SetActive(false);
            anim.SetBool("cambio", false);
            anim.SetBool("golpe", false);
            

            await Task.Delay(10000);
            anim.SetBool("Regreso", true);

            gameObject.SetActive(true);

        }
       
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (anim.GetBool("cambio")== true)
        {
            return;
        }
        if (collision.CompareTag("Dtecho") )
        
            anim.SetBool("golpe", true);
            await Task.Delay(500);
            partesMensaje();
    }  
}
