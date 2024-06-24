using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{
    public bool Mata;
    public float VelKill;
    private bool Activa;
    private bool HaceCamino;

    private Rigidbody2D rb;
    private CapsuleCollider2D colider;

    private BoxCollider2D camino;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        TestVelocidad();
        Quieto();
    }

    void Quieto()
    {
        if (rb.velocity == Vector2.zero && Activa)
        {
            if (HaceCamino)
            {
                rb.bodyType = RigidbodyType2D.Static;
                camino.isTrigger = false;
                colider.enabled = false;
            }
            else
            {
                colider.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        //else
        //{
        //    colider.enabled = true;
        //}
    }

    void TestVelocidad()
    {
        //Debug.Log(Mathf.Abs(rb.velocity.x) + "  " + Mathf.Abs(rb.velocity.y));
        if (Mathf.Abs(rb.velocity.y) > VelKill || Mathf.Abs(rb.velocity.x) > VelKill)
        {
            Mata = true;
            Activa = true;
        }
        else
        {
            Mata = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "solidificator")
        {
            Debug.Log("deberia quedar el collider");
            camino = collision.GetComponent<BoxCollider2D>();
            HaceCamino = true;
        }

        if (Mata)
        {

            switch (collision.tag)
            {
                case "Player":
                    {
                        Caballero jugador = collision.GetComponent<Caballero>();
                        jugador.SetMuerte(); //("roca"); //en caso de distintas animaciones
                        break;
                    }
                case "Goblin":
                    {
                        Goblin goblin = collision.GetComponent<Goblin>();
                        goblin.Muerte();
                        break;
                    }
            }
            //Debug.Log("aplasto" + collision.name);
        }
    }

}
