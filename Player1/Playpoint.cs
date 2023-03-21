using System.Security.Principal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playpoint : MonoBehaviour
{

    public int pp = 0;
    [Range (0,10)]
    public int maxpp = 0;

    public Text currentpp;
    public Text currentmaxpp;

    
    void Start(){
        currentpp.text = pp.ToString();
        currentmaxpp.text = maxpp.ToString();
    }

    public void ppReset(){
        maxpp +=1;
        pp = maxpp;
        currentpp.text = pp.ToString();
        currentmaxpp.text = maxpp.ToString();
    }

    public bool decpp(int usepp){
        pp -= usepp;
        if(pp < 0){
            Debug.Log("必要なppが不足しています");
            return false;
        }
        else{
            currentpp.text = pp.ToString();
            return true;
        }
    }
}
