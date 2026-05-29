using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BTNfrases: MonoBehaviour
{
    public TMP_InputField respuesta;

    public Button BTNrespuesta;

    public TextMeshProUGUI mensaje;

    private string respuestaCorrecta;

    private Cajas Actual;

    void Start()
    {
        BTNrespuesta.onClick.AddListener(
            VerificarRespuesta
        );

        gameObject.SetActive(false);

    }

    public void MostrarPregunta(
        string frase,
        string respuesta,
        Cajas actual)
    {
        gameObject.SetActive(true);

        mensaje.text = frase;

        respuestaCorrecta = respuesta;

        Actual = actual;

        this.respuesta.text = "";

        this.respuesta.ActivateInputField();
    }

    public void VerificarRespuesta()
    {
        string textoJugador =
            respuesta.text.Trim().ToLower();

        if (textoJugador ==
            respuestaCorrecta.ToLower())
        {
            mensaje.text = "Correcto";

        }
        else
        {
            mensaje.text =
                "Incorrecto";
        }
    }
}