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
    float tiempoEntreEnemigo;
    int numeroOrda;

    void Start()
    {
        numeroOrda = 0;
        tiempoEntreEnemigo = 1;
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
            enemigosAgua[i].GetComponent<Enemigo>().elite = false;
            Instantiate(enemigosAgua[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(0.5f);
            enemigosPlanta[i].GetComponent<Enemigo>().elite = false;
            Instantiate(enemigosPlanta[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(0.5f);
            enemigosFuego[i].GetComponent<Enemigo>().elite = false;
            Instantiate(enemigosFuego[i], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            yield return new WaitForSeconds(4f);
        }
        gameController.desplegandoEnemigos = false;
        numeroDeEnemigosQueGenerar++;
        numeroOrda++;
        PararOrda("primeraOrda");
    }

    IEnumerator Oleadas()
    {
        int numeroRandon;
        int numeroEnemigo;
        for (int i = 0; i <= numeroDeEnemigosQueGenerar; i++)
        {
            numeroRandon = Random.Range(0, 1);
            print(numeroRandon);
            numeroEnemigo = Random.Range(0, 8);
            print(numeroRandon);

            if(numeroOrda > 7)
            {
                ElegirElite(numeroEnemigo, numeroRandon);
            }

            PowerUpPorRondas(numeroEnemigo);

            Instantiate(enemigos[numeroEnemigo], transform.position, Quaternion.identity, referenciaEnemigo.transform);
            
            if(tiempoEntreEnemigo > 0)
            {
                yield return new WaitForSeconds(tiempoEntreEnemigo);
            }else
            {
                yield return new WaitForSeconds(0.05f);
            }

        }
        gameController.desplegandoEnemigos = false;
        tiempoEntreEnemigo /= 1.1f;
        numeroDeEnemigosQueGenerar += 5;
        numeroOrda++;
        PararOrda("Oleadas");
    }

    void ElegirElite(int enemigoElegido,int numeroRandom)
    {

        if (numeroRandom == 0)
        {
            enemigos[enemigoElegido].GetComponent<Enemigo>().elite = true;
        }
        else
        {
            enemigos[enemigoElegido].GetComponent<Enemigo>().elite = false;
        }
    }
    
    void PowerUpPorRondas(int enemigoElegido)
    {
        if(numeroOrda > 10 && numeroOrda < 15)
        {
            print("1");
            enemigos[enemigoElegido].GetComponent<Enemigo>().vida *= 2;
        }else if(numeroOrda > 25)
        {
            print("2");
            enemigos[enemigoElegido].GetComponent<Enemigo>().vida *= 4;
            enemigos[enemigoElegido].GetComponent<Enemigo>().oroQueDa *= 2;
        }
        else if(numeroOrda > 50)
        {
            print("3");
            enemigos[enemigoElegido].GetComponent<Enemigo>().vida *= 6;
        }
        else if(numeroOrda > 70)
        {
            print("4");
            enemigos[enemigoElegido].GetComponent<Enemigo>().vida *= 10;
            enemigos[enemigoElegido].GetComponent<Enemigo>().oroQueDa *= 5;
        }
    }

    void PararOrda(string nombreOrda)
    {
        StopCoroutine(nombreOrda);
    }
}
