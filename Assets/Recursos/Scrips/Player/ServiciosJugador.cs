using DreamNoms.HeartSystem;
using DreamNoms.HeartSystem.Effect;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiciosJugador : MonoBehaviour
{
    float inicioX;
    float inicioY;
    public int puntos = 0;
    public int vidas = 3;

    private bool recibiendoDaño = false;

    [Header("MensajeMuerte")]
    public TextMeshProUGUI mensajeMuerte;

    [Header("Animator")]
    public Animator animator;

    [Header("Texto")]
    public TextMeshProUGUI variables;


    [Header("TextoPuntos")]
    public TextMeshProUGUI TXTpuntos;

    [Header("Fondo")]
    public Canvas fondo;

    [Header("Fondo Muerte")]
    public Canvas fondoMuerte;


    [Header("BTNcontinuar")]
    public GameObject btnContinuar;

    [Header("BTNsalir")]
    public GameObject btnSalir;


    [Header("Corazones")]
    public GameObject corazones;

    [Header("CorazonesSystemaEfectos")] 
    public HeartEffectSystem heartEffects;

    [Header("CorazonesEfectos")] 
    public HeartEffectSO efectosCorazon;


    void Awake()
    {
        vidas = 3;
        fondoMuerte.gameObject.SetActive(false);

        if (animator == null)
            animator = GetComponent<Animator>();
        variables.gameObject.SetActive(false);
        variables.text = "Vidas: " + vidas;

        TXTpuntos.text = puntos.ToString();
        
        btnContinuar.SetActive(false);

        btnSalir.SetActive(false);
        inicioX = transform.position.x;
        inicioY = transform.position.y; 

    }

    public void PuntoInicio()
    {
        inicioX = transform.position.x;
        inicioY = transform.position.y;
    }
    public async Task PerderVida()
    {
        heartEffects.BeginEffect(efectosCorazon,BeginEffectMode.Single);
        await Task.Delay(700);
        heartEffects.StopEffect(efectosCorazon);
    }

    public async void GenerarCorazon()
    {
        vidas++;
        if (vidas <= 3)
        {
            corazones.GetComponent<HealthController>().Heal(1);
            variables.text = "Vidas: " + vidas;
            heartEffects.BeginEffect(efectosCorazon, BeginEffectMode.Additive);
            await Task.Delay(500);
            heartEffects.StopEffect(efectosCorazon);
            return;
        }
        else if (vidas >= 3)
        {
            variables.gameObject.SetActive(true);
            variables.text = "Vidas: " + vidas;
            return;

        }
    }


    public async void Muerte()
    {
        if (recibiendoDaño) return;

        recibiendoDaño = true;
        vidas--;
        variables.gameObject.SetActive(false);
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.recibirDanio);

        await PerderVida();
        if (vidas == 0)
        {
            recibiendoDaño = false;
            GameOver();
            return;
        }


        if (vidas >= 3)
        {
            variables.gameObject.SetActive(true);
         
            variables.text = "Vidas: " + vidas;

            animator.SetBool("Muerte", true);

            await Task.Delay(500);

            animator.SetBool("Muerte", false);

            transform.position = new Vector2(inicioX, inicioY);

            await Task.Delay(500);
            
            recibiendoDaño = false;
            
            return;
        }
           

            corazones.GetComponent<HealthController>().TakeDamage(1);

            variables.text = "Vidas: " + vidas;

            animator.SetBool("Muerte", true);

            await Task.Delay(500);

            animator.SetBool("Muerte", false);

            transform.position = new Vector2(inicioX, inicioY);

             await Task.Delay(500);
        recibiendoDaño = false;
    } 

    void GameOver()
    {
        //  SONIDO DE GAME OVER
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.gameOver);
            AudioManager.instance.StopMusica();                            
        }

        TXTpuntos.gameObject.SetActive(false);
        corazones.gameObject.SetActive(false);

        variables.gameObject.SetActive(false);
        mensajeMuerte.text = "GAME OVER";
        mensajeMuerte.gameObject.SetActive(true);
        btnContinuar.SetActive(true);
        btnSalir.SetActive(true);
        fondoMuerte.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BTNcontinuar()
    {
        AudioManager.instance.CambiarMusica(AudioManager.instance.musicaNivel);

        vidas = 3;
        fondoMuerte.gameObject.SetActive(false);
        TXTpuntos.gameObject.SetActive(true);
        corazones.gameObject.SetActive(true);
        corazones.GetComponent<HealthController>().Heal(vidas);

   
        transform.position = new Vector2(inicioX, inicioY);

        btnContinuar.SetActive(false);
        btnSalir.SetActive(false);
        mensajeMuerte.gameObject.SetActive(false);
        fondo.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void Tp(float X, float Y, string lugar)
    {
        if (lugar == "nivel2")
        {
            transform.position = new Vector2(X, Y);
        }
    }

    public void BTNsalir2()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        // Cambiamos el cuadro de diálogo para que anuncie la victoria
        bool regresarAlMenu = UnityEditor.EditorUtility.DisplayDialog(
            "¡VICTORIA!",
            "¡Felicidades! Has completado el nivel con éxito. ¿Deseas regresar al menú principal?",
            "Ir al Menú",
            "Ver el mapa un momento"
        );

        if (regresarAlMenu)
        {
            SceneManager.LoadScene("menu");
        }
#else
    // En el juego final, como no se pueden usar ventanas del Editor,
    // aquí saltará directo al menú.
    SceneManager.LoadScene("menu");
#endif
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