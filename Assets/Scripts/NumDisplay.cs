using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumDisplay : MonoBehaviour
{
    public int num = 100;
    //public Component GetComponent(Type game);
    public GameObject game;

    void start(){
        // game = GameObject.Find("Game");
    }

    public Text AliveNum;
    // Update is called once per frame
    void Update()
    {
        num = game.GetComponent<Game>().numberAlive;
        AliveNum.text = "Alive Number : " + num;

        if(Input.GetKeyDown(KeyCode.Space)){
            num--;
        }
    }
}
