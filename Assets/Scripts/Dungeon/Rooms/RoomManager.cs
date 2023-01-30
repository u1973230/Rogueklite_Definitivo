using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    
    private static RoomManager _instance;
    public static RoomManager instance{
        get{
            if(_instance == null){
                Debug.Log("Room Manager is Null!!!");
            }
            return _instance;
        }
    }



    public GameObject[] bottomRooms, topRooms, leftRooms, rightRooms;
    [SerializeField] int neededRooms, bossNeededRooms;
    [SerializeField] GameObject lobbyPrefab;
    [SerializeField] GameObject text;

    int runCount = 0, runCountBoss=0;

    int size;
    bool isExitRoom = false;
    [SerializeField] GameObject wallPrefab, spawnPoint;


    GameObject        roomRoot;

    private void Awake() {
        _instance = this;
    }


    private void Start() {
       roomRoot =  GameManager.instance.roomsGenerated;
    }


    public void AddRunCount(){
        runCount++;
        runCountBoss++;
    }




    public bool CheckRun(Vector3 vec){
        bool aux = false;
        if(runCount>=neededRooms){
            //Debug.Log("BARRASEANDO");
            vec.x += 4;
            vec.y -= 8;
            var myNewSmoke = Instantiate(lobbyPrefab, vec, Quaternion.identity);
            finishingBeta();
            //, roomsContainer.transform
            //LobbyManager.sharedInstance.Move(vec);
            aux = true;
            runCount=0;
        }
        return aux;
    }


    public void BuildBridge(Vector3 wallPosition){


        var roomParent = new GameObject();
        roomParent.name = "roomGenerated";
        var roomsContainer=new GameObject();
        roomsContainer.name = "roomInstantiated";
        var bridgeContainer=new GameObject();
        bridgeContainer.name = "bridgeGenerated";


        roomsContainer.transform.SetParent(roomParent.transform);
        bridgeContainer.transform.SetParent(roomParent.transform);
        roomParent.transform.SetParent(roomRoot.transform);
        
        

        //Instantiate(roomsContainer,transform.position,Quaternion.identity);
        //Instantiate(bridgeContainer,transform.position,Quaternion.identity);
        
        
        
        AddRunCount();



        Vector3 wallPositionL = wallPosition;
        wallPositionL.x -= 2;
        Vector3 wallPositionR = wallPosition;
        wallPositionR.x += 1;


        for (int i = 0; i < size; i++)
        {
            Instantiate(wallPrefab, wallPositionL, Quaternion.identity, bridgeContainer.transform);
            Instantiate(wallPrefab, wallPositionR, Quaternion.identity, bridgeContainer.transform);
            wallPositionL.y -= 1;
            wallPositionR.y -= 1;
        }

        wallPositionL.x -= 1;
        //wallPositionR.x -= 1;

        for (int i = 0; i < 2; i++)
        {
            Instantiate(wallPrefab, wallPositionL, Quaternion.identity, bridgeContainer.transform);
            Instantiate(wallPrefab, wallPositionR, Quaternion.identity, bridgeContainer.transform);
            wallPositionL.x -= 1;
            wallPositionR.x -= 1;
            wallPositionL.y -= 1;
            wallPositionR.y -= 1;
        }

        for (int i = 0; i < 10-size; i++)
        {
            Instantiate(wallPrefab, wallPositionL, Quaternion.identity, bridgeContainer.transform);
            Instantiate(wallPrefab, wallPositionR, Quaternion.identity, bridgeContainer.transform);
            wallPositionL.y -= 1;
            wallPositionR.y -= 1;
        }

    
        //por que wallpositions e resta antes en uno y depsues en otro? ERRORRRRRR
        if(CheckRun(wallPositionL)){
            
            wallPositionL.x +=2;
            wallPositionL.y -=4;
        }

        else{
            

            wallPositionL.x +=2;
            wallPositionL.y -=4;
            Instantiate(spawnPoint, wallPositionL, Quaternion.identity, roomsContainer.transform);

        }
    }

        

    public void finishingBeta(){
        text.SetActive(true);
    }


}
