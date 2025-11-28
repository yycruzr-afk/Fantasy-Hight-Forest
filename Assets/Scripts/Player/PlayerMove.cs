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


    // Update is called once per frame
    void FixedUpdate()
    {
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
}
