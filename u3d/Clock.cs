using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock
{
    public UInt32 Id;
    public GameObject Obj;
    public Image BgObj;
    public Text NumObj;

    public int Num;

    public long UpdateDur;
    private long lifeTimeMs;

    public DateTime StartTime;
    public DateTime EndTime;
    private DateTime lastUpdateTime;

    private bool isValid;

    public delegate void OnUpdate(); //计时器更新事件
    public delegate void OnEnd(); //计时器终止事件

    List<OnUpdate> updateTasks = new List<OnUpdate>();
    List<OnEnd> endTasks = new List<OnEnd>();

    public Clock(UInt32 id, GameObject obj, long updateDur) {
        Id = id;
        Obj = obj;
        UpdateDur = updateDur;
        BgObj = Obj.transform.Find("bg").GetComponent<Image>();
        NumObj = Obj.transform.Find("num").GetComponent<Text>();
    }

    public Clock(UInt32 id, GameObject obj, long updateDur, long lifeMs)
    {
        Id = id;
        Obj = obj;
        UpdateDur = updateDur;
        lifeTimeMs = lifeMs;
        BgObj = Obj.transform.Find("bg").GetComponent<Image>();
        NumObj = Obj.transform.Find("num").GetComponent<Text>();
    }

    public void Destroy()
    {
        isValid = false;
        GameObject.Destroy(Obj);
    }

    public void UpdateInfo() { 
        if(!isValid)
            return;
        if (DateTime.Now >= lastUpdateTime.AddMilliseconds(UpdateDur))
        {
            lastUpdateTime = DateTime.Now;
            foreach (OnUpdate f in updateTasks)
            {
                f();
            }
        }
        if (DateTime.Now >= EndTime) { 
            foreach(OnEnd f in endTasks) { 
                f();    
            }   
        }
    }

    public void Reset(long ms)
    {
        isValid = false;
        lifeTimeMs = ms;
        Start(ms);
    }

    public void Start(long ms)
    {
        isValid = true;
        Obj.SetActive(true);
        StartTime = DateTime.Now;
        lifeTimeMs = ms;
        EndTime = DateTime.Now.AddMilliseconds(ms);
    }

    public void Stop()
    { 
        isValid = false;
        Obj.SetActive(false);
    }

    public void AddUpdateCallBack(OnUpdate f) { 
        updateTasks.Add(f);
    }

    public void AddEndCallBack(OnEnd f) { 
        endTasks.Add(f);
    }

    /// <summary>
    /// 闹钟震动动画
    /// </summary>
    public void Shake() { 
        //todo
    }
}
