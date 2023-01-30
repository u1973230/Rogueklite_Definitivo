using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Body : MonoBehaviour
{

    [SerializeField] int health = 25;

    enum stateMachine
    {
        fase1,
        follow,
        rest,
        idle,
        punchMelee,
        punchEarth,
        restingFromPunch,
        tPose,
        shoot,
    }

    [SerializeField] float attackRange, detectRange;
    bool playerInArea, playerOutArea;

    float timeInArea, timeOutArea;

    GameObject m_sword, m_player;
    int attack = 0;

    stateMachine m_State;

    public float speed = 10f;

    //variables de la epxlosion
    float cumSpeed = 2f;
    [SerializeField] GameObject objToSpawn;
    float radius = 6f;
    float degree = 360f;
    float numberOfSpawns = 15f;
    float nextSpawnTime = 2f;
    float spawnTimer = 0;
    float direction = 1;

    //variabbles state machine
    bool vulnerable, attacking, playerHitted    ;

    private float timeRolling = 1.5f,
        timePosingT = 1.5f,
        timeRestitng = 2f,
        timeRestitngPunch = 4f,
        timeFollowing = 4f,
        timePunching = 0.5f,
        timePunchingMelee = 1.5f,
        timeToEndState;

    //variables de tp + putaso
    Vector3 newPosition, oldPosition;




    [SerializeField] private float stoppingDistance, retreadDistance;

    private void Start()
    {




        playerInArea = false;
        playerInArea = false;
        timeInArea = 0f;

        //m_sword = transform.parent.transform.Find("Sword");
        //ParentGameObject.transform.GetChild (0).gameObject; 
        m_sword = GameObject.FindGameObjectWithTag("Sword");
        m_player = GameObject.FindGameObjectWithTag("Player");

        vulnerable = false;
        attacking = false;

        PrepareFase2();


    }


    private void FixedUpdate()
    {

            switch (m_State)
            {
                case stateMachine.fase1:
                    if (playerInArea) {
                        timeInArea += Time.deltaTime;
                        if (timeInArea > 2) {
                            BodyAnimationController.sharedInstance.animPlaDanceSword();
                            m_sword.SetActive(false);
                        }
                    }

                    else
                    {
                        //mirar el problema de que el personaje st'e dentro una vez est'e llamada la funcion
                        timeOutArea += Time.deltaTime;
                        if (timeOutArea > 7) {
                            attackSword();
                        }
                    }
                    break;
                
                case stateMachine.follow:
                    followPlayer();
                    //Debug.Log(Time.time + " " + timeToEndState);
                    if (playerHitted) {
                        m_State = stateMachine.punchMelee;
                        changeState(m_State);
                    }
                    else if (Time.time > timeToEndState) {
                        
                        m_State = stateMachine.rest;
                        changeState(m_State);
                    }
                    break;

                case stateMachine.punchEarth:
                    if (Time.time > timeToEndState) {
                        if (playerHitted) m_State = stateMachine.idle;
                        else m_State = stateMachine.restingFromPunch;
                        changeState(m_State);

                    }
                    break;
                
                case stateMachine.punchMelee:
                    if (Time.time > timeToEndState) {
                        Roll();
                    }
                    break;
                
                case stateMachine.tPose:
                    if (Time.time > timeToEndState) {
                        Cumeada();
                        m_State = stateMachine.shoot;
                        changeState(m_State);
                    }

                    break;
                case stateMachine.shoot:
                    if (Time.time > timeToEndState) {
                        Roll();
                    }

                    break;
                case stateMachine.rest:
                    if (Time.time > timeToEndState) {
                        Roll();
                    }
                    break;
                case stateMachine.idle:
                    if (Time.time > timeToEndState) {
                        Roll();
                    }

                    break;
                case stateMachine.restingFromPunch:
                    if (Time.time > timeToEndState) {
                        transform.position = oldPosition;
                        Roll();
                    }

                    break;


            
        }
    }


    void PrepareFase2()
    {
        m_State = stateMachine.rest;
        changeState(m_State);
        transform.GetChild(0).gameObject.SetActive(false);
        stoppingDistance = 1f;
        retreadDistance = 1f;
        speed = 6;
    }


    void Roll()
    {

        attack = Random.Range(0, 10);
        /*if (attack < 5)
        {
            m_State = stateMachine.follow;
        }
        else if (attack > 7)
        {
            
        }
        else
        {
            m_State = stateMachine.tPose;
            Cumeada();
        }*/


        m_State = stateMachine.follow;
        changeState(m_State);


    }

    void changeState(stateMachine state)
    {
        switch (state)
        {
            case stateMachine.follow:
                //DEBUGEANDO

                BodyAnimationController.sharedInstance.animPlayIdle();
                attacking = true;
                vulnerable = false;
                timeToEndState = Time.time + timeFollowing;
                break;
            case stateMachine.punchMelee:
                BodyAnimationController.sharedInstance.animPlayPunch();
<<<<<<< Updated upstream
=======
                //MARC
                mano_mas_pisoton.Play();
>>>>>>> Stashed changes
                timeToEndState = Time.time + timePunchingMelee;
                attacking = true;
                vulnerable = false;
                break;
            case stateMachine.restingFromPunch:
                BodyAnimationController.sharedInstance.animRestAttackPunchEarth();
                timeToEndState = Time.time + timeRestitngPunch;
                attacking = false;
                vulnerable = true;
                break;
            case stateMachine.punchEarth:
<<<<<<< Updated upstream
                timeToEndState = 100f;
                StartCoroutine(attackWithoutSword());
=======
                //MARC
                heavy.Play();
                timeToEndState = Time.time + timePunchInEarth;
>>>>>>> Stashed changes
                attacking = true;
                vulnerable = false;
                break;
            case stateMachine.tPose:
                BodyAnimationController.sharedInstance.animPlaytPose();
                vulnerable = true;
                attacking = false;
                timeToEndState = Time.time + timePosingT;
                break;
            case stateMachine.shoot:
                Cumeada();
                BodyAnimationController.sharedInstance.animPlayIdle();
                vulnerable = true;
                attacking = false;
                timeToEndState = Time.time + timeRestitng;
                break;
            case stateMachine.rest:
                BodyAnimationController.sharedInstance.animPlayIdle();
                vulnerable = true;
<<<<<<< Updated upstream
=======
                //MARC
                vulnerable_sonido.Play();
                attacking = false;
                timeToEndState = Time.time + timeRestitng;
                break;
            case stateMachine.attackSwordEarth:
                BodyAnimationController.sharedInstance.animPlayAttackSword();
                //MARC
                heavy_espada.Play();
                vulnerable = false;
                attacking = true;
                timeToEndState = Time.time + timeRestitng;
                break;
            case stateMachine.attackSwoordNoEffect:
                BodyAnimationController.sharedInstance.animPlayAttackSwordNoEffect();
                vulnerable = false;
>>>>>>> Stashed changes
                attacking = false;
                timeToEndState = Time.time + timeRestitng;
                break;
            default:
                BodyAnimationController.sharedInstance.animPlayIdle();
                vulnerable = false;
                attacking = false;
                timeToEndState = Time.time + timeRestitng;
                break;

        }
        playerHitted = false;
    }


    void followPlayer()
    {
        if (Vector2.Distance(transform.position, m_player.transform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position,
                speed / 2 * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, m_player.transform.position) < retreadDistance)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, m_player.transform.position, -(speed - 2) * Time.deltaTime);
        }
