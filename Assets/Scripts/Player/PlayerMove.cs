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

    private bool danioRecibe = false;
    public float fuerzaRebotea = 1f;
    public float tiempoInvensibilidad = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (danioRecibe) return;

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


    }

    public void RecibeDanio(Vector2 direccion, int cantidadDanio)
    {
        if (!danioRecibe)
        {
            danioRecibe = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, .5f);
            rb2d.AddForce(rebote.normalized * fuerzaRebotea, ForceMode2D.Impulse);
        }

        Invoke("desactivaDanio" , tiempoInvensibilidad);
    }

    public void desactivaDanio()
    {
        danioRecibe = false;
    }
}
