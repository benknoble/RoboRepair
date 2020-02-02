using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGroundController : MonoBehaviour
{
    public float sightRange;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool follow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        var robot = GameObject.Find("Robot");
        if (robot != null)
        {
            var robotPosition = robot.transform.position;
            var currentPosition = this.rb.transform.position;

            if (follow)
            {
                var direction = (robotPosition.x - currentPosition.x) / Math.Abs(robotPosition.x - currentPosition.x);
                rb.velocity = new Vector2(direction * speed, 0);
                sprite.flipX = direction > 0;
            }
            else if (Math.Abs(robotPosition.x - currentPosition.x) < sightRange)
            {
                follow = true;
            }
        }
    }
}
