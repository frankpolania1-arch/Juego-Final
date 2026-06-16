using UnityEngine;

public class CargaCircularBonita : MonoBehaviour
{
    public Transform[] objetos;

    public float radio = 1.5f;
    public float velocidadRotacion = 120f;
    public float velocidadEscala = 4f;
    public float escalaMin = 0.7f;
    public float escalaMax = 1.2f;

    void Update()
    {
        float tiempo = Time.time;

        for (int i = 0; i < objetos.Length; i++)
        {
            float angulo = tiempo * velocidadRotacion + (360f / objetos.Length) * i;

            float x = Mathf.Cos(angulo * Mathf.Deg2Rad) * radio;
            float y = Mathf.Sin(angulo * Mathf.Deg2Rad) * radio;

            objetos[i].position = transform.position + new Vector3(x, y, 0);

            float escala = Mathf.Lerp(
                escalaMin,
                escalaMax,
                (Mathf.Sin(tiempo * velocidadEscala + i) + 1f) / 2f
            );

            objetos[i].localScale = Vector3.one * escala;
        }
    }
}