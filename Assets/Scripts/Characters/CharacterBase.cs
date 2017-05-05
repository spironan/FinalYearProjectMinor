using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterBase : MonoBehaviour 
{
    //Character Basic Data Variable(s)
    bool inAir;
    bool canJump;
    bool stunned;
    bool isDead;
    bool isBlocking;
    bool canUlti;
    CHARACTERS name;
    ATTACKTYPE type;
    uint health;
    uint ultiBar;
    uint stunMeter;
    uint jumpForce;
    uint moveSpeed;
    Vector2 direction;
    Image characterArt;
    Image characterIcon;
    Rigidbody2D rigidbody;

    //Getter(s)
    public bool GetDead() { return isDead; }
    public uint GetHealth() { return health; }
    public Vector2 GetDirection() { return direction; }
    public Image GetIcon() { return characterIcon; }
    //Setter(s)
    public void SetDead(bool newIsDead) { isDead = newIsDead; }

    //Copy Constructor
    public CharacterBase(CharacterBase copy)
    {
        type = copy.type;
        name = copy.name;
        health = copy.health;
        ultiBar = copy.ultiBar;
        stunMeter = copy.stunMeter;
        jumpForce = copy.jumpForce;
        moveSpeed = copy.moveSpeed;
    }

    //This data are always the same,thus been place here
    public void Start()
    {
        name = CHARACTERS.PLAYTEST_CHAR;
        type = ATTACKTYPE.MID_RANGE;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Init();
    }
    //This Data Are to Be Loaded from a DataBase Next Time
    public virtual void Init()
    {
        canUlti = isBlocking = inAir = stunned = false;
        canJump = true;
        health = 100;
        ultiBar = 0;
        stunMeter = 50;
        moveSpeed = 15;
        jumpForce = 100000;
        direction = new Vector2(0, 0);
        this.transform.position = new Vector3(0, -2.5f, 0);
        rigidbody.velocity = new Vector2(0, 0);
    }

    //Overall Structure of how the code should flow
    public virtual void Update() 
    {
        ReadControl();
        if (MoveCondition())
            Move();
        if (JumpCondition())
            Jump();

        Recalculate();
    }

    //Read the Different Inputs and convert into the same input
    public virtual void ReadControl()
    {
        //WASD for player 1 and up down left right for player 2

        //Standard Keyboard Controls
        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.UpArrow))
        {
            direction.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        //xBox360 Controller Support
        else if (Input.GetAxis("leftStick_X_xBox360") != 0 
            || Input.GetAxis("leftStick_Y_xBox360") < -0.9)
        {
            if (Input.GetAxis("leftStick_Y_xBox360") > -0.9)
                direction.Set(Input.GetAxis("leftStick_X_xBox360"), 0);    
            else
                direction.Set(Input.GetAxis("leftStick_X_xBox360"), 1);
        }
    }
    public virtual bool MoveCondition()
    {
        return !direction.x.Equals(0);
    }
    public virtual void Move() 
    {
        gameObject.transform.position += new Vector3(moveSpeed * direction.x * Time.deltaTime, 0, 0);
        //Debug.Log("Move!");
    }
    public virtual bool JumpCondition()
    {
        return (!inAir && direction.y > 0.1);
    }
    public virtual void Jump() 
    {
        inAir = true;
        rigidbody.AddForce(new Vector2(0, jumpForce * Time.deltaTime));
        //Debug.Log("Jump!");
    }
    public virtual void Recalculate()
    {
        if (isDead)
            Die();
        if (ultiBar > 0 && !canUlti)
            canUlti = true;
        direction = new Vector2(0,0);
    }

    
    //Check to See if Player Touch the ground,so that it can jump again
    public virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground" && inAir)
            inAir = false;
    }

    public virtual void Attack() 
    {
        switch (type)
        {
        case ATTACKTYPE.GLOBAL:
            break;
        case ATTACKTYPE.MID_RANGE:
            break;
        case ATTACKTYPE.MELEE:
            break;
        }
    }

    public virtual void Block() 
    {
        isBlocking = true;
    }

    public virtual void CastSkillA() { }
    public virtual void CastSkillB() { }
    public virtual void CastSkillC() { }
    public virtual void CastSkillD() { }
    public virtual void CastUltimate() 
    {
        if (canUlti)
        {
            //cast ulti here
            canUlti = false;
            ultiBar = 0;
        }
    }

    //Go through this function to make character take damage
    public virtual void TakeDamage(uint damage)
    {
        uint temp = damage;
        if (isBlocking)
        {
            //Temporary damage nerf
            temp /= 5;
        }
        health -= temp;
        if (health <= 0)
            isDead = true;
    }
    //When a Character Dies , can only be called internally
    public virtual void Die() 
    {
        //Player Lose Ability to Control
        //Death Animation Plays
        //Win for Opponent Animation Plays
        Debug.Log("Died");
        Reset();
    }
    //Reset Player to Default Position
    public virtual void Reset()
    {
        Init();
        isDead = false;
        Debug.Log("Reset");
    }

}
