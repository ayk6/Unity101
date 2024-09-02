using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f;
	public float jumpSpeed = 1f , jumpFrequncy = 1f, nextJumpTime = 1f;

	bool facingRight = true;

    public bool isGrounded = false;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    void Avake()
    {
        print("awake method");
    }


	// Start is called before the first frame update
	void Start()
    {
		playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {	
        HorizontalMove();
        OnGroundCheck();

        if (playerRB.velocity.x < 0 && facingRight)
        {
           facingRight = false;
            FlipFace();
        } else { 
            facingRight = true; 
            FlipFace();
		}

        if (Input.GetAxis("Vertical")>0 && isGrounded && (nextJumpTime<Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequncy;
            Jump();
        }

    }

	void FixedUpdate()
	{

	}

    void HorizontalMove()
    {
		playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
	}

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void Jump()
    {
        playerRB.AddForce(new Vector2(0f,jumpSpeed));
    }

	void OnGroundCheck()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGrounded", isGrounded);
	}
}
