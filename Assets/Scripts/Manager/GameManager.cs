using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //prueba para la mazmorra
    public int a, b, c, d;

    private static GameManager _instance;
    public static GameManager instance{
        get{
            if(_instance == null){
                Debug.Log("Game Manager is Null!!!");
            }
            return _instance;
        }
    }


    public GameObject roomsGenerated;
    

    int numEnemies;

    GameObject actualRoom, m_player;
    public List<GameObject> roomsArray= new List<GameObject>();


    //public GameObject[] roomsArray;


    private void Awake(){
        _instance = this;
        roomsGenerated = new GameObject();
        roomsGenerated.name = "roomsGenerated";
    }

    private void Start() {
        actualRoom = GameObject.Find("LobbyBeta");
        numEnemies = 0;

       


    }





    public void roomCreated(GameObject lastRoomCreated){
        actualRoom = lastRoomCreated;
        roomsArray.Add(lastRoomCreated);
    }


    public void addEnemy(){
        numEnemies++;
    }

    public void substractEnemy(){
        numEnemies--;
        enemiesAlive();
    }

    public void enemiesAlive(){
    
        if(numEnemies==0){
            var _RoomManager = actualRoom.transform.Find("Room Manager");
            _RoomManager.GetComponent<instancedRoomManager>().finishRoom();

            
        }
    }




    public void GenerateRoom(){
        GameManager.instance.a=Random.Range(0, 2);
        GameManager.instance.b=Random.Range(0, 2);
        GameManager.instance.c=Random.Range(0, 2);
        GameManager.instance.d=Random.Range(0, 2);
    }
}


/*
    private static GameManager _instance;
    public static GameManager instance{
        get{
            if(_instance == null){
                Debug.Log("Game Manager is Null!!!");
            }
            return _instance;
        }
    }


    int sceneNum;

    public GameObject roomsGenerated;
    int numEnemies;
    GameObject actualRoom;
    public List<GameObject> roomsArray= new List<GameObject>();
    //public GameObject[] roomsArray;


    private void Awake(){

        if(instance == null){
            _instance = this;
            DontDestroyOnLoad(this);
        } else if(instance !=this){
            Destroy(gameObject);
        }


        _instance = this;
        roomsGenerated = new GameObject();
        roomsGenerated.name = "roomsGenerated";
    }

    private void Start() {
        actualRoom = GameObject.Find("LobbyBeta");

        numEnemies = 0;

        sceneNum = 0;
    }


    void NextScene(){
        sceneNum++;
        if(sceneNum>1) sceneNum = 0;
        SceneManager.LoadScene(sceneNum);
    }



    public void roomCreated(GameObject lastRoomCreated){
        actualRoom = lastRoomCreated;
        roomsArray.Add(lastRoomCreated);
    }


    public void addEnemy(){
        numEnemies++;
    }

    public void substractEnemy(){
        numEnemies--;
        enemiesAlive();
    }

    public void enemiesAlive(){
    
        if(numEnemies==0){
            var _RoomManager = actualRoom.transform.Find("Room Manager");
            _RoomManager.GetComponent<instancedRoomManager>().finishRoom();
        }
    }
}*/