using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random=UnityEngine.Random;

public class EnemyController : CharacterController
{

    private enum EnemyType
    {
        pathfinding,
        followPlayer,
        staticEnemy,
    }



    [SerializeField] EnemyType m_enemyType = EnemyType.pathfinding;


    //VARIABLES SEGUIR
    [SerializeField] private float initialStoppingDistance, initialRetreadDistance;
    private float stoppingDistance, retreadDistance;

    private Transform m_player;
    Vector2 toTackle;
    Vector3 direction;


    //VARIABLES DISPARAR Y PLACAR
    private float timeBtwShoots, chargeRate = 3f, nextChargeTime;
    [SerializeField] private GameObject[] projectile;
    [SerializeField] private float initialTimeBtwShoots, initialTimeBtwCharge;
    [SerializeField] bool canShoot, canCharge, canAttack;
    private bool charging = false;


    //VARIABLES PATHFINDING
    private int randomSpot;
    [SerializeField] bool canTeleport;
    [SerializeField] Transform[] moveSpots;
    [FormerlySerializedAs("startWaitTime")] [SerializeField] private float initialMoveWaitTime;
    float waitTime;

    Rigidbody2D m_Rb;
    private Animator m_Animator;


    private bool playerInArea;

    private float timeInArea;
    [SerializeField] float timeToattack = 4f;
    [SerializeField] private int maxBullet = 2;
    
    [SerializeField] bool canBlock;


    private void Awake()
    {

        randomSpot = Random.Range(0, moveSpots.Length);
        m_Rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Animator = GetComponent<Animator>();

        //Physics2D.IgnoreCollision(m_player.gameObject.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        //Physics2D.IgnoreCollision(m_player.gameObject.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());

        PrepareObject();
    }

    private void Start()
    {


        waitTime = initialMoveWaitTime;
        timeBtwShoots = initialTimeBtwShoots;
        stoppingDistance = initialStoppingDistance;
        nextChargeTime = chargeRate = initialTimeBtwCharge;

        retreadDistance = initialRetreadDistance;

        playerInArea = false;
        timeInArea = 0f;

        GameManager.instance.addEnemy();

    }


    private void FixedUpdate()
    {
        switch (m_enemyType)
        {
            case (EnemyType.pathfinding):
                if (canTeleport)
                {
                    transform.position = moveSpots[randomSpot].position;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position,
                        speed * Time.deltaTime);
                   

                }

                if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
                {
                    if(timeBtwShoots >= 3) m_Animator.Play("Idle");
                    if (waitTime <= 0)
                    {
                        randomSpot = Random.Range(0, moveSpots.Length);
                        waitTime = initialMoveWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }
                
                else  m_Animator.Play("Sliding");

                break;

            case (EnemyType.followPlayer):



                if (!charging)
                {

                    direction = (m_player.transform.position - transform.position).normalized;

                    if (Vector2.Distance(transform.position, m_player.position) > stoppingDistance)
                    {

                        transform.position = Vector2.MoveTowards(transform.position, m_player.position,
                            speed / 2 * Time.deltaTime);
                        //Debug.Log("sadasdas");
                        //rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                    }
                    //else if(Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) > stoppingDistance){
                    //    transform.position = this.transform.position; transform.position = this.transform.position;}

                    else if (Vector2.Distance(transform.position, m_player.position) < retreadDistance)
                    {
                        transform.position =
                            Vector2.MoveTowards(transform.position, m_player.position, -speed * Time.deltaTime);
                        //rb.MovePosition(transform.position + direction * -speed * Time.deltaTime);
                    }
                }


                else
                {
    
                 if (Vector2.Distance(transform.position, toTackle) < 0.1f) m_Animator.Play("Resting");
                    transform.position = Vector2.MoveTowards(transform.position, toTackle,
                        speed * 2 * Time.deltaTime);

                }

                break;
        }


        if(canBlock){
            if (Vector2.Distance(transform.position, m_player.transform.position) < 2f){

            }
            else m_Animator.Play("Block"); 
}

        if (canShoot)
        {
            if (timeBtwShoots <= 0)
            {

                StartCoroutine(Shoot());
                //Shoot();

            }
            else
            {
                timeBtwShoots -= Time.deltaTime;
            }
        }


        //Debug.Log(Time.time + " : " + nextChargeTime);

        if (canCharge && Time.time > nextChargeTime)
        {
            Charge();
        }

        if (canAttack && playerInArea)
        {
            timeInArea += Time.deltaTime;
            if (timeInArea > timeToattack) Attack();
        }

    }



    void Attack()
    {
        Debug.Log("asadas");
        m_Animator.Play("Attack");
        timeInArea = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && charging)
        {

            PlayerManager.instance.ReduceHealth(transform.position.x);
            RebootCharge();
        }

        if (other.CompareTag("Player") && canAttack)
        {
            m_Animator.Play("Idle"); 
            playerInArea = true;
            timeInArea = 0f;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            m_Animator.Play("Block"); 
            playerInArea = false;
            timeInArea = 0f;
        }
    }




