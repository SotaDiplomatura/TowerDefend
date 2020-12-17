using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSoplete : MonoBehaviour
{
    public float dañoPorSegundo;
    public float tiempoDisparando;

    void Start()
    {
        Invoke("Destruir",tiempoDisparando);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Enemigo>().RecivirDaño(dañoPorSegundo * Time.deltaTime);
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
}
