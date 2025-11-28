using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    private CheckGround checkGround;

    private void Start()
    {
        checkGround = GetComponentInChildren<CheckGround>();
    }

    void Update()
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

        if(Input.GetKeyDown (KeyCode.X) && enSuelo)
        {
            animator.SetTrigger("ataque");
        }
    }
}
