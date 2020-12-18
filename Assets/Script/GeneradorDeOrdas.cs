using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeOrdas : MonoBehaviour
{
    GameController gameController;
    GameObject enemigos;

    [SerializeField]
    GameObject[] enemigosFuego;
    [SerializeField]
    GameObject[] enemigosAgua;
    [SerializeField]
    GameObject[] enemigosPlanta;

    int orda;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        enemigos = GameObject.Find("Enemigos");
        StartCoroutine("primeraOrda");
    }

    void Update()
    {
        if(orda >= 1)
        {

        }
    }

    IEnumerator primeraOrda()
    {
        for(int i=0;i<=2;i++)
        {
            Instantiate(enemigosAgua[i],transform.position,Quaternion.identity,enemigos.transform);
            yield return new WaitForSeconds(1f);
            Instantiate(enemigosPlanta[i], transform.position, Quaternion.identity, enemigos.transform);
            yield return new WaitForSeconds(1f);
            Instantiate(enemigosFuego[i], transform.position, Quaternion.identity, enemigos.transform);
            yield return new WaitForSeconds(5f);
        }
        PararCorrutina();
    }
    void PararCorrutina()
    {
        StopCoroutine("primeraOrda");
    }
}
