using System;
using System.Threading.Tasks;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public int puntos = 0;
    Animator anim;
    float inicioX;
    float inicioY;
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
    }

    async void Update()
    {
        if (this == null) return;
        
        if (puntos >= 5)
        {
            gameObject.SetActive(false);
            anim.SetBool("abrir", true);
            await Task.Delay(5000);
            Cajas[] todasLasCajas = FindObjectsByType<Cajas>(FindObjectsSortMode.None);
            foreach (Cajas caja in todasLasCajas)
            {
                caja.anim.SetBool("cambio", true);
                caja.gameObject.SetActive(false);
            }
            return;
        }
    }
}
