using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class spawn1 : MonoBehaviour
{
    
    public List<GameObject> roomList;
    public GameObject[] roomListArray;



    private void Start() {
    

    GameManager.instance.GenerateRoom();
    string e = (GameManager.instance.a.ToString() + GameManager.instance.b.ToString());
    Debug.Log(GameManager.instance.a.ToString() + GameManager.instance.b.ToString() + GameManager.instance.c.ToString() + GameManager.instance.d.ToString());


    roomListArray = Resources.LoadAll<GameObject>("1/" + e);
    roomList = roomListArray.ToList();

    GameObject roomToBuild = roomList [Random.Range (0, roomList.Count)];
    GameObject newRoom = Instantiate (roomToBuild, transform.position, Quaternion.identity) as GameObject;
    newRoom.transform.parent = gameObject.transform;
    
    }


}
