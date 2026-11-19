using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListaQuizzes : MonoBehaviour
{
    // Prefab del boton (debe tener un Button y un texto TMP dentro)
    public GameObject prefabBoton;

    // Objeto donde se colocan los botones (el "Content" de la lista)
    public Transform contenedor;

    // Aqui se guarda el quiz que escoge el jugador
    public QuizJSON quizSeleccionado;

    void Start()
    {
        ActualizarLista();
    }

    // Tambien puedes enlazar esta funcion a un boton "Actualizar"
    public void ActualizarLista()
    {
        // Primero borramos los botones que ya hubiera
        foreach (Transform hijo in contenedor)
        {
            Destroy(hijo.gameObject);
        }

        // Buscamos todos los archivos .json de la carpeta
        string[] archivos = Directory.GetFiles(Application.dataPath, "*.json");

        foreach (string ruta in archivos)
        {
            // Leemos el archivo y lo convertimos a quiz
            string texto = File.ReadAllText(ruta);
            QuizJSON quiz = JsonUtility.FromJson<QuizJSON>(texto);

            // Si no es un quiz valido (o esta vacio), lo saltamos
            if (quiz == null || quiz.preguntas == null || quiz.preguntas.Count == 0)
            {
                continue;
            }

            // Creamos el boton dentro de la lista
            GameObject nuevoBoton = Instantiate(prefabBoton, contenedor);

            // Le ponemos como texto el nombre del archivo (sin .json)
            string nombre = Path.GetFileNameWithoutExtension(ruta);
            nuevoBoton.GetComponentInChildren<TMP_Text>().text = nombre;

            // Al pulsarlo, se escoge este quiz
            QuizJSON quizDelBoton = quiz;
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() =>
            {
                EscogerQuiz(quizDelBoton, nombre);
            });
        }
    }

    void EscogerQuiz(QuizJSON quiz, string nombre)
    {
        quizSeleccionado = quiz;
        Debug.Log("Quiz escogido: " + nombre + " (" + quiz.preguntas.Count + " preguntas)");
    }
}
