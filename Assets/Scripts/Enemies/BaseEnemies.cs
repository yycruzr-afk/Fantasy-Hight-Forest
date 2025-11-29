using UnityEngine;
using UnityEngine.UIElements;

public class BaseEnemies : MonoBehaviour
{
    [SerializeField]
    protected bool GizmosActive = true;
    protected enum EstadoEnemigo { patrullar, observar, persecucion}
    protected EstadoEnemigo estadoActual;

    //OBJETOS REQUERIDOS
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected Rigidbody2D rb2d;
    [SerializeField]
    protected GameObject player;

    //LIMITES
    protected Vector2 limiteIzquierdo;
    protected Vector2 limiteDerecho;

    //VALORES
    [SerializeField]
    protected float velocidad = 0.5f;
    [SerializeField]
    protected float radioDeteccion = 3f;
    [SerializeField]
    protected float distanciaPatrullaje = 1f;
    [SerializeField]
    protected int CantidadDanioHaciaPlayer = 1;

    //Extras para movimiento
    protected Vector2 objetivoPatrulla;
    protected float direccion;

    protected virtual void Start()
    {
        limiteIzquierdo = new Vector2(transform.position.x - distanciaPatrullaje, transform.position.y);
        limiteDerecho = new Vector2(transform.position.x + distanciaPatrullaje, transform.position.y);
        estadoActual = EstadoEnemigo.patrullar;
        objetivoPatrulla = limiteDerecho;
    }

    protected void Patrullar()
    {
        Vector2 haciaObjetivo = objetivoPatrulla - (Vector2)transform.position;

        direccion = Mathf.Sign(haciaObjetivo.x);

        rb2d.linearVelocityX = velocidad * direccion;

        if(haciaObjetivo.sqrMagnitude <= 0.1f)
        {
            if(objetivoPatrulla == limiteDerecho)
            {
                objetivoPatrulla = limiteIzquierdo;
            }
            else
            {
                objetivoPatrulla = limiteDerecho;
            }
        }
    }

    protected void PerseguirJugador()
    {
        Vector2 haciaPlayer = player.transform.position - transform.position;
        direccion = Mathf.Sign(haciaPlayer.x);
        rb2d.linearVelocityX = velocidad * direccion;
    }

    protected void DeterminarObjetivoInical()
    {
        float distanciaIzquierda = Vector2.Distance(limiteIzquierdo, transform.position);
        float distanciaDerecha = Vector2.Distance(limiteDerecho, transform.position);

        if(distanciaIzquierda < distanciaDerecha)
        {
            objetivoPatrulla = limiteIzquierdo;
        }
        else
        {
            objetivoPatrulla = limiteDerecho;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (GizmosActive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeteccion);

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
