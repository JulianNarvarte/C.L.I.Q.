using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Llamado : MonoBehaviour
{
    [HideInInspector] public Habitacion habitacion;
    public Jugador jugador;
    
    public void DestruirHabitacion()
    {
        if (!jugador.Muerto)
        {
            habitacion = GetComponentInParent<Habitacion>();
            //Debug.Log("padre actual " + habitacion.name);
            habitacion.VolverABase();
        }
    }

    public void ChkMuerte()
    {
        if (jugador.Valor <= 0)
        {
            jugador.Muerto = true;
            jugador.Accion("Muere");
        }
    }
}
