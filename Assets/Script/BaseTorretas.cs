using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTorretas : MonoBehaviour
{
    GameController gameController;
    MeshRenderer disponiblidadBase;

    public bool colocando;

    [SerializeField]
    Material baseDisponible;
    [SerializeField]
    Material baseOcupada;

    public bool disponible;
    [SerializeField]
    float distanciaDetectarTorreta;

    [SerializeField]
    LayerMask torreta;


    void Start()
    {
        disponiblidadBase = transform.GetChild(0).GetComponent<MeshRenderer>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    
    void Update()
    {
        DetectarColocando();
    }

    void DetectarColocando()
    {
        colocando = gameController.desplegandoTorretas;
        if(colocando)
        {
            DetectarDisponible();
        }
        else
        {
            OcultarColores();
        }
    }

    void DetectarDisponible()
    {
        RaycastHit hit;
        disponible = !Physics.Raycast(transform.position, Vector3.up, out hit, distanciaDetectarTorreta, torreta);
        if(disponible)
        {
            disponiblidadBase.enabled = true;
            disponiblidadBase.material = baseDisponible;
        }
        else
        {
            disponiblidadBase.enabled = true;
            disponiblidadBase.material = baseOcupada;
        }
        Debug.DrawRay(transform.position, Vector3.up * distanciaDetectarTorreta, Color.red);
    }

    void OcultarColores()
    {
        disponiblidadBase.enabled = false;
    }
}
