using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeOrdas : MonoBehaviour
{
    GameController gameController;
    GameObject referenciaEnemigo;

    [SerializeField]
    GameObject[] enemigosFuego;
    [SerializeField]
    GameObject[] enemigosAgua;
    [SerializeField]
    GameObject[] enemigosPlanta;

    [SerializeField]
    List<GameObject> enemigos = new List<GameObject> { };

    int orda;
    int numeroDeEnemigosQueGenerar;

    void Start()
    {
        numeroDeEnemigosQueGenerar = 8;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        referenciaEnemigo = GameObject.Find("Enemigos");
        AñadirALaLista();
    }

    void AñadirALaLista()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i < 3)
            {
                print("i");
                enemigos.Add(enemigosFuego[i]);
            }
            else if (i < 6)
            {
                enemigos.Add(enemigosAgua[i - 3]);
            }
            else
            {
                enemigos.Add(enemigosPlanta[i - 6]);
            }
        }
    }

    public void IniciarOrda()
    {
        gameController.desplegandoEnemigos = true;
        if (numeroDeEnemigosQueGenerar == 8)
        {
            StartCoroutine("primeraOrda");
        }
        else
        {
            StartCoroutine("Oleadas");
        }
    }

    IEnumerator primeraOrda()
    {
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(enemigosAgua[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(1f);
            Instantiate(enemigosPlanta[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(1f);
            Instantiate(enemigosFuego[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(6f);
        }
        gameController.desplegandoEnemigos = false;
        numeroDeEnemigosQueGenerar++;
        PararOrda("primeraOrda");
    }

    IEnumerator Oleadas()
    {
        for (int i = 0; i <= numeroDeEnemigosQueGenerar; i++)
        {
            Instantiate(enemigos[Random.Range(0, 8)], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(1f);
        }
        gameController.desplegandoEnemigos = false;
        numeroDeEnemigosQueGenerar += 3;
        PararOrda("Oleadas");
    }

    void PararOrda(string nombreOrda)
    {
        StopCoroutine(nombreOrda);
    }
}
