using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastilloJugador : MonoBehaviour
{
    public int vidaMaxima;
    public int vida;
    public int defensaMaxima;
    public int defensa;

    void Start()
    {
        vida = vidaMaxima;
    }

    public void PerderVidaDefensa(int daño)
    {
        if(daño > defensa)
        {
            daño -= defensa;
            vida -= daño;
        }
        else if(daño < defensa)
        {
            defensa -= daño;
        }
    }
}
