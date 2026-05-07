using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    float movimiento;
    public float movimientoSpeed = 5f;
    public float fuerzaSalto = 5f;
    Rigidbody2D rb;

    BoxCollider2D suelo;
    public SpriteRenderer render;
    public Animator animator;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        suelo = GameObject.Find("Dsuelo").GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();    
    }


    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("correr", true);
        }

        // SALTO
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Dsuelo.tocandoSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // MOVER DERECHA
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            animator.SetBool("correr", true);
            movimiento = 1f;
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            transform.localScale = escala;

      
        }

        // MOVER IZQUIERDA
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            if (animator != null)
            {
                animator.SetBool("correr", movimiento != 0);
            }
            else
            {
                return;
            }
            animator.SetBool("correr", true);
            movimiento = -1f;

            Vector3 escala = transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            transform.localScale = escala;

     
        }
        else
        {
            if (animator == null) return;
            animator.SetBool("correr", false);
            movimiento = 0f;
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento * movimientoSpeed, rb.linearVelocity.y);
    }
}