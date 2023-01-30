using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimation : MonoBehaviour
{

    //crear un bool que sea "se puede comenzar una acción" que se ponga en true cuando se acabe una animacion y en fasle cuando se pulse un bootn que vaya a coemnzarla

    enum AnimationState {
    Idle,
    Running,
    AttackMelee,
    AttackMage,
    AttackDistance
    }

<<<<<<< Updated upstream
=======

    public AnimationState m_State;

>>>>>>> Stashed changes
    private static PlayerAnimation _instance;
    
    public static PlayerAnimation instance{
        get{
            if(_instance == null){
                Debug.Log("Player Manager is Null!!!");
            }
            return _instance;
        }
    }

    Animator anim;

    //AnimationState currentAnimationState = AnimationState.Idle;

    bool running = false, attacking = false;


    private void Awake()
    {
        _instance = this;
        anim=GetComponent<Animator>();
    }




    private void PlayState(AnimationState state)
    {
        string animName = string.Empty;
        switch (state)
        {
            case AnimationState.AttackDistance:
                animName = "AttackDistance";
                break;
            case AnimationState.AttackMage:
                animName = "AttackMage" ;   
                break;
            case AnimationState.AttackMelee:
                animName = "AttackMelee";   
                break;
            case AnimationState.Running:
                animName = "Running";
                break;
            default:
                animName = "Idle";
                break;

        }
        anim.Play(animName);
    }

<<<<<<< Updated upstream



    public void run(){
        if(!running && !attacking)
        {
   
            running = true;
            //currentAnimationState = AnimationState.running;
            //anim.Play("Running");
            PlayState(AnimationState.Running);
=======
    public float timeToEnd = 0.0f,
        sprintDelay = 0.5f,
        attackMageDelay = 2f,
        attackMeleeDay = 1f,
        hurtDelay = 1f,
        attDistanceDelay = 4f,
        nextAttackMageTime = 0,
        nextAttackDistanceTime = 0,
        nextAttackMeleeTime = 0,
        nextHurt = 0,
        nextSprint = 0,
        changeStateTime = 0.5f;
    
    [SerializeField] private AudioSource caminar_sound;
    [SerializeField] private AudioSource correr_sound;
    [SerializeField] private AudioSource hurt_sonido;
    [SerializeField] private AudioSource escupir;
    [SerializeField] private AudioSource melee;
>>>>>>> Stashed changes
        
        }

<<<<<<< Updated upstream
    }

    public void noRun(){
        running = false;
        //if(!attacking){
        //    anim.Play("Idle");
        //}
        if(!attacking){
            PlayState(AnimationState.Idle);

        }

    }

    public void Attacking(string typeOfAttack){
        attacking = true;
        running = false;
        switch (typeOfAttack){
            case "distance":
                PlayState(AnimationState.AttackDistance);
=======
    public void changeState(AnimationState state) {
        caminar_sound.Stop();
        correr_sound.Stop();
        switch (state) {
            case AnimationState.Idle:
                if (m_State != AnimationState.Apuntando) {
                    anim.SetFloat("a_speed", 1);
                    m_State = AnimationState.Idle;
                    anim.Play("Idle");
                    PlayerAttack.instance.NoAttack();
                }
                break;
            
            case AnimationState.Walking:
                if ((m_State != AnimationState.Apuntando)) {
                    anim.SetFloat("a_speed", 1);

                    //PlayerManager.instance.SetSpeed(PlayerManager.instance.GetInitialSpeed());
                    m_State = AnimationState.Walking;
                    anim.Play("Running");
                    caminar_sound.Play();
                    PlayerAttack.instance.NoAttack();
                }
                break;
            
            case AnimationState.Running:
                if (PlayerManager.instance.ReduceStamina(PlayerManager.instance.runCost))
                {
                    if ((m_State == AnimationState.Walking) && (m_State != AnimationState.Apuntando)) {
                        anim.SetFloat("a_speed", 2);

                        m_State = AnimationState.Running;
                        anim.Play("Running");
                        correr_sound.Play();
                        PlayerAttack.instance.NoAttack();
                    }
                }
                break;

            
            case AnimationState.Sprint:
                if ((m_State == AnimationState.Walking) || (m_State == AnimationState.Running)) {
                    if (Time.time > timeToEnd && Time.time > nextSprint) {
                        PlayerMovement.instance.Sprint();
                        m_State = AnimationState.Walking;
                        anim.Play("Running");
                        correr_sound.Play();
                        timeToEnd = Time.time + changeStateTime;
                        nextSprint = Time.time + sprintDelay;
                    }
                }
                break;
            
            case AnimationState.Apuntando:
                if ((m_State == AnimationState.Walking) || (m_State == AnimationState.Idle) || (m_State == AnimationState.Running)) {
                    if (Time.time > timeToEnd && Time.time > nextAttackMageTime) {
                        if (PlayerManager.instance.ReduceStamina(PlayerManager.instance.AttMageCost())) {
                            PlayerMovement.instance.Apuntando();
                            m_State = AnimationState.Apuntando;
                            anim.Play("AttackMage");
                            escupir.Play();
                        }
                    }
                }
>>>>>>> Stashed changes
                break;
            case "mage":
                PlayState(AnimationState.AttackMage);
                break;
<<<<<<< Updated upstream
            default:
                PlayState(AnimationState.AttackMelee);
=======
            
            case AnimationState.AttackMelee:
                if((m_State == AnimationState.Walking) || (m_State == AnimationState.Idle) || (m_State == AnimationState.Running)) {
                    if (Time.time > timeToEnd && Time.time > nextAttackMeleeTime) {
                        if (PlayerManager.instance.ReduceStamina(PlayerManager.instance.AttMeleeCost())) {
                            PlayerAttack.instance.AttackingAsMelee();
                            m_State = AnimationState.AttackMelee;
                            anim.Play("AttackMelee");
                            melee.Play();
                            timeToEnd = Time.time + changeStateTime;
                            nextAttackMageTime = Time.time + attackMeleeDay;
                        }
                    }
                }
                break;
            
            case AnimationState.AttackDistance:
                if((m_State == AnimationState.Walking) || (m_State == AnimationState.Idle) || (m_State == AnimationState.Running)) {
                    if (Time.time > timeToEnd && Time.time > nextAttackDistanceTime) {
                        if (PlayerManager.instance.ReduceStamina(PlayerManager.instance.AttDistanceCost())) {
 
                            PlayerAttack.instance.AttackingAsDistance();
                            m_State = AnimationState.AttackDistance;
                            anim.Play("AttackDistance");
                            timeToEnd = Time.time + changeStateTime;
                            nextAttackDistanceTime = Time.time + attDistanceDelay;
                        }
                    }
                }
>>>>>>> Stashed changes
                break;
        }
    }

    public void animationSpeed()
    {
<<<<<<< Updated upstream
        if (PlayerMovement.instance.IsRunning()) anim.SetFloat("a_speed", 2);
        else anim.SetFloat("a_speed", 1);
    }
    
    
    public void noAttacking(){
        attacking = false;
=======
        
        
        if (Time.time > timeToEnd && Time.time > nextHurt)

        {

            anim.Play("Hurted");
            hurt_sonido.Play();
            PlayerManager.instance.ReduceHealth(x);


            m_State = AnimationState.Hurt;
            timeToEnd =  Time.time  + 1f;
            nextHurt = Time.time + hurtDelay;



        }
>>>>>>> Stashed changes

    }


}
