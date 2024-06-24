using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Habitacion : MonoBehaviour, IDropHandler
{
    
    
    public int valor;
    public string Operacion;

    private TMP_Text txtValor;
    private Animator animator;

    private Jugador jugador;
    //public Anim_Llamado animaciones;

    public GameObject Suma, Resta, Multiplicacion, Division, Enemigo;


    private void Start()
    {
        SettearHabitacion();
        EncontrarJugador();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("drop");
        if (transform.childCount == 1)
        {
            //GameObject drop = eventData.pointerDrag;
            //Jugador jugador = drop.GetComponent<Jugador>();
            jugador.Padre = transform;
            //Debug.Log(jugador.Padre);
            //animaciones.habitacion = transform.GetComponent<Habitacion>();
            //Debug.Log(animaciones.habitacion);

            if (Operacion == "Enemigo")
            {
                if (jugador.Valor > valor)
                {
                    //Debug.Log("Gana el jugador");

                    AccionYAnimacion("Gana");

                }
                else
                {
                    //Debug.Log("Pierde el jugador");

                    AccionYAnimacion("Pierde");
                }
            }
            else 
            {
                AccionYAnimacion("Operacion");
            }
        }
    }

    void SettearHabitacion()
    {
        
        
        
        switch (Operacion)
        {

            case "+": //Escudo
                {
                    Instancia_Op(Suma);
                    break;
                }

            case "-": //Mujer tira estrella
                {
                    Instancia_Op(Resta);
                    break;
                }

            case "*": //Altar 
                {
                    Instancia_Op(Multiplicacion);
                    break;
                }

            case "/": //Mujer da manzana
                {
                    Instancia_Op(Division);
                    break;
                }

            case "Enemigo": //Enemigo
                {
                    Instancia_Op(Enemigo);
                    break;
                }
            default:
                {
                    //Se destruye la habitacion para evitar errores
                    Destroy(gameObject);
                    break; 
                }
        }
    }

    void Instancia_Op(GameObject instanciado)
    {
        Instantiate(instanciado, transform);
        txtValor = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        //Mostrar valores habitaciones
        if (Operacion != "Enemigo")
        {
            txtValor.text = Operacion.ToString() + " " + valor.ToString();
        }
        else
        {
            txtValor.text = valor.ToString();
        }
    }

    void AccionYAnimacion(string Accion)
    {
        switch (Accion)
        {
            case "Gana":
                {
                    jugador.Valor += valor;
                    jugador.txtValor.text = jugador.Valor.ToString();
                    jugador.Accion("Ataca");
                    animator.SetTrigger("Pierde"); //Pierde enemigo

                    break;
                }

            case "Pierde":
                {
                    jugador.Accion("Muere");
                    animator.SetTrigger("Gana"); //Gana enemigo
                    break;
                }

            case "Operacion":
                {
                    switch (Operacion)
                    {
                        case "+":
                            {
                                jugador.Valor += valor;
                                jugador.txtValor.text = jugador.Valor.ToString();
                                jugador.Accion("Salta");
                                break;
                            }
                        case "-":
                            {
                                jugador.Valor -= valor;
                                jugador.txtValor.text = jugador.Valor.ToString();
                                jugador.Accion("Sufre");
                                animator.SetTrigger("Accion");


                                break;
                            }
                        case "*":
                            {
                                jugador.Valor *= valor;
                                jugador.txtValor.text = jugador.Valor.ToString();
                                jugador.Accion("Patea");
                                animator.SetTrigger("Accion");
                                break;
                            }
                        case "/":
                            {
                                if(valor != 0)
                                {
                                    jugador.Valor /= valor;
                                    jugador.txtValor.text = jugador.Valor.ToString();
                                    jugador.Accion("Come");
                                    animator.SetTrigger("Accion");
                                }
                                break;
                            }
                    }
                    break;
                }
        }
    }

    public void VolverABase()
    {
        jugador.transform.SetParent(jugador.PadreInicial);
        Destroy(gameObject);
    }

    void EncontrarJugador()
    {
        jugador = FindObjectOfType<Jugador>().GetComponent<Jugador>();
    }

    private void OnDestroy()
    {
        jugador.ChkTorres();
    }
}