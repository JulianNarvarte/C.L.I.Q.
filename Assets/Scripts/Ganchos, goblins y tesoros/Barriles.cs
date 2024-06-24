using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriles : MonoBehaviour
{
    public float velX,velY,velMax = 4;
    public LayerMask suelo, pared;
    private Rigidbody2D rb;
    public Light luz;
    private Color ColorOriginal;
    private SpriteRenderer sprite;
    public GameObject explosion;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        ColorOriginal = sprite.color;

        InvokeRepeating("Velocidades", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        CambiarColor();    
    }

    private void Velocidades()
    {
        velX = Mathf.Abs(rb.velocity.x);
        velY = Mathf.Abs(rb.velocity.y);
    }
    private void CambiarColor()
    {
        float Actual = 0.65f + (Mathf.Max(velX, velY) / 20);

        sprite.color = new Color(Actual, ColorOriginal.g, ColorOriginal.b, ColorOriginal.a);

        if (Mathf.Max(velX, velY) > velMax)
        {
            luz.enabled = true;
        }
        else
        {
            luz.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string capa = LayerMask.LayerToName(collision.gameObject.layer);

        if (capa == "Pared")
        {
            if (velX > velMax)
            {
                Debug.Log("Muy veloz");
                Explotar(collision.gameObject);
            }
            return;
        }
        else if (capa == "Suelo")
        {
            if (velY > velMax)
            {
                Debug.Log("Cae shapidismo");
                Explotar(collision.gameObject);

            }
            return;
        }
        else
        {
            if (velX > velMax)
            {
                Debug.Log("Muy veloz");
                Explotar(collision.gameObject);
            }
            else if (velY > velMax)
            {
                Debug.Log("Cae shapidismo");
                Explotar(collision.gameObject);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Explota
        //Instantiate(explosion, transform.position, Quaternion.identity);
        //Destroy(gameObject);
        //Destroy(collision.gameObject);
        //Explota collision (posible solucion set trigger ceniza)

        //if (velX > velMax)
        //{
        //    Debug.Log("Muy veloz");
        //    Explotar(collision.gameObject);
        //}
        //else if (velY > velMax)
        //{
        //    Debug.Log("Cae shapidismo");
        //    Explotar(collision.gameObject);

        //}
    }

    void Explotar(GameObject otro)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(otro.gameObject);
        Destroy(gameObject);
    }
}
