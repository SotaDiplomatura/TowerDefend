using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMenu : MonoBehaviour
{
    CambiaScena cambioEscena;
    AudioSource audioBotones;

    void Start()
    {
        cambioEscena = GameObject.Find("ControlScenas").GetComponent<CambiaScena>();
        audioBotones = GetComponent<AudioSource>();
    }

    public void BotonJugar()
    {
        audioBotones.Play();
        
        Invoke("Jugar",1);
    }

    void Jugar()
    {
        cambioEscena.Jugar();
    }

    public void BotonTutorial()
    {
        audioBotones.Play();

        Invoke("Tutorial", 1);
    }

    void Tutorial()
    {
        cambioEscena.Tutorial();
    }

    public void BotonSalir()
    {
        audioBotones.Play();

        Invoke("Salir", 1);
    }

    void Salir()
    {
        cambioEscena.Salir();
    }
}
