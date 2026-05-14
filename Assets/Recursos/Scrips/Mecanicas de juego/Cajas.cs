using System.Threading.Tasks;
using UnityEngine;

public class Cajas : MonoBehaviour
{
    BoxCollider2D Bx;
    Animator anim;
    Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        Bx = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dtecho"))
        {
            System.Random ale = new System.Random();
            int random = ale.Next(0, 11);
         
            anim.SetBool("golpe", true);
        
            await Task.Delay(500);
            if (random == 2 || random == 4 || random == 6 || random == 8 || random == 10)
            {
                anim.SetBool("cambio", true);
                gameObject.transform.localScale = new Vector3(3f, 3f, 1);

                Bx.isTrigger = true;
            }
            else gameObject.SetActive(false);

        }   
    }
}
