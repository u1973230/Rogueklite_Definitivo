using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterController
{
    private static PlayerManager _instance;
    private float attMageCost= 25f, attDistCost= 25f, attMeleeCost=25f;
    public static PlayerManager instance{
        get{
            if(_instance == null){
                Debug.Log("Player Manager is Null!!!");
            }
            return _instance;
        }
    }

    
    [SerializeField] private Camera cam;

    private void Start() {
        _instance = this;
        
        PrepareObject();
    }

    private void FixedUpdate()
    {
        if (!PlayerMovement.instance.IsRunning() && !PlayerMovement.instance.IsPointing()) stamina++;
    }


    public Camera GetCamera(){
        return cam;
    }


    public void DuplicateVelocity(){
        
        if (instance.ReduceStamina(25))
        {
            speed = runSpeed;
            PlayerMovement.instance.Running();
            PlayerAnimation.instance.animationSpeed();
        }
    }


    public void SplitVelocity(){
        speed = INITIAL_SPEED;
        
        PlayerMovement.instance.notRunning();
        PlayerAnimation.instance.animationSpeed();
    }

    public float AttMeleeCost()
    {
        return attMeleeCost;
    }

    public float AttDistanceCost()
    {
        return attDistCost;
    }
    
    public float AttMageCost()
    {
        return attMageCost;
    }

}
