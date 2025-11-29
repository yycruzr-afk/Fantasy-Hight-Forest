using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject colision;

    [SerializeField]
    private PlayerMove playerMove;

    private CheckGround checkGround;

    private void Start()
    {
        checkGround = GetComponentInChildren<CheckGround>();
    }

    public void ActualizarAnimaciones()
    {
        bool corriendo = false;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            corriendo = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            corriendo = true;
        }

        animator.SetBool("isRun", corriendo);


        bool enSuelo = checkGround.GetEnSuelo();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("salto");
        }

        if (enSuelo)
        {
            animator.SetBool("enSuelo", true);
        }
        else
        {
            animator.SetBool("enSuelo", false);
        }
    }

    public void atacar()
    {
        animator.SetTrigger("ataque");
    }

    public void DispararAnimacionMuerte()
    {
        animator.SetTrigger("Muerte");
    }
}
