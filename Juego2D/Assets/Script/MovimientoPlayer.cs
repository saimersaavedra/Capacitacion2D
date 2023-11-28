using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MovimientoPlayer : MonoBehaviour
{
    private Rigidbody2D rb2D;
    

    [Header("Movimineto")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velMovi;
    [SerializeField] private float suavizadoMov;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;


    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;


    [Header("Animacion")]
    private Animator animator;

    [Header("Vida")]

    public Slider VidaSlider;
    /* Vida a través de texto
    [SerializeField] private TextMeshProUGUI VidaText;
    [SerializeField] private int vida = 100;
    */
    /* Vida a través de imagen/sprite
    [SerializeField] private Image Corazon;
    [SerializeField] private int CantCorazon;
    [SerializeField] private RectTransform PosicionCorazon;
    [SerializeField] private Canvas MyCanvas;
    [SerializeField] private int OffSet;
    */


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    /*
        Transform PosCorazon = PosicionCorazon;
        for (int i = 0; i < CantCorazon; i++) {
            Image NewCorazon = Instantiate(Corazon, 
                PosCorazon.position, Quaternion.identity);
            NewCorazon.transform.parent = MyCanvas.transform;
            PosCorazon.position = new 
                Vector2(PosCorazon.position.x + OffSet, 
                PosCorazon.position.y);
        }
        VidaText.text = "" + vida;
    */
    }
    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velMovi;
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));
        animator.SetFloat("VelocidadY", rb2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            salto = true;
        }
        //    VidaText.text = "" + vida;

        if(VidaSlider.value <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        Mover(movimientoHorizontal * Time.fixedDeltaTime);
        animator.SetBool("enSuelo", enSuelo);
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        Jump(salto);
        salto = false;
    }

    private void Mover(float mover)
    {

        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoMov);
        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }
    }
    public void Jump(bool saltar)
    {
        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, JumpForce));
            
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public void DanoVida(int dano)
    {
        //  vida -= dano;
        VidaSlider.value -= dano;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Daño")
        {
            //  Destroy(MyCanvas.transform.GetChild(CantCorazon + 1).gameObject);
            //  CantCorazon -= 1;
            VidaSlider.value -= 10;
        }
    }
    public void Rebote()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, velocidadRebote);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

}

