using DreamNoms.HeartSystem;
using DreamNoms.HeartSystem.Effect;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiciosJugador : MonoBehaviour
{
    public int puntos;
    public int puntosParaVidaExtra;
    float inicioX;
    float inicioY;

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

    [Header("Fondo Carga")]
    public Canvas fondoCarga;

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

    [Header("Rigidbody")]
    public Rigidbody2D rb;


    void Awake()
    {
        vidas = 3;
        fondoMuerte.gameObject.SetActive(false);
        fondoCarga.gameObject.SetActive(false);

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
        await Task.Delay(400);
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
        if (recibiendoDaño)
            return;

        recibiendoDaño = true;
        vidas--;

        variables.gameObject.SetActive(false);

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.recibirDanio);

        await PerderVida();

        // Game Over
        if (vidas <= 0)
        {
            recibiendoDaño = false;
            GameOver();
            return;
        }

        // Actualizar UI
        if (vidas >= 3)
        {
            variables.gameObject.SetActive(true);
            variables.text = "Vidas: " + vidas;
        }
        else
        {
            corazones.GetComponent<HealthController>().TakeDamage(1);
            variables.text = "Vidas: " + vidas;
        }

        // Animación de muerte
        animator.SetBool("Muerte", true);

        await Task.Delay(500);

        animator.SetBool("Muerte", false);

        // Respawn
        transform.position = new Vector2(inicioX, inicioY);

        // Detener cualquier velocidad residual
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

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


        mensajeMuerte.gameObject.SetActive(false);
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