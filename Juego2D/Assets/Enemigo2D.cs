using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2D : MonoBehaviour
{
    [SerializeField] private int rutina;
    [SerializeField] private float vida = 2;
    [SerializeField] private float cronometro;
    [SerializeField] private Animator ani;
    [SerializeField] private int direccion;
    [SerializeField] private float speed_walk;
    [SerializeField] private float speed_run;
    [SerializeField] private GameObject target;
    [SerializeField] public bool atacando;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    [SerializeField] private float rango_vision;
    [SerializeField] private float rango_ataque;
    [SerializeField] private GameObject rango;
    [SerializeField] private GameObject Hit;
    private Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        rigidbody2 = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Comportamientos();
        if (vida == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Comportamientos()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);

        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:

                    switch (direccion)
                    {
                        case 0:
                            if (informacionSuelo == false)
                            {
                                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
                            }
                            else
                            {
                                transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            }

                            break;

                        case 1:
                            if (informacionSuelo == false)
                            {
                                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
                            }
                            else
                            {
                                transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            }
                            break;
                    }
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    ani.SetBool("attack", false);
                }
                else
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    ani.SetBool("attack", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    ani.SetBool("walk", false);
                    ani.SetBool("run", false);
                }
            }
        }
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void DanoVida(float daño)
    {
        vida -= daño;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(gameObject);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }

}
