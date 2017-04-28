using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Characters
{
    PLAYTEST_CHAR = 0,
    MAX_CHARACTER
};

public class CharacterBase : MonoBehaviour 
{
    //Character Basic Data
    bool inAir;
    bool canJump;
    Characters name;
    uint health;
    uint ultiBar;
    uint staggerMeter;
    uint moveSpeed;
    uint jumpSpeed;
    Vector3 position;
    Image characterArt;

    public void Start() 
    {
        Init();
    }

    public virtual void Init()
    {
        name = Characters.PLAYTEST_CHAR;
        health = 100;
        ultiBar = 0;
        staggerMeter = 50;
        position = new Vector3(0, 0, 0);
        Debug.Log("this is ran");
    }
    public virtual void Update() 
    {
        Move();
        Jump();
    }

    public virtual void Move() 
    {
        if(Input.GetKeyDown(KeyCode.A)
        || Input.GetKeyDown(KeyCode.LeftArrow)
        || Input.GetKeyDown(KeyCode.D)
        || Input.GetKeyDown(KeyCode.RightArrow)
        || Input.GetAxis("leftStick_X_player1") != 0)
        {
            position += new Vector3 (moveSpeed * Input.GetAxis("leftStick_X_player1"),0,0);
            Debug.Log("Character Moved " + "leftStick_X_player1 value : " + Input.GetAxis("leftStick_X_player1"));
        }

    }
    public virtual void Jump() 
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetAxis("leftStick_Y_player1") > 0)
        {

        }
    }
    public virtual void Attack() { }
    public virtual void Block() { }

    public virtual void SkillA() { }
    public virtual void SkillB() { }
    public virtual void SkillC() { }
    public virtual void SkillD() { }

    //When a Character Dies
    public virtual void Reset() { }
}
