using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 PlayerMovement;
    Vector2 LightMovement;
    public float PlayerSpeed;
    public float LightSpeed;
    public float JumpForce;
    public float LadderSpeed;
    public float DropTimer;
    public bool IsGrounded;
    private bool IsJumping;
    private bool FacingRight = true;
    private Transform LightObject;
    private Rigidbody2D RB;
    private Animator Anim;

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(DropTimer);
        IsJumping = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        LightObject = GameObject.FindGameObjectWithTag("Light").GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(PlayerMovement.x, 0) * PlayerSpeed * Time.deltaTime;
        LightObject.transform.Translate(new Vector2(LightMovement.x, LightMovement.y) * LightSpeed * Time.deltaTime);

        Flip();
        AnimatePlayer();
        Jump();
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;

        if (PlayerMovement.x > 0 && !FacingRight || PlayerMovement.x < 0 && FacingRight)
        {
            FacingRight = !FacingRight;
            Scale.x *= -1;
        }

        transform.localScale = Scale;
    }
    void AnimatePlayer()
    {
        if (PlayerMovement.x > 0 || PlayerMovement.x < 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        if (IsGrounded == false)
        {
            Anim.SetBool("Grounded", false);
        }
        else if (IsGrounded == true)
        {
            Anim.SetBool("Grounded", true);
        }
    }
    void Jump()
    {
        //RB.AddForce(Vector2.up * JumpForce);
        //IsJumping = true;

        if (IsJumping && IsGrounded)
        {
            RB.AddForce(Vector2.up * JumpForce);
            //StartCoroutine(StopJump());
            IsJumping = false;
        }

    }

    #region InputActions
    public void OnMovePlayer(InputAction.CallbackContext ctx)
    {
        PlayerMovement = ctx.ReadValue<Vector2>();
    }

    public void OnMoveLight(InputAction.CallbackContext ctx)
    {
        LightMovement = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            IsJumping = true;
        }
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "GroundTilemap")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "GroundTilemap")
        {
            IsGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "GroundTilemap")
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder" && PlayerMovement.y == 0)
        {
            Anim.SetBool("IsStill", true);
            Anim.SetBool("IsClimbing", false);
        }
        else if (collision.gameObject.tag == "Ladder" && PlayerMovement.y >= 0 || PlayerMovement.y <= 0)
        {
            Anim.SetBool("IsStill", false);
            Anim.SetBool("IsClimbing", true);
            RB.velocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * LadderSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            Anim.SetBool("IsStill", false);
            Anim.SetBool("IsClimbing", false);
        }
    }
    #endregion
}
