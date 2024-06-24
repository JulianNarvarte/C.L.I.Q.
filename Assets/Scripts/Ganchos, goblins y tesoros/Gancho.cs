using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gancho : MonoBehaviour
{
    public BoxCollider2D Palo;
    
    private void OnMouseDown()
    {
        transform.DOLocalMoveY(4f, 0.4f);
        Palo.enabled = false;
        transform.DOScaleX(0f, 0.4f);
        Destroy(gameObject, 0.5f);
    }
}
