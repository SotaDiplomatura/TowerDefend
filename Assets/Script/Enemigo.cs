using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    Rigidbody myRb;
    CastilloJugador castillo;
    GameController gameController;

    public Transform[] puntosRuta;
    [SerializeField]
    int daño;
    [SerializeField]
    float vida;
    [SerializeField]
    float velocidad;
    float velocidadOriginal;
    [SerializeField]
    float distanciaCambioPunto;
    [SerializeField]
    int oroQueDa;

    int puntoSiguiente;

    Vector3 distanciaAlSiguientePunto;
    void Start()
    {
        puntoSiguiente = 0;
        velocidadOriginal = velocidad;
        myRb = GetComponent<Rigidbody>();
        castillo = GameObject.Find("CastilloDelJugador").GetComponent<CastilloJugador>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        for(int i=0;i<=puntosRuta.Length-1;i++)
        {
            puntosRuta[i] = GameObject.Find("Punto" + i).GetComponent<Transform>();
        }
    }

    void Update()
    {
        AsignarPuntoRuta();
        MovimientoYMirada();
    }

    void AsignarPuntoRuta()
    {
        distanciaAlSiguientePunto = transform.position - puntosRuta[puntoSiguiente].position;
        if (distanciaAlSiguientePunto.magnitude < distanciaCambioPunto)
        {
            puntoSiguiente++;
        }
    }
    void MovimientoYMirada()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntosRuta[puntoSiguiente].position,velocidad * 5 * Time.deltaTime);
        transform.LookAt(puntosRuta[puntoSiguiente]);
    }

    public void RecivirDaño(float daño)
    {
        vida -= daño;
        if(vida <= 0)
        {
            DarOro();
            Destroy(gameObject);
        }
    }

    public void Relentizar(float relentizar)
    {
        velocidad /= relentizar;
    }
    public void RecuperarVelocidad()
    {
        velocidad = velocidadOriginal;
    }

    void DarOro()
    {
        gameController.oro += oroQueDa;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Castillo"))
        {
            castillo.PerderVidaDefensa(daño);
            Destroy(gameObject);
        }
    }
}
