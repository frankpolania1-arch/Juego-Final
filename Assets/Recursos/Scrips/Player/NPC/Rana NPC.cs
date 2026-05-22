using Assets.Recursos.Scrips.Mecanicas_de_juego;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RanaNPC : MonoBehaviour
{
    public TextMeshProUGUI  Drana;
    BoxCollider2D colicion;
    ServiciosJuego Sjuego;
    Button BTNfrase;
    public byte fragmentosR = 0; 

    void Start()
    {
        colicion = GetComponent<BoxCollider2D>();
        Sjuego = FindFirstObjectByType<ServiciosJuego>();
        Drana.text = "";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(Sjuego.fragmentosRecogidos);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (fragmentosR== 0)
            {
                Drana.text = "Hola, soy la rana, ¿quieres jugar conmigo? Busca los 4 fragmentos";
            }
            else if (fragmentosR == 1)
            {
                Drana.text = "Busca 3 fragmentos mas.";
            }
            else if (fragmentosR == 2)
            {
                Drana.text = "Busca 2 fragmentos mas.";
            }
            else if ( fragmentosR == 3)
            {
                Drana.text = "Busca un fragmento mas.";
            }
            else if (   fragmentosR == 4)
            {
                Drana.text = "Tienes todos los fregmentos completa la frase.";
             
            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Drana.text = "";
            }
    }
}
