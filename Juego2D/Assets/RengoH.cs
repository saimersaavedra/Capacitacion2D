using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RengoH : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.transform.GetComponent<MovimientoPlayer>().DanoVida(10);
        }
    }
}
