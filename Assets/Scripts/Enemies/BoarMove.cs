using UnityEngine;

public class BoarMove : BaseEnemies
{
    [SerializeField]
    private float radioVision = 1.5f;

    [SerializeField]
    private float velocidadCorrer = 0.5f;
    [SerializeField]
    private float velocidadCaminar = 0.5f;


    ////EXTRAS PARA DANIO
    //private bool danioRecibe = false;
    //[SerializeField]
    //private float fuerzaRebote = 1f;
    //[SerializeField]
    //private float tiempoInvencibilidad = 0.5f;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (danioRecibe || !player.GetComponent<PlayerMove>().RetornarEstadoVida() || !estaVivo) return;

        float distancia = Vector2.Distance(transform.position, player.transform.position);
        if(distancia <= radioDeteccion)
        {
            estadoActual = EstadoEnemigo.persecucion;
        }
        else if((estadoActual == EstadoEnemigo.persecucion || estadoActual == EstadoEnemigo.patrullar) && distancia > radioDeteccion && distancia <= radioVision)
        {
            estadoActual = EstadoEnemigo.observar;
        }
        else if((estadoActual == EstadoEnemigo.observar || estadoActual == EstadoEnemigo.persecucion) && distancia > radioVision)
        {
            estadoActual = EstadoEnemigo.patrullar;
            DeterminarObjetivoInical();
        }

        if(estadoActual == EstadoEnemigo.persecucion)
        {
            animator.SetBool("Correr", true);
            animator.SetBool("Caminar", false);
            animator.SetBool("Mirar", false);
            velocidad = velocidadCorrer;
            PerseguirJugador();
        }
        else if(estadoActual == EstadoEnemigo.observar)
        {
            animator.SetBool("Correr", false);
            animator.SetBool("Caminar", false);
            animator.SetBool("Mirar", true);
            Observar();
        }
        else if(estadoActual == EstadoEnemigo.patrullar)
        {
            animator.SetBool("Correr", false);
            animator.SetBool("Caminar", true);
            animator.SetBool("Mirar", false);
            velocidad = velocidadCaminar;
            Patrullar();
        }

        if (vidaEnemigo <= 0 && estaVivo)
        {
            estaVivo = false;
            animator.SetTrigger("Muerte");
        }


        if (direccion < 0) spriteRenderer.flipX = false;
        else if (direccion > 0) spriteRenderer.flipX = true;
    }

    protected void Observar()
    {
        velocidad = 0;
        Vector2 enemigoAPlayer = player.transform.position - transform.position;
        direccion = Mathf.Sign(enemigoAPlayer.x);
    }

    //DANIO HACIA JUGADOR
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMove>().RecibeDanioPlayer(transform.position, CantidadDanioHaciaPlayer);
        }
    }


    //DANIO A ENEMIGO
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            RecibeDanio(player.transform.position, player.GetComponent<PlayerMove>().RetornarDanioEspada());
        }
    }
    private void RecibeDanio(Vector2 direccion, int cantidadDanio)
    {
        if (!danioRecibe)
        {
            danioRecibe = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, .01f);
            vidaEnemigo -= cantidadDanio;
            rb2d.AddForce(rebote.normalized * fuerzaRebote, ForceMode2D.Impulse);
        }

        //Invoke("desactivaDanio", tiempoInvencibilidad);
        animator.SetTrigger("Danio");
    }

    public void desactivaDanio()
    {
        danioRecibe = false;
    }

    protected override void OnDrawGizmosSelected()
    {
        if (GizmosActive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeteccion);

            Gizmos.color = Color.orange;
            Gizmos.DrawWireSphere(transform.position, radioVision);


            if (limiteIzquierdo != null && limiteDerecho != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(limiteIzquierdo, limiteDerecho);

                Gizmos.DrawSphere(limiteIzquierdo, 0.15f);
                Gizmos.DrawSphere(limiteDerecho, 0.15f);
            }
        }
    }
}
