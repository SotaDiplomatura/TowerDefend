using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscarcha : MonoBehaviour
{
    public float relentizar;
    public float tiempoDisparando;

    void Start()
    {
        Invoke("Destruir", tiempoDisparando);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Enemigo>().Relentizar(relentizar);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Enemigo>().RecuperarVelocidad();
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
