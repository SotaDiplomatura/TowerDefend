using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscarcha : MonoBehaviour
{
    public float relentizar;
    public float tiempoDisparando;
    public float duracionRelentizado;

    void Start()
    {
        Invoke("Destruir", tiempoDisparando);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Enemigo>().duracionRelentizado = duracionRelentizado;
            other.gameObject.GetComponent<Enemigo>().Relentizar(relentizar);
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
