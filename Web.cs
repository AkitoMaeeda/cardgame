using System.Linq.Expressions;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Net;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Xml.XPath;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class CardStates{
    public int id;
    public string name;
    public int cost;
    public int atack;
    public int shield;
}

public class Web : MonoBehaviour
{

    public FollowerCard CardPrefab;
    private string gettext;
    public int ShuffleCount = 100;

    //player1のオブジェクト
    public GameObject Playerhand1;
    public Playpoint ppObj1;
    public Field field1;


    //player2のオブジェクト
    public GameObject Playerhand2;
    public Playpoint2 ppObj2;
    public Field2 field2;

    //面倒だから1の時player1,2の時player2
    public int turnplayer;
    


    public int number = 1;



    //プレイヤーのステータス
    public Text deck;
    public Text trush;
    public Text hand;

    
    public string url = "http://localhost/UnityBackendTutorial/selecttest.php";


    //それぞれのデッキ
    List<FollowerCard.Data> cards1;
    List<FollowerCard.Data> cards2;

    

    //デッキの作成（引数の枚数分デッキにカードを追加する）
    void InitCards(List<FollowerCard.Data> cards, int numbers){
        Dictionary<string, object> dic = new Dictionary<string, object>();
        cards = new List<FollowerCard.Data>(40);

        for(int i = 1; i <= 10; i++){
            StartCoroutine(GetRequest(url, dic, cards, number));
           
        }

    }

    //phpにpostして指定したカードをSQLから取り出す。
    IEnumerator GetRequest(string url, Dictionary<string, object> states, List<FollowerCard.Data> cards, int number) 
    {
        UnityWebRequest webRequest = UnityWebRequest.Post(url, number.ToString());
        
        CardStates getcard = new CardStates();
        {
            
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                gettext = webRequest.downloadHandler.text;
                var getdata = JsonUtility.FromJson<CardStates>(gettext);
                Debug.Log(":\nReceived: " + getdata.id);

                for (int i = 0; i < number; i++){
                    var card = new FollowerCard.Data(){
                        Id = getdata.id,
                        Name = getdata.name,
                        Cost = getdata.cost,
                        Atack = getdata.atack,
                        Shield = getdata.shield,
                        };
                    cards.Add(card); 
                }
            }
        }
    }

    //動きの処理を作る

        //ドローするカードを作成
        FollowerCard.Data DrowCard(List<FollowerCard.Data> cards){
            if (cards.Count <= 0) return null;

            var card = cards[0];
            cards.Remove(card);
            return card;
        }

        //ドローするときの処理
        void drow(GameObject Playerhand, List<FollowerCard.Data> cards){
            //プレイヤーごとに処理することになったらgameobjectを引数で受け取る
            var cardObj = Object.Instantiate(CardPrefab, Playerhand.transform);
            var card = DrowCard(cards);
            cardObj.Setfollowercard(card.Id, card.Name, card.Cost, card.Atack, card.Shield);
        }

        //デッキをシャッフルするときの処理
        void ShuffleCard(List<FollowerCard.Data> cards){
            var random = new System.Random();
            for(var i = 0; i<ShuffleCount; i++){
                var index = random.Next(cards.Count);
                var index2 = random.Next(cards.Count);
                var tmp = cards[index];
                cards[index] = cards[index2];
                cards[index2] = tmp;
            }

        }

        //ターンエンドがおされたときの処理(適当)
        public void Turnend(int turnplayer){
            if(turnplayer == 1){
                turnplayer = 2;
                gameLoopCoroutine = StartCoroutine(GameLoop(turnplayer));
            }
            if(turnplayer == 2){
                turnplayer = 1;
                gameLoopCoroutine = StartCoroutine(GameLoop(turnplayer));
            }

        }

        
    Coroutine gameLoopCoroutine;
    public void Start(){
        var random  = new System.Random();
        turnplayer = random.Next(1,2);
        gameLoopCoroutine = StartCoroutine(GameLoop(turnplayer));
    }

    //ターンの流れを作る
    IEnumerator GameLoop(int turnplayer){
        
        if(turnplayer == 1){
            ppObj1.ppReset();
            drow(Playerhand1, cards1);
        }

        if(turnplayer ==2){
            ppObj2.ppReset();
            drow(Playerhand2, cards2);
        }

        yield return null;



        


    }















    
}
