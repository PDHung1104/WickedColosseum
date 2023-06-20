using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    float maxSeconds, elapsedSeconds;
    bool running = false, start = false;
    void Start()
    {
        elapsedSeconds = 0;
    }

    public void Run()
    {
        if (start == true)
        {
            return;
        }
        if (maxSeconds > 0)
        {
            running = true;
            start = true;
        }
    }

    public float Duration
    {
        set { maxSeconds = value; }
    }

    public bool Finished
    {
        get { return start && !running; }
    }

    public void Restart()
    {
        elapsedSeconds = 0;
        running = true;
    }

    public void Finish()
    {
        start = true;
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= maxSeconds) { 
                running = false;
            }
        }
    }
}
