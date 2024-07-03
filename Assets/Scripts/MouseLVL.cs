using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MouseLVL : MonoBehaviour, IPointerExitHandler
{
    public string minijuego;
    
    public void OnPointerExit(PointerEventData eventdata)
    {
        UI_Manager.Instance.MenuNiveles(minijuego);
    } 
}
