using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerAttack : MonoBehaviour
{
    private static PlayerAttack _instance;
    
    public static PlayerAttack instance{
        get{
            if(_instance == null){
                Debug.Log("Player Manager is Null!!!");
            }
            return _instance;
        }
    }

    SpriteRenderer m_SpriteRenderer;
    GameObject m_player;


    [SerializeField]int meleeDamage, distanceDamage, mageDamage; 

    //veces qe podemos atacar por segundo, segundos a esperar para el siguiente ataque
    float attackRate = 0.5f, nextAttackTime = 0f;
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask whatIsEnemies;

    float bulletDistanceForce = 10f, bulletMageForce = 20f;
    //variables distancia
    [SerializeField] Transform rangePoint;
    [SerializeField] GameObject bulletPrefab;


    //variables magia
    [SerializeField] Transform spellPoint;
    [SerializeField] GameObject spellPrefab;
    


    private void Awake()
    {
        _instance = this;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_player =  GameObject.Find("Player");

    }

    

    
    public void AttackingAsMelee(){
        if(Time.time > nextAttackTime ){
            if( PlayerManager.instance.ReduceStamina(PlayerManager.instance.AttMeleeCost()) ){
                PlayerAnimation.instance.Attacking("melee");
                
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                //***** DAÑAR A ENEMIGOS *****
                foreach (Collider2D enemies in hitEnemies){
                    if(enemies.CompareTag("Enemy")){
                        enemies.GetComponent<EnemyController>().ReduceHealth(transform.position.x);
                    }
                    if(enemies.CompareTag("BossBody")){
                        enemies.GetComponent<Body>().ReduceHealth();
                    }
                    if(enemies.CompareTag("Sword")){
                       // enemies.GetComponent<Sword>().ReduceHealth();
                    }
                    else if(enemies.CompareTag("EnemyProjectile")){
                        Destroy(enemies.gameObject);
                    }
                }
            }
            
            nextAttackTime = Time.time + attackRate;
            
        }
        else Debug.Log("Te has quedado sin energia");
    }

    
    public void AttackingAsDistance(){
        if(Time.time > nextAttackTime ){
            if(PlayerManager.instance.ReduceStamina(PlayerManager.instance.AttDistanceCost())){
            GameObject spell = Instantiate(spellPrefab, spellPoint.position, spellPoint.rotation);
            Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(bulletMageForce * PlayerMovement.instance.Direction() , 0) , ForceMode2D.Impulse);
            PlayerAnimation.instance.Attacking("distance");
            nextAttackTime = Time.time + attackRate;
            }
            else Debug.Log("Sin energia");
        }
    }


    public void AttackinAsMage(){
        //fase de apuntar y fase de disaprar
        if(Time.time > nextAttackTime ){
            if( PlayerMovement.instance.IsPointing() ){

                

                Vector2 pointTo = PlayerMovement.instance.ReturnMove();
                pointTo.Normalize();

                GameObject bullet = Instantiate(bulletPrefab, rangePoint.position, rangePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(pointTo * bulletDistanceForce, ForceMode2D.Impulse);
                PlayerAnimation.instance.Attacking("mage");
                nextAttackTime = Time.time + attackRate;


                

            }
            else Debug.Log("Sin maná");
        }
    }


    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
