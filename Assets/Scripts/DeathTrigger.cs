﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{

    public GameObject loss_screen;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") {
            GameObject.Find("Robot").SetActive(false);
            loss_screen.SetActive(true);
        }      
    }
}