using System.Diagnostics.Tracing;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reader2 : MonoBehaviour, IDropHandler
{
    public int life = 20;
    public int maxlife = 20;

    public bool readerstates = true;

    public Text currentlife;

    public void Start(){
        currentlife.text = life.ToString();
    }

    //ライフに攻撃されたときの処理
    public void OnDrop(PointerEventData data){
        Debug.Log(gameObject.name);

        //リーダーへの攻撃が不可能な時の処理
        if(readerstates = false){

        }
        else{
            FollowerCard dragObj = data.pointerDrag.GetComponent<FollowerCard>();
            if(dragObj != null){
                life -= dragObj.Atack;
                currentlife.text = life.ToString();
            }
        } 
    }
}