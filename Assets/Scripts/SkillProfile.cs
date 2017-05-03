using UnityEngine;
using System.Collections;

public class SkillProfile : MonoBehaviour {

    [Range(1,5)]
    public int keysToActivate;
    public int player_ID;
    public float damagePerSecond;
    public float lifetime = 5;
    public float pSpeed = 20;

    public BoxCollider2D collider;

    //[HideInInspector]
    public GameObject owner;
    public GameObject enemy;

    public Vector2 direction = new Vector2(0,0);

    [HideInInspector]
    public int[] directionToPress = new int[5];

    //[HideInInspector]
    public bool activateSkill = false;


    private CharacterBase[] listOfPlayers;

    public virtual void determineKeyDirections()//virtual, so if dont want random spawn, we can set it ourselves
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

    public virtual void Start()
    {
        
    }

    public virtual void findEnemy()
    {
        listOfPlayers = GameObject.FindObjectsOfType<CharacterBase>();
        foreach (CharacterBase cb in listOfPlayers)
        {
            if (cb.gameObject != owner)
            {
                enemy = cb.gameObject;
            }
        }
    }
    public virtual void offSetSpawn(Vector2 dir, float offset)
    {
        //Debug.Log("hi");
    }
}
