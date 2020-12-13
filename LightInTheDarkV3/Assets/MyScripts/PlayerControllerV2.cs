using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : MonoBehaviour
{
    Vector2 PlayerMovement;
    Vector2 LightMovement;
    public float PlayerSpeed;
    public float LightSpeed;
    public float JumpForce;
    public float DropTimer;
    public bool IsJumping;
    public bool IsGrounded;
    private bool FacingRight = true;
    private Rigidbody2D RB;
    private Transform Light;

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(DropTimer);
        IsJumping = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Light = GameObject.FindGameObjectWithTag("Light").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(PlayerMovement.x, 0) * PlayerSpeed * Time.deltaTime;
        Light.transform.Translate(new Vector2(LightMovement.x, LightMovement.y) * LightSpeed * Time.deltaTime);

        Jump();
        Flip();
    }

    void Jump()
    {
        if (IsJumping && IsGrounded)
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            
            //StartCoroutine(StopJump());
            //transform.Translate(Vector3.up * JumpForce);
            IsJumping = false;
        }
        
    }

    void AnimatePlayer()
    {

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
            //RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
