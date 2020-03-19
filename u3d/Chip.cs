using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chip
{
    /// <summary>
    /// 筹码对象
    /// </summary>
    public GameObject Obj;

    /// <summary>
    /// 筹码数值对象
    /// </summary>
    public Text ValueObj;

    /// <summary>
    /// 筹码类型
    /// </summary>
    public FgfChipType Type;

    /// <summary>
    /// 筹码数值
    /// </summary>
    public int Value;

    private bool doMove = false;
    private float moveSpeed = 0.0f;
    private DateTime moveStartTime;
    private DateTime moveEndTime;
    private Vector3 moveEndPoint;


    public Chip(GameObject obj, FgfChipType type, int value)
    {
        Obj = obj;
        ValueObj = Obj.transform.Find("num").GetComponent<Text>();
        Type = type;
        Value = value;
    }

    /// <summary>
    /// 设置筹码数值
    /// </summary>
    /// <param name="num"></param>
    public void SetNum(int num)
    { 
        Value = num;
        UpdateInfo();
    }

    /// <summary>
    /// 更新筹码
    /// </summary>
    public void UpdateInfo()
    { 
        ValueObj.text = Value.ToString();

        //移动
        if (!doMove)
            return;
        if (DateTime.Now < moveStartTime)
            return;
        if (DateTime.Now > moveEndTime)
        {
            doMove = false;
            return;
        }
        Obj.transform.position = Vector3.MoveTowards(Obj.transform.position, moveEndPoint, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 移动到指定区域
    /// </summary>
    /// <param name="position"></param>
    public void MoveTo(Rect rect) {
        float x = UnityEngine.Random.Range(rect.left, rect.right);
        float y = UnityEngine.Random.Range(rect.bottom, rect.top);

        Obj.transform.localPosition = new Vector3(x, y);
    }

    /// <summary>
    /// 移动到指定区域
    /// </summary>
    /// <param name="position"></param>
    public void MoveTo(Vector3 p1, Vector3 p2, double durs)
    {
        float x = UnityEngine.Random.Range(p1.x, p2.x);
        float y = UnityEngine.Random.Range(p1.y, p2.y);
        Vector3 endPoint = new Vector3(x, y, 0);
        Vector3 startPoint = new Vector3(Obj.transform.position.x, Obj.transform.position.y, 0);

        doMove = true;
        double len = Vector3.Distance(startPoint, endPoint);
        moveSpeed = (float)(len / durs); //(像素/s)
        Obj.transform.position = startPoint;

        moveStartTime = DateTime.Now;
        moveEndTime = moveStartTime.AddSeconds(durs);
        moveEndPoint = endPoint;
    }
}