    public void Stunned(int stunningTime)
    {
        float speedAux = this.speed;
        this.speed = 0;
        StartCoroutine(this.StunTimer(stunningTime, speedAux));
    }

    private IEnumerator StunTimer(int stunningTime, float speedAux)
    {
        yield return new WaitForSecondsRealtime(stunningTime);
        this.speed = speedAux;
    }


    IEnumerator Shoot()
    {
        timeBtwShoots = initialTimeBtwShoots;
        Debug.Log("BUUUUUM");
        int projectileToInstantiate = Random.Range(0, projectile.Length);
        for (int i = 0; i < Random.Range(0, maxBullet); i++)
        {
            if (gameObject.name != "Sword") m_Animator.Play("Attack");
            else Instanciar(projectileToInstantiate);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Instanciar(int x)
    {
        var newProjectile =  Instantiate(projectile[x], transform.position, Quaternion.identity, transform);
        newProjectile.transform.parent = gameObject.transform.parent;
        
    }
    /*


           
            var newProjectile = Instantiate(projectile[projectileToInstantiate], transform.position, Quaternion.identity, transform);
            newProjectile.transform.parent = gameObject.transform.parent.parent;
            timeBtwShoots = initialTimeBtwShoots;

        }
    }*/


    


    void Charge()
    {
        if(!charging){
                    Debug.Log("cargar");
        canCharge = false;
        toTackle = new Vector2(m_player.transform.position.x, m_player.transform.position.y);
        stoppingDistance = 1;
        retreadDistance = 1;
        charging = true;
   
        
        StartCoroutine(ExecuteAfterTime(5f));
        }

    }

     IEnumerator ExecuteAfterTime(float time)
    {
         yield return new WaitForSeconds(time);
 

        if(charging){
            RebootCharge();
        }


    }

    void RebootCharge(){
            m_Animator.Play("Idle");
                stoppingDistance = initialStoppingDistance;
        retreadDistance = initialRetreadDistance;
        nextChargeTime = Time.time + chargeRate;
        charging = false;
        canCharge = true;
    }





    /*void Die() 
    {
        //Destroy(gameObject);
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = falsse;
    //}

    //GameObject.Instantiate(BloodParticleMeat, transform.position, transform.rotation);
        //BloodParticleMeat.SetActive(true);
        
       
        StartCoroutine(Die_Enemy());

        //GameObject.Instantiate(BloodParticle, transform.position, transform.rotation);

        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = falsse;
    }
    private IEnumerator Die_Enemy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        yield return null;
    }*/

    
    
    /*public void TakeProgressiveDamage(int damage, int damageTime, int nextDamageTime)
    {
        for(int i = 0; i < damageTime; i += nextDamageTime)
        {
            StartCoroutine(this.WaitAndDamage(i + 1, damage));
        }
    }

    private IEnumerator WaitAndDamage(int damageTime, int damage)
    {
        yield return new WaitForSecondsRealtime(damageTime);
        this.TakeDamage(damage);
    }*/

}
