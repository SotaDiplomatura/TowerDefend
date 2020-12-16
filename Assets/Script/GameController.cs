using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Animator animacionPanelTorretas;
    CastilloJugador castilloJugador;
    public GameObject panelPausa;
    //Estados
    [Header("Estados")]
    public bool desplegando;
    public bool pausa;
    //Vida Y Defensa
    [Header("Vida y Defensa")]
    public Slider sliderVida;
    public Slider sliderDefensa;
    //Defensas Comprar...
    [SerializeField]
    int valorDefensor;
    [SerializeField]
    Text textoValorDefensor;
    //Oro
    [Header("Oro")]
    public int oro;
    [SerializeField]
    Text textoOro;
    //Torretas Seleccionadas
    bool boolTorretaSimple;
    bool boolTorretaMultiple;
    bool boolTorretaSoplete;
    bool boolTorretaLatigo;
    int torretaSelecionada;
    //Torretas Prefas Y Precio
    [Header("Torretas Prefas Y Precio")]
    public GameObject TorretaSimple;
    public GameObject TorreraMultiple;
    public GameObject TorretaSoplete;
    public GameObject TorretaLatigo;
    [SerializeField]
    int precioToSimple;
    [SerializeField]
    int precioToMultiple;
    [SerializeField]
    int precioToSoplete;
    [SerializeField]
    int precioToLatigo;
    List<int> precios = new List<int> {0,0,0,0};
    //Texto Precio Torretas
    [Header("Texto Precio Torretas")]
    [SerializeField]
    Text textoPrecioToSimple;
    [SerializeField]
    Text textoPrecioToMultiple;
    [SerializeField]
    Text textoPrecioToSoplete;
    [SerializeField]
    Text textoPrecioToLatigo;
    //Padre Torretas
    [Header("Padre Torretas")]
    public GameObject padreTorretas;
    //LayerBases
    [Header("LayerBases")]
    [SerializeField]
    LayerMask _bases;
    void Start()
    {
        DarValorAlasTorretas();
        animacionPanelTorretas = GameObject.Find("PanelTorretas").GetComponent<Animator>();
        castilloJugador = GameObject.Find("CastilloDelJugador").GetComponent<CastilloJugador>();
    }

    void DarValorAlasTorretas()
    {
        precios[0] = precioToSimple;
        precios[1] = precioToMultiple;
        precios[2] = precioToSoplete;
        precios[3] = precioToLatigo;
        textoPrecioToSimple.text = precioToSimple.ToString();
        textoPrecioToMultiple.text = precioToMultiple.ToString();
        textoPrecioToSoplete.text = precioToSoplete.ToString();
        textoPrecioToLatigo.text = precioToLatigo.ToString();
    }

    void Update()
    {
        Estados();
        RecuentoDeVidaYDefensa();
        ValorDefensa();
        ActualizarOro();
    }

    void Estados()
    {
        if(desplegando)
        {
            ColocarTorretas();
        }
        else
        {
            DeseleccionarTorretas();
        }
        if(pausa)
        {
            Time.timeScale = 0;
            panelPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            panelPausa.SetActive(false);
        }
    }

    void RecuentoDeVidaYDefensa()
    {
        sliderVida.maxValue = castilloJugador.vidaMaxima;
        sliderDefensa.maxValue = castilloJugador.defensaMaxima;
        sliderVida.value = castilloJugador.vida;
        sliderDefensa.value = castilloJugador.defensa;
    }

    void ValorDefensa()
    {
        if(castilloJugador.defensa < 1)
        {
            valorDefensor = 10;
            textoValorDefensor.text = valorDefensor.ToString();
        }
        else if(castilloJugador.defensa >= 100)
        {
            valorDefensor = oro + 2;
            textoValorDefensor.text = "∞∞";
        }
        else
        {
            valorDefensor = castilloJugador.defensa * 10;
            textoValorDefensor.text = valorDefensor.ToString();
        }

    }

    void ActualizarOro()
    {
        textoOro.text = oro.ToString();
    }

    void ColocarTorretas()
    {
        RaycastHit ray;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direcion =new Vector3(0, -43f, 11f);
        //Debug.DrawRay(mousePosition, direcion * 100, Color.red, 10);
        if (boolTorretaSimple && Input.GetMouseButtonDown(0))
        { 
            if (Physics.Raycast(mousePosition, direcion, out ray,20000,_bases) && ray.collider.gameObject.GetComponent<BaseTorretas>().disponible)
            {
                InstanciarTorreta(TorretaSimple,ray.collider.transform.position,padreTorretas);
            }
            boolTorretaSimple = false;
        }
        if (boolTorretaMultiple && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePosition, direcion, out ray, 20000, _bases) && ray.collider.gameObject.GetComponent<BaseTorretas>().disponible)
            {
                InstanciarTorreta(TorreraMultiple, ray.collider.transform.position, padreTorretas);
            }
            boolTorretaMultiple = false;
        }
        if (boolTorretaSoplete && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePosition, direcion, out ray, 20000, _bases) && ray.collider.gameObject.GetComponent<BaseTorretas>().disponible)
            {
                InstanciarTorreta(TorretaSoplete, ray.collider.transform.position, padreTorretas);
            }
            boolTorretaSoplete = false;
        }
        if (boolTorretaLatigo && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePosition, direcion, out ray, 20000, _bases) && ray.collider.gameObject.GetComponent<BaseTorretas>().disponible)
            {
                InstanciarTorreta(TorretaLatigo, ray.collider.transform.position, padreTorretas);
            }
            boolTorretaLatigo = false;
        }
    }

    void InstanciarTorreta(GameObject Torreta, Vector3 baseTorreta, GameObject pTorretas)
    {
        if (oro >= precios[torretaSelecionada])
        {
            Instantiate(Torreta, baseTorreta, Quaternion.identity, pTorretas.transform);
            oro -= precios[torretaSelecionada];
        }
        else
        {
            print("te falta oro");
        }

    }

    void DeseleccionarTorretas()
    {
        boolTorretaSimple = false;
        boolTorretaMultiple = false;
        boolTorretaSoplete = false;
        boolTorretaLatigo = false;
    }

    public void ComprarDefensa()
    {
        if(oro >= valorDefensor)
        {
            castilloJugador.defensa++;
            oro -= valorDefensor;
        }
    }

    public void ActivarDesactivarDespliegueDespliegue()
    {
        if(desplegando)
        {
            desplegando = false;
            animacionPanelTorretas.SetBool("Desplegado", false);
        }
        else
        {
            desplegando = true;
            animacionPanelTorretas.SetBool("Desplegado", true);
        }

    }

    public void Pausar()
    {
        if(pausa)
        {
            pausa = false;
        }
        else
        {
            pausa = true;
        }
    }

    public void TorretaSimpleSelecionada()
    {
        if(boolTorretaSimple)
        {
            boolTorretaSimple = false;
        }
        else
        {
            torretaSelecionada = 0;
            boolTorretaSimple = true;
        }
    }

    public void TorretaMultipleSelecionada()
    {
        if(boolTorretaMultiple)
        {
            boolTorretaMultiple = false;
        }
        else
        {
            torretaSelecionada = 1;
            boolTorretaMultiple = true;
        }
    }

    public void TorretaSopleteSelecionada()
    {
        if (boolTorretaSoplete)
        {
            boolTorretaSoplete = false;
        }
        else
        {
            torretaSelecionada = 2;
            boolTorretaSoplete = true;
        }
    }

    public void TorretaLatigoSelecionada()
    {
        if (boolTorretaLatigo)
        {
            boolTorretaLatigo = false;
        }
        else
        {
            torretaSelecionada = 3;
            boolTorretaLatigo = true;
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
