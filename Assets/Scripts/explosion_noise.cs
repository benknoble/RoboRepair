using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_noise : MonoBehaviour
{

    private AudioSource explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D _)
    {
        if (!explosion.isPlaying)
            explosion.Play();
    }
}
