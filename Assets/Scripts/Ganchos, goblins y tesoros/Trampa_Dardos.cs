using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class Trampa_Dardos : MonoBehaviour
{
    public GameObject PrefabDardo;
    public Transform PosPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.DOLocalMoveY(0.2f, .3f);
        if (PosPrefab.childCount < 1)
        {
            Instantiate(PrefabDardo, PosPrefab.position, PrefabDardo.transform.rotation, PosPrefab);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.DOLocalMoveY(0.5f, .3f);
    }
}
