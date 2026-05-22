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

    private Rigidbody2D rb;
    private Animator animator;

    private float movimiento;

    ServiciosJugador servicios;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        servicios = GetComponent<ServiciosJugador>();
        servicios.PuntoInicio();
    }

    void Update()
    {
        movimiento = 0f;

        // IZQUIERDA
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            movimiento = -1f;

            Vector3 escala = transform.localScale;escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;
        }

        // DERECHA
        if (Keyboard.current.dKey.isPressed ||Keyboard.current.rightArrowKey.isPressed)
        {
            movimiento = 1f;

            Vector3 escala = transform.localScale;escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;
        }

        if (animator != null)
        {
            animator.SetBool("correr", movimiento != 0);
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame && Dsuelo.tocandoSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);  
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento * movimientoSpeed, rb.linearVelocity.y);

    }
}


