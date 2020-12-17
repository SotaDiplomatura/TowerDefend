using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public Transform objetivo;
    [SerializeField]
    float velocidad;
    [SerializeField]
    public float daño;

    void Update()
    {        
        if(objetivo == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            other.gameObject.GetComponent<Enemigo>().RecivirDaño(daño);
            Destroy(gameObject);
        }
    }
}
