using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour{

    public int m_health = 75;
    
    GameObject m_player;
    Rigidbody2D rb;

    public float offset;
     
    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
     
      void Start () 
    {
             target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         }
     
      void LateUpdate()
         {  //quitar detectionarea del body
             targetPos = target.position;
             thisPos = transform.position;
             targetPos.x = targetPos.x - thisPos.x;
             targetPos.y = targetPos.y - thisPos.y;
             angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
         }

}

