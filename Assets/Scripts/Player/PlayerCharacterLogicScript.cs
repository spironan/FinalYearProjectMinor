using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PlayerControllerManager))]
public class PlayerCharacterLogicScript : MonoBehaviour
{
    /// VALUES TO BE CHANGED OVER THE COURSE OF THE BATTLE ///                                
    protected int ultiCharge;       //The Meter that increases and when hit ultiMax,Character can Cast Ultimate
    protected float stunMeter;      //The Meter that increases when player gets hit and Gets Stunned when hit 100%
    protected float stunTimeLeft;   //The Time Left to wait before stunned is finished

    protected StunMeterManager stunManager;
    protected float StunResistance = 0;
    protected int GeneralResistance = 0;

    public void setStunResistance(float value) { StunResistance = value; }
    public void setGeneralResistance(int value) { GeneralResistance = value; }
    public float getStunResistance() { return StunResistance; }
    public int getGeneralResistance() { return GeneralResistance; }


    protected bool isDead;          //If The Character is Dead
    protected bool inAir;           //If The Character Is in Air
    protected bool canJump;         //If The Character Can Jump
    protected bool stunned;         //If The Character is Stunned
    protected bool isBlocking;      //If The Character is Blocking
    protected bool canUlti;         //Whether the character can Cast his Ultimate Skill

    protected Vector2 direction;    //The Direction The Character is Facing

    /// VALUES TO BE SET ON ASSIGNATION OF PLAYER TO CHARACTER ///
    //Public variables (temp for testing)
    public PLAYER playerID;         //ID Used To Determine Owner Of this Character
    public PlayerControllerManager controller;//Controller Attached To determine Controls for Character
    public GameObject enemy;
    public SpriteRenderer sprite;
    [Range(0f, 100f)]
    public float amountOfManaToStart;
    [Range(0f,100f)]
    protected float manaAmount;
    protected float maxMana = 100;
    protected Rigidbody2D rigidbody; //The 2d Rigidbody Attached to the Charactere to apply Physics
    protected CharacterBase character; //The Character Data That Stores its Variables

    public void SetPlayerID(PLAYER id) { playerID = id; }
    public void SetController(PlayerControllerManager controller) { this.controller = controller; }
    public void SetDead(bool newIsDead) { isDead = newIsDead; }
    public void SetCharacter(CharacterBase charaBase) { character = charaBase; }

    public bool IsDead() { return isDead; }
    public bool InAir() { return inAir; }
    public bool CanJump() { return canJump; }
    public bool Stunned() { return stunned; }
    public bool Blocking() { return isBlocking; }
    public bool CanUlt() { return canUlti; }
    public Vector2 GetDirection() { return direction; }
    public PLAYER GetPlayerID() { return playerID; }
    public PlayerControllerManager GetController() { return controller; }
    
    //mana
    public float getManaAmount() { return manaAmount; }
    public void resetManaAmount() { manaAmount = amountOfManaToStart; }
    public void decreaseMana(float amount) { manaAmount -= amount; }
    public float percentageOfMana() { return manaAmount / maxMana; }

    public CharacterBase GetCharacterData() { return character; }


    //This data are always the same,thus been place here
    public void Start()
    {
        //name = CHARACTERS.PLAYTEST_CHAR;
        //name = "";
        //type = ATTACKTYPE.MID_RANGE;
        //controller = GetComponent<PlayerControllerManager>();
        //GetComponent<SpriteRenderer>().sprite = character.GetChar();
        
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        controller.init(playerID);

        PlayerCharacterLogicScript[] listOfPlayers = GameObject.FindObjectsOfType<PlayerCharacterLogicScript>();
        foreach (PlayerCharacterLogicScript cb in listOfPlayers)
        {
            if (cb.gameObject != this.gameObject)
            {
                enemy = cb.gameObject;
            }
        }
        //enemy = Object.FindObjectOfType<PlayerCharacterLogicScript>().gameObject;
        sprite = GetComponent<SpriteRenderer>();
        stunManager = GetComponent<StunMeterManager>();
        resetManaAmount();
        character = gameObject.GetComponent<CharacterBase>();
        
    }

    //Overall Structure of how the code should flow
    public virtual void Update()
    {
        if (stunned)
            UpdateStun();
        else
        {
            ReadControl();
            if (MoveCondition())
                Move();
            if (JumpCondition())
                Jump();
        }

        Recalculate();
        if(enemy.transform.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    //This Data Are to Be Loaded from a DataBase Next Time
    public virtual void Init()
    {
        ultiCharge = 0;
        stunMeter = 0.0f;
        isDead = false;
        inAir = stunned = isBlocking = canUlti = false;
        canJump = true;
        character.ResetHealth();

        direction = new Vector2(0, 0);
        //this.transform.position = new Vector3(0, -2.5f, 0);
        if (rigidbody != null)
            rigidbody.velocity = new Vector2(0, 0);
    }

    //Function Called To Update Stun
    public virtual void UpdateStun()
    {
        if (stunTimeLeft > 0.0f)
            stunTimeLeft -= Time.deltaTime;
        else
        {
            stunTimeLeft = 0.0f;
            stunned = false;
        }
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
        gameObject.transform.position += new Vector3(character.GetMoveSpeed() * direction.x * Time.deltaTime, 0, 0);
    }
    public virtual bool JumpCondition()
    {
        return (!inAir && direction.y > 0.1);
    }
    public virtual void Jump()
    {
        inAir = true;
        rigidbody.AddForce(new Vector2(0, character.GetJumpForce() * Time.deltaTime));
    }
    public virtual void Recalculate()
    {
        if (isDead)
            Die();
        direction = new Vector2(0, 0);
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
        if (!canUlti && increaseAmount > 0 && ultiCharge < character.GetUltiMax())
        {
            ultiCharge += increaseAmount;
            if (ultiCharge >= character.GetUltiMax())
            {
                ultiCharge = character.GetUltiMax();
                canUlti = true;
            }
        }
    }
    //To Be Called in the Relevant Places you Deem Fit To Increase Stun Meter
    public void GainStunMeter(float increaseAmount)
    {
        //float temp = increaseAmount - StunResistance;
        
        stunManager.addStunValue(Mathf.Clamp(increaseAmount - StunResistance,0,100));
        //if (!stunned && increaseAmount > 0 && stunMeter < 100.0f)
        //{
        //    stunMeter += increaseAmount * (1.0f - character.GetStunResistance());
        //    if (stunMeter >= 100.0f)
        //    {
        //        stunMeter = 0.0f;
        //        stunTimeLeft = character.GetStunDuration();
        //        stunned = true;
        //    }
        //}
    }

    public virtual void Attack()
    {
        switch (character.GetType())
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
        //int calculated_dmg = BlockCheck(damage);
        int calculated_dmg = Mathf.Clamp(damage - GeneralResistance, 0, 100);
        character.TakeDamage(calculated_dmg);
        if (character.GetHealth() <= 0)
            isDead = true;
    }
    public virtual int BlockCheck(int damage)
    {
        int newDmg = damage;
        if (isBlocking)
        {
            newDmg = (int)(newDmg * character.GetBlockResist());
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