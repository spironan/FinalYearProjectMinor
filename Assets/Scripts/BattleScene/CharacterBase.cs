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
    Characters name;
    AttackType type;
    uint health;
    uint ultiBar;
    uint stunMeter;
    uint jumpForce;
    uint moveSpeed;
    Vector2 direction;
    Image characterArt;
    Rigidbody2D rigidbody;

    //Getter(s)
    public bool GetDead() { return isDead; }
    //Setter(s)
    public void SetDead(bool newIsDead) { isDead = newIsDead; }


    //This data are always the same,thus been place here
    public void Start()
    {
        isBlocking = inAir = stunned = false;
        canJump = true;
        canUlti = false;
        direction = new Vector2(0, 0);
        Init();
    }
    //This Data Are to Be Loaded from a DataBase Next Time
    public virtual void Init()
    {
        name = Characters.PLAYTEST_CHAR;
        type = AttackType.MID_RANGE;
        health = 100;
        ultiBar = 0;
        stunMeter = 50;
        moveSpeed = 15;
        jumpForce = 100000;
        this.transform.position = new Vector3(0, -2.5f, 0);
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
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
        else if (Input.GetAxis("leftStick_X_xBox360") != 0 || Input.GetAxis("leftStick_Y_xBox360") != 0)
        {
            direction.Set(Input.GetAxis("leftStick_X_xBox360"), -Input.GetAxis("leftStick_Y_xBox360"));    
        }
    }
    public virtual bool MoveCondition()
    {
        return !direction.x.Equals(0);
    }
    public virtual void Move() 
    {
        gameObject.transform.position += new Vector3(moveSpeed * direction.x * Time.deltaTime, 0, 0);
        Debug.Log("Move!");
    }
    public virtual bool JumpCondition()
    {
        return (!inAir && direction.y > 0.1);
    }
    public virtual void Jump() 
    {
        inAir = true;
        rigidbody.AddForce(new Vector2(0, jumpForce * Time.deltaTime));
        Debug.Log("Jump!");
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

    public virtual void Attack() { }
    public virtual void Block() { }

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
        if (isBlocking)
        {
            //Temporary damage nerf
            damage /= 5;
        }
        health -= damage;
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
