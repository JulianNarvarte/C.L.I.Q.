using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Test : MonoBehaviour, IPointerExitHandler
{
    private UI_Manager manager;
    private void Start()
    {
        manager = FindObjectOfType<UI_Manager>().GetComponent<UI_Manager>();
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        manager.HGT_Menus();
    } 
}
