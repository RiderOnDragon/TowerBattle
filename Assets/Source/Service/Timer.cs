using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _delay;

    public event Action Finished;

    public Timer(float delay)
    {
        _delay = delay;
    }

    public void UpdateTime()
    {
        _delay -= Time.deltaTime;

        if (_delay <= 0)
            Finished?.Invoke();
    }
}
