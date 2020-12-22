using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class prueba : MonoBehaviour
{

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }
    }
}
