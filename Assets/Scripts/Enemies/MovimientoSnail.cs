using UnityEngine;

public class MovimientoSnail : MonoBehaviour
{
    enum EstadoEnemigo { Patrulla, Persecucion }

    EstadoEnemigo estadoActual;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb2d;
    public float velocidad = 0.5f;
    public float radioDeteccion = 5f;

    private Vector2 objetivoPatrulla;
    private float direccion = 0f;

    public GameObject player;

    [SerializeField]
    private Transform limiteIzquierdo;
    [SerializeField]
    private Transform limiteDerecho;

    void Start()
    {
        estadoActual = EstadoEnemigo.Patrulla;

        if (limiteDerecho != null)
        {
            objetivoPatrulla = limiteDerecho.position;
        }
        else
        {
            Debug.LogError("Asigna los límites de patrulla.");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);

        if (distancia <= radioDeteccion)
        {
            estadoActual = EstadoEnemigo.Persecucion;
        }
        else if (estadoActual == EstadoEnemigo.Persecucion && distancia > radioDeteccion)
        {
            estadoActual = EstadoEnemigo.Patrulla;
            DeterminarObjetivoInicial();
        }

        if (estadoActual == EstadoEnemigo.Persecucion)
        {
            PerseguirJugador();
        }
        else 
        {
            Patrullar();
        }

        if (direccion < 0) spriteRenderer.flipX = false;
        else if (direccion > 0) spriteRenderer.flipX = true;
    }

    void Patrullar()
    {

        Vector2 haciaObjetivo = objetivoPatrulla - (Vector2)transform.position;

        direccion = Mathf.Sign(haciaObjetivo.x);


        rb2d.linearVelocityX = direccion * velocidad;

        if (haciaObjetivo.sqrMagnitude <= 0.01f)
        {
            if (objetivoPatrulla == (Vector2)limiteDerecho.position)
            {
                objetivoPatrulla = limiteIzquierdo.position;
            }
            else
            {
                objetivoPatrulla = limiteDerecho.position;
            }
        }
    }

    void PerseguirJugador()
    {
        Vector2 vectorHaciaPlayer = player.transform.position - transform.position;
        direccion = Mathf.Sign(vectorHaciaPlayer.x);
        rb2d.linearVelocityX = direccion * velocidad;
    }

    void DeterminarObjetivoInicial()
    {
        float distIzquierda = Vector2.Distance(transform.position, limiteIzquierdo.position);
        float distDerecha = Vector2.Distance(transform.position, limiteDerecho.position);

        if (distIzquierda < distDerecha)
        {
            objetivoPatrulla = limiteIzquierdo.position;
        }
        else
        {
            objetivoPatrulla = limiteDerecho.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().RecibeDanio(transform.position, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);

        if (limiteIzquierdo != null && limiteDerecho != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(limiteIzquierdo.position, limiteDerecho.position);

            Gizmos.DrawSphere(limiteIzquierdo.position, 0.15f);
            Gizmos.DrawSphere(limiteDerecho.position, 0.15f);
        }
    }
}