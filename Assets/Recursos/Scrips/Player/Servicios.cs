using System.Threading.Tasks;
using UnityEngine;

public class Servicios : MonoBehaviour
{
    float inicioX;
    float inicioY;
    byte vidas;

    [Header("Animator")]
    public Animator animator;

    void Awake()
    {
        vidas = 3;
        animator = GetComponent<Animator>();
    }

    public void PuntoInicio()
    {
        inicioX = transform.position.x;
        inicioY = transform.position.y;
    }

    public async void Muerte()
    {
        if (vidas > 0)
        {
            animator.SetBool("Muerte", true);



            await Task.Delay(500);

            animator.SetBool("Muerte", false);

            transform.position = new Vector2(inicioX, inicioY);

            vidas--;
        }
        else
        {
            Debug.Log("Muerte Fin");
        }
    }
}