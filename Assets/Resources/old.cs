using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class old : MonoBehaviour
{
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    //GameObject[] objects;




    private void Awake() {
    
    string e;

    GameManager.instance.a=Random.Range(0, 2);
    GameManager.instance.b=Random.Range(0, 2);
    GameManager.instance.c=Random.Range(0, 2);
    GameManager.instance.d=Random.Range(0, 2);
    
    e = (GameManager.instance.a.ToString() + GameManager.instance.b.ToString());
    Debug.Log(e + GameManager.instance.c.ToString() + GameManager.instance.d.ToString());

    GameObject patata = new GameObject();
    //cambiarlo por un while por rendimiento
    for(int i=0; i<objects.Count; i++){
        if(objects[i].name == e) {
            patata = objects[i];
            break;
        }
    }

        var myNewSmoke = Instantiate(patata, transform.position, Quaternion.identity);
        myNewSmoke.transform.parent = gameObject.transform;



        
    }
}
