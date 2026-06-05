using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesInicio : MonoBehaviour
{

    [Header("Botones")]
    public Button botonInicio;

    [Header("Botones")]
    public Button botonSalir;


    public void OnButtonInicioClick()
    {
       SceneManager.LoadScene("nivel1");
    }

    public void OnButtonSalirClick()
    {
        Application.Quit();
    }
}