<<<<<<< Updated upstream
    }


    void FollowAndAttack()
    {

=======
        //MARC
        levitar.Play();
>>>>>>> Stashed changes
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            if (m_State == stateMachine.fase1) {
                playerInArea = true;
                playerOutArea = false;
                timeOutArea = 0f;
                timeInArea = 0f;
            }
            else if (attacking) {
                PlayerManager.instance.ReduceHealth(transform.position.x);
                playerHitted = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
            playerOutArea = true;
            timeOutArea = 0f;
            timeInArea = 0f;
        }
        //CAMBIAR POR GO IDLE CON DELAY
        //BodyAnimationController.sharedInstance.animPlayIdle();
    }


    void attackSword()
    {
        StartCoroutine(GoAttackSword());

    }


    IEnumerator attackWithoutSword()
    {

        // This will wait 1 second like Invoke could do, remove this if you don't need it
        playerInArea = false;
        playerOutArea = false;
        timeOutArea = 0;


        BodyAnimationController.sharedInstance.animPlayMoveSword();
        yield return new WaitForSeconds(2f);
        newPosition = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 1.5f);
        yield return new WaitForSeconds(0.25f);
        BodyAnimationController.sharedInstance.animPlayAttackPunchEarth();
        oldPosition = transform.position;
        transform.position = newPosition;


        timeOutArea = 0;

        timeToEndState = Time.time + timePunching;

        //m_State = stateMachine.;
        //changeState(m_State);
    }

    

    public IEnumerator GoAttackSword()
    {

        Vector3 newPosition, oldPosition;
        // This will wait 1 second like Invoke could do, remove this if you don't need it
        playerInArea = false;
        playerOutArea = false;
        timeOutArea = 0;


        BodyAnimationController.sharedInstance.animPlayMoveSword();
        yield return new WaitForSeconds(1f);
        m_sword.SetActive(false);
        yield return new WaitForSeconds(1f);
        newPosition = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 1.5f);
        oldPosition = transform.position;
        yield return new WaitForSeconds(0.75f);
        transform.position = newPosition;
        BodyAnimationController.sharedInstance.animPlayAttackSword();
        yield return new WaitForSeconds(1.5f);
        BodyAnimationController.sharedInstance.animPlayIdle();
        yield return new WaitForSeconds(0.5f);
        transform.position = oldPosition;
        //mover la la espada al lado
        m_sword.SetActive(true);
        timeOutArea = 0;

        //transform.position = 
    }





    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }



    public void Cumeada()
    {
<<<<<<< Updated upstream

=======
        
        //MARC
        escupir.Play();
>>>>>>> Stashed changes
        float arclenght = (degree / 360) * 2 * Mathf.PI;
        float nextAngle = arclenght / numberOfSpawns;
        float angle = 0;
        for (int i = 0; i < numberOfSpawns; i++)
        {
            float x = Mathf.Cos(angle) * radius * direction;
            float y = Mathf.Sin(angle) * radius * direction;

            var obj = Instantiate(objToSpawn, transform.position, Quaternion.identity);
            var rb = obj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(x, y) * cumSpeed;
            angle += nextAngle;

            Destroy(obj, 2f);
        }
    }

    public GameObject[] GameObjectList;

    void Teleport()
    {
<<<<<<< Updated upstream
=======
        //MARC
        boss_tp.Play();
>>>>>>> Stashed changes
        float FurthestDistance = 0;
        GameObject FurthestObject = null;
        foreach (GameObject Object in GameObjectList)
        {
            float ObjectDistance = Vector3.Distance(transform.position, Object.transform.position);
            if (ObjectDistance > FurthestDistance)
            {
                FurthestObject = Object;
                FurthestDistance = ObjectDistance;
            }
        }

        transform.position = FurthestObject.transform.position;
    }


    public void ReduceHealth()
    {
        if (vulnerable)
        {
            health--;
        
            Debug.Log("recibio danooo");
        
            if (health <= 0)
            {
                //acabar partida
                Destroy(gameObject);
            }
        }

    }

}
/*
//putaso
if(playerInArea){
    timeInArea+=Time.deltaTime;
    if(timeInArea>2){
            BodyAnimationController.sharedInstance.animPlayPunch();
            if(timeInArea>3){
                BodyAnimationController.sharedInstance.animPlaySecondPunch();
                timeInArea=0;
            }    
        }
    }
//putaso al suelo
else {
    //mirar el problema de que el personaje st'e dentro una vez est'e llamada la funcion
    timeOutArea +=Time.deltaTime;

    if(timeOutArea>5) {
        if(attack==0){
            Debug.Log("deberia dar putaso al suelo");
            StartCoroutine(attackWithoutSword());
        }
        else {
            Debug.Log("deberia cumear");
            StartCoroutine(tPose());
        }
    }
}*/