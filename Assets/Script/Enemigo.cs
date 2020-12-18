using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    Rigidbody myRb;
    Collider myCollider;
    Renderer myRenderer;
    CastilloJugador castillo;
    GameController gameController;

    [SerializeField]
    EstiloEnemigo estiloEnemigo;
    [SerializeField]
    bool elite;
    [SerializeField]
    float tiempoQueDuraHabilidad;
    [SerializeField]
    float tiempoCargaHabilidad;

    [SerializeField]
    public Transform[] puntosRuta;
    [SerializeField]
    int daño;
    [SerializeField]
    float vida;
    [SerializeField]
    float velocidad;
    public float duracionRelentizado;
    float velocidadOriginal;
    [SerializeField]
    float distanciaCambioPunto;
    [SerializeField]
    int oroQueDa;

    int puntoSiguiente;
    
    Vector3 distanciaAlSiguientePunto;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myRenderer = GetComponent<Renderer>();
        if (elite)
        {
            AjustarEstadicticasDeElite();
        }
        puntoSiguiente = 0;
        velocidadOriginal = velocidad;
        castillo = GameObject.Find("CastilloDelJugador").GetComponent<CastilloJugador>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        for(int i=0;i<=puntosRuta.Length-1;i++)
        {
            puntosRuta[i] = GameObject.Find("Punto" + i).GetComponent<Transform>();
        }
        gameController.enemigosEnEscena++;
    }

    void AjustarEstadicticasDeElite()
    {
        if(estiloEnemigo == EstiloEnemigo.Fuerte)
        {
            daño *= 2;
            vida *= 2;
            oroQueDa *= 3;
            InvokeRepeating("IniciarHabilidadInvulnerabilidad", tiempoCargaHabilidad, tiempoCargaHabilidad);
        }
        else if(estiloEnemigo == EstiloEnemigo.Rapido)
        {
            vida *= 2;
            velocidad *= 1.25f;
            oroQueDa *= 3;
            InvokeRepeating("IniciarHabilidadDash", tiempoCargaHabilidad, tiempoCargaHabilidad);
        }
        else
        {
            distanciaCambioPunto = 1.015f;
            daño *= 2;
            vida *= 2;
            velocidad /= 2;
            oroQueDa *= 3;
            Invoke("IniciarHabilidadTanque",0);
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(puntosRuta[puntoSiguiente].position.x, transform.position.y, puntosRuta[puntoSiguiente].position.z),velocidad * 5 * Time.deltaTime);
        transform.LookAt(new Vector3(puntosRuta[puntoSiguiente].position.x, transform.position.y, puntosRuta[puntoSiguiente].position.z));
    }

    public void RecivirDaño(float daño)
    {
        vida -= daño;
        if(vida <= 0)
        {
            DarOro();
            gameController.puntos++;
            gameController.enemigosEnEscena--;
            Destroy(gameObject);
        }
    }

    public void Relentizar(float relentizar)
    {
        velocidad /= relentizar;
        Invoke("RecuperarVelocidad", duracionRelentizado);
    }
    public void RecuperarVelocidad()
    {
        velocidad = velocidadOriginal;
    }

    void DarOro()
    {
        gameController.oro += oroQueDa;
    }

    void IniciarHabilidadInvulnerabilidad()
    {
        StartCoroutine("Invulnerable");
    }
    IEnumerator Invulnerable()
    {
        Color colorMaterial = myRenderer.material.color;
        myCollider.enabled = false;
        colorMaterial.a = 0.5f;
        myRenderer.material.color = colorMaterial;
        yield return new WaitForSeconds(tiempoQueDuraHabilidad);
        colorMaterial.a = 1f;
        myRenderer.material.color = colorMaterial;
        myCollider.enabled = true;
        print("pp");
        StopCoroutine("Invulnerable");
    }

    void IniciarHabilidadDash()
    {
        StartCoroutine("Dash");
    }
    IEnumerator Dash()
    {
        transform.localScale /= 2;
        velocidad *= 2f;
        yield return new WaitForSeconds(tiempoQueDuraHabilidad);
        transform.localScale *= 2;
        velocidad /= 2f;
        StopCoroutine("Dash");
    }

    void IniciarHabilidadTanque()
    {
        transform.localScale *= 2;
        daño *= 2;
        vida *= 2;
        velocidad *= 2;
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Castillo"))
        {
            castillo.PerderVidaDefensa(daño);
            gameController.enemigosEnEscena--;
            Destroy(gameObject);
        }
    }
}
