using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{


    public float TimeToLive = 1f;

    private void Start() {

        Destroy(gameObject, TimeToLive);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player")){ }

        else
        {

            if(other.CompareTag("Enemy")){
                other.GetComponent<EnemyController>().ReduceHealth(transform.position.x);
            }

            else if(other.CompareTag("EnemyProjectile")){
                Destroy(other.gameObject);
            }


            /*else if(other.CompareTag("Wall")){
                DestroyProjectile();
            }*/
        

            else if(other.CompareTag("BossBody")){
                other.GetComponent<Body>().ReduceHealth();
            }
            else if(other.CompareTag("Sword")){
                // enemies.GetComponent<Sword>().ReduceHealth();
            }
            DestroyProjectile();
        }
        


        
       
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
