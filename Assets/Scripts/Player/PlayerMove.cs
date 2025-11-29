using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb2d;

    [SerializeField]
    private float velocidadX = 1f;

    [SerializeField]
    private float velocidadSalto = 3f;

    [SerializeField]
    private int vida = 5;

    private bool danioRecibe = false;
    private bool atacando = false;
    private bool estaVivo = true;
    public float fuerzaRebotea = 1f;
    public float tiempoInvensibilidad = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (danioRecibe || atacando || !estaVivo) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.linearVelocityX = velocidadX;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.linearVelocityX = -velocidadX;
        }
        else rb2d.linearVelocityX = 0f;


        if (Input.GetKey(KeyCode.Space) && GetComponentInChildren<CheckGround>().GetEnSuelo())
        {
            rb2d.linearVelocityY = velocidadSalto;
        }


        if (Input.GetKey(KeyCode.X) && atacando == false)
        {
            atacar();
        }


        if(vida <= 0 && estaVivo)
        {
            estaVivo = false;
            GetComponent<PlayerAnimation>().DispararAnimacionMuerte();
            rb2d.linearVelocity = Vector2.zero;
        }

        GetComponent<PlayerAnimation>().ActualizarAnimaciones();
    }

    public void atacar()
    {
        if(atacando == false)
        {
            atacando = true;
            GetComponent<PlayerAnimation>().atacar();
        }
    }

    public void desactivarAtaque()
    {
        atacando = false;
    }
    public void RecibeDanioPlayer(Vector2 direccion, int cantidadDanio)
    {
        if (!danioRecibe || estaVivo)
        {
            danioRecibe = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, .5f);
            vida -= cantidadDanio;
            rb2d.AddForce(rebote.normalized * fuerzaRebotea, ForceMode2D.Impulse);
        }

        Invoke("desactivaDanio" , tiempoInvensibilidad);
    }

    public void desactivaDanio()
    {
        danioRecibe = false;
    }

    public bool RetornarEstadoVida()
    {
        return estaVivo;
    }
}
