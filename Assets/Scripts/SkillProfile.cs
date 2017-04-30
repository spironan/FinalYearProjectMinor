using UnityEngine;
using System.Collections;

public class SkillProfile : MonoBehaviour {

    [Range(1,5)]
    public int keysToActivate;
    public int player_ID;
    public float damagePerSecond;

    [HideInInspector]
    public int[] directionToPress = new int[5];

    //[HideInInspector]
    public bool activateSkill = false;

    public void determineKeyDirections()
    {
        for(int i = 0; i < 5; ++i)
        {
            if(i < keysToActivate)
            {
                directionToPress[i] = Random.Range(0, 4);
               // Debug.Log(directionToPress[i]);
            }
            else
            {
                directionToPress[i] = -1;//set all the slots to -1
            }
            
        }
    }
}
