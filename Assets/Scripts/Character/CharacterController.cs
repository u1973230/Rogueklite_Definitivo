using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour
{
    
    //VECTORES
    private Vector3 startPosition;

    //variables efecto recibir daño
    SpriteRenderer m_SpriteRenderer;
    private bool isFlickerEnabled = false;

    //VARIABLES
   protected float speed, runSpeed, stamina;
   protected int health, mana;
    [SerializeField] protected int INITIAL_HEALTH = 5, INITIAL_SPEED = 3, INITIAL_RUNSPEED = 8, INITIAL_MANA = 4;
    [SerializeField] protected float  INITIAL_STAMINA =150;

    protected int dmgToTake = 1, manaToReduce = 1, MAX_MANA,MIN_MANA, MIN_HEALTH, MAX_HEALTH;
    protected float MAX_STAMINA,MIN_STAMINA;

    private float pushForce = 1000;



    //variables que no usaremos en esta berrsion
    //int MIN_DEFENSE = 0, INITIAL_DMG = 1, INITIAL_DMG_D = 2, INITIAL_DMG_M = 3, INITIAL_DEFENSE = 0, defense


    protected void PrepareObject()
    {
 
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        
        startPosition = transform.position;
        SetStats();
    }





    ///                 ***** FUNCIONES PUBLICAS *****

    //devolver variables del jugador
    public int GetHealth(){
        return this.health;
    }

    public int GetMaxHealth(){
        return this.MAX_HEALTH;
    }
    
    public int GetMana(){
        return this.mana;
    }

    public int GetMaxMana(){
        return this.MAX_MANA;
    }
    
    public float GetStamina(){
        return this.stamina;
    }

    public float GetMaxStamina(){
        return this.MAX_STAMINA;
    }

    public float GetSpeed(){
        return this.speed;
    }



    public bool ReduceMana()
    {
        bool aux = true;
        //el mana no puede ser negativo
        if (this.mana - manaToReduce>=MIN_MANA){
            this.mana -= manaToReduce;
            Mana.sharedInstance.ReduceCharge(PlayerManager.instance.GetMana());
            //Debug.Log("mana actual: " + mana);
            
        }
        else {
            aux = false;}
        return aux;
    }

    public bool ReduceStamina(float take)
    {
        bool aux = true;
        if (this.stamina - take>=MIN_STAMINA){
            this.stamina -= take;
            //Debug.Log("energia actual: " + stamina);
        }
        else aux = false;
        return aux;
    }



    public void RestoreMana()
    {
        this.mana += manaToReduce;
        if(this.mana>MAX_MANA) this.mana=MAX_MANA;
    }

    public void RestoreStamina(float bonus)
    {
        this.stamina += bonus;
        if(this.stamina>MAX_STAMINA) this.stamina=MAX_STAMINA;
    }



    public void IncreaseHealth(){
        this.MAX_HEALTH += dmgToTake;
        this.health += dmgToTake;
        
    }

      public void IncreaseMana(){
        this.MAX_MANA += manaToReduce;
        this.mana += manaToReduce;
        Mana.sharedInstance.addCharge(PlayerManager.instance.GetMana()-1);
    }



    public void IncreaseStamina(float bonus){
        this.MAX_STAMINA += bonus;
        this.stamina += bonus;
    }
    

    public void SetMana(int x){
        this.mana = x;
        this.MAX_MANA = x;
    }

    public void SetHealth(int x){
        this.health = x;
        this.MAX_HEALTH = x;
    }

    public void SetStamina(int x){
        this.stamina = x;
        this.MAX_STAMINA = x;
    }


    public void SetSpeed(int x){
        this.speed=x;
    }

    public void SetStats(){

        MIN_HEALTH=0;
        MIN_STAMINA=0;
        MIN_MANA = 0;

        MAX_HEALTH = INITIAL_HEALTH;
        MAX_MANA = INITIAL_MANA;
        MAX_STAMINA = INITIAL_STAMINA;

        speed = INITIAL_SPEED;
        health = INITIAL_HEALTH;
        mana = INITIAL_MANA;
        stamina = INITIAL_STAMINA;
        runSpeed = INITIAL_RUNSPEED;
    }




    public void ReduceHealth(float xPos)
    {
<<<<<<< Updated upstream
        //la vida puede ser negativa, muere igualk con -1 que con 10
        //if (take - (take * (defense / 100))<take){
        //    this.health -= (take - (take * (defense / 100)));
        //    if(this.health<=0) Die();
        //}
        health -= dmgToTake;
        isFlickerEnabled = true;

        if(xPos > transform.position.x){
            gameObject.transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.y);
        } else {
            gameObject.transform.position = new Vector3(transform.position.x+0.5f, transform.position.y, transform.position.y);
        }
        
=======
        health = health -1;
        hurt_sound.Play();
        if(health < 1 ){
            Die(skullsToInstantiate);
        }

        else
        {
            if(xPos > transform.position.x){
                gameObject.transform.position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
            } else {
                gameObject.transform.position = new Vector3(transform.position.x+0.5f, transform.position.y, transform.position.z);
            }
        }
        
        //PDamage.SetActive(true);
>>>>>>> Stashed changes
        
        //Debug.Log(m_SpriteRenderer.color);
        //m_SpriteRenderer.color = Color.red;        
        //StartCoroutine(colorFlickerRoutine());
        //yield return new WaitForSeconds(0.75f);
        if(this.health <= 0 ){
            Die();
        }
    }


    public void Die(){
        Destroy(gameObject);
        if(gameObject.CompareTag("Player")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //AQUI DEBE IR CARGAR LA ESCENA DE MUERTE
        //ELIMINA LA LINEA DE TIME.TIMESCALE = 0
        }
        else if(gameObject.CompareTag("Enemy")){
            GameManager.instance.substractEnemy();
        }
    }


    /*IEnumerator colorFlickerRoutine(){
        while (isFlickerEnabled){
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.25f);
            isFlickerEnabled = false;
        }
    }*/


}
