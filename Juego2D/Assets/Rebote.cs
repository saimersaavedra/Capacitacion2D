using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebote : MonoBehaviour
{
   
   [SerializeField] private int vida = 0;

    private void Update()
    {
        if (vida >= 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in other.contacts)
            {
                if (punto.normal.y <= -0.9)
                {
                    other.gameObject.GetComponent<MovimientoPlayer>().Rebote();
                    vida += 1;
                   
                }
            }
        }
    }
}
