using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesInicio : MonoBehaviour
{
    [Header("Botones")]
    public Button botonInicio;
    public Button botonSalir;

    void Start()
    {
        // Activa la música del menú al iniciar la escena
        if (AudioManager.instance != null)
            AudioManager.instance.CambiarMusica(AudioManager.instance.musicaMenu);
    }

    public void OnButtonInicioClick()
    {
        // Cambia a música del nivel antes de cargar
        if (AudioManager.instance != null)
            AudioManager.instance.CambiarMusica(AudioManager.instance.musicaNivel);

        SceneManager.LoadScene("nivel1");
    }

    public void OnButtonSalirClick()
    {
        Application.Quit();
    }
}