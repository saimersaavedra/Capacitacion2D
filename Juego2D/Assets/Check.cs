using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    [Header("Animacion")]
    private Animator animator;
    private bool activada = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("Check", activada);
    }
    [SerializeField] private GameObject Plataforma;
    [SerializeField] private GameObject Enemigo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Plataforma.SetActive(true);
            Enemigo.SetActive(true);
            activada = true;
        }
    }
}
