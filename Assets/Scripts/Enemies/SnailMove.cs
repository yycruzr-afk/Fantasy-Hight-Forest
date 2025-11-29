using UnityEngine;

public class SnailMove : BaseEnemies
{
    protected override void Start()
    {
        base.Start();

    }
    private void FixedUpdate()
    {
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

        if(direccion < 0) spriteRenderer.flipX = false;
        else if(direccion > 0) spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMove>().RecibeDanio(transform.position, 1);
        }
    }
}
