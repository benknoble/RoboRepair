using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Math;

public class timer_text : MonoBehaviour
{
    public float time;
    public bool running = false;
    public int truncate = 100;
    // Start is called before the first frame update
    void Start()
    {
        running = true;
    }

    public string format(string prefix) => $"{prefix}: {Truncate(time * truncate) / truncate}";

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            time = Time.timeSinceLevelLoad;
            GetComponent<Text>().text = format("Time");
        }
    }
}
