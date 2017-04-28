using UnityEngine;
using System.Collections;

public class DeadZoneScript : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CharacterBase>().Die();
    }
}
