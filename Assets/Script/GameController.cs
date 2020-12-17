using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Panel comprar torretas
    Animator animacionPanelTorretas;
    //Panel levelUp torretas
    Animator animacionPanelUpTorretas;
    bool desplegadoUpTorreta = false;
    public int identificadorDeTorretas;
    //Panel Pausa
    public GameObject panelPausa;
    //Castillo del jugador
    CastilloJugador castilloJugador;
    //Torretas
    [Header("Torretas En Escena")]
    public List<GameObject> torretasEnEscena;
    public int numeroTorreta;
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
    List<int> precios = new List<int> { 0, 0, 0, 0 };
    [SerializeField]
    List<int> precioSubirNivelTorSim = new List<int> { };
    [SerializeField]
    List<int> precioSubirNivelTorMul = new List<int> { };
    [SerializeField]
    List<int> precioSubirNivelTorSop = new List<int> { };
    [SerializeField]
    List<int> precioSubirNivelTorEsc = new List<int> { };
    int precioFinalSubirNivel;
    [Header("Imagenes Botones Torretas...")]
    [SerializeField]
    Image imagenBotonDePanelTorretas;
    [SerializeField]
    Image imagenBotonSubirNivel;
    [SerializeField]
    Image imagenBotonDestruir;
    [SerializeField]
    Sprite[] imagenesTorretas = new Sprite[4];
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
    [SerializeField]
    Text textoPrecioSubirNivel;
    //Padre Torretas
    [Header("Padre Torretas")]
    public GameObject padreTorretas;
    //LayerBases
    [Header("LayerBases")]
    [SerializeField]
    LayerMask _bases;
    void Start()
    {
        numeroTorreta = 0;
        DarValorAlasTorretas();
        animacionPanelTorretas = GameObject.Find("PanelTorretas").GetComponent<Animator>();
        animacionPanelUpTorretas = GameObject.Find("UPTorretas").GetComponent<Animator>();
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

    public void ActivarDesactivarDespliegue()
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

    public void ActivarDesactivarPanelUpTorretas()
    {
        if (desplegadoUpTorreta)
        {
            desplegadoUpTorreta = false;
            animacionPanelUpTorretas.SetBool("Desplegado", false);
            Invoke("DejarVerBotonPanelTorretas", 0.2f);
        }
        else
        {
            desplegadoUpTorreta = true;
            animacionPanelUpTorretas.SetBool("Desplegado", true);
            imagenBotonDePanelTorretas.enabled = false;

            if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().tipo == TipoTorreta.Simple)
            {
                imagenBotonSubirNivel.sprite = imagenesTorretas[0];
                imagenBotonDestruir.sprite = imagenesTorretas[0];
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel1)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSim[0].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSim[0];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel2)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSim[1].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSim[1];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel3)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSim[2].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSim[2];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel4)
                {
                    textoPrecioSubirNivel.text = "∞∞";
                    precioFinalSubirNivel = oro + 2;
                }
            }
            if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().tipo == TipoTorreta.Multiple)
            {
                imagenBotonSubirNivel.sprite = imagenesTorretas[1];
                imagenBotonDestruir.sprite = imagenesTorretas[1];
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel1)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorMul[0].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorMul[0];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel2)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorMul[1].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorMul[1];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel3)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorMul[2].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorMul[2];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel4)
                {
                    textoPrecioSubirNivel.text = "∞∞";
                    precioFinalSubirNivel = oro + 2;
                }
            }
            if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().tipo == TipoTorreta.Soplete)
            {
                imagenBotonSubirNivel.sprite = imagenesTorretas[2];
                imagenBotonDestruir.sprite = imagenesTorretas[2];
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel1)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSop[0].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSop[0];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel2)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSop[1].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSop[1];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel3)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorSop[2].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorSop[2];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel4)
                {
                    textoPrecioSubirNivel.text = "∞∞";
                    precioFinalSubirNivel = oro + 2;
                }
            }
            if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().tipo == TipoTorreta.Escarcha)
            {
                imagenBotonSubirNivel.sprite = imagenesTorretas[3];
                imagenBotonDestruir.sprite = imagenesTorretas[3];
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel1)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorEsc[0].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorEsc[0];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel2)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorEsc[1].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorEsc[1];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel3)
                {
                    textoPrecioSubirNivel.text = precioSubirNivelTorEsc[2].ToString();
                    precioFinalSubirNivel = precioSubirNivelTorEsc[2];
                }
                if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel4)
                {
                    textoPrecioSubirNivel.text = "∞∞";
                    precioFinalSubirNivel = oro + 2;
                }
            }
        }
    }

    public void SubirNivelTorretas()
    {
        if(oro >= precioFinalSubirNivel)
        {
            if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel1)
            {
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().nivel++;
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel = NivelTorreta.Nivel2;
            }
            else if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel2)
            {
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().nivel++;
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel = NivelTorreta.Nivel3;
            }
            else if (torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel == NivelTorreta.Nivel3)
            {
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().nivel++;
                torretasEnEscena[identificadorDeTorretas].GetComponent<Torretas>().decirNivel = NivelTorreta.Nivel4;
            }
            oro -= precioFinalSubirNivel;
        }
        else
        {
            print("te falta oro");
        }
    }

    public void DestruirTorreta()
    {
        Destroy(torretasEnEscena[identificadorDeTorretas]);
        torretasEnEscena.Remove(torretasEnEscena[identificadorDeTorretas]);
        desplegadoUpTorreta = false;
        animacionPanelUpTorretas.SetBool("Desplegado", false);
        Invoke("DejarVerBotonPanelTorretas", 0.2f);
    }

    void DejarVerBotonPanelTorretas()
    {
        imagenBotonDePanelTorretas.enabled = true;
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
#if UNITY_EDITOR
       
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }
}

public enum TipoTorreta { Simple, Multiple, Soplete, Escarcha };
public enum NivelTorreta { Nivel1, Nivel2, Nivel3, Nivel4 };
