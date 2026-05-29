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

    public bool panel = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        servicios = GetComponent<ServiciosJugador>();

        servicios.PuntoInicio();
    }

    void Update()
    {
        if (panel)
        {
            return;
        }

        if (Dsuelo.tocandoSuelo)
        {
            // Eliminar micro rebotes
            if (Mathf.Abs(rb.linearVelocity.y) < 0.1f)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    0
                );
            }

            // SALTO
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    fuerzaSalto
                );
            }
        }

        // IZQUIERDA
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            movimiento = -1f;

            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;
        }

        // DERECHA
        else if (Keyboard.current.dKey.isPressed ||
                 Keyboard.current.rightArrowKey.isPressed)
        {
            movimiento = 1f;

            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;
        }
           else
            {
                movimiento = 0f;

                // detener horizontal
                rb.linearVelocity = new Vector2(
                    0,
                    rb.linearVelocity.y
                );

                // eliminar micro rebotes
                if (Dsuelo.tocandoSuelo &&
                    Mathf.Abs(rb.linearVelocity.y) < 0.1f)
                {
                    rb.linearVelocity = new Vector2(
                        0,
                        0
                    );
                }
            }
        

        if (animator != null)
        {
            animator.SetBool("correr", movimiento != 0);
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento * movimientoSpeed,rb.linearVelocity.y);


  
    }
}