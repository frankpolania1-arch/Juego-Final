using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float movimientoSpeed = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;

    [Header("Referencias")]
    public Dsuelo Dsuelo;

    private Rigidbody2D rb;
    private Animator animator;

    private float movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movimiento = 0f;

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
        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            movimiento = 1f;

            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;
        }

        // ANIMACIÓN
        if (animator != null)
        {
            animator.SetBool("correr", movimiento != 0);
        }

        // SALTO
        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            Dsuelo.tocandoSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(
                Vector2.up * fuerzaSalto,
                ForceMode2D.Impulse
            );
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimiento * movimientoSpeed,
            rb.linearVelocity.y
        );
    }
}


