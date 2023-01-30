using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    //particula olaia
    [SerializeField] private GameObject Correr;

    private static PlayerMovement _instance;
    
    public static PlayerMovement instance{
        get{
            if(_instance == null){
                Debug.Log("Player Manager is Null!!!");
            }
            return _instance;
        }
    }


    private Vector2 movement, mousePos;

    private Rigidbody2D rb;
    private SpriteRenderer m_mySpriteRenderer;
    private LineRenderer m_lr;

    private int sprintIncrease = 5;

    //variables para pulsardos veces
    bool reset = false, firstButtonPressed=false;
    float timeOfFirstButton=0f, sprintForce = 5000f;

    private bool running, apuntando;

    float angle;
    [SerializeField] Vector2 lookDir;




    private void Awake() {
        _instance = this;

        rb=GetComponent<Rigidbody2D>();
        m_mySpriteRenderer = GetComponent<SpriteRenderer>();
        m_lr = GetComponent<LineRenderer>();

        running = false;
    }


    private void FixedUpdate() {



        
        //apuntado
        //in scene
        //Debug.DrawRay(transform.position, new Vector2(movement.x*10, movement.y*10) , Color.green);
        //in game
        
        //fase de apuntar y fase de disaprar
        if(apuntando){
            
            PlayerAnimation.instance.Attacking("mage");
            m_lr.enabled = true;
            m_lr.SetPosition(0,  transform.position);
            m_lr.SetPosition(1, new Vector3(transform.position.x + movement.x*7, transform.position.y + movement.y*7,0));
        }

        else{
            m_lr.enabled = false;
            rb.MovePosition(rb.position + movement * (PlayerManager.instance.GetSpeed() * Time.fixedDeltaTime));
            lookDir = mousePos - rb.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            if ((movement.magnitude) != 0)
            {
                if (running && !PlayerManager.instance.ReduceStamina(1)) PlayerManager.instance.SplitVelocity();

                PlayerAnimation.instance.run();
            }
            else PlayerAnimation.instance.noRun();

            if(movement.x>0) m_mySpriteRenderer.flipX = true;
            else if(movement.x<0) m_mySpriteRenderer.flipX = false;
        }
        

    }

    public int Direction()
    {
        return m_mySpriteRenderer.flipX ? 1 : -1;
    }


    void LastUpdate(){
    if(!running)RecoverStamina();
    }


    public void MoveLogicMethod(Vector2 moveGet){
    movement = moveGet;
    }

    public Vector2 ReturnMove(){
        return movement;
    }



    void RecoverStamina(){
        PlayerManager.instance.RestoreStamina(10*Time.deltaTime);
    }


    void RecoverMana(){
        PlayerManager.instance.RestoreMana();
    }


    //o programar o borrar
    public void notRunning(){
        
        running = false;
        


    }

    public void Running()
    {
        running = true;
        
    }

    public void Apuntando(){
        if(PlayerManager.instance.ReduceStamina(25)) apuntando = true;
    }

    public bool IsPointing()
    {
        return apuntando;
    }

    public void Caminando(){
        apuntando = false;
    }


    public bool IsRunning()
    {
        return running;
    }

    public void Sprint()
    {
        if (PlayerManager.instance.ReduceStamina(25))
        {
            rb.AddForce(new Vector2(movement.x*sprintForce/4, movement.y*sprintForce/4), ForceMode2D.Force);   
        }
    }

  
}
