using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Characters
{
    PLAYTEST_CHAR = 0,
    PLAYTEST_CHAR_2,
    MAX_CHARACTER
};

public class CharacterBase : MonoBehaviour 
{
    //Character Basic Data
    bool inAir;
    bool canJump;
    bool staggered;
    bool isDead;
    Characters name;
    uint health;
    uint ultiBar;
    uint staggerMeter;
    uint jumpForce;
    uint moveSpeed;
    Vector3 direction;
    Image characterArt;
    Rigidbody2D rigidbody;

    public void Start()
    {
        inAir = staggered = false;
        canJump = true;
        Init();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Init()
    {
        name = Characters.PLAYTEST_CHAR;
        health = 100;
        ultiBar = 0;
        staggerMeter = 50;
        moveSpeed = 500;
        jumpForce = 10000;
        gameObject.transform.position = new Vector3(0, 0, 0);
        direction =  new Vector3(0, 0, 0);
    }

    public virtual void Update() 
    {
        ReadControl();
        if (MoveCondition())
            Move();
        if (JumpCondition())
            Jump();

        Recalculate();
    }

    public virtual void ReadControl()
    {
        //Standard Keyboard Controls
        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.UpArrow))
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        //xBox360 Controller Support
        if (Input.GetAxis("leftStick_X_xBox360") != 0 || Input.GetAxis("leftStick_Y_xBox360") != 0)
        {
            direction.Set(Input.GetAxis("leftStick_X_xBox360"), -Input.GetAxis("leftStick_Y_xBox360"), 0);    
        }
    }

    public virtual bool MoveCondition()
    {
        return !direction.x.Equals(0);
    }
    public virtual void Move() 
    {
        //if(Input.GetKeyDown(KeyCode.A)
        //|| Input.GetKeyDown(KeyCode.LeftArrow)
        //|| Input.GetKeyDown(KeyCode.D)
        //|| Input.GetKeyDown(KeyCode.RightArrow)
        //|| Input.GetAxis("leftStick_X_xBox360") != 0)
        //{
        rigidbody.AddRelativeForce(new Vector2(moveSpeed * direction.x * Time.deltaTime, 0));
        Debug.Log("Move!");
        //}
    }
    public virtual bool JumpCondition()
    {
        return (!inAir && direction.y > 0);
    }
    public virtual void Jump() 
    {
        //Jumping
        //if (Input.GetKeyDown(KeyCode.Space)
        //    || Input.GetAxis("leftStick_Y_xBox360") < 0)
        //{
        inAir = true;
        rigidbody.AddRelativeForce(new Vector2(0, jumpForce * Time.deltaTime));
        Debug.Log("Jump! ");
        //}
    }

    public virtual void Recalculate()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (inAir && rigidbody.velocity.y == 0)
        {
            inAir = false;
        }
        direction = new Vector3(0, 0, 0);
    }

    public virtual void Attack() { }
    public virtual void Block() { }

    public virtual void SkillA() { }
    public virtual void SkillB() { }
    public virtual void SkillC() { }
    public virtual void SkillD() { }

    //When a Character Dies
    public virtual void Die() 
    {
        //Player Lose Ability to Control
        //Death Animation Plays
        //Win for Opponent Animation Plays
        Debug.Log("Died");
    }
    public virtual void Reset() { }
}
