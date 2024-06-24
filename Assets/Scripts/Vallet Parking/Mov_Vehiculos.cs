using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mov_Vehiculos : MonoBehaviour
{
    
    //stats vehiculo y estados
    public float Vel;
    private bool mover;
    private bool salio;

    //posicion luego de chocar
    private float NuevaPos;

    //saber el punto de contacto para cuando sale del estacionamiento
    private Vector3 punto;

    //evitar choques fuera del estacionamiento
    public LayerMask vehiculos;

    //prefab/color random
    public List<Sprite> V_Color;

    private void Start()
    {
        if (V_Color.Count != 0)
        {
            transform.GetComponent<SpriteRenderer>().sprite = V_Color[Random.Range(0, V_Color.Count)];
        }
    }

    private void OnMouseDown()
    {
        mover = true;
    }

    private void Update()
    {
        if (mover)
        {
            transform.Translate(new Vector2(0f, Vel * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector2.zero);
        }

        if (salio)
        {
            Fuera();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "calle":
                {
                    Vector3 rotacionAtual = transform.localEulerAngles;
                    rotacionAtual.z -= 90;
                    transform.localEulerAngles = rotacionAtual;
                    punto = collision.ClosestPoint(transform.position);
                    transform.position = punto;
                    break;
                }
            case "Finish":
                {
                    salio = true;
                    break;
                }
            default:
                {
                    if (mover)
                    {
                        NuevaPos = Mathf.Round(transform.localPosition.y);
                        transform.DOLocalMoveY(NuevaPos, 0.5f);
                        mover = false;
                    }
                    break;
                }
        }
    }

    

    private void Fuera()
    {
        Vector2 direccion;
        Vector2 origen = Vector2.zero;
        float longitud = 0;

        if (Mathf.Approximately(punto.x, transform.position.x))
        {
            if(punto.y > transform.position.y)
            {
                //se mueve hacia abajo(rayo a la izquierda)
                direccion.x = -1;
                direccion.y = 0;
            }
            else
            {
                //se mueve hacia arriba(rayo a la derecha)
                direccion.x = 1;
                direccion.y = 0;
            }
        }
        else
        {
            if (punto.x > transform.position.x)
            {
                //se mueve hacia la izquierda(rayo arriba)
                direccion.x = 0;
                direccion.y = 1;
            }
            else
            {
                //se mueve hacia la derecha(rayo abajo)
                direccion.x = 0;
                direccion.y = -1;
            }
        }

        switch (transform.name)
        {
            case "auto":
                {
                    longitud = 0.6f;
                    origen.x = transform.position.x + ((-longitud / 2) * direccion.x) + (-0.48f * direccion.y);
                    origen.y = transform.position.y + ((-longitud / 2) * direccion.y) + (0.48f * direccion.x);
                    break;
                }
            case "camioneta":
                {
                    longitud = 0.8f;
                    origen.x = transform.position.x + ((-longitud / 2) * direccion.x) + (-0.9f * direccion.y);
                    origen.y = transform.position.y + ((-longitud / 2) * direccion.y) + (0.9f * direccion.x);
                    break;
                }
            case "bondi":
                {
                    longitud = 0.9f;
                    origen.x = transform.position.x + ((-longitud / 2) * direccion.x) + (-1.4f * direccion.y);
                    origen.y = transform.position.y + ((-longitud / 2) * direccion.y) + (1.4f * direccion.x);
                    break;
                }
        }

        Ray2D ray = new Ray2D(origen, direccion);
        Debug.DrawRay(ray.origin, ray.direction*longitud, Color.magenta);
        
        RaycastHit2D rayo = Physics2D.Raycast(origen, direccion, longitud, vehiculos);

        if (rayo)
        {
            mover = false;
            Vel = 3;
        }
        else
        {
            mover = true;
        }
    }
}
