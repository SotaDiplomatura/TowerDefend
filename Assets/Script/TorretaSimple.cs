using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaSimple : MonoBehaviour
{
    public TipoTorreta tipo;
    Transform cabezaTorreta;
    Transform cañon;
    Transform objetivo;

    [SerializeField]
    float velocidadAtaque;
    [SerializeField]
    float tiempoDisparando;
    [SerializeField]
    float cadenciaDisparo;
    [SerializeField]
    float daño;
    [SerializeField]
    float rango;

    [SerializeField]
    float resetearObjetivo;

    [Header("Balas")]
    [SerializeField]
    GameObject bala;
    bool disparado;
    void Start()
    {
        cadenciaDisparo = 0;
        cabezaTorreta = transform.GetChild(1).GetComponent<Transform>();
        cañon = cabezaTorreta.GetChild(0).GetComponent<Transform>();
        InvokeRepeating("ActualizarObjetivo", 0, resetearObjetivo);
    }

    void ActualizarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        float distanciaAlEnemigoMasCercano = Mathf.Infinity;
        GameObject enemigoObjetivo = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distanciaAlEnemigo = Vector3.Distance(transform.position, enemigo.transform.position);

            if(distanciaAlEnemigo < distanciaAlEnemigoMasCercano)
            {
                distanciaAlEnemigoMasCercano = distanciaAlEnemigo;
                enemigoObjetivo = enemigo;
            }
        }

        if (enemigoObjetivo != null && distanciaAlEnemigoMasCercano <= rango)
        {
            objetivo = enemigoObjetivo.transform;
        }
        else
        {
            objetivo = null;
        }
    }

    void Update()
    {
        if(objetivo == null)
        {
            return;
        }
        MirarObjetivo();
        Atacando();
    }

    void MirarObjetivo()
    {
        cabezaTorreta.LookAt(new Vector3(objetivo.position.x,cabezaTorreta.position.y,objetivo.position.z));
    }

    void Atacando()
    {
        if(cadenciaDisparo <= 0)
        {
            if(tipo == TipoTorreta.Simple)
            {
                CrearBalaSimple();
            }
            if (tipo == TipoTorreta.Multiple)
            {
                CrearBalaMultiple();
            }
            if (tipo == TipoTorreta.Soplete)
            {
                CrearSoplete();
            }
            if (tipo == TipoTorreta.Latigo)
            {
                CrearBalaLatigo();
            }
            cadenciaDisparo = 1f / velocidadAtaque;
        }
        cadenciaDisparo -= Time.deltaTime;
    }

    void CrearBalaSimple()
    {
        bala.gameObject.GetComponent<Bala>().objetivo = objetivo;
        Instantiate(bala, cañon.position, Quaternion.identity);
    }
    void CrearBalaMultiple()
    {

    }
    void CrearSoplete()
    {
        bala.gameObject.GetComponent<BalaSoplete>().tiempoDisparando = tiempoDisparando;
        Instantiate(bala, cañon.position, cañon.rotation,cañon);
    }
    void CrearBalaLatigo()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}

public enum TipoTorreta {Simple, Multiple,Soplete,Latigo};
