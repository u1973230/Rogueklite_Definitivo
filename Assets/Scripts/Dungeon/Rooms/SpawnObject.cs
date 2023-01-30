using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject[] objects;


    private void Start() {

        int rand = Random.Range(0, objects.Length);
        var myNewSmoke = Instantiate(objects[rand], transform.position, Quaternion.identity);
        myNewSmoke.transform.parent = gameObject.transform;
    }



}
