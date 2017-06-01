using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PlayerControllerManager))]
public class CharacterBase : MonoBehaviour 
{
    //Character protected Basic Data Variable(s)
    /// CONSTANT VALUES TO BE SET ONCE ///
    //protected CHARACTERS name;
    protected string name;          //Name of Character
    protected ATTACKTYPE type;      //Attack Type of Character for basic Attacks
    protected int maxHp;            //MaxHp of this Character
    protected int health;           //Current Hp of this Character
    protected int jumpForce;        //The Jumping Force Applied to this Character when it Jumps
    protected int moveSpeed;        //The Movement Speed Applied to this Character when it moves
    protected int ultiMax;          //The Value to Determine when your Ultimate Is Maxed
    protected float blockResist;      //Resistnace when Blocking,Receive lesser damage by %
    protected float stunResistance; //The Resistance to Stun that reduces the stun you take by %
    protected float stunDuration;   //The Duration your character Gets Stunned For

    /// VALUES TO BE CHANGED OVER THE COURSE OF THE BATTLE ///                                
    protected int ultiCharge;       //The Meter that increases and when hit ultiMax,Character can Cast Ultimate
    protected float stunMeter;      //The Meter that increases when player gets hit and Gets Stunned when hit 100%
    protected float stunTimeLeft;   //The Time Left to wait before stunned is finished

    protected bool isDead;          //If The Character is Dead
    protected bool inAir;           //If The Character Is in Air
    protected bool canJump;         //If The Character Can Jump
    protected bool stunned;         //If The Character is Stunned
    protected bool isBlocking;      //If The Character is Blocking
    protected bool canUlti;         //Whether the character can Cast his Ultimate Skill

    /// CONSTANT VALUES TO BE SET ONCE ///
    protected Sprite characterArt;  //The Art of the Character Used in Character Select
    protected Sprite characterIcon; //The Icon of the Character used to display the mini icons in Character Select
    protected Sprite character;     //The Actual Art of the Character in the BattleScene
    protected Vector2 direction;    //The Direction The Character is Facing
    protected Rigidbody2D rigidbody;//The 2d Rigidbody Attached to the Charactere to apply Physics

    /// VALUES TO BE SET ON ASSIGNATION OF PLAYER TO CHARACTER ///
    //Public variables (temp for testing)
    public PLAYER playerID;         //ID Used To Determine Owner Of this Character
    public PlayerControllerManager controller;//Controller Attached To determine Controls for Character

    //Getter(s)
    //public CHARACTERS GetName() { return name; }
    public string GetName() { return name; }
    public ATTACKTYPE GetType() { return type; }
    public int GetMaxHp() { return maxHp; }
    public int GetHealth() { return health; }
    public int GetJumpForce() { return jumpForce; }
    public int GetMoveSpeed() { return moveSpeed; }
    public int GetUltiMax() { return ultiMax; }
    public float GetBlockResist() { return blockResist; }
    public float GetStunResistance() { return stunResistance; }
    public float GetStunDuration() { return stunDuration; }

    public bool IsDead() { return isDead; }
    public bool InAir() { return inAir; }
    public bool CanJump() { return canJump; }
    public bool Stunned() { return stunned; }
    public bool Blocking() { return isBlocking; }
    public bool CanUlt() { return canUlti; }

    public Sprite GetCharArt() { return characterArt; }
    public Sprite GetIcon() { return characterIcon; }
    public Sprite GetChar() { return character; }
    public Vector2 GetDirection() { return direction; }
    public PLAYER GetPlayerID() { return playerID; }
    public PlayerControllerManager GetController() { return controller; }

    
    //Setter(s)
    //public void SetName(CHARACTERS name) { this.name = name; }
    public void SetName(string name) { this.name = name; }
    public void SetType(ATTACKTYPE type) { this.type = type; }
    public void SetMaxHealth(int health) { this.maxHp = this.health = health; }
    public void SetJumpForce(int force) { this.jumpForce = force; }
    public void SetMoveSpeed(int movespeed) { this.moveSpeed = movespeed; }
    public void SetUltiMax(int ultiMax) { this.ultiMax = ultiMax; }
    public void SetBlockResistance(float blockResist) { Mathf.Clamp(blockResist, 0.0f, 1.0f); this.blockResist = blockResist; } // value range should be from 0 - 1
    public void SetStunResistance(float stunResistance) { Mathf.Clamp(stunResistance, 0.0f, 1.0f); this.stunResistance = stunResistance; } // value range should be from 0 - 1
    public void SetStunDuration(float stunDuration) { this.stunDuration = stunDuration; }

    public void SetDead(bool newIsDead) { isDead = newIsDead; }

    public void SetCharArt(Sprite charArt) { this.characterArt = charArt; }
    public void SetCharIcon(Sprite charIcon) { this.characterIcon = charIcon; }
    public void SetChar(Sprite chara) { this.character = chara; }
    public void SetPlayerID(PLAYER id) { playerID = id; }

