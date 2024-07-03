using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sis_Puntaje : MonoBehaviour
{
    private float Puntos = 1000;
    public int PuntajeFinal;

    public int PuntosEstrella1;
    public int PuntosEstrella2;
    public int PuntosEstrella3;

    // Update is called once per frame
    void Update()
    {
        ReducirPuntos();
    }

    private void ReducirPuntos()
    {
        if (Puntos > 0)
        {
            Puntos -= Time.deltaTime;
            UI_Manager.Instance.txtContadorPuntos.text = $"Score: {Mathf.RoundToInt(Puntos)}";
        }
    }

    public void Resultados()
    {
        PuntajeFinal = Mathf.FloorToInt(Puntos);

        if (PuntajeFinal > PuntosEstrella1)
        {
            if (PuntajeFinal > PuntosEstrella2)
            {
                if (PuntajeFinal > PuntosEstrella3)
                {
                    //Estrellas tiene un numero mas para poder sumar IQ en caso de pasarse el nivel sin estrellas

                    GameManager.Instance.ScoreYSiguiente(PuntajeFinal, 4);
                    //Debug.Log("Gano con 3 estrellas");
                    return;
                }
                GameManager.Instance.ScoreYSiguiente(PuntajeFinal, 3);
                //Debug.Log("Gano con 2 estrellas");
                return;
            }
            GameManager.Instance.ScoreYSiguiente(PuntajeFinal, 2);
            //Debug.Log("Gano con 1 estrellas");
            return;
        }
        GameManager.Instance.ScoreYSiguiente(PuntajeFinal, 1);
        //Debug.Log("Gano sin estrellas");
        return;
    }
}
