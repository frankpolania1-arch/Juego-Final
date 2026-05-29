using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ServiciosJugador : MonoBehaviour
{
    float inicioX;
    float inicioY;
    byte vidas;

    private bool recibiendoDaño = false;

    [Header("Animator")]
    public Animator animator;

    [Header("Texto")]
    public TextMeshProUGUI variables;

    void Awake()
    {
        vidas = 3;

        if (animator == null)
            animator = GetComponent<Animator>();

        variables.text = "Vidas: " + vidas;
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
        }
        else
        {
            Debug.Log("GAME OVER");
        }

        // Invulnerabilidad temporal
        await Task.Delay(1500);

        recibiendoDaño = false;
    }
}