using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public float velX;
    public float velY;
    [SerializeField] private LayerMask Paredes, suelo;
    private bool enSuelo;
    private bool Atacando;
    public bool Muerto;
    public bool CambiandoDir;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        caer();
        if (!Muerto)
        {
            atacar();
            if (!Atacando)
            {
                Patrullar();
            }
            
        }
    }

    private void Patrullar()
    {
        Ray ray = new Ray(new Vector2(transform.position.x - 2, transform.position.y), Vector2.right);
        RaycastHit2D rayo = Physics2D.Raycast(new Vector2(transform.position.x - 1.5f, transform.position.y), transform.right, 3f, Paredes);
        Debug.DrawRay(ray.origin, ray.direction * 3, Color.green);

        if (enSuelo)
        {
            rb.AddForce(new Vector2(velX * Time.deltaTime, rb.velocity.y * Time.deltaTime));
            //rb.velocity = new Vector2(velX,0);

            if (rayo && !CambiandoDir)
            {
                velX *= -1;
                CambiandoDir = true;
                SpriteRenderer.flipX = !SpriteRenderer.flipX;
            }
            else if (!rayo && CambiandoDir)
            {
                CambiandoDir = false;
            }
        }   
    }

    private void caer()
    {
        //Ray ray = new Ray(new Vector3(transform.position.x - 0.55f, transform.position.y - 0.55f, transform.position.z), Vector2.right);
        //Debug.DrawRay(ray.origin, ray.direction * 1.1f, Color.blue);

        RaycastHit2D rayo = Physics2D.Raycast(new Vector2(transform.position.x - 0.6f, transform.position.y - 0.75f), Vector2.right, 1.2f, suelo);


        if (rayo)
        {
            enSuelo = true;
            animator.SetBool("Cae", false);
            if (rayo.collider.tag == "lago")
            {
                rb.AddForce(new Vector2(rb.velocity.x * Time.deltaTime, velY * Time.deltaTime));
            }
        }
        else
        {
            enSuelo = false;
            animator.SetBool("Cae", true);
        }
    }

    private void atacar()
    {
        Caballero jugador = FindObjectOfType<Caballero>().GetComponent<Caballero>();
        
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 1.2f, transform.position.y), Vector2.right, 2.4f);
        Ray ray = new Ray(new Vector2(transform.position.x - 1.2f, transform.position.y), Vector2.right);
        Debug.DrawRay(ray.origin, ray.direction * 2.4f, Color.red);
        
        if(hit.collider.tag == "Player" && !jugador.muerto)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("ataca");
            Atacando = true;
            animator.SetTrigger("Ataca");
            jugador.SetMuerte(); //("goblin"); //en caso de tener distintas animaciones
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.transform.tag)
        {
            case "pinchos":
                {
                    Muerte();
                    break;
                }
            case "roca":
                {
                    Muerte();
                    break;
                }
            case "lava":
                {
                    Muerte();
                    break;
                }
            case "dardo":
                {
                    Muerte();
                    break;
                }
        }
    }

    public void Muerte() // se podria agregar un parametro para distintas animaciones
    {
        if (!Muerto)
        {
            animator.SetTrigger("Muere");
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            transform.GetComponent<CapsuleCollider2D>().enabled = false;
            Muerto = true;
        }
    }
}
