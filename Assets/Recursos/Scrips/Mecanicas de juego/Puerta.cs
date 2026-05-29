using System;
using System.Threading.Tasks;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public int puntos = 0;
    Animator anim;
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
            anim.SetBool("abrir", true);
            await Task.Delay(5000);
            gameObject.SetActive(false);
        }
        
    }
}
