using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Recursos.Scrips.Mecanicas_de_juego.ServiciosJuego;
using Random = UnityEngine.Random;

namespace Assets.Recursos.Scrips.Mecanicas_de_juego
{
    public class ServiciosJuego : MonoBehaviour
    {
        [Header("TXTvariables")]
        public TextMeshProUGUI texto;

        [Header("TXTjugador")]
        public TextMeshProUGUI txtjugador; 


        [System.Serializable]
        public class Fragmento
        {
            public int orden;
            public string texto;
        }
        [System.Serializable]
        public class Frase
        {
            public string titulo;

            public List<Fragmento> partes;
        }
        public Dictionary<int, Frase> frases = new Dictionary<int, Frase>();
        public byte Mensaje;
        public string orden1, orden2, orden3, orden4, orden;
        public bool msj1, msj2, msj3, msj4 = true;
        public byte fragmentosRecogidos = 0; 


        private void Awake()
        {
            AgregarDic();
            Mensaje = (byte)(Random.Range(0, 30));
            ElegirMensaje();
        }

        public void ElegirMensaje()
        {
            msj1 = true;
            msj2 = true; 
            msj3 = true; 
            msj4 = true;
           
            orden1 = frases[Mensaje].partes[0].texto;
            orden2 = frases[Mensaje].partes[1].texto;
            orden3 = frases[Mensaje].partes[2].texto;
            orden4 = frases[Mensaje].partes[3].texto;
            orden = frases[Mensaje].partes[4].texto;
        }
          

        public void AgregarDic()
        {
            
            Frase frase1 = new Frase();
            frase1.titulo = "Refrán 1";
            frase1.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "A caballo" },
    new Fragmento(){ orden = 1, texto = "regalado" },
    new Fragmento(){ orden = 2, texto = "no se le" },
    new Fragmento(){ orden = 3, texto = "mira el diente." },
    new Fragmento(){ orden = 4, texto = "A caballo regalado no se le mira el diente." }
};
            frases.Add(0, frase1);

