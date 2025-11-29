using UnityEngine;

public class SnailMove : BaseEnemies
{
    ////EXTRAS PARA DANIO
    //private bool danioRecibe = false;
    //[SerializeField]
    //private float fuerzaRebote = 0f;
    //[SerializeField]
    //private float tiempoInvencibilidad = 0f;

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
        else if(estadoActual == EstadoEnemigo.persecucion && distancia > radioDeteccion)
        {
            estadoActual = EstadoEnemigo.patrullar;
            DeterminarObjetivoInical();
        }


        if(estadoActual == EstadoEnemigo.persecucion)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }

        if (vidaEnemigo <= 0 && estaVivo)
        {
            estaVivo = false;
            animator.SetTrigger("Muerte");
        }

        if (direccion < 0) spriteRenderer.flipX = false;
        else if(direccion > 0) spriteRenderer.flipX = true;
    }


    //DANIO A PLAYER
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
}
