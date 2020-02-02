using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTouch : MonoBehaviour
{
    public bool working = true;
    void OnCollisionEnter2D(Collision2D other) {
        print("collided");
        if (working && other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
