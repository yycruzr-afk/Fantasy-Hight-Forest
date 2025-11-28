using UnityEngine;

public class MovimientoSnail : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb2d;
    public float velocidad = 0.5f;
    public float radioDeteccion = 5f;

    public GameObject player;

    void Start()
    {

    }

    
    void FixedUpdate()
    {
       
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        

        if(distancia <= radioDeteccion)
        {
            Vector2 vectorHaciaPlayer = player.transform.position - transform.position;

            float direccion = Mathf.Sign(vectorHaciaPlayer.x);

            rb2d.linearVelocityX = direccion * velocidad;

            if (direccion < 0) spriteRenderer.flipX = false;
            else if (direccion > 0) spriteRenderer.flipX = true;
        }
        else
        {
            rb2d.linearVelocityX = 0;
        }
    }

    private void OnDrawGizmosSelect()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radioDeteccion);
    }
}
