using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    // connected save controller
    [SerializeField] private SaveController _toSave;

    // function to call save function when a player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            _toSave.SaveGame();   
        }
    }
}
