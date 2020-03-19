using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgBox : MonoBehaviour
{
    /// <summary>
    /// gameobj对象
    /// </summary>
    public GameObject Obj;

    /// <summary>
    /// obj对象
    /// </summary>
    public GameObject ContentObj;

    /// <summary>
    /// 文本对象
    /// </summary>
    public Text TxtObj;

    private DateTime startTime;
    private DateTime endTime;

    public MsgBox(GameObject obj) { 
        Obj = obj;
        Obj.SetActive(false);
        ContentObj = Obj.transform.Find("content").gameObject;
        ContentObj.SetActive(false);
        TxtObj = Obj.transform.Find("txt").GetComponent<Text>();
        TxtObj.gameObject.SetActive(false);
    }

    public void Show(double ms)
    {
        startTime = DateTime.Now;
        endTime = startTime.AddMilliseconds(ms);
        Obj.SetActive(true);
        ContentObj.gameObject.SetActive(true);
        TxtObj.gameObject.SetActive(true);
    }

    public void Show(string str, double ms) { 
        startTime = DateTime.Now;
        endTime = startTime.AddMilliseconds(ms);
        TxtObj.text = str;
        Obj.SetActive(true);
        TxtObj.gameObject.SetActive(true);
    }

    public void Show(GameObject obj, double ms) { 
        startTime = DateTime.Now;
        endTime = startTime.AddMilliseconds(ms);
        obj.transform.SetParent(ContentObj.transform);
        Obj.SetActive(true);
        ContentObj.gameObject.SetActive(true);
        obj.SetActive(true);
    }

    public void UpdateInfo() {
        if (DateTime.Compare(DateTime.Now, endTime) > 0)
        {
            Obj.SetActive(false);
            ContentObj.SetActive(false);
            TxtObj.gameObject.SetActive(false);
        }
    }
}
