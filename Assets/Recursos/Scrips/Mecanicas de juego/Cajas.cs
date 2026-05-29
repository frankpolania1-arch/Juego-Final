using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cajas : MonoBehaviour
{
    [Header("Fondo")]
    public Canvas fondo;

    [Header("Jugador")]
    public GameObject jugador;

    [Header("Puerta")]
    public GameObject puerta;

    [Header("Animator Puerta")]
    public Animator animP;

    // NUEVO
    private Puerta scriptPuerta;

    [System.Serializable]
    public class Pregunta
    {
        public string titulo;

        [TextArea]
        public string pregunta;

        public string correcta;
        public string incorrecta1;
        public string incorrecta2;
    }

    public Dictionary<int, Pregunta> preguntas =
        new Dictionary<int, Pregunta>();

    BoxCollider2D Bx;
    Animator anim;
    Rigidbody2D rb;

    public byte Mensaje;

    [Header("UI Compartida")]
    public Button btnA;
    public Button btnB;
    public Button btnC;

    public TextMeshProUGUI txtA;
    public TextMeshProUGUI txtB;
    public TextMeshProUGUI txtC;

    private string respuestaCorrecta;

    public TextMeshProUGUI mensajeTXT;

    private bool ocupado = false;

    private static Canvas cachedFondo;
    private static GameObject cachedJugador;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Bx = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (cachedFondo == null)
        {
            GameObject fObj = GameObject.Find("CanvasFondo");

            if (fObj != null)
            {
                cachedFondo = fObj.GetComponent<Canvas>();
            }
        }

        fondo = cachedFondo;

        if (cachedJugador == null)
        {
            cachedJugador = GameObject.Find("Jugador");
        }

        jugador = cachedJugador;

        if (fondo != null)
        {
            fondo.gameObject.SetActive(false);
        }
        if (puerta != null)
        {
            animP = puerta.GetComponent<Animator>();

            scriptPuerta = puerta.GetComponent<Puerta>();

            if (scriptPuerta == null)
            {
                Debug.LogError(
                    "El objeto puerta NO tiene el script Puerta"
                );
            }
        }
        else
        {
            Debug.LogError(
                "No asignaste la puerta en el inspector"
            );
        }

        AgregarPreguntas();
        if (btnA != null) btnA.gameObject.SetActive(false);
        if (btnB != null) btnB.gameObject.SetActive(false);
        if (btnC != null) btnC.gameObject.SetActive(false);

        if (mensajeTXT != null)
        {
            mensajeTXT.gameObject.SetActive(false);
        }
    }

    public void RespuestaCorrecta()
    {
        anim.SetBool("cambio", true);

        if (btnA != null) btnA.gameObject.SetActive(false);
        if (btnB != null) btnB.gameObject.SetActive(false);
        if (btnC != null) btnC.gameObject.SetActive(false);

        if (mensajeTXT != null)
        {
            mensajeTXT.gameObject.SetActive(false);
        }
    }

    public async Task CambioInicio()
    {
        anim.SetBool("cambio", false);

        await Task.Delay(10000);

        anim.SetBool("Regreso", true);

        gameObject.SetActive(true);
    }
    public void ValidarRespuesta(string respuesta)
    {
        if (respuesta == respuestaCorrecta)
        {
            mensajeTXT.text = "¡Correcto!";

            if (fondo != null)
            {
                fondo.gameObject.SetActive(false);
            }

            anim.SetBool("cambio", true);

            if (scriptPuerta != null)
            {
                scriptPuerta.puntos++;

                Debug.Log(
                    "Puntos puerta: " +
                    scriptPuerta.puntos
                );

                if (scriptPuerta.puntos >= 5)
                {
                    if (animP != null)
                    {
                        animP.SetBool("abrir", true);
                    }

                    Debug.Log("PUERTA ABIERTA");
                }
            }
        }
        else
        {
            mensajeTXT.text = "Incorrecto";

            gameObject.SetActive(false);

            _ = CambioInicio();
        }

        mensajeTXT.gameObject.SetActive(false);

        btnA.gameObject.SetActive(false);
        btnB.gameObject.SetActive(false);
        btnC.gameObject.SetActive(false);

        if (fondo != null)
        {
            fondo.gameObject.SetActive(false);
        }
    }


    public void MostrarPregunta()
    {
        Pregunta p = preguntas[Mensaje];

        mensajeTXT.text = p.pregunta;

        List<string> opciones = new List<string>()
        {
            p.correcta,
            p.incorrecta1,
            p.incorrecta2
        };


        for (int i = 0; i < opciones.Count; i++)
        {
            string temp = opciones[i];

            int random = Random.Range(i, opciones.Count);

            opciones[i] = opciones[random];
            opciones[random] = temp;
        }

        txtA.text = opciones[0];
        txtB.text = opciones[1];
        txtC.text = opciones[2];

        respuestaCorrecta = p.correcta;

        btnA.onClick.RemoveAllListeners();
        btnB.onClick.RemoveAllListeners();
        btnC.onClick.RemoveAllListeners();


        btnA.onClick.AddListener(() =>
            ValidarRespuesta(txtA.text));

        btnB.onClick.AddListener(() =>
            ValidarRespuesta(txtB.text));

        btnC.onClick.AddListener(() =>
            ValidarRespuesta(txtC.text));
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (ocupado) return;

        if (anim.GetBool("cambio")) return;

        Mensaje = (byte)Random.Range(0, 20);

        if (collision.CompareTag("Dtecho"))
        {
            MostrarPregunta();

            if (fondo != null)
            {
                fondo.gameObject.SetActive(true);
            }

            mensajeTXT.gameObject.SetActive(true);

            btnA.gameObject.SetActive(true);
            btnB.gameObject.SetActive(true);
            btnC.gameObject.SetActive(true);

            ocupado = true;

            anim.SetBool("golpe", true);

            await Task.Delay(2000);

            ocupado = false;
        }
    }

    public void AgregarPreguntas()
    {
        preguntas.Add(0, new Pregunta() { titulo = "Carrera", pregunta = "Vas en segundo lugar en una carrera. Adelantas al que va de segundo. ¿En qué posición quedas?", correcta = "Segundo lugar", incorrecta1 = "Primer lugar", incorrecta2 = "Tercer lugar" });
        preguntas.Add(1, new Pregunta() { titulo = "Avión", pregunta = "Un avión cae justo en la frontera entre Colombia y Ecuador. ¿Dónde entierran a los sobrevivientes?", correcta = "No se entierran", incorrecta1 = "En Colombia", incorrecta2 = "En Ecuador" });
        preguntas.Add(2, new Pregunta() { titulo = "Meses", pregunta = "¿Cuántos meses tienen 28 días?", correcta = "Todos", incorrecta1 = "Uno", incorrecta2 = "Doce" });
        preguntas.Add(3, new Pregunta() { titulo = "Cuarto oscuro", pregunta = "Entras a un cuarto oscuro con una vela, una lámpara y una estufa. Solo tienes un fósforo. ¿Qué enciendes primero?", correcta = "El fósforo", incorrecta1 = "La vela", incorrecta2 = "La lámpara" });
        preguntas.Add(4, new Pregunta() { titulo = "Agujero", pregunta = "Si haces un agujero en una camiseta, ¿cuántos agujeros tienes?", correcta = "Dos", incorrecta1 = "Uno", incorrecta2 = "Cuatro" });
        preguntas.Add(5, new Pregunta() { titulo = "Silencio", pregunta = "Cuando más me nombras, menos existo. ¿Qué soy?", correcta = "El silencio", incorrecta1 = "El eco", incorrecta2 = "El viento" });
        preguntas.Add(6, new Pregunta() { titulo = "Médico", pregunta = "Un médico dice: 'No puedo operarlo, es mi hijo'. ¿Cómo es posible?", correcta = "Es su madre", incorrecta1 = "Es adoptado", incorrecta2 = "Está mintiendo" });
        preguntas.Add(7, new Pregunta() { titulo = "Conductor", pregunta = "Un hombre pasa semáforos en rojo y va en sentido contrario, pero no lo arrestan. ¿Por qué?", correcta = "Iba caminando", incorrecta1 = "Era policía", incorrecta2 = "No había tráfico" });
        preguntas.Add(8, new Pregunta() { titulo = "Padre", pregunta = "El padre de Ana tiene 5 hijas: Nana, Nene, Nini, Nono y... ¿cómo se llama la quinta?", correcta = "Ana", incorrecta1 = "Nunu", incorrecta2 = "Nina" });
        preguntas.Add(9, new Pregunta() { titulo = "Llave", pregunta = "¿Qué tiene muchas llaves pero no puede abrir ninguna puerta?", correcta = "Un piano", incorrecta1 = "Un mapa", incorrecta2 = "Un carro" });
        preguntas.Add(10, new Pregunta() { titulo = "Seco", pregunta = "¿Qué se moja mientras seca?", correcta = "La toalla", incorrecta1 = "El agua", incorrecta2 = "La ropa" });
        preguntas.Add(11, new Pregunta() { titulo = "Sube y baja", pregunta = "¿Qué sube pero nunca baja?", correcta = "La edad", incorrecta1 = "El ascensor", incorrecta2 = "La temperatura" });
        preguntas.Add(12, new Pregunta() { titulo = "Manos", pregunta = "¿Qué tiene manos pero no puede aplaudir?", correcta = "Un reloj", incorrecta1 = "Un robot", incorrecta2 = "Una estatua" });
        preguntas.Add(13, new Pregunta() { titulo = "Cabeza", pregunta = "¿Qué tiene cabeza y cola pero no cuerpo?", correcta = "Una moneda", incorrecta1 = "Una serpiente", incorrecta2 = "Un pez" });
        preguntas.Add(14, new Pregunta() { titulo = "Dormir", pregunta = "¿Qué puedes atrapar pero no lanzar?", correcta = "Un resfriado", incorrecta1 = "Una pelota", incorrecta2 = "Una piedra" });
        preguntas.Add(15, new Pregunta() { titulo = "Espejo", pregunta = "¿Qué siempre está frente a ti pero no puedes ver?", correcta = "El futuro", incorrecta1 = "Tu nariz", incorrecta2 = "La espalda" });
        preguntas.Add(16, new Pregunta() { titulo = "Pesado", pregunta = "¿Qué pesa más: un kilo de hierro o un kilo de algodón?", correcta = "Pesan igual", incorrecta1 = "El hierro", incorrecta2 = "El algodón" });
        preguntas.Add(17, new Pregunta() { titulo = "Letra", pregunta = "Qué palabra está mal escrita en el diccionario?", correcta = "Mal escrita", incorrecta1 = "Diccionario", incorrecta2 = "Palabra" });
        preguntas.Add(18, new Pregunta() { titulo = "Familia", pregunta = "Dos padres y dos hijos van en un carro, pero solo hay tres personas. ¿Cómo es posible?", correcta = "Abuelo, padre e hijo", incorrecta1 = "Uno iba escondido", incorrecta2 = "Uno no era familia" });
        preguntas.Add(19, new Pregunta() { titulo = "Respirar", pregunta = "¿Qué puedes romper sin tocarlo?", correcta = "Una promesa", incorrecta1 = "Un vidrio", incorrecta2 = "Una cuerda" });
    }
}