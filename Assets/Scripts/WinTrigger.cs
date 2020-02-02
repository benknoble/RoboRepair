using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    [Tooltip("Level Canvas > Timer")]
    public timer_text timert;
    [Tooltip("WonScreen > TimerText")]
    public Text to_change;
    [Tooltip("WonScreen")]
    public GameObject wonscreen;
    [Tooltip("Level Canvas")]
    public GameObject levelcanvas;

    private AudioSource noise;

    void Start() {
        noise = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!noise.isPlaying)
                noise.Play();

            var robot = GameObject.Find("Robot");
            robot.SetActive(false);

            timert.running = false;
            to_change.text = timert.format("Your time");

            levelcanvas.SetActive(false);
            wonscreen.SetActive(true);
        }
    }
}