    //Constructor(s)
    public CharacterBase()
    {
        //name = CHARACTERS.MAX_CHARACTER;
        name = "";
        type = ATTACKTYPE.MAX_ATTACKTYPE;
        maxHp = health = 0;
        moveSpeed = jumpForce = 0;
        ultiMax = 0;
        stunResistance = stunDuration = 0.0f;

        Init();
    }
    public CharacterBase(CharacterBase copy)
    {
        name = copy.name;
        type = copy.type;
        maxHp = copy.maxHp;
        health = copy.health;
        jumpForce = copy.jumpForce;
        moveSpeed = copy.moveSpeed;
        ultiMax = copy.ultiMax;
        stunResistance = copy.stunResistance;
        stunDuration = copy.stunDuration;

        Init();
    }

    //This data are always the same,thus been place here
    public void Start()
    {
        //name = CHARACTERS.PLAYTEST_CHAR;
        //name = "";
        //type = ATTACKTYPE.MID_RANGE;
        
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerControllerManager>();
        controller.init(playerID);
    }
    //This Data Are to Be Loaded from a DataBase Next Time
    public virtual void Init()
    {
        ultiCharge = 0;
        stunMeter = 0.0f;
        isDead = false;
        inAir = stunned = isBlocking = canUlti = false;
        canJump = true;
        health = maxHp;

        direction = new Vector2(0, 0);
        //this.transform.position = new Vector3(0, -2.5f, 0);
        rigidbody.velocity = new Vector2(0, 0);
    }

    //Overall Structure of how the code should flow
    public virtual void Update() 
    {
        if (stunned)
        {
            if (stunTimeLeft > 0.0f)
                stunTimeLeft -= Time.deltaTime;
            else
            {
                stunTimeLeft = 0.0f;
                stunned = false;
            }
        }
        else
        {
            ReadControl();
            if (MoveCondition())
                Move();
            if (JumpCondition())
                Jump();
        }

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
        else if (controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_X).getFloat() != 0 
            || controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_Y).getFloat() < -0.9)
        {
            if (controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_Y).getFloat() > -0.9)
                direction.Set(controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_X).getFloat(), 0);    
            else
                direction.Set(controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_X).getFloat(), 1);
        }
    }
    public virtual bool MoveCondition()
    {
        return !direction.x.Equals(0);
    }
    public virtual void Move() 
    {
        gameObject.transform.position += new Vector3(moveSpeed * direction.x * Time.deltaTime, 0, 0);
    }
    public virtual bool JumpCondition()
    {
        return (!inAir && direction.y > 0.1);
    }
    public virtual void Jump() 
    {
        inAir = true;
        rigidbody.AddForce(new Vector2(0, jumpForce * Time.deltaTime));
    }
    public virtual void Recalculate()
    {
        if (isDead)
            Die();
        direction = new Vector2(0,0);
    }

    //Check to See if Player Touch the ground,so that it can jump again
    public virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground" && inAir)
            inAir = false;
    }

    //To Be Called in the Relevant Places you Deem Fit To Increase The Ult Meter
    public void GainUltMeter(int increaseAmount)
    {
        if (!canUlti && increaseAmount > 0 && ultiCharge < ultiMax)
        {
            ultiCharge += increaseAmount;
            if (ultiCharge >= ultiMax)
            {
                ultiCharge = ultiMax;
                canUlti = true;
            }
        }
    }
    
    //To Be Called in the Relevant Places you Deem Fit To Increase Stun Meter
    public void GainStunMeter(int increaseAmount)
    {
        if (!stunned && increaseAmount > 0 && stunMeter < 100.0f)
        {
            stunMeter += (float)(increaseAmount * (1.0f - stunResistance));
            if (stunMeter >= 100.0f)
            {
                stunMeter = 0.0f;
                stunTimeLeft = stunDuration;
                stunned = true;
            }
        }
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
            ultiCharge = 0;
        }
    }

    //Go through this function to make character take damage
    public virtual void TakeDamage(int damage)
    {
        int calculated_dmg = BlockCheck(damage);
        health -= calculated_dmg;
        if (health <= 0)
            isDead = true;
    }
    public int BlockCheck(int damage)
    {
        int newDmg = damage;
        if (isBlocking)
        {
            newDmg = (int)(newDmg * blockResist);
        }
        return newDmg;
    }

    //When a Character Dies , can only be called internally
    public void Die() 
    {
        //Player Lose Ability to Control
        //Death Animation Plays
        //Win for Opponent Animation Plays
        Debug.Log("Died");
        Reset();
    }
    //Reset Player to Default Position
    public void Reset()
    {
        Init();
        Debug.Log("Reset");
    }

}
