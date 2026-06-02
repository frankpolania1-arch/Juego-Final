using System.Threading.Tasks;
using UnityEngine;

public class SensorCheck : MonoBehaviour
{
    public GameManager gameManager;
    public Animator anim;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.OnCheckTriggered(other);
            anim.SetBool("check", true);
        }
    }
    private async void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            await Task.Delay(500);
            anim.SetBool("check", false);
        }
    }
}