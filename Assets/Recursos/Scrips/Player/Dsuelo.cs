using UnityEngine;

public class DSuelo : MonoBehaviour
{
    private int cantidadSuelos = 0; // Cuenta cuántos bloques de suelo tocamos
    public bool tocandoSuelo => cantidadSuelos > 0; // Es verdadero si el contador es mayor a 0

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            cantidadSuelos++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            cantidadSuelos--;
            if (cantidadSuelos < 0) cantidadSuelos = 0; // Evita números negativos por si acaso
        }
    }
}