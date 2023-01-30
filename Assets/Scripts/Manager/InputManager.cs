using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    //particula olaia
        [SerializeField] private GameObject Correr;

    public void OnAttackMelee(InputAction.CallbackContext context){
        PlayerAttack.instance.AttackingAsMelee();
    }

    public void OnAttackDistance(InputAction.CallbackContext context){
        //if(context.started) Debug.Log("boton pulsado");
        //if(context.canceled) \
        PlayerAttack.instance.AttackingAsDistance();
    }


    
    public void OnAttackMage(InputAction.CallbackContext context){

        
        if(context.started) PlayerMovement.instance.Apuntando();
        if(context.canceled) {
            
            PlayerAttack.instance.AttackinAsMage();
            PlayerMovement.instance.Caminando();
        }
    
    }


    public void OnMove(InputAction.CallbackContext value){
        Vector2 movement = value.ReadValue<Vector2>();
        //revisar esto, creo que está simplemente para devovler stamina al nomoverse. tendria que usarse una variable diferente 
        //a la de correr
        PlayerMovement.instance.MoveLogicMethod(movement);
        if(value.canceled) PlayerMovement.instance.notRunning();
        //Debug.Log(movement);
    }

    public void OnRun(InputAction.CallbackContext context){
        if(context.started) {
            PlayerManager.instance.DuplicateVelocity();
            Correr.SetActive(true);}
        if(context.canceled) {
            PlayerManager.instance.SplitVelocity();
            Correr.SetActive(false);
        }
    }
    
    
    public void OnSprint(InputAction.CallbackContext context){
        if(context.started)  PlayerMovement.instance.Sprint();
            
    }

}
