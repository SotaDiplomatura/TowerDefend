using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public Transform objetivo;
    [SerializeField]
    float velocidad;
    [SerializeField]
    float daño;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
        if(objetivo == null)
        {
            Destroy(gameObject);
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