            // 2.
            Frase frase2 = new Frase();
            frase2.titulo = "Refrán 2";
            frase2.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Más vale" },
    new Fragmento(){ orden = 1, texto = "pájaro en mano" },
    new Fragmento(){ orden = 2, texto = "que ciento" },
    new Fragmento(){ orden = 3, texto = "volando." },
    new Fragmento(){ orden = 4, texto = "Más vale pájaro en mano que ciento volando." }
};
            frases.Add(1, frase2);

            // 3.
            Frase frase3 = new Frase();
            frase3.titulo = "Refrán 3";
            frase3.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "La unión" },
    new Fragmento(){ orden = 1, texto = "hace" },
    new Fragmento(){ orden = 2, texto = "la" },
    new Fragmento(){ orden = 3, texto = "fuerza." },
    new Fragmento(){ orden = 4, texto = "La unión hace la fuerza." }
};
            frases.Add(2, frase3);

            // 4.
            Frase frase4 = new Frase();
            frase4.titulo = "Refrán 4";
            frase4.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Perro que" },
    new Fragmento(){ orden = 1, texto = "ladra" },
    new Fragmento(){ orden = 2, texto = "no" },
    new Fragmento(){ orden = 3, texto = "muerde." },
    new Fragmento(){ orden = 4, texto = "Perro que ladra no muerde." }
};
            frases.Add(3, frase4);

            // 5.
            Frase frase5 = new Frase();
            frase5.titulo = "Refrán 5";
            frase5.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "No por mucho" },
    new Fragmento(){ orden = 1, texto = "madrugar" },
    new Fragmento(){ orden = 2, texto = "amanece más" },
    new Fragmento(){ orden = 3, texto = "temprano." },
    new Fragmento(){ orden = 4, texto = "No por mucho madrugar amanece más temprano." }
};
            frases.Add(4, frase5);

            // 6.
            Frase frase6 = new Frase();
            frase6.titulo = "Refrán 6";
            frase6.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Al que" },
    new Fragmento(){ orden = 1, texto = "madruga," },
    new Fragmento(){ orden = 2, texto = "Dios lo" },
    new Fragmento(){ orden = 3, texto = "ayuda." },
    new Fragmento(){ orden = 4, texto = "Al que madruga, Dios lo ayuda." }
};
            frases.Add(5, frase6);

            // 7.
            Frase frase7 = new Frase();
            frase7.titulo = "Refrán 7";
            frase7.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Camarón que" },
    new Fragmento(){ orden = 1, texto = "se duerme" },
    new Fragmento(){ orden = 2, texto = "se lo lleva" },
    new Fragmento(){ orden = 3, texto = "la corriente." },
    new Fragmento(){ orden = 4, texto = "Camarón que se duerme se lo lleva la corriente." }
};
            frases.Add(6, frase7);

            // 8.
            Frase frase8 = new Frase();
            frase8.titulo = "Refrán 8";
            frase8.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "De tal" },
    new Fragmento(){ orden = 1, texto = "palo," },
    new Fragmento(){ orden = 2, texto = "tal" },
    new Fragmento(){ orden = 3, texto = "astilla." },
    new Fragmento(){ orden = 4, texto = "De tal palo, tal astilla." }
};
            frases.Add(7, frase8);

            // 9.
            Frase frase9 = new Frase();
            frase9.titulo = "Refrán 9";
            frase9.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "En casa" },
    new Fragmento(){ orden = 1, texto = "de herrero," },
    new Fragmento(){ orden = 2, texto = "cuchillo" },
    new Fragmento(){ orden = 3, texto = "de palo." },
    new Fragmento(){ orden = 4, texto = "En casa de herrero, cuchillo de palo." }
};
            frases.Add(8, frase9);

            // 10.
            Frase frase10 = new Frase();
            frase10.titulo = "Refrán 10";
            frase10.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "El que" },
    new Fragmento(){ orden = 1, texto = "mucho abarca" },
    new Fragmento(){ orden = 2, texto = "poco" },
    new Fragmento(){ orden = 3, texto = "aprieta." },
    new Fragmento(){ orden = 4, texto = "El que mucho abarca poco aprieta." }
};
            frases.Add(9, frase10);

            // 11.
            Frase frase11 = new Frase();
            frase11.titulo = "Refrán 11";
            frase11.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Ojos que" },
    new Fragmento(){ orden = 1, texto = "no ven," },
    new Fragmento(){ orden = 2, texto = "corazón que" },
    new Fragmento(){ orden = 3, texto = "no siente." },
    new Fragmento(){ orden = 4, texto = "Ojos que no ven, corazón que no siente." }
};
            frases.Add(10, frase11);

            // 12.
            Frase frase12 = new Frase();
            frase12.titulo = "Refrán 12";
            frase12.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Dime con" },
    new Fragmento(){ orden = 1, texto = "quién andas" },
    new Fragmento(){ orden = 2, texto = "y te diré" },
    new Fragmento(){ orden = 3, texto = "quién eres." },
    new Fragmento(){ orden = 4, texto = "Dime con quién andas y te diré quién eres." }
};
            frases.Add(11, frase12);

            // 13.
            Frase frase13 = new Frase();
            frase13.titulo = "Refrán 13";
            frase13.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "A palabras" },
    new Fragmento(){ orden = 1, texto = "necias," },
    new Fragmento(){ orden = 2, texto = "oídos" },
    new Fragmento(){ orden = 3, texto = "sordos." },
    new Fragmento(){ orden = 4, texto = "A palabras necias, oídos sordos." }
};
            frases.Add(12, frase13);

            // 14.
            Frase frase14 = new Frase();
            frase14.titulo = "Refrán 14";
            frase14.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Más vale" },
    new Fragmento(){ orden = 1, texto = "tarde" },
    new Fragmento(){ orden = 2, texto = "que" },
    new Fragmento(){ orden = 3, texto = "nunca." },
    new Fragmento(){ orden = 4, texto = "Más vale tarde que nunca." }
};
            frases.Add(13, frase14);

            // 15.
            Frase frase15 = new Frase();
            frase15.titulo = "Refrán 15";
            frase15.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Cría" },
    new Fragmento(){ orden = 1, texto = "cuervos y" },
    new Fragmento(){ orden = 2, texto = "te sacarán" },
    new Fragmento(){ orden = 3, texto = "los ojos." },
    new Fragmento(){ orden = 4, texto = "Cría cuervos y te sacarán los ojos." }
};
            frases.Add(14, frase15);

            // 16.
            Frase frase16 = new Frase();
            frase16.titulo = "Refrán 16";
            frase16.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Quien siembra" },
    new Fragmento(){ orden = 1, texto = "vientos," },
    new Fragmento(){ orden = 2, texto = "cosecha" },
    new Fragmento(){ orden = 3, texto = "tempestades." },
    new Fragmento(){ orden = 4, texto = "Quien siembra vientos, cosecha tempestades." }
};
            frases.Add(15, frase16);

            // 17.
            Frase frase17 = new Frase();
            frase17.titulo = "Refrán 17";
            frase17.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "El que ríe" },
    new Fragmento(){ orden = 1, texto = "el último" },
    new Fragmento(){ orden = 2, texto = "ríe" },
    new Fragmento(){ orden = 3, texto = "mejor." },
    new Fragmento(){ orden = 4, texto = "El que ríe el último ríe mejor." }
};
            frases.Add(16, frase17);

            // 18.
            Frase frase18 = new Frase();
            frase18.titulo = "Refrán 18";
            frase18.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Agua que" },
    new Fragmento(){ orden = 1, texto = "no has de beber," },
    new Fragmento(){ orden = 2, texto = "déjala" },
    new Fragmento(){ orden = 3, texto = "correr." },
    new Fragmento(){ orden = 4, texto = "Agua que no has de beber, déjala correr." }
};
            frases.Add(17, frase18);

            // 19.
            Frase frase19 = new Frase();
            frase19.titulo = "Refrán 19";
            frase19.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "A mal" },
    new Fragmento(){ orden = 1, texto = "tiempo," },
    new Fragmento(){ orden = 2, texto = "buena" },
    new Fragmento(){ orden = 3, texto = "cara." },
    new Fragmento(){ orden = 4, texto = "A mal tiempo, buena cara." }
};
            frases.Add(18, frase19);

            // 20.
            Frase frase20 = new Frase();
            frase20.titulo = "Refrán 20";
            frase20.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Del dicho" },
    new Fragmento(){ orden = 1, texto = "al trecho" },
    new Fragmento(){ orden = 2, texto = "hay mucho" },
    new Fragmento(){ orden = 3, texto = "trecho." },
    new Fragmento(){ orden = 4, texto = "Del dicho al trecho hay mucho trecho." }
};
            frases.Add(19, frase20);

            // 21.
            Frase frase21 = new Frase();
            frase21.titulo = "Dicho 21";
            frase21.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Cada loco" },
    new Fragmento(){ orden = 1, texto = "con" },
    new Fragmento(){ orden = 2, texto = "su" },
    new Fragmento(){ orden = 3, texto = "tema." },
    new Fragmento(){ orden = 4, texto = "Cada loco con su tema." }
};
            frases.Add(20, frase21);

            // 22.
            Frase frase22 = new Frase();
            frase22.titulo = "Dicho 22";
            frase22.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Lo cortés" },
    new Fragmento(){ orden = 1, texto = "no quita" },
    new Fragmento(){ orden = 2, texto = "lo" },
    new Fragmento(){ orden = 3, texto = "valiente." },
    new Fragmento(){ orden = 4, texto = "Lo cortés no quita lo valiente." }
};
            frases.Add(21, frase22);

            // 23.
            Frase frase23 = new Frase();
            frase23.titulo = "Refrán 23";
            frase23.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Quien tiene" },
    new Fragmento(){ orden = 1, texto = "un amigo" },
    new Fragmento(){ orden = 2, texto = "tiene un" },
    new Fragmento(){ orden = 3, texto = "tesoro." },
    new Fragmento(){ orden = 4, texto = "Quien tiene un amigo tiene un tesoro." }
};
            frases.Add(22, frase23);

            // 24.
            Frase frase24 = new Frase();
            frase24.titulo = "Refrán 24";
            frase24.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Por la" },
    new Fragmento(){ orden = 1, texto = "boca" },
    new Fragmento(){ orden = 2, texto = "muere el" },
    new Fragmento(){ orden = 3, texto = "pez." },
    new Fragmento(){ orden = 4, texto = "Por la boca muere el pez." }
};
            frases.Add(23, frase24);

            // 25.
            Frase frase25 = new Frase();
            frase25.titulo = "Dicho 25";
            frase25.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Mucho" },
    new Fragmento(){ orden = 1, texto = "ruido y" },
    new Fragmento(){ orden = 2, texto = "pocas" },
    new Fragmento(){ orden = 3, texto = "nueces." },
    new Fragmento(){ orden = 4, texto = "Mucho ruido y pocas nueces." }
};
            frases.Add(24, frase25);

            // 26.
            Frase frase26 = new Frase();
            frase26.titulo = "Refrán 26";
            frase26.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Donde hubo" },
    new Fragmento(){ orden = 1, texto = "fuego," },
    new Fragmento(){ orden = 2, texto = "cenizas" },
    new Fragmento(){ orden = 3, texto = "quedan." },
    new Fragmento(){ orden = 4, texto = "Donde hubo fuego, cenizas quedan." }
};
            frases.Add(25, frase26);

            // 27.
            Frase frase27 = new Frase();
            frase27.titulo = "Dicho 27";
            frase27.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Zapatero" },
    new Fragmento(){ orden = 1, texto = "a tus" },
    new Fragmento(){ orden = 2, texto = "zapatos," },
    new Fragmento(){ orden = 3, texto = "siempre." },
    new Fragmento(){ orden = 4, texto = "Zapatero a tus zapatos siempre." }
};
            frases.Add(26, frase27);

            // 28.
            Frase frase28 = new Frase();
            frase28.titulo = "Dicho 28";
            frase28.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Genio y" },
    new Fragmento(){ orden = 1, texto = "figura hasta" },
    new Fragmento(){ orden = 2, texto = "la" },
    new Fragmento(){ orden = 3, texto = "sepultura." },
    new Fragmento(){ orden = 4, texto = "Genio y figura hasta la sepultura." }
};
            frases.Add(27, frase28);

            // 29.
            Frase frase29 = new Frase();
            frase29.titulo = "Refrán 29";
            frase29.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "El hábito" },
    new Fragmento(){ orden = 1, texto = "no" },
    new Fragmento(){ orden = 2, texto = "hace al" },
    new Fragmento(){ orden = 3, texto = "monje." },
    new Fragmento(){ orden = 4, texto = "El hábito no hace al monje." }
};
            frases.Add(28, frase29);

            // 30.
            Frase frase30 = new Frase();
            frase30.titulo = "Dicho 30";
            frase30.partes = new List<Fragmento>()
{
    new Fragmento(){ orden = 0, texto = "Mal de" },
    new Fragmento(){ orden = 1, texto = "muchos," },
    new Fragmento(){ orden = 2, texto = "consuelo de" },
    new Fragmento(){ orden = 3, texto = "tontos." },
    new Fragmento(){ orden = 4, texto = "Mal de muchos, consuelo de tontos." }
};
            frases.Add(29, frase30);

        }
    }
}