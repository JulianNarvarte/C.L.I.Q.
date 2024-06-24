using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Disolver : MonoBehaviour
{
    private Liquidos liquido;

    public Color agua, lava;

    private void Start()
    {
        liquido = FindObjectOfType<Liquidos>().GetComponent<Liquidos>();
    }
    private void Update()
    {        
        if (liquido.liquido >= liquido.MaxLiquido && tag == liquido.fluido)
        {
            Evaporar(liquido.fluido);
        }

        if (liquido.solidos >= liquido.MaxLiquido - 15 && liquido.lleno && tag != liquido.fluido)
        {
            //liquido.Endurecer(tag, gameObject);
            Evaporar(tag);
        }
    }

    public void Evaporar(string liquido)
    {
        switch (liquido)
        {
            case "agua":
                {
                    transform.GetComponent<SpriteRenderer>().DOColor(agua, 0.5f);
                    Destroy(gameObject, 0.5f);
                    break;
                }

            case "lava":
                {
                    transform.GetComponent<SpriteRenderer>().DOColor(lava, 0.5f);
                    Destroy(gameObject, 0.5f);
                    break;
                }
        }
    
    }
}
