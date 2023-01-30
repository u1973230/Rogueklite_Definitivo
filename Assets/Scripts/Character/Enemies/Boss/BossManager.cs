using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    
    private static BossManager _instance;
    
    public static BossManager instance{
        get{
            if(_instance == null){
                Debug.Log("Boss Manager is Null!!!");
            }
            return _instance;
        }
    }

    public enum fases
    {
        fase1,
        fase2
    }
    
    private int health;
    
    //EnemyType m_enemyType = EnemyType.pathfinding;

    public fases m_Fase = fases.fase1;
    

    private void Start()
    {
        _instance = this;
        health = 50;
    
    }

    /*

    public static BossManager sharedInstance;



    private void Awake() {
        sharedInstance = this;
        
    }

    void Start()
    {   
        //sword_health = transform.GetChild(0).GetComponent<Sword>().m_health;
        //body_health = transform.GetChild(1).GetComponent<Body>().m_health;
        //INITIAL_HEALTH = sword_health + body_health;
        //esto puede estar mal
        //ealth = MAX_HEALTH = INITIAL_HEALTH;


        BodyAnimationController.sharedInstance.animPlayIdle();
    }

*/

}
