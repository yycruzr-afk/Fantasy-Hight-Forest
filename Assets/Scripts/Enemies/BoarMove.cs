using UnityEngine;

public class BoarMove : BaseEnemies
{
    [SerializeField]
    private float radioVision = 1.5f;

    [SerializeField]
    private float velocidadCorrer = 0.5f;
    [SerializeField]
    private float velocidadCaminar = 0.5f;

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

        if (direccion < 0) spriteRenderer.flipX = false;
        else if (direccion > 0) spriteRenderer.flipX = true;
    }

    protected void Observar()
    {
        velocidad = 0;
        Vector2 enemigoAPlayer = player.transform.position - transform.position;
        direccion = Mathf.Sign(enemigoAPlayer.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMove>().RecibeDanio(transform.position, 1);
        }
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
