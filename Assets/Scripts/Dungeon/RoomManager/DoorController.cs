using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] GameObject objects, bossDoor;
    [SerializeField] bool close, bossOpened;

    private void Awake() {
        if(!close) Close();
    }


        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player"))
            {
                
                if(close)
                    Close();
                if(!bossOpened)
                    OpenBossDoor();
                    
                    
            }
    }

    public void Close(){
        var myNewSmoke = Instantiate(objects,  (transform.position + new Vector3(0.5f, 0f, 0f)), Quaternion.identity);  
        myNewSmoke.transform.parent = gameObject.transform;
    }

    public void OpenBossDoor(){
        bossDoor.SetActive(false);
    }

    
    
    //[SerializeField] GameObject[] objects;


    private void Start() {
        //int rand = Random.Range(0, objects.Length);
        //
        //
        
    }

}
