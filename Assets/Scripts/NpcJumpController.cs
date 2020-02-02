using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcJumpController : MonoBehaviour
{
    public float sightRange;
    public float jumpSpeed;
    public float angleIncrease;
    public float jumpCooldown;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private bool follow;
    private bool canJump = true;
    private bool isOnGround
    {
        get
        {
            return this.rigidBody.velocity.y == 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        var robotPosition = GameObject.Find("Robot").transform.position;
        var currentPosition = this.rigidBody.transform.position;

        if (follow && isOnGround && canJump)
        {
            var yDist = robotPosition.y - currentPosition.y;
            var xDist = robotPosition.x - currentPosition.x;
            var angle = Math.Atan2(yDist, xDist);

            if (angle - angleIncrease > Math.PI / 2)
            {
                angle -= angleIncrease;
            }
            else if (angle + angleIncrease < Math.PI / 2)
            {
                angle += angleIncrease;
            }

            //rigidBody.AddForce(new Vector2(jumpSpeed * Convert.ToSingle(Math.Cos(angle)), jumpSpeed * Convert.ToSingle(Math.Sin(angle))));

            print(angle);
            print(new Vector2(jumpSpeed * Convert.ToSingle(Math.Cos(angle)), jumpSpeed * Convert.ToSingle(Math.Sin(angle))));

            var direction = (robotPosition.x - currentPosition.x) / Math.Abs(robotPosition.x - currentPosition.x);
            sprite.flipX = direction > 0;
            StartCoroutine(Jump(jumpSpeed, angle));
        }
        else if(Math.Abs(robotPosition.x - currentPosition.x) < sightRange)
        {
            follow = true;
        }
    }

    IEnumerator Jump(float jumpSpeed, double angle)
    {
        canJump = false;
        GetComponent<Animator>().SetTrigger("Jump");
        yield return new WaitForSeconds(.775f);
        rigidBody.AddForce(new Vector2(jumpSpeed * Convert.ToSingle(Math.Cos(angle)), jumpSpeed * Convert.ToSingle(Math.Sin(angle))));
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
        
    }
}
