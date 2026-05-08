using TMPro;
using UnityEngine;

public class RanaNPC : MonoBehaviour
{
    public TextMeshProUGUI  Drana;
    BoxCollider2D colicion;
    void Start()
    {
        colicion = GetComponent<BoxCollider2D>();
        Drana.text = "";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hola, soy la rana, ¿quieres jugar conmigo?");

            Drana.text = "Hola, soy la rana, ¿quieres jugar conmigo?";
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
