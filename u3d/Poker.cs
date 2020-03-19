using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PokerCard
{
    public string Name;
    public byte Point;
    public byte Color;
    public byte Id;

    public GameObject Obj;
    public Image FrontObj;
    public Image BackObj;

    private bool doMove = false;
    private DateTime moveStartTime;
    private DateTime moveEndTime;
    private float moveSpeed;
    private Vector3 moveEndPoint;

    public PokerCard() { }

    public PokerCard(GameObject obj)
    {
        Obj = obj;
        FrontObj = Obj.transform.Find("front").GetComponent<Image>();
        BackObj = Obj.transform.Find("back").GetComponent<Image>();
        Obj.transform.position = new Vector3(0, 0, 0);
    }

    public void Set(string name, byte id, Sprite sp)
    { 
        Name = name;
        Id = id;
        Point = GetPoint(id);
        Color = GetColor(id);

        FrontObj.sprite = sp;
    }

    /// <summary>
    /// 显示正面
    /// </summary>
    public void ShowFront()
    { 
        FrontObj.gameObject.SetActive(true);
        BackObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示背面
    /// </summary>
    public void ShowBack()
    { 
        FrontObj.gameObject.SetActive(false);
        BackObj.gameObject.SetActive(true);
    }

    /// <summary>
    /// 扑克牌移动 (世界坐标)
    /// </summary>
    public void Move(Vector3 startPoint, Vector3 endPoint, DateTime startTime, double durs)
    { 
        doMove = true;
        
        double len = Vector3.Distance(startPoint, endPoint);
        moveSpeed = (float)(len / durs); //(像素/s)
        Obj.transform.position = startPoint;

        moveStartTime = startTime;
        moveEndTime = moveStartTime.AddSeconds(durs);
        moveEndPoint = endPoint;

    }

    /// <summary>
    /// 移动刷新
    /// </summary>
    public void MoveUpdate() {
        if(!doMove)
            return;
        if (DateTime.Now < moveStartTime)
            return;
        if (DateTime.Now > moveEndTime) {
            doMove = false;
            return;
        }
        Obj.transform.position = Vector3.MoveTowards(Obj.transform.position, moveEndPoint, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 拿到牌的点数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static byte GetPoint(byte cardId)
    { 
        return (byte)(cardId & (byte)PokerMask.Value);
    }

    /// <summary>
    /// 拿到拍的颜色
    /// </summary>
    /// <param name="cardId"></param>
    /// <returns></returns>
    public static byte GetColor(byte cardId)
    { 
        return (byte)(cardId & (byte)PokerMask.Color);
    }

    /// <summary>
    /// 点击回调
    /// </summary>
    public ClickListener ClickCallBack;

    /// <summary>
    /// 点击委托
    /// </summary>
    public delegate void ClickListener(byte cardId);
}