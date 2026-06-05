using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float movimientoSpeed = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;

    [Header("Detectar Suelo")]
    public Dsuelo Dsuelo;

    // Sonidos
    private float pasoCooldown = 0f;
    public float pasoIntervalo = 0.4f;  // Tiempo entre pasos

    private Rigidbody2D rb;
    private Animator animator;
    private float movimiento;
    private ServiciosJugador servicios;
    public bool panel = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        servicios = GetComponent<ServiciosJugador>();
   
    }

    void Update()
    {
        if (panel) return;

        bool enSuelo = Dsuelo.tocandoSuelo;

        if (enSuelo && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.saltar);
        }


        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            movimiento = -1f;
            Girar(-1);
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            movimiento = 1f;
            Girar(1);
        }
        else
        {
            movimiento = 0f;
        }


        if (enSuelo && Mathf.Abs(movimiento) > 0.1f)
        {
            pasoCooldown -= Time.deltaTime;
            if (pasoCooldown <= 0f)
            {
                pasoCooldown = pasoIntervalo;
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySFX(AudioManager.instance.caminar);
            }
        }
        else
        {
            pasoCooldown = 0f;
        }


        if (animator != null)
            animator.SetBool("correr", movimiento != 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento * movimientoSpeed, rb.linearVelocity.y);
    }

    private void Girar(int direccion)
    {
        Vector3 escala = transform.localScale;
        escala.x = direccion * Mathf.Abs(escala.x);
        transform.localScale = escala;
    }
}