using System.Diagnostics.Tracing;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field2 : MonoBehaviour, IDropHandler
{

    public Playpoint2 ppObj;

    //場に出した時の処理
    public void OnDrop(PointerEventData data){
        Debug.Log(gameObject.name);

        FollowerCard dragObj = data.pointerDrag.GetComponent<FollowerCard>();
        if(dragObj != null){
            //必要なコストが足りてるか確認
            if(ppObj.decpp(dragObj.Cost) == true)
            {
                dragObj.defaultParent = this.transform;

                if (dragObj.resetcard == true)
                {
                    Debug.Log(gameObject.name+"に"+data.pointerDrag.name+"をドロップ");
                    dragObj.resetcard = false;
                }
            }
            else{
                
            }
        }       
    }
}
