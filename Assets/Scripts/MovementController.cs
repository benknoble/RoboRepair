using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float maxHorizSpeed;
    public float horizAccel;
    public float jumpAccel;
    public float doubleJumpCooldown;
    public float gravity;

    public bool canSingleJump = true;
    public bool canDoubleJump = true;
    public bool canWallJump = false;

    public Transform downLeftTransform;
    public Transform downRightTransform;
    public Transform rightTopTransform;
    public Transform rightTransform;
    public Transform rightBottomTransform;
    public Transform leftTopTransform;
    public Transform leftTransform;
    public Transform leftBottomTransform;
    public bool onGround = true;
    public bool onRightWall = false;
    public bool onLeftWall = false;
    private bool jumpedLeft = false;
    private bool jumpedRight = false;

    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVectors();
        ResetJumps();
        float horizAxis = Input.GetAxis("Horizontal");
        bool jump = Input.GetButton("Jump");
        Vector2 vel = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        if (horizAxis > 0)
        {
            if (!onRightWall)
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (horizAxis < 0)
        {
            if (!onLeftWall)
            {
                vel.x = Mathf.Clamp(vel.x + (horizAxis * horizAccel * Time.deltaTime), -maxHorizSpeed, maxHorizSpeed);
            }
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (onGround)
        {
            if (Mathf.Abs(vel.x) > .01)
            {
                vel.x = vel.x / 1.2f; 
            } else
            {
                vel.x = 0;
            }
        }

        if (jump && canSingleJump)
        {
            vel.y = jumpAccel;
            canSingleJump = false;
            StartCoroutine(DoubleJumpCooldown());
        } else if (jump && canWallJump && onWall)
        {
            if (onLeftWall && !jumpedLeft)
            {
                vel.x = 8;
                vel.y = jumpAccel;
                jumpedLeft = true;
            }
            else if (onRightWall && !jumpedRight)
            {
                vel.x = -8;
                vel.y = jumpAccel;
                jumpedRight = true;
            }
        }
        else if (jump && !canSingleJump && canDoubleJump && !onWall)
        {
            vel.y = jumpAccel;
            canDoubleJump = false;
        }
        else
        {
            if (((onRightWall && !jumpedRight && horizAxis > 0) || (onLeftWall && !jumpedLeft && horizAxis < 0)) && !onGround)
            {
                vel.y = -1;
            } else if (!onGround)
            {
                vel.y -= gravity * Time.deltaTime;
            }
        }

        _rigidbody.velocity = vel;
    }

    IEnumerator DoubleJumpCooldown()
    {
        yield return new WaitForSeconds(doubleJumpCooldown);
        canDoubleJump = true;
        canWallJump = true;
    }

    private bool onWall {
        get {
            return onLeftWall || onRightWall;
        }
    }

    void ResetJumps()
    {
        if (onGround)
        {
            canSingleJump = true;
            canDoubleJump = true;
            jumpedLeft = false;
            jumpedRight = false;
            canWallJump = false;
            GetComponent<Animator>().SetBool("InAir", false);
        } else
        {
            GetComponent<Animator>().SetBool("InAir", true);
        }
        if (onLeftWall)
        {
            jumpedRight = false;
        }
        if (onRightWall)
        {
            jumpedLeft = false;
        }
    }

    void CheckVectors()
    {
        RaycastHit2D downLeft = Physics2D.Raycast(downLeftTransform.position, Vector2.down, .1f);
        RaycastHit2D downRight = Physics2D.Raycast(downRightTransform.position, Vector2.down, .1f);
        if ((downLeft.collider != null && downLeft.collider.CompareTag("Environment")) || 
            (downRight.collider != null && downRight.collider.CompareTag("Environment")))
        {
            onGround = true;
        } else
        {
            onGround = false;
        }
        RaycastHit2D rightTop = Physics2D.Raycast(rightTopTransform.position, Vector2.right, .1f);
        RaycastHit2D right = Physics2D.Raycast(rightTransform.position, Vector2.right, .1f);
        RaycastHit2D rightBottom = Physics2D.Raycast(rightBottomTransform.position, Vector2.right, .1f);
        if ((right.collider != null && right.collider.CompareTag("Environment")) || (rightTop.collider != null && rightTop.collider.CompareTag("Environment"))
                || (rightBottom.collider != null && rightBottom.collider.CompareTag("Environment")))
        {
            onRightWall = true;
        }
        else
        {
            onRightWall = false;
        }
        RaycastHit2D leftTop = Physics2D.Raycast(leftTopTransform.position, Vector2.left, .1f);
        RaycastHit2D left = Physics2D.Raycast(leftTransform.position, Vector2.left, .1f);
        RaycastHit2D leftBottom = Physics2D.Raycast(leftBottomTransform.position, Vector2.left, .1f);
        if ((left.collider != null && left.collider.CompareTag("Environment")) || (leftTop.collider != null && leftTop.collider.CompareTag("Environment")) || 
            (leftBottom.collider != null && leftBottom.collider.CompareTag("Environment")))
        {
            onLeftWall = true;
        } else
        {
            onLeftWall = false;
        }
    }
}
