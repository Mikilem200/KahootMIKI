using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ----- Clases que definen la estructura del JSON -----

[Serializable]
public class PreguntaJSON
{
    public int numero;
    public string pregunta;
    public string respuesta1;
    public string respuesta2;
    public string respuesta3;
    public string respuesta4;
    public int correcta; // 1, 2, 3 o 4
}

[Serializable]
public class QuizJSON
{
    public List<PreguntaJSON> preguntas = new List<PreguntaJSON>();
}

// ----- Script principal -----

public class GuardarQuizJSON : MonoBehaviour
{
    // Enlaza aqui el objeto que tiene el script CreadorPreguntas
    public CreadorPreguntas creador;

    // Nombre del archivo que se va a crear
    public string nombreArchivo = "quiz.json";

    // Enlaza esta funcion al boton (OnClick)
    public void GuardarJSON()
    {
        // Si no hay preguntas guardadas, no hacemos nada
        if (creador.preguntas.Count == 0)
        {
            Debug.Log("No hay preguntas para guardar");
            return;
        }

        QuizJSON quiz = new QuizJSON();

        // Recorremos las listas y juntamos los datos de cada pregunta
        for (int i = 0; i < creador.preguntas.Count; i++)
        {
            PreguntaJSON p = new PreguntaJSON();
            p.numero = creador.numeros[i];
            p.pregunta = creador.preguntas[i];
            p.respuesta1 = creador.respuestas1[i];
            p.respuesta2 = creador.respuestas2[i];
            p.respuesta3 = creador.respuestas3[i];
            p.respuesta4 = creador.respuestas4[i];
            p.correcta = creador.correctas[i];

            quiz.preguntas.Add(p);
        }

        // Convertimos a texto JSON (true = con saltos de linea, facil de leer)
        string json = JsonUtility.ToJson(quiz, true);

        // Guardamos el archivo en la carpeta Assets del proyecto
        string ruta = Path.Combine(Application.dataPath, nombreArchivo);
        File.WriteAllText(ruta, json);

        Debug.Log("JSON guardado en: " + ruta);

#if UNITY_EDITOR
        // Para que el archivo aparezca enseguida en la ventana Project
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
