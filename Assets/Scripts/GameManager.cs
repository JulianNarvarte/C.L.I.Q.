using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int IQ;
    public int[,] NivelesScore = new int[27,3]; //puntos, estrellas
    private string[] NombreLvl = new string[27];

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NombreNiveles();
        CargarDatos();
    }

    private void CargarDatos()
    {
        Data data = Save_Load.Load();
        IQ = data.IQ;
        for (int i = 0; i < NivelesScore.GetLength(0); i++)
        {
            for (int c = 0; c < NivelesScore.GetLength(1); c++)
            {
                NivelesScore[i, c] = data.Scores[i, c];
                //Debug.Log(NivelesScore[i, c]);
            }
        }
        Debug.Log("Datos cargados");
        //Debug.Log($"IQ Actual {IQ}");
    }

    public void ScoreYSiguiente(int Puntaje, int Estrellas)
    {
        int Num_Siguiente_Lvl;
        int Num_Lvl_Actual;
        string NivelActual = SceneManager.GetActiveScene().name;
        string SiguienteNivel;

        for (int i = 0; i < NombreLvl.Length; i++)
        {
            if (NombreLvl[i] == NivelActual)
            {
                //Siguiente
                Num_Lvl_Actual = ExtraerNumeroEsc(NivelActual);
                Num_Siguiente_Lvl = ExtraerNumeroEsc(NombreLvl[i + 1]);
                //Debug.Log($"numero de escena en array {Num_Lvl_Actual}");
                //Debug.Log($"numero siguiente de escena en array {Num_Siguiente_Lvl}");

                if (Num_Lvl_Actual < Num_Siguiente_Lvl)
                {
                    Debug.Log("Podria pasarse a siguiente nivel");
                    SiguienteNivel = NombreLvl[i + 1];
                    UI_Manager.Instance.Victoria(SiguienteNivel, Puntaje, Estrellas);
                }
                else if (Num_Lvl_Actual > Num_Siguiente_Lvl)
                {
                    Debug.Log("No podria pasarse a siguiente nivel");
                    UI_Manager.Instance.Victoria("Bloquear", Puntaje, Estrellas);
                }

                //Guardado
                if (Estrellas > NivelesScore[i, 1])
                {
                    int nIQ = SumarIQ(Estrellas, NivelesScore[i, 2]);

                    NivelesScore[i, 0] = Puntaje;
                    NivelesScore[i, 1] = Estrellas;
                    IQ -= NivelesScore[i, 2];
                    IQ += nIQ;
                    NivelesScore[i, 2] = nIQ;
                    //Debug.Log($"el IQ actual es {IQ}");
                    UI_Manager.Instance.txtNewHigh.SetActive(true);

                }
                else if (Puntaje > NivelesScore[i,0])
                {
                    NivelesScore[i, 0] = Puntaje;
                    UI_Manager.Instance.txtNewHigh.SetActive(true);
                }
            }
        }
        Save_Load.Save(this);
    }

    private int SumarIQ(int estrellas, int ant_IQ)
    {
        int iq;
        //Debug.Log("estrellas " + estrellas + " anterior iq" +  ant_IQ);

        switch (estrellas)
        {
            case 1:
                {
                    iq = 1;
                    StartCoroutine(UI_Manager.Instance.EscalaSumaIQ(iq - ant_IQ));
                    return iq;
                }
            case 2:
                {
                    iq = UnityEngine.Random.Range(2, 5);
                    StartCoroutine(UI_Manager.Instance.EscalaSumaIQ(iq - ant_IQ));
                    return iq;
                }
            case 3:
                {
                    iq = UnityEngine.Random.Range(5, 8);
                    StartCoroutine(UI_Manager.Instance.EscalaSumaIQ(iq - ant_IQ));
                    return iq;
                }
            case 4:
                {
                    iq = 9;
                    StartCoroutine(UI_Manager.Instance.EscalaSumaIQ(iq - ant_IQ));
                    return iq;
                }
            default:
                {
                    iq = 0;
                    return iq;
                }
        }
    }

    private static int ExtraerNumeroEsc(string cadena)
    {
        // Busca la parte numérica en la cadena
        Match match = Regex.Match(cadena, @"\d+$");

        if (match.Success)
        {
            // Convierte la parte numérica a entero
            return int.Parse(match.Value);
        }
        else
        {
            throw new ArgumentException("La cadena no contiene un número al final.");
        }
    }

    private void NombreNiveles()
    {

        for (int i = 0; i < 27; i++)
        {
            if (i < 9)
            {
                NombreLvl[i] = $"MT_{i + 1}";
            }
            if (i >= 9 && i < 18)
            {
                NombreLvl[i] = $"HGT_{i - 8}";
            }
            if (i >= 18 && i < 27)
            {
                NombreLvl[i] = $"VP_{i - 17}";
            }
            //Debug.Log(NombreLvl[i]);
        }
    }

    public void Guardar()
    {
        Save_Load.Save(this);
        Debug.Log("Datos guardados");
    }

    public void Cargar()
    {
        Data data = Save_Load.Load();
        Debug.Log("Datos cargados");
        IQ = data.IQ;
        for (int i = 0; i < NivelesScore.GetLength(0); i++)
        {
            for (int c = 0; c < NivelesScore.GetLength(1); c++)
            {
                NivelesScore[i,c] = data.Scores[i,c];
            }
        }
    }
}
