using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float movimientoSpeed = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;

    [Header("Detectar Suelo")]
    public DSuelo Dsuelo;

    private bool estabaEnSuelo;
    private float alturaInicioCaida;
    private bool ignorarCaida = false;

    // Sonidos
    private float pasoCooldown = 0f;
    public float pasoIntervalo = 0.4f;  // Tiempo entre pasos

    private Rigidbody2D rb;
    private Animator animator;
    private float movimiento;
    private ServiciosJugador servicios;
    private int saltosRestantes = 2;


    public float alturaMortal = 8f;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        servicios = GetComponent<ServiciosJugador>();

    }

    void Update()
    {
        bool enSuelo = Dsuelo.tocandoSuelo;

        // Comenzó a caer
        if (estabaEnSuelo && !enSuelo && !ignorarCaida)
        {
            alturaInicioCaida = transform.position.y;
        }

        // Aterrizó
        if (!estabaEnSuelo && enSuelo && !ignorarCaida)
        {
            float distanciaCaida = alturaInicioCaida - transform.position.y;

            if (distanciaCaida >= alturaMortal)
            {
                ignorarCaida = true;
                servicios.Muerte();
                return;
            }
        }


        estabaEnSuelo = enSuelo;

        if (enSuelo)
        {
            animator.SetBool("salto", false);
            animator.SetBool("caida", false);
        }
        else
        {
            animator.SetBool("salto", rb.linearVelocity.y > 0.1f);
            animator.SetBool("caida", rb.linearVelocity.y < -0.1f);
        }
        if (enSuelo && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.saltar);
        }

        // Movimiento
        if (Keyboard.current.aKey.isPressed)
        {
            movimiento = -1f;
            Girar(-1);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            movimiento = 1f;
            Girar(1);
        }
        else
        {
            movimiento = 0f;
        }

        // Animaciones
        animator.SetBool("correr", enSuelo && movimiento != 0);

        if (!enSuelo)
        {
            if (rb.linearVelocity.y > 0.1f)
            {
                animator.SetBool("salto", true);
                animator.SetBool("caida", false);
            }
            else if (rb.linearVelocity.y < -0.1f)
            {
                animator.SetBool("salto", false);
                animator.SetBool("caida", true);
            }
        }
        else
        {
            animator.SetBool("salto", false);
            animator.SetBool("caida", false);
        }

        // Sonido pasos
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