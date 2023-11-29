using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedChangeVolume : MonoBehaviour
{
    [SerializeField] private PlayerController _toChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            _toChange.AddAttackSpeed();
        }
    }
}
