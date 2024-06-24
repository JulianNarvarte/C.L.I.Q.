using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Liquidos : MonoBehaviour
{
    
    private float escala,move;

    //controladores de fluido y cantidad
    public int MaxLiquido;
    [HideInInspector] public int liquido, solidos;
    private bool vacio = true;
    [HideInInspector] public bool lleno;
    [HideInInspector] public string fluido;

    //colores
    public Color Agua, Lava;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (vacio)
        {
            if (collision.tag == "lava" || collision.tag == "agua")
            {
                fluido = collision.tag;
                vacio = false;

                switch (collision.tag) //Define el color que tendra el liquido estatico
                {
                    case "agua":
                        {
                            transform.GetComponent<SpriteRenderer>().color = Agua;
                            transform.tag = "lago";
                            break;
                        }

                    case "lava":
                        {
                            transform.GetComponent<SpriteRenderer>().color = Lava;
                            transform.tag = "lava";
                            break;
                        }
                }
                Destroy(collision.gameObject);
                MasLiquido();
            }
        }
        else
        {
            if (collision.tag == fluido)
            {
                Destroy(collision.gameObject);
                MasLiquido();

                if (liquido >= MaxLiquido)
                {
                    lleno = true;
                    LiquidoLleno();
                }
            }
            else
            {
                Endurecer(collision.tag, collision.gameObject);
            }
        }
    }

    private void MasLiquido()
    {
        liquido++;
        escala += 0.01f;
        move += 0.0047f;
        transform.DOScaleY(escala, 0.3f);
        transform.DOLocalMoveY(move, 0.1f);
    }

    private void LiquidoLleno()
    {
        transform.DOLocalMoveY(1.4f, 0.5f);
        transform.DOScaleY(3, 0.5f);
        transform.tag = "calle";
        Debug.Log(transform.tag);
    }

    public void Endurecer(string Fluido, GameObject Entra)
    {
        solidos++;
        switch (Fluido)
        {
            case "agua":
                {
                    Entra.GetComponent<SpriteRenderer>().color = Color.black;
                    break;
                }

            case "lava":
                {
                    Entra.GetComponent<SpriteRenderer>().color = Color.grey;
                    break;
                }
        }

        if (solidos >= MaxLiquido - 15 && lleno)
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
            switch (Fluido)
            {
                case "agua":
                    {
                        transform.GetComponent<SpriteRenderer>().color = Color.black;
                        break;
                    }

                case "lava":
                    {
                        transform.GetComponent<SpriteRenderer>().color = Color.grey;
                        break;
                    }
            }
        }
    }

}
