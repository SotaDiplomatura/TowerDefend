using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretas : MonoBehaviour
{
    public TipoTorreta tipo;
    public NivelTorreta decirNivel;
    public int identificador;

    GameController gameController;
    Transform cabezaTorreta;
    Transform cañon;

    [SerializeField]
    List<Enemigo> _enemigos = null;
    [SerializeField]
    List<GameObject> enemigosEnRango = null;
    [SerializeField]
    List<Transform> objetivo = null;

    [Header("Variables para torreta simple, multiple y soplete")]
    [SerializeField]
    float daño;

    [Header("Variables exclusiva torreta multiple")]
    [SerializeField]
    int enemigosMaximos;

    [Header("Variables para torreta soplete y escarcha")]
    [SerializeField]
    float tiempoDisparando;

    [Header("Variables exclusiva torreta escarcha")]
    [SerializeField]
    float relentizar;

    [Header("Variables Comunes")]
    [SerializeField]
    float velocidadAtaque;
    [SerializeField]
    float rango;
    [SerializeField]
    float comprobarObjetivo;

    float cadenciaDisparo;
    public int nivel;
    int nivelActual;

    [Header("Balas")]
    [SerializeField]
    GameObject bala;
    bool disparado;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.numeroTorreta++;
        gameController.torretasEnEscena.Add(gameObject);
        nivelActual = nivel;
        enemigosEnRango = new List<GameObject>();
        objetivo = new List<Transform>();
        cadenciaDisparo = 0;
        cabezaTorreta = transform.GetChild(1).GetComponent<Transform>();
        cañon = cabezaTorreta.GetChild(0).GetComponent<Transform>();
        InvokeRepeating("ActualizarObjetivo", 0, comprobarObjetivo);
    }

    void ActualizarObjetivo()
    {
        _enemigos = new List<Enemigo>(FindObjectsOfType<Enemigo>());
        foreach (Enemigo en in _enemigos)
        {

            float distanciaAlEnemigo = Vector3.Distance(transform.position, en.transform.position);

            if (distanciaAlEnemigo < rango)
            {
                if(!enemigosEnRango.Contains(en.gameObject))
                {
                    enemigosEnRango.Add(en.gameObject);
                    objetivo.Add(en.gameObject.transform);
                }
            }
        }
        foreach (Enemigo en in _enemigos)
        {
            if(Vector3.Distance(en.gameObject.transform.position,transform.position)>rango)
            {
                enemigosEnRango.Remove(en.gameObject);
                objetivo.Remove(en.gameObject.transform);
            }
        }      
    }

    void Update()
    {
        if (objetivo.Count <=0)
        {
            return;
        }
        if(objetivo[0] == null || enemigosEnRango[0] == null || _enemigos[0] == null)
        {
            objetivo.Remove(objetivo[0]);
            enemigosEnRango.Remove(enemigosEnRango[0]);
            _enemigos.Remove(_enemigos[0]);
            return;
        }
        if(nivel != nivelActual)
        {
            nivelActual = nivel;
            AjustarPorNivel();
        }
        MirarObjetivo();
        Atacando();
    }

    void AjustarPorNivel()
    {
        //Level 1
        if (decirNivel == NivelTorreta.Nivel1 && tipo == TipoTorreta.Simple)
        {
            daño = 1;
            velocidadAtaque = 1;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel1 && tipo == TipoTorreta.Multiple)
        {
            daño = 0.5f;
            velocidadAtaque = 1;
            enemigosMaximos = 2;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel1 && tipo == TipoTorreta.Soplete)
        {
            daño = 1.5f;
            tiempoDisparando = 2;
            velocidadAtaque = 1 / 3;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel1)
        {
            tiempoDisparando = 2;
            relentizar = 1.5f;
            velocidadAtaque = 1 / 3;
            rango = 7;
        }
        //Level 2
        if (decirNivel == NivelTorreta.Nivel2 && tipo == TipoTorreta.Simple)
        {
            daño = 2;
            velocidadAtaque = 1;
            rango = 8;
        }
        else if (decirNivel == NivelTorreta.Nivel2 && tipo == TipoTorreta.Multiple)
        {
            daño = 0.67f;
            velocidadAtaque = 1;
            enemigosMaximos = 3;
            rango = 8;
        }
        else if (decirNivel == NivelTorreta.Nivel2 && tipo == TipoTorreta.Soplete)
        {
            daño = 3f;
            tiempoDisparando = 3;
            velocidadAtaque = 1 / 4;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel2)
        {
            tiempoDisparando = 2;
            relentizar = 2f;
            velocidadAtaque = 1 / 3;
            rango = 7;
        }
        //Level 3
        if (decirNivel == NivelTorreta.Nivel3 && tipo == TipoTorreta.Simple)
        {
            daño = 3;
            velocidadAtaque = 2;
            rango = 8;
        }
        else if (decirNivel == NivelTorreta.Nivel3 && tipo == TipoTorreta.Multiple)
        {
            daño = 1.5f;
            velocidadAtaque = 1;
            enemigosMaximos = 4;
            rango = 8;
        }
        else if (decirNivel == NivelTorreta.Nivel3 && tipo == TipoTorreta.Soplete)
        {
            daño = 7f;
            tiempoDisparando = 4;
            velocidadAtaque = 1 / 5;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel3)
        {
            tiempoDisparando = 3;
            relentizar = 2f;
            velocidadAtaque = 1 / 4;
            rango = 7;
        }
        //Level 4
        if (decirNivel == NivelTorreta.Nivel4 && tipo == TipoTorreta.Simple)
        {
            daño = 5;
            velocidadAtaque = 4;
            rango = 9;
        }
        else if (decirNivel == NivelTorreta.Nivel4 && tipo == TipoTorreta.Multiple)
        {
            daño = 6f;
            velocidadAtaque = 1.5f;
            enemigosMaximos = 5;
            rango = 8;
        }
        else if (decirNivel == NivelTorreta.Nivel4 && tipo == TipoTorreta.Soplete)
        {
            daño = 14f;
            tiempoDisparando = 5;
            velocidadAtaque = 1 / 5;
            rango = 7;
        }
        else if (decirNivel == NivelTorreta.Nivel4)
        {
            tiempoDisparando = 3;
            relentizar = 3f;
            velocidadAtaque = 1 / 4;
            rango = 7;
        }
    }

    void MirarObjetivo()
    {
        cabezaTorreta.LookAt(new Vector3(objetivo[0].position.x,cabezaTorreta.position.y,objetivo[0].position.z));
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
            if (tipo == TipoTorreta.Escarcha)
            {
                CrearEscarcha();
            }
            cadenciaDisparo = 1f / velocidadAtaque;
        }
        cadenciaDisparo -= Time.deltaTime;
    }

    void CrearBalaSimple()
    {
        bala.gameObject.GetComponent<Bala>().objetivo = objetivo[0];
        bala.gameObject.GetComponent<Bala>().daño = daño;
        Instantiate(bala, cañon.position, Quaternion.identity);
    }
    void CrearBalaMultiple()
    {
        if(objetivo.Count < 5)
        {
            for (int i = 0; i < objetivo.Count; i++)
            {
                bala.gameObject.GetComponent<Bala>().daño = daño;
                bala.gameObject.GetComponent<Bala>().objetivo = objetivo[i];
                Instantiate(bala, cañon.position, Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < enemigosMaximos; i++)
            {
                bala.gameObject.GetComponent<Bala>().daño = daño;
                bala.gameObject.GetComponent<Bala>().objetivo = objetivo[i];
                Instantiate(bala, cañon.position, Quaternion.identity);
            }
        }
    }
    void CrearSoplete()
    {
        bala.gameObject.GetComponent<BalaSoplete>().dañoPorSegundo = daño;
        bala.gameObject.GetComponent<BalaSoplete>().tiempoDisparando = tiempoDisparando;
        Instantiate(bala, cañon.position, cañon.rotation,cañon);
    }
    void CrearEscarcha()
    {
        bala.gameObject.GetComponent<BalaEscarcha>().relentizar = relentizar;
        bala.gameObject.GetComponent<BalaEscarcha>().tiempoDisparando = tiempoDisparando;
        Instantiate(bala, cañon.position, cañon.rotation, cañon);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}

