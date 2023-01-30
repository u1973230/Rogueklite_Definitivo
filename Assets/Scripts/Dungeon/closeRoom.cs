using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeRoom : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] GameObject objects;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            doors[0].SetActive(true);
            doors[1].SetActive(true);
            objects.SetActive(false);
        }
    }

    private void Update() {
        //hacer esto al matar un enemigo en vez de cada frame
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) {
            Debug.Log("todos los enemigos muertos");
            doors[0].SetActive(false);
            doors[1].SetActive(false);
            objects.transform.GetChild (0).gameObject.SetActive(true);
        }
    }


}
