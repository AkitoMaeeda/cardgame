using System.Xml.Schema;
using System.Transactions;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class FollowerCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public class Data{
        public int Id;
        public string Name;
        public int Cost;
        public int Atack;
        public int Shield;
    }

    public Transform defaultParent;

    //場から離れたときにステータスをリセットするためのもの
    public bool resetcard = true;

    public bool Dru = false;

    public int Id = 0;
    public string Name = "sample";
    public int Cost = 10;
    public int Atack = 1;
    public int Shield = 1;
    

    public void Setfollowercard(int id, string name, int cost, int atack, int shield){
        Id = id;
        Name = name;
        Cost = cost;
        Atack = atack;
        Shield = shield;

        //コストの反映
        var costObj = transform.Find("Costtext");
        var costText = costObj.GetComponent<Text>();
        costText.text = cost.ToString();

        var nameObj = transform.Find("CardName");
        var nameText = nameObj.GetComponent<Text>();
        nameText.text = name;

        //攻撃力の反映
        var atackObj = transform.Find("Atacktext");
        var atackText = atackObj.GetComponent<Text>();
        atackText.text = atack.ToString();

        //体力の反映
        var shieldObj = transform.Find("Shieldtext");
        var shieldText = shieldObj.GetComponent<Text>();
        shieldText.text = shield.ToString();

    }

    private void OnValidate(){
        Setfollowercard(Id, Name, Cost, Atack, Shield);
    }















//カードの動き
    public void OnBeginDrag(PointerEventData data){
        
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
        defaultParent = transform.parent;

        transform.SetParent(defaultParent.parent);

    }

    public void OnDrag(PointerEventData data){
        transform.position = data.position;

    }

    public void OnEndDrag(PointerEventData data){
        transform.SetParent(defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //batleメゾットの呼び出し（）親グループを取得して判断
    public void OnDrop(PointerEventData data){

        defaultParent = transform.parent;
        FollowerCard dragObj = data.pointerDrag.GetComponent<FollowerCard>();
        if(defaultParent != dragObj.defaultParent){
            Debug.Log("battleスタート");
            Debug.Log(dragObj.defaultParent);
            Debug.Log(defaultParent);
        }
        else{
            Debug.Log("なんもおこらない");
        }

    }

}
