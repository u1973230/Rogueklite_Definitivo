using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controller : MonoBehaviour
{
    [SerializeField] private GameObject ParticulasVida;
    [SerializeField] private GameObject ParticulasStamina;
    [SerializeField] private GameObject ParticulasDamage;


    //[SerializeField] GameObject parent;



    private enum objectType{
        health,
        mana,
        stamina
    }

    [SerializeField] objectType m_objectType = objectType.health;



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if(m_objectType == objectType.health){
                PlayerManager.instance.IncreaseHealth();
                ParticulasVida.SetActive(true);

            }
            else if(m_objectType == objectType.mana){
                PlayerManager.instance.IncreaseMana();
                ParticulasDamage.SetActive(true);
                Mana.sharedInstance.addCharge(PlayerManager.instance.GetMana()-1);
            }
            else{
                PlayerManager.instance.IncreaseStamina(50);
                ParticulasStamina.SetActive(true);

            }


            transform.parent.gameObject.SetActive(false);
        }
    }
   
}
