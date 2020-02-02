using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseWhenNear : MonoBehaviour
{

    private AudioSource clip;
    [Tooltip("Probably set this to less than Controller's sightRange")]
    public float sightRange;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (clip == null)
            clip = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var robot = GameObject.Find("Robot");
        if (robot != null)
        {
            var robot_pos = robot.transform.position;
            var current = rb.transform.position;
            if (!clip.isPlaying && Abs(robot_pos.x - current.x) < sightRange)
            {
                clip.Play();
            }
        }
    }
}
