using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Jugador : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    //private RectTransform rectTransform;
    //[HideInInspector]
    [HideInInspector] public Transform Padre;
    public Transform PadreInicial;
    public Animator animator;

    [HideInInspector]public bool Muerto;
    public int Valor;
    public TMP_Text txtValor;

    //public Image imgJugador;

    public CanvasGroup canvasGroup;

    public List<CanvasGroup> Torres;

    private void Start()
    {
        txtValor.text = Valor.ToString();
        SetTorres();
        ActivarTorres();
    }

    public void Accion(string accion)
    {
        animator.SetTrigger(accion);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Muerto)
        {
            //Debug.Log("Begin drag");

            Padre = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            //desactivacion de raycast target (se desactiva para que se pueda detectar lo que esta "detras" del objeto que se tiene agarrado)
            //
            //sirve cuando es mas de un objeto, es decir, que tiene hijos 
            canvasGroup.blocksRaycasts = false;
            //
            //sirve en caso de ser un solo objeto
            //imgJugador.raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Muerto)
        {
            //Debug.Log("end drag");
            transform.SetParent(Padre);

            // Activacion raycast target (se activa para que pueda volver a interactuarse con el objeto una vez soltado)
            //
            //sirve cuando es mas de un objeto, es decir, que tiene hijos 
            canvasGroup.blocksRaycasts = true;
            animator.SetBool("Cuelga", false);
            //
            //sirve en caso de ser un solo objeto
            //imgJugador.raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Muerto)
        {
            //Debug.Log("Drag");

            //rectTransform.anchoredPosition += eventData.delta; //se mueve en relacion a la escala del canvas, en caso de que el canvas no tenga una escala de 1 es util
            transform.position = Input.mousePosition;
            animator.SetBool("Cuelga", true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("pointer down");
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop");
    }

    public void ChkTorres()
    { 
        int hijos = Torres[0].transform.GetChild(0).GetChild(0).childCount - 1;
        Debug.Log(hijos + " hijos");

        if (hijos == 0)
        {
            Destroy(Torres[0].gameObject);
            Torres.RemoveAt(0);
            ActivarTorres();
        }
    }

    void ActivarTorres()
    {
        Torres[0].blocksRaycasts = true;
        
        for (int i = 1; i < Torres.Count; i++)
        {
            Torres[i].blocksRaycasts = false;
        }
    }

    void SetTorres()
    {
        Transform jejox = transform.parent.transform.parent;
        Debug.Log(jejox.name);

        for (int i = 1; i < jejox.childCount; i++)
        {
            Torres.Add(jejox.GetChild(i).GetComponent<CanvasGroup>());
        }
    }
}
