using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ServiciosJugador : MonoBehaviour
{
    float inicioX;
    float inicioY;
    byte vidas;

    [Header("Animator")]
    public Animator animator;

    public TextMeshProUGUI variables;

    private void Start()
    {
        variables.text = "Vidas " + vidas;
    }
    void Awake()
    {
        vidas = 3;
        variables = FindAnyObjectByType<TextMeshProUGUI>();
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
            variables.text = "Vidas " + vidas;
        }
        else
        {
            Debug.Log("Muerte Fin");
        }
    }
}