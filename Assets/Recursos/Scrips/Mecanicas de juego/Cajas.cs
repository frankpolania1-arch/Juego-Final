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

    [Header("Jugador")]
    public ServiciosJugador sjugador;

    [Header("Puerta")]
    public GameObject puerta;

    [Header("Animator Puerta")]
    public Animator animP;

    [Header("UI Compartida")]
    public Button btnA;
    public Button btnB;
    public Button btnC;

    public TextMeshProUGUI txtA;
    public TextMeshProUGUI txtB;
    public TextMeshProUGUI txtC;

    public Dictionary<int, Pregunta> preguntas = new Dictionary<int, Pregunta>();

    BoxCollider2D Bx;
    public Animator anim;
    Rigidbody2D rb;

    public byte Mensaje;

    private Puerta scriptPuerta;
    private string respuestaCorrecta;
    public TextMeshProUGUI mensajeTXT;
    public TextMeshProUGUI variables;

    private bool ocupado = false;

    private static Canvas cachedFondo;
    private static GameObject cachedJugador;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Bx = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();


        if (cachedFondo == null)
        {
            GameObject fObj = GameObject.Find("CanvasFondo");
            if (fObj != null)
                cachedFondo = fObj.GetComponent<Canvas>();
        }
        fondo = cachedFondo;

        if (cachedJugador == null)
            cachedJugador = GameObject.Find("Jugador");
        jugador = cachedJugador;

        if (fondo != null)
            fondo.gameObject.SetActive(false);

        if (puerta != null)
        {
            animP = puerta.GetComponent<Animator>();
            scriptPuerta = puerta.GetComponent<Puerta>();
            if (scriptPuerta == null)
                Debug.LogError("El objeto puerta NO tiene el script Puerta");
        }
        else
        {
            Debug.LogError("No asignaste la puerta en el inspector");
        }

        AgregarPreguntas();

        if (btnA != null) btnA.gameObject.SetActive(false);
        if (btnB != null) btnB.gameObject.SetActive(false);
        if (btnC != null) btnC.gameObject.SetActive(false);

        if (mensajeTXT != null)
            mensajeTXT.gameObject.SetActive(false);
    }

    public void RespuestaCorrecta()
    {
        anim.SetBool("cambio", true);
        

        if (btnA != null) btnA.gameObject.SetActive(false);
        if (btnB != null) btnB.gameObject.SetActive(false);
        if (btnC != null) btnC.gameObject.SetActive(false);

        if (mensajeTXT != null)
            mensajeTXT.gameObject.SetActive(false);

        jugador.GetComponent<ServiciosJugador>().variables.gameObject.SetActive(true);
        Time.timeScale = 1f;
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

            //  Sonido de respuesta correcta (moneda)
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.respuestaCorrecta);

            if (fondo != null)
                fondo.gameObject.SetActive(false);

            anim.SetBool("cambio", true);

            if (scriptPuerta != null)
            {
                scriptPuerta.puntos++;
                Debug.Log("Puntos puerta: " + scriptPuerta.puntos);

                if (scriptPuerta.puntos >= 5)
                {
                    if (animP != null)
                        animP.SetBool("abrir", true);

                    Cajas[] todasLasCajas = FindObjectsByType<Cajas>(FindObjectsSortMode.None);
                    foreach (Cajas caja in todasLasCajas)
                    {
                        caja.anim.SetBool("cambio", true);
                        caja.gameObject.SetActive(false);
                    }
                    Debug.Log("PUERTA ABIERTA");
                }
            }
        }
        else
        {
            mensajeTXT.text = "Incorrecto";

            //  Sonido de respuesta incorrecta
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.respuestaIncorrecta);

            gameObject.SetActive(false);
            _ = CambioInicio();
        }

        sjugador.variables.gameObject.SetActive(true);
        sjugador.TXTpuntos.gameObject.SetActive(true);
        sjugador.corazones.gameObject.SetActive(true);

        mensajeTXT.gameObject.SetActive(false);
        btnA.gameObject.SetActive(false);
        btnB.gameObject.SetActive(false);
        btnC.gameObject.SetActive(false);

        if (fondo != null)
            fondo.gameObject.SetActive(false);

        variables.gameObject.SetActive(true);
        Time.timeScale = 1f;
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

        btnA.onClick.AddListener(() => ValidarRespuesta(txtA.text));
        btnB.onClick.AddListener(() => ValidarRespuesta(txtB.text));
        btnC.onClick.AddListener(() => ValidarRespuesta(txtC.text));
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (ocupado) return;
        if (anim.GetBool("cambio")) return;

        Mensaje = (byte)Random.Range(0, 70); // 0 a 69

        if (collision.CompareTag("Dtecho"))
        {
            //  Sonido al tocar la caja (activar pregunta)
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.tocarCaja);

            Time.timeScale = 0f;
            variables.gameObject.SetActive(false);

            MostrarPregunta();

            if (fondo != null)
                fondo.gameObject.SetActive(true);

            sjugador.variables.gameObject.SetActive(false);
            sjugador.TXTpuntos.gameObject.SetActive(false);
            sjugador.corazones.gameObject.SetActive(false);

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
        // === COMPRENSIÓN LECTORA (0 - 34) ===
        preguntas.Add(0, new Pregunta() { titulo = "Lectura 1", pregunta = "Carlos tiene un gato negro y un perro marrón. El gato duerme en el sofá y el perro en la alfombra. ¿Dónde duerme el animal negro?", correcta = "En el sofá", incorrecta1 = "Incrustado en la alfombra", incorrecta2 = "En la cama" });
        preguntas.Add(1, new Pregunta() { titulo = "Lectura 2", pregunta = "María fue a la tienda a comprar manzanas y peras, pero al final solo compró fresas porque estaban en oferta. ¿Qué fruta compró María?", correcta = "Fresas", incorrecta1 = "Manzanas", incorrecta2 = "Peras" });
        preguntas.Add(2, new Pregunta() { titulo = "Inferencia", pregunta = "El cielo se oscureció de repente, sopló un viento fuerte y la gente empezó a abrir sus paraguas. ¿Qué está a punto de pasar?", correcta = "Va a llover", incorrecta1 = "Va a amanecer", incorrecta2 = "Va a caer nieve" });
        preguntas.Add(3, new Pregunta() { titulo = "Lectura 3", pregunta = "El tren hacia Bogotá sale todas las mañanas a las 8:00 AM, excepto los domingos que sale a las 10:00 AM. Si hoy es sábado, ¿a qué hora sale?", correcta = "8:00 AM", incorrecta1 = "10:00 AM", incorrecta2 = "9:00 AM" });
        preguntas.Add(4, new Pregunta() { titulo = "Lectura 4", pregunta = "Juan es más alto que Pedro, pero más bajo que Luis. ¿Quién es el más alto de los tres?", correcta = "Luis", incorrecta1 = "Juan", incorrecta2 = "Pedro" });
        preguntas.Add(5, new Pregunta() { titulo = "Sinónimos", pregunta = "En el texto: 'El científico caminaba con un paso cauteloso por el laboratorio'. ¿Qué significa la palabra 'cauteloso'?", correcta = "Cuidadoso", incorrecta1 = "Rápido", incorrecta2 = "Distraído" });
        preguntas.Add(6, new Pregunta() { titulo = "Lectura 5", pregunta = "Sofía olvidó su bufanda en la biblioteca y sus guantes en el salón de clases. Regresó a casa solo con su mochila y su abrigo. ¿Qué olvidó en el salón?", correcta = "Los guantes", incorrecta1 = "La bufanda", incorrecta2 = "La mochila" });
        preguntas.Add(7, new Pregunta() { titulo = "Lectura 6", pregunta = "A pesar de que el letrero decía 'No pasar, pintura fresca', Mateo no lo vio y se apoyó en la pared de madera. ¿Qué le pasó a Mateo?", correcta = "Se manchó la ropa", incorrecta1 = "Se cayó al suelo", incorrecta2 = "Se golpeó la cabeza" });
        preguntas.Add(8, new Pregunta() { titulo = "Causa y Efecto", pregunta = "Como Lucía no estudió para el examen de historia porque se quedó dormida, obtuvo una calificación muy baja. ¿Por qué reprobó Lucía?", correcta = "Porque se quedó dormida y no estudió", incorrecta1 = "Porque el examen estaba en otro idioma", incorrecta2 = "Porque el profesor no asistió" });
        preguntas.Add(9, new Pregunta() { titulo = "Lectura 7", pregunta = "El automóvil rojo cruzó la meta justo después del azul, y el verde llegó un minuto más tarde. ¿Qué automóvil ganó la carrera?", correcta = "El azul", incorrecta1 = "El rojo", incorrecta2 = "El verde" });
        preguntas.Add(10, new Pregunta() { titulo = "Inferencia 2", pregunta = "Llegamos al restaurante y vimos las sillas sobre las mesas, las luces apagadas y la puerta cerrada con candado. ¿Qué podemos deducir?", correcta = "El restaurante está cerrado", incorrecta1 = "Están cocinando", incorrecta2 = "Hay una fiesta adentro" });
        preguntas.Add(11, new Pregunta() { titulo = "Lectura 8", pregunta = "Ana sembró girasoles en su jardín norte y rosas en su jardín sur. En el centro puso un árbol de limones. ¿Qué sembró en el jardín sur?", correcta = "Rosas", incorrecta1 = "Girasoles", incorrecta2 = "Limones" });
        preguntas.Add(12, new Pregunta() { titulo = "Lectura 9", pregunta = "El museo abre de martes a viernes. Los fines de semana permanece cerrado por mantenimiento. Si hoy es sábado, ¿puedes entrar?", correcta = "No, está cerrado", incorrecta1 = "Sí, está abierto", incorrecta2 = "Solo en la tarde" });
        preguntas.Add(13, new Pregunta() { titulo = "Opuestos", pregunta = "En la frase: 'El agua del estanque estaba completamente turbia'. ¿Cuál es el opuesto de la palabra 'turbia'?", correcta = "Clara", incorrecta1 = "Sucia", incorrecta2 = "Fría" });
        preguntas.Add(14, new Pregunta() { titulo = "Lectura 10", pregunta = "Tomás prefiere leer novelas de ciencia ficción, mientras que a su hermano menor le encantan los cómics de superhéroes. ¿Qué le gusta leer al hermano menor?", correcta = "Cómics de superhéroes", incorrecta1 = "Novelas de ciencia ficción", incorrecta2 = "Libros de poesía" });
        preguntas.Add(15, new Pregunta() { titulo = "Lectura 11", pregunta = "El panadero hornea el pan integral a las 5:00 AM y las galletas dulces a las 6:30 AM. Si visitas la panadería a las 6:00 AM, ¿qué producto ya está horneado?", correcta = "El pan integral", incorrecta1 = "Las galletas dulces", incorrecta2 = "Ninguno" });
        preguntas.Add(16, new Pregunta() { titulo = "Lectura 12", pregunta = "Laura vive en el tercer piso de un edificio. Diego vive dos pisos más arriba que Laura. ¿In qué piso vive Diego?", correcta = "En el quinto piso", incorrecta1 = "En el segundo piso", incorrecta2 = "En el cuarto piso" });
        preguntas.Add(17, new Pregunta() { titulo = "Lectura 13", pregunta = "Para preparar el pastel se necesita harina, azúcar y huevos. Si ya tengo el azúcar y la harina en el tazón, ¿qué ingrediente me falta agregar?", correcta = "Los huevos", incorrecta1 = "La leche", incorrecta2 = "La mantequilla" });
        preguntas.Add(18, new Pregunta() { titulo = "Lectura 14", pregunta = "El cartero dejó tres cartas en la caja de madera de la entrada y un paquete grande en el porche de la casa. ¿Dónde está el paquete?", correcta = "En el porche", incorrecta1 = "En la caja de madera", incorrecta2 = "En el jardín" });
        preguntas.Add(19, new Pregunta() { titulo = "Lectura 15", pregunta = "A Lucas no le gusta el frío, por eso decidió pasar sus vacaciones en la playa en lugar de ir a las montañas nevadas. ¿A dónde fue Lucas?", correcta = "A la playa", incorrecta1 = "A las montañas", incorrecta2 = "Se quedó en casa" });
        preguntas.Add(20, new Pregunta() { titulo = "Lectura 16", pregunta = "Aunque el perro ladraba mucho hacia la ventana, el gato seguía durmiendo plácidamente junto a la chimenea sin inmutarse. ¿Qué hacía el gato?", correcta = "Dormía junto a la chimenea", incorrecta1 = "Ladraba en la ventana", incorrecta2 = "Buscaba comida" });
        preguntas.Add(21, new Pregunta() { titulo = "Lectura 17", pregunta = "El equipo azul anotó tres goles en el primer tiempo y ninguno en el segundo. El equipo blanco anotó dos goles al final. ¿Quién ganó?", correcta = "El equipo azul", incorrecta1 = "El equipo blanco", incorrecta2 = "Quedaron empatados" });
        preguntas.Add(22, new Pregunta() { titulo = "Inferencia 3", pregunta = "David se despertó con dolor de garganta, fiebre alta y mucha tos. Su madre llamó por teléfono para cancelar su asistencia a la escuela. ¿A dónde irán?", correcta = "Al médico", incorrecta1 = "Al parque de diversiones", incorrecta2 = "A un restaurante" });
        preguntas.Add(23, new Pregunta() { titulo = "Lectura 18", pregunta = "La película de acción comenzaba a las 7:00 PM, pero los comerciales duraron 15 minutos antes del inicio real. ¿A qué hora empezó la película?", correcta = "7:15 PM", incorrecta1 = "7:00 PM", incorrecta2 = "6:45 PM" });
        preguntas.Add(24, new Pregunta() { titulo = "Lectura 19", pregunta = "Camila guardó sus lápices de colores en la cartuchera metálica y los cuadernos en el cajón de su escritorio. ¿Dónde guardó los cuadernos?", correcta = "En el cajón del escritorio", incorrecta1 = "En la cartuchera metálica", incorrecta2 = "En su mochila" });
        preguntas.Add(25, new Pregunta() { titulo = "Lectura 20", pregunta = "El carpintero usó madera de pino para hacer la mesa grande y madera de roble para las cuatro sillas pequeñas. ¿De qué material son las sillas?", correcta = "Madera de roble", incorrecta1 = "Madera de pino", incorrecta2 = "Plástico rígido" });
        preguntas.Add(26, new Pregunta() { titulo = "Comprensión 1", pregunta = "Si un texto dice: 'La paciencia es la clave para resolver acertijos complejos'. ¿Qué se necesita según el autor para resolverlos?", correcta = "Paciencia", incorrecta1 = "Velocidad", incorrecta2 = "Herramientas" });
        preguntas.Add(27, new Pregunta() { titulo = "Lectura 21", pregunta = "Elena compró un libro ayer. Leyó la mitad por la tarde y la otra mitad antes de dormir. ¿Cuánto del libro le falta por leer hoy?", correcta = "Nada, ya lo terminó", incorrecta1 = "La mitad", incorrecta2 = "Todo el libro" });
        preguntas.Add(28, new Pregunta() { titulo = "Lectura 22", pregunta = "El semáforo peatonal cambió a verde, por lo que los niños cruzaron la calle por la zona peatonal de manera segura. ¿De qué color estaba el semáforo?", correcta = "Verde", incorrecta1 = "Rojo", incorrecta2 = "Amarillo" });
        preguntas.Add(29, new Pregunta() { titulo = "Lectura 23", pregunta = "Mateo sembró una semilla que tardó 3 semanas en germinar y una semana más en dar su primera hoja. ¿Cuántas semanas pasaron para ver la hoja?", correcta = "4 semanas", incorrecta1 = "3 semanas", incorrecta2 = "1 semana" });
        preguntas.Add(30, new Pregunta() { titulo = "Comprensión 2", pregunta = "En la oración: 'El rugido del motor asustó a las aves que descansaban en los árboles'. ¿Qué causó el susto de las aves?", correcta = "El rugido del motor", incorrecta1 = "La caída de un árbol", incorrecta2 = "La presencia de un cazador" });
        preguntas.Add(31, new Pregunta() { titulo = "Lectura 24", pregunta = "Sonia viaja en autobús al trabajo de lunes a viernes, y los sábados prefiere usar su bicicleta. ¿Cómo viaja Sonia al trabajo los días martes?", correcta = "En autobús", incorrecta1 = "En bicicleta", incorrecta2 = "Caminando" });
        preguntas.Add(32, new Pregunta() { titulo = "Lectura 25", pregunta = "El granjero recolectó huevos de las gallinas por la mañana y manzanas del huerto por la tarde. ¿Qué hizo el granjero por la tarde?", correcta = "Recolectó manzanas", incorrecta1 = "Recolectó huevos", incorrecta2 = "Alimentó al ganado" });
        preguntas.Add(33, new Pregunta() { titulo = "Inferencia 4", pregunta = "Sonó la campana, todos los estudiantes guardaron sus libros apresuradamente en las mochilas y salieron corriendo al patio. ¿Qué momento empezó?", correcta = "El recreo o descanso", incorrecta1 = "La clase de matemáticas", incorrecta2 = "La hora de dormir" });
        preguntas.Add(34, new Pregunta() { titulo = "Lectura 26", pregunta = "Felipe guardó el helado en el congelador, la leche en la nevera y las papas en la alacena. ¿Dónde colocó el helado para que no se derrita?", correcta = "En el congelador", incorrecta1 = "En la alacena", incorrecta2 = "En la mesa" });

        // === MATEMÁTICAS SIMPLES (35 - 69) ===
        preguntas.Add(35, new Pregunta() { titulo = "Suma", pregunta = "¿Cuánto es 15 + 27?", correcta = "42", incorrecta1 = "32", incorrecta2 = "44" });
        preguntas.Add(36, new Pregunta() { titulo = "Resta", pregunta = "Si tienes 50 bombones y regalas 18, ¿cuántos bombones te quedan?", correcta = "32", incorrecta1 = "38", incorrecta2 = "42" });
        preguntas.Add(37, new Pregunta() { titulo = "Multiplicación", pregunta = "¿Cuál es el resultado de multiplicar 7 por 8?", correcta = "56", incorrecta1 = "54", incorrecta2 = "64" });
        preguntas.Add(38, new Pregunta() { titulo = "División", pregunta = "Si repartes 24 manzanas en partes iguales entre 4 niños, ¿cuántas recibe cada uno?", correcta = "6", incorrecta1 = "4", incorrecta2 = "8" });
        preguntas.Add(39, new Pregunta() { titulo = "Secuencia 1", pregunta = "¿Qué número sigue en la siguiente serie: 2, 4, 6, 8, ...?", correcta = "10", incorrecta1 = "9", incorrecta2 = "12" });
        preguntas.Add(40, new Pregunta() { titulo = "Lógica Mateo", pregunta = "Un paquete contiene 3 galletas. ¿Cuántas galletas habrá en total en 5 paquetes iguales?", correcta = "15", incorrecta1 = "12", incorrecta2 = "18" });
        preguntas.Add(41, new Pregunta() { titulo = "Suma Doble", pregunta = "¿Cuál es el doble de 14 más 5?", correcta = "33", incorrecta1 = "23", incorrecta2 = "38" });
        preguntas.Add(42, new Pregunta() { titulo = "Geometría básica", pregunta = "¿Cuántos lados tiene un pentágono?", correcta = "Cinco", incorrecta1 = "Cuatro", incorrecta2 = "Seis" });
        preguntas.Add(43, new Pregunta() { titulo = "Resta simple", pregunta = "Si a 100 le restas 45, ¿cuánto obtienes?", correcta = "55", incorrecta1 = "65", incorrecta2 = "45" });
        preguntas.Add(44, new Pregunta() { titulo = "Secuencia 2", pregunta = "¿Qué número falta en la serie: 5, 10, 15, __, 25?", correcta = "20", incorrecta1 = "16", incorrecta2 = "22" });
        preguntas.Add(45, new Pregunta() { titulo = "Tiempo", pregunta = "Si una clase dura 45 minutos y empieza a las 9:00 AM, ¿a qué hora termina?", correcta = "9:45 AM", incorrecta1 = "10:00 AM", incorrecta2 = "9:30 AM" });
        preguntas.Add(46, new Pregunta() { titulo = "Multiplicación 2", pregunta = "¿Cuánto es 9 multiplicado por 6?", correcta = "54", incorrecta1 = "63", incorrecta2 = "45" });
        preguntas.Add(47, new Pregunta() { titulo = "Fracciones", pregunta = "Si divides una pizza en 8 pedazos iguales y te comes 2 pedazos, ¿qué fracción te comiste?", correcta = "2/8 (un cuarto)", incorrecta1 = "1/8", incorrecta2 = "2/6" });
        preguntas.Add(48, new Pregunta() { titulo = "Conteo", pregunta = "¿Cuántas decenas hay en el número 80?", correcta = "8", incorrecta1 = "80", incorrecta2 = "4" });
        preguntas.Add(49, new Pregunta() { titulo = "Suma tres dígitos", pregunta = "¿Cuánto es 10 + 20 + 35?", correcta = "65", incorrecta1 = "55", incorrecta2 = "75" });
        preguntas.Add(50, new Pregunta() { titulo = "Resta Lógica", pregunta = "En un árbol había 12 pájaros. Volaron 4 y luego llegaron 3 más. ¿Cuántos pájaros quedan en el árbol?", correcta = "11", incorrecta1 = "13", incorrecta2 = "8" });
        preguntas.Add(51, new Pregunta() { titulo = "División exacta", pregunta = "¿Cuánto es 45 dividido entre 5?", correcta = "9", incorrecta1 = "8", incorrecta2 = "7" });
        preguntas.Add(52, new Pregunta() { titulo = "Mayor o Menor", pregunta = "¿Cuál de los siguientes números es el mayor: 145, 154, 129?", correcta = "154", incorrecta1 = "145", incorrecta2 = "129" });
        preguntas.Add(53, new Pregunta() { titulo = "Multiplicación por 0", pregunta = "¿Cuál es el resultado de 1,250 multiplicado por 0?", correcta = "0", incorrecta1 = "1,250", incorrecta2 = "1" });
        preguntas.Add(54, new Pregunta() { titulo = "Lógica Monedas", pregunta = "Si tienes 3 monedas de $500 pesos cada una, ¿cuánto dinero tienes en total?", correcta = "$1,500", incorrecta1 = "$1,000", incorrecta2 = "$2,000" });
        preguntas.Add(55, new Pregunta() { titulo = "Secuencia 3", pregunta = "Completa la secuencia descendente: 30, 27, 24, 21, ... ¿Qué número sigue?", correcta = "18", incorrecta1 = "19", incorrecta2 = "15" });
        preguntas.Add(56, new Pregunta() { titulo = "Geometría 2", pregunta = "¿Cuántos vértices (esquinas) tiene un cuadrado?", correcta = "4", incorrecta1 = "3", incorrecta2 = "6" });
        preguntas.Add(57, new Pregunta() { titulo = "Doble", pregunta = "¿Cuál es el triple del número 6?", correcta = "18", incorrecta1 = "12", incorrecta2 = "24" });
        preguntas.Add(58, new Pregunta() { titulo = "Par o Impar", pregunta = "¿Cuál de los siguientes números es un número impar?", correcta = "17", incorrecta1 = "22", incorrecta2 = "40" });
        preguntas.Add(59, new Pregunta() { titulo = "Suma Dinero", pregunta = "Compraste un juguete de $35 y pagaste con un billete de $50. ¿Cuánto vuelto te deben dar?", correcta = "15", incorrecta1 = "25", incorrecta2 = "10" });
        preguntas.Add(60, new Pregunta() { titulo = "Multiplicación 3", pregunta = "¿Cuánto es 11 x 4?", correcta = "44", incorrecta1 = "41", incorrecta2 = "48" });
        preguntas.Add(61, new Pregunta() { titulo = "División 2", pregunta = "Si tienes 100 dulces para repartir equitativamente entre 10 bolsas, ¿cuántos pones por bolsa?", correcta = "10", incorrecta1 = "5", incorrecta2 = "20" });
        preguntas.Add(62, new Pregunta() { titulo = "Lógica Patas", pregunta = "En una granja hay 4 vacas. ¿Cuántas patas de vaca se pueden contar en total?", correcta = "16", incorrecta1 = "12", incorrecta2 = "8" });
        preguntas.Add(63, new Pregunta() { titulo = "Suma Centena", pregunta = "¿Cuánto es 85 + 15?", correcta = "100", incorrecta1 = "90", incorrecta2 = "110" });
        preguntas.Add(64, new Pregunta() { titulo = "Mitad", pregunta = "¿Cuál es la mitad exacta de 36?", correcta = "18", incorrecta1 = "16", incorrecta2 = "14" });
        preguntas.Add(65, new Pregunta() { titulo = "Lógica Libros", pregunta = "Si lees 5 páginas de un libro al día, ¿cuántas páginas habrás leído al cabo de 6 días?", correcta = "30", incorrecta1 = "25", incorrecta2 = "35" });
        preguntas.Add(66, new Pregunta() { titulo = "Secuencia 4", pregunta = "¿Qué número sigue en la serie: 10, 20, 40, 80, ...?", correcta = "160", incorrecta1 = "100", incorrecta2 = "120" });
        preguntas.Add(67, new Pregunta() { titulo = "Resta 3 dígitos", pregunta = "¿Cuánto es 250 menos 50?", correcta = "200", incorrecta1 = "150", incorrecta2 = "210" });
        preguntas.Add(68, new Pregunta() { titulo = "Horas", pregunta = "¿Cuántas horas completas hay en 2 días?", correcta = "48 horas", incorrecta1 = "24 horas", incorrecta2 = "36 horas" });
        preguntas.Add(69, new Pregunta() { titulo = "Operación combinada", pregunta = "¿Cuánto da la operación: (5 x 2) + 4?", correcta = "14", incorrecta1 = "30", incorrecta2 = "12" });
    }
}