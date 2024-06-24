using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;


    public GameObject HGT_Menu;
    public GameObject HGT_Levels;
    private bool HGT_Alt;

    public Image pntCarga;
    public GameObject objCarga;
    public GameObject pntMenu;
    public GameObject pntLVL;

    

    private void Awake()
    {
        if (UI_Manager.Instance == null)
        {
            UI_Manager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseExit()
    {
        HGT_Menus();
    }

    public void MathTowels()
    {
        StartCoroutine(CargaEscenas("Math Towels"));
        //altCanvas("nivel");
    }

    public void ValletParking()
    {
        StartCoroutine(CargaEscenas("Vallet Parking"));
        //altCanvas("nivel");
    }

    public void HGT_Menus()
    {
        //alterna entre niveles y imagen
        HGT_Alt = !HGT_Alt;
        //Deselecciona para que el boton siga funcionando
        //correctamente luego de sacar el mouse de la imagen lvls
        EventSystem.current.SetSelectedGameObject(null);

        if (HGT_Alt)
        {
            HGT_Levels.SetActive(true);
            HGT_Menu.SetActive(false);
        }
        else
        {
            HGT_Levels.SetActive(false);
            HGT_Menu.SetActive(true);
        }
    }

    public void HGT_levels(string Nivel)
    {
        StartCoroutine(CargaEscenas(Nivel));
        //altCanvas("nivel");
    }

    private IEnumerator CargaEscenas(string escena)
    {
        objCarga.SetActive(true);
        AsyncOperation carga = SceneManager.LoadSceneAsync(escena);

        while (!carga.isDone)
        {
            pntCarga.fillAmount = carga.progress;
            yield return null;
        }
        //objCarga.SetActive(false);
        altCanvas(escena);
    }

    public void Reiniciar()
    {
        string scnActual = SceneManager.GetActiveScene().name;
        StartCoroutine(CargaEscenas(scnActual));
    }

    public void Casa()
    {
        StartCoroutine(CargaEscenas("Menu Principal"));
        //altCanvas("menu");
    }

    public void altCanvas(string caso)
    {
        if (caso == "Menu Principal")
        {
            pntLVL.SetActive(false);
            objCarga.SetActive(false);
            pntMenu.SetActive(true);
        }
        else
        {
            pntLVL.SetActive(true);
            objCarga.SetActive(false);
            pntMenu.SetActive(false);
        }
    }
}
