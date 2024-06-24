using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caballero : MonoBehaviour
{
    public float velX;
    public float velY;
    [SerializeField] private LayerMask Paredes, suelo;
    private bool enSuelo;
    public bool muerto;
    private bool Ganado;
    private Rigidbody2D rb;
    private Animator animator;
    public string SigNivel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!muerto && !Ganado)
        {    
            //si el jugador esta en el suelo y no hay paredes en frente
            Caminar();
            //si esta en contacto con el suelo
            caer();
        }
    }
    
    private void Caminar()
    {
        Ray ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.magenta);
        //Debug.DrawRay(ray.origin, ray.direction * 3f, Color.green);

        if (!Physics2D.Raycast(transform.position,transform.right , 2f, Paredes) && enSuelo)
        {
            rb.AddForce(new Vector2(velX * Time.deltaTime, rb.velocity.y * Time.deltaTime));
            animator.SetBool("Camina", true);
        }
        else if(enSuelo)
        {
            rb.velocity = new Vector2(0f, 0f);
            animator.SetBool("Camina", false);
        }
    }

    private void caer()
    {
        //Ray ray = new Ray(new Vector3(transform.position.x-0.6f, transform.position.y-0.75f, transform.position.z), Vector2.right);
        //Debug.DrawRay(ray.origin, ray.direction * 1.2f);

        RaycastHit2D rayo = Physics2D.Raycast(new Vector2(transform.position.x - 0.6f, transform.position.y - 0.75f), Vector2.right, 1.2f, suelo);


        if  (rayo)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            
            case "pinchos":
                {
                    SetMuerte();
                    break;
                }
            //case "agua":
            //    {
            //        SetMuerte();
            //        break;
            //    }
            case "lava":
                {
                    SetMuerte();
                    break;
                }
            case "dardo":
                {
                    SetMuerte();
                    break;
                }
            case "explosivo":
                {
                    SetMuerte();
                    break;
                }
            case "Finish":
                {
                    collision.GetComponent<Animator>().SetTrigger("Abrir");
                    Ganado = true;
                    animator.SetBool("Camina", false);
                    FindObjectOfType<UI_Manager>().GetComponent<UI_Manager>().HGT_levels(SigNivel);
                    break;
                }
                //default:
                //    {
                //        Debug.Log("no mata al jugador");
                //        break;
                //    }
        }
    }

    public void SetMuerte()
    {
        if (!muerto)
        {
            muerto = true;
            animator.SetTrigger("Morir");//morir se puede cambiar por forma y habria distantas animaciones
            rb.velocity = new Vector2(0f, 0f);
            //set trigger Forma
        }
    }
}
