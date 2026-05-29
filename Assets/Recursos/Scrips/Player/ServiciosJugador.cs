using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiciosJugador : MonoBehaviour
{
    float inicioX;
    float inicioY;
    byte vidas;

    private bool recibiendoDaño = false; 

    [Header ("MensajeMuerte")]
    public TextMeshProUGUI mensajeMuerte;

    [Header("Animator")]
    public Animator animator;

    [Header("Texto")]
    public TextMeshProUGUI variables;

    [Header("Fondo")]
    public Canvas fondo;

    [Header("BTNcontinuar")]
    public GameObject btnContinuar;

    [Header("BTNsalir")]
    public GameObject btnSalir;

    void Awake()
    {
        vidas = 3;

        if (animator == null)
            animator = GetComponent<Animator>();

        variables.text = "Vidas: " + vidas;

        
        btnContinuar.SetActive(false);
        btnSalir.SetActive(false);
    }

    public void PuntoInicio()
    {
        inicioX = transform.position.x;
        inicioY = transform.position.y;
    }

    public async void Muerte()
    {
        if (recibiendoDaño) return;

        recibiendoDaño = true;

        if (vidas > 0)
        {
            vidas--;

            variables.text = "Vidas: " + vidas;

            animator.SetBool("Muerte", true);

            await Task.Delay(1000);

            animator.SetBool("Muerte", false);

            transform.position =
                new Vector2(inicioX, inicioY);
            if (vidas == 0)
            {
                GameOver();
            }
        }
        await Task.Delay(1500);

        recibiendoDaño = false;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        variables.gameObject.SetActive(false);
        mensajeMuerte.text = "GAME OVER";
        mensajeMuerte.gameObject.SetActive(true);
        btnContinuar.SetActive(true);
        btnSalir.SetActive(true);
        fondo.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BTNcontinuar()
    {
        vidas = 3;

        variables.gameObject.SetActive(true);
        variables.text = "Vidas: " + vidas;

        transform.position = new Vector2(inicioX, inicioY);

        btnContinuar.SetActive(false);
        btnSalir.SetActive(false);
        mensajeMuerte.gameObject.SetActive(false);
        fondo.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void BTNsalir()
    {

        Time.timeScale = 1f;
#if UNITY_EDITOR
        bool salir = UnityEditor.EditorUtility.DisplayDialog(
            "Salir",
            "Si abandonas perderás todo tu progreso. ¿Deseas salir?",
            "Sí",
            "No"
        );

        if (salir)
        {
            SceneManager.LoadScene("menu");
        }
#else
        // En compilación normal
        SceneManager.LoadScene("menu");
#endif
    }
}