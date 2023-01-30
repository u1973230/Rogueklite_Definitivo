using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeDoor : DoorController
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("PROBANDO PROBANDO");
        Close();
    }
}
