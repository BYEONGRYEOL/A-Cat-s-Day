using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager 
{
    Time timenow;
    List<Timer> timerlist = new List<Timer>();
    
    public void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
    }
    public void OnUpdate()
    {
        if(timerlist.Count > 0)
        {
            for (int i = 0; i < timerlist.Count; i++)
            {
                timerlist[i].waitsec -= Time.deltaTime;
                if (timerlist[i].waitsec < 0)
                {
                    timerlist[i].action();
                    timerlist.RemoveAt(i);
                }
            }
        }
        
    }
    public void SetTimer(float time, System.Action action)
    {
        timerlist.Add(new Timer(time, action));
    }
}
public class Timer
{
    public System.Action action;
    public float waitsec;
    public Timer(float waitsec, System.Action action)
    {
        this.waitsec = waitsec;
        this.action = action;
    }
}
