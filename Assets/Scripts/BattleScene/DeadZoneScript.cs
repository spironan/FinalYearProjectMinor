using UnityEngine;
using System.Collections;

public class DeadZoneScript : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something Entered : " + other.gameObject.tag);
        if(other.gameObject.tag == "Player")
            other.gameObject.GetComponent<CharacterBase>().SetDead(true);
    }
}
