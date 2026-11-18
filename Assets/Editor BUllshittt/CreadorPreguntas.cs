using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreadorPreguntas : MonoBehaviour
{
    // ----- Campos de la escena (se enlazan desde el Inspector) -----
    public TMP_InputField campoNumero;
    public TMP_InputField campoPregunta;
    public TMP_InputField campoRespuesta1;
    public TMP_InputField campoRespuesta2;
    public TMP_InputField campoRespuesta3;
    public TMP_InputField campoRespuesta4;
    public TMP_Dropdown desplegableCorrecta; // opciones: Respuesta 1, 2, 3, 4
    public TMP_Text textoEstado;             // para mostrar mensajes (opcional)

    // ----- Listas donde se guarda todo -----
    public List<int> numeros = new List<int>();
    public List<string> preguntas = new List<string>();
    public List<string> respuestas1 = new List<string>();
    public List<string> respuestas2 = new List<string>();
    public List<string> respuestas3 = new List<string>();
    public List<string> respuestas4 = new List<string>();
    public List<int> correctas = new List<int>(); // 1, 2, 3 o 4

    // Cuantas preguntas se pueden hacer (cambialo en el Inspector)
    public int maxPreguntas = 5;

    void Start()
    {
        // Preparamos el primer numero de pregunta
        campoNumero.text = "1";
        MostrarMensaje("Pregunta 1 de " + maxPreguntas);
    }

    // Enlaza esta funcion al boton "Guardar" (OnClick)
    public void GuardarPregunta()
    {
        // Si ya hay el maximo de preguntas, no dejamos guardar mas
        if (preguntas.Count >= maxPreguntas)
        {
            MostrarMensaje("Ya has hecho las " + maxPreguntas + " preguntas");
            return;
        }

        // Comprobamos que no haya campos vacios
        if (campoNumero.text == "" || campoPregunta.text == "" ||
            campoRespuesta1.text == "" || campoRespuesta2.text == "" ||
            campoRespuesta3.text == "" || campoRespuesta4.text == "")
        {
            MostrarMensaje("Rellena todos los campos");
            return;
        }

        // Guardamos cada dato en su lista
        numeros.Add(int.Parse(campoNumero.text));
        preguntas.Add(campoPregunta.text);
        respuestas1.Add(campoRespuesta1.text);
        respuestas2.Add(campoRespuesta2.text);
        respuestas3.Add(campoRespuesta3.text);
        respuestas4.Add(campoRespuesta4.text);
        correctas.Add(desplegableCorrecta.value + 1); // value empieza en 0, sumamos 1

        MostrarMensaje("Pregunta " + preguntas.Count + " guardada");

        // Limpiamos los campos para la siguiente pregunta
        LimpiarCampos();
    }

    void LimpiarCampos()
    {
        campoNumero.text = (preguntas.Count + 1).ToString(); // siguiente numero
        campoPregunta.text = "";
        campoRespuesta1.text = "";
        campoRespuesta2.text = "";
        campoRespuesta3.text = "";
        campoRespuesta4.text = "";
        desplegableCorrecta.value = 0;
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoEstado != null)
        {
            textoEstado.text = mensaje;
        }
        Debug.Log(mensaje);
    }
}
