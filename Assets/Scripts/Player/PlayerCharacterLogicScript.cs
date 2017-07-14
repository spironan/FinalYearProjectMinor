using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PlayerControllerManager))]
public class PlayerCharacterLogicScript : MonoBehaviour
{
    private bool toUpdate = true;
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
    //public ListOfControllerActions controller;
    public GameObject enemy;
    public SpriteRenderer sprite;
    [Range(0f, 100f)]
    public int amountOfManaToStart;
    public GameObject player1Aura;
    public GameObject player2Aura;
    public GameObject ultimateAura;
    ParticleSystem mainAuraParticles;
    float minimumNumOfParticlesForAura;
    bool isSetAura = false;


    SkillActivator skillActivator;
    BasicAttack basicAttack;
    WordingsHolder wordingsHolder;

    [Range(0f,100f)]
    protected int manaAmount;
    protected int maxMana = 100;
    protected float timerToIncreaseMana = 0f;
    protected Rigidbody2D rigidbody; //The 2d Rigidbody Attached to the Charactere to apply Physics
    protected CharacterBase character; //The Character Data That Stores its Variables
    protected Vector2 player_sprite_size;
    protected Vector2 player_local_sprite_size;
    protected float player_sprite_across_length;

    protected RaycastHit2D collision_right;
    protected RaycastHit2D collision_rightUp;
    protected RaycastHit2D collision_rightDown;
    protected RaycastHit2D collision_left;
    protected RaycastHit2D collision_leftUp;
    protected RaycastHit2D collision_leftDown;
    protected RaycastHit2D collision_up;
    protected RaycastHit2D collision_down;

    private int playerLayerNum = 0;
    private int enemyLayerNum = 0;
    private int layersToCheckAgainst = 0;

    public void SetPlayerID(PLAYER id) { playerID = id; }
    public void SetController(PlayerControllerManager controller) { this.controller = controller; }
    //public void SetController(ListOfControllerActions controller) { this.controller = controller; }
    public void SetDead(bool newIsDead) { isDead = newIsDead; }
    public void SetCharacter(CharacterBase charaBase) { character = charaBase;  }
    public void SetCharacter(string charaName) { character = CharacterManager.GetInstance().GetCharacterByName(charaName); }
    public void SetStunManager(StunMeterManager stun) { stunManager = stun; }

    public bool IsDead() { return isDead; }
    public bool InAir() { return inAir; }
    public bool CanJump() { return canJump; }
    public bool Stunned() { return stunned; }
    public bool Blocking() { return isBlocking; }
    public bool CanUlt() { return canUlti; }
    public float GetHealthPercentage() { return character.GetHealthPercentage(); }
    public float GetUltiPercentage() { return (float)ultiCharge / (float)character.GetUltiMax(); }
    public void SetUltiChargeAmount(int value) { ultiCharge = value; }
    public Vector2 GetDirection() { return direction; }
    public PLAYER GetPlayerID() { return playerID; }
    public PlayerControllerManager GetController() { return controller; }
    public SkillActivator GetSkillActivator() { return skillActivator; }
    //public ListOfControllerActions GetController() { return controller; }
    
    //mana
    public float getManaAmount() { return manaAmount; }
    public void resetManaAmount() { manaAmount = amountOfManaToStart; }
    public void decreaseMana(int amount) { manaAmount = Mathf.Clamp(manaAmount - amount, 0, 100); }
    public void increaseMana(int amount) { manaAmount = Mathf.Clamp(manaAmount + amount,0,100); }
    public float percentageOfMana() { return (float)((float)manaAmount / (float)maxMana); }
    public void Init(PlayerData player)
    {
        SetCharacter(player.GetInGameData().GetCharName());
        SetPlayerID(player.GetPlayerID());
        SetController(player.gameObject.GetComponent<PlayerControllerManager>());
    }
    public int GetMaxManaCost() { return maxMana; }

    public CharacterBase GetCharacterData() { return character; }

    public void DisableAndResetAllAttacks()// if you wan the combos to be resetted
    {
        skillActivator.resetCurrentCastingSkill();
        skillActivator.enabled = false;
        basicAttack.resetTimer();
        basicAttack.enabled = false;
    }

    public void DisableAllAttacks()// disable the component from updating
    {
        skillActivator.enabled = false;
        basicAttack.enabled = false;
    }

    public void EnableAllAttacks()//enable the componets to use skills again
    {
        skillActivator.enabled = true;
        basicAttack.enabled = true;
    }

    public void ResetStunValue()
    {
        stunManager.resetStunValue();
    }

    //This data are always the same,thus been place here
    public void Start()
    {
        toUpdate = true;
        //GetComponent<SpriteRenderer>().sprite = character.GetChar();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
            rigidbody.velocity = new Vector2(0, 0);
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
        skillActivator = GetComponent<SkillActivator>();
        basicAttack = GetComponent<BasicAttack>();
        wordingsHolder = GetComponent<WordingsHolder>();

        player_sprite_size = sprite.sprite.rect.size;
        player_local_sprite_size = player_sprite_size / sprite.sprite.pixelsPerUnit;
        player_sprite_across_length = Mathf.Sqrt(player_local_sprite_size.x * player_local_sprite_size.x + player_local_sprite_size.y * player_local_sprite_size.y);

        character.ResetHealth();
        stunManager.resetStunValue();
        resetManaAmount();
        skillActivator.resetCurrentCastingSkill();
        basicAttack.resetTimer();
        wordingsHolder.resetWordings();
        DestroySkillObjects();

        if (GetPlayerID() == PLAYER.PLAYER_ONE)
        {
            playerLayerNum = 9;
            enemyLayerNum = 10;
            layersToCheckAgainst = LayerMask.GetMask("Player_Two", "Default");
            ChangePlayerLayer(gameObject, playerLayerNum);
        }
        else
        {
            playerLayerNum = 10;
            enemyLayerNum = 9;
            layersToCheckAgainst = LayerMask.GetMask("Player_One", "Default");
            ChangePlayerLayer(gameObject, playerLayerNum);
        }

        // character = gameObject.GetComponent<CharacterBase>(); should be removed because character base is no longer a component

    }

    public void ChangePlayerLayer(GameObject obj, int layer)
    {
        if(obj == null)
        {
            return;
        }

        obj.layer = layer;

        foreach(Transform child in obj.transform)
        {
            if(child == null)
            {
                continue;
            }
            ChangePlayerLayer(child.gameObject, layer);
        }
    }

    //Overall Structure of how the code should flow
    public virtual void Update()
    {
        if (toUpdate)
        {
            if (!isSetAura)
            {
                if (playerID == PLAYER.PLAYER_ONE)
                {
                    isSetAura = true;
                    mainAuraParticles = player1Aura.GetComponent<ParticleSystem>();
                    minimumNumOfParticlesForAura = 20;
                    player1Aura.SetActive(true);
                    player2Aura.SetActive(false);
                }
                else
                {
                    isSetAura = true;
                    mainAuraParticles = player2Aura.GetComponent<ParticleSystem>();
                    minimumNumOfParticlesForAura = 20;
                    player1Aura.SetActive(false);
                    player2Aura.SetActive(true);
                }
            }
            if(GetUltiPercentage() >= 1)
            {
                if (!ultimateAura.activeSelf)
                {
                    ultimateAura.SetActive(true);
                } 
            }
            else
            {
                if (ultimateAura.activeSelf)
                {
                    ultimateAura.SetActive(false);
                }
            }
            if(skillActivator.GetIsCastingUlti())
            {
                //if(mainAuraParticles.emission.rate.constantMin != minimumNumOfParticlesForAura * 5)
                {
                    mainAuraParticles.startSpeed = 2;
                    mainAuraParticles.gravityModifier = -0.5f;
                }
            }
            else
            {
                //if (mainAuraParticles.emission.rate.constantMin != minimumNumOfParticlesForAura)
                {
                    mainAuraParticles.startLifetime = 1;
                    mainAuraParticles.gravityModifier = -0.1f;
                }
            }
            if (!controller.isControllerDisabled())
            {
                ReadControl();
                if (MoveCondition())
                    Move();
                if (JumpCondition())
                    Jump();
            }

            //if (stunManager.stunTime > 0)
            //    UpdateStun();
            //else
            //{
            //}

            Recalculate();
            if (enemy.transform.position.x > transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            timerToIncreaseMana += Time.deltaTime;

            if (timerToIncreaseMana >= 1f)
            {
                timerToIncreaseMana = 0;
                increaseMana(2);
            }

            if(gameObject.layer != playerLayerNum)
            {
                playerLayerNum = gameObject.layer;
            }
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
        //resets
        character.ResetHealth();
        stunManager.resetStunValue();
        resetManaAmount();
        skillActivator.resetCurrentCastingSkill();
        basicAttack.resetTimer();
        wordingsHolder.resetWordings();
        DestroySkillObjects();
        direction = new Vector2(0, 0);
        player_sprite_size = sprite.sprite.rect.size;
        player_local_sprite_size = player_sprite_size / sprite.sprite.pixelsPerUnit;
        player_sprite_across_length = Mathf.Sqrt(player_local_sprite_size.x * player_local_sprite_size.x + player_local_sprite_size.y * player_local_sprite_size.y);

        //this.transform.position = new Vector3(0, -2.5f, 0);
        if (rigidbody != null)
            rigidbody.velocity = new Vector2(0, 0);

        if (GetPlayerID() == PLAYER.PLAYER_ONE)
        {
            playerLayerNum = 9;
            enemyLayerNum = 10;
            layersToCheckAgainst = LayerMask.GetMask("Player_Two","Default");
            ChangePlayerLayer(gameObject, playerLayerNum);
        }
        else
        {
            playerLayerNum = 10;
            enemyLayerNum = 9;
            layersToCheckAgainst = LayerMask.GetMask("Player_One","Default");
            ChangePlayerLayer(gameObject, playerLayerNum);
        }
    }

    //Function Called To Update Stun//dont need this -3-
    //public virtual void UpdateStun()
    //{
    //    if (stunTimeLeft > 0.0f)
    //        stunTimeLeft -= Time.deltaTime;
    //    else
    //    {
    //        stunTimeLeft = 0.0f;
    //        stunned = false;
    //    }
    //}

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
            || controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_Y).getFloat() > 0.9)
        {
            if (controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_Y).getFloat() < 0.9)
                direction.Set(controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_X).getFloat(), 0);
            else
                direction.Set(controller.getValueFromAxis(JOYSTICK_AXIS_INPUT.L3_X).getFloat(), 1);
        }
    }
    public virtual bool MoveCondition()
    {
        //Debug.Log(11111);
        return !direction.x.Equals(0) && !CheckIfIsHittingAWallInAir();
    }
    public virtual void Move()
    {
        //Debug.Log(555555);
        rigidbody.velocity = new Vector2(character.GetMoveSpeed() * direction.x /** Time.deltaTime*/, rigidbody.velocity.y);
        //rigidbody.AddForce(new Vector2(character.GetMoveSpeed() * direction.x * Time.deltaTime, 0));
        //gameObject.transform.position += new Vector3(character.GetMoveSpeed() * direction.x * Time.deltaTime, 0, 0);
    }
    public virtual bool JumpCondition()
    {
        CheckIfIsOnGround();
        return (!inAir && direction.y > 0.1);
    }
    public virtual void Jump()
    {
        inAir = true;
        //Debug.Log("JumpForce : " + character.GetJumpForce() * Time.deltaTime);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, character.GetJumpForce() /** Time.deltaTime*/);
        //rigidbody.AddForce(new Vector2(0, Mathf.Clamp(character.GetJumpForce() * Time.deltaTime,1,2500)));
    }
    public virtual void Recalculate()
    {
        if (isDead)
            Die();
        direction = new Vector2(0, 0);
    }

    //Check to See if Player Touch the ground,so that it can jump again
    //public virtual void OnCollisionStay2D(Collision2D other)
    //{
    //    //Vector2 sprite_size = other.collider.gameObject.GetComponent<SpriteRenderer>().sprite.rect.size;
    //    //Vector2 local_sprite_size = sprite_size / other.collider.gameObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    //    //Vector2 localScaleOfImage = (Vector2)other.collider.gameObject.transform.localScale;
    //    //Debug.Log(localScaleOfImage);
    //    //if (other.collider.tag == "Ground" 
    //    //    && inAir && other.collider.gameObject.transform.position.y < transform.position.y//
    //    //   /* && (other.collider.gameObject.transform.position.y +(local_sprite_size.y/2 * localScaleOfImage.y)  < transform.position.y - player_local_sprite_size.y / 2)*/)
    //    //    inAir = false;
    //}

    

    public virtual void CheckIfIsOnGround()
    {
        collision_down = Physics2D.Raycast(transform.position, Vector3.down, player_local_sprite_size.y, layersToCheckAgainst);
        collision_rightDown = Physics2D.Raycast(transform.position, new Vector3(0.5f, -0.5f, 0), player_sprite_across_length, layersToCheckAgainst);
        collision_leftDown = Physics2D.Raycast(transform.position, new Vector3(-0.5f, -0.5f, 0), player_sprite_across_length, layersToCheckAgainst);
        collision_right = Physics2D.Raycast(transform.position, Vector3.right, player_local_sprite_size.x, layersToCheckAgainst);
        collision_left = Physics2D.Raycast(transform.position, Vector3.left, player_local_sprite_size.x, layersToCheckAgainst);

        //RaycastHit2D collision = Physics2D.Raycast(transform.position, Vector3.down, player_local_sprite_size.y, 9);
        //Debug.Log(collision.collider.tag);
        if (collision_down.collider != null)
        {
            if(collision_down.collider.tag == "Player" && GetPlayerID() == PLAYER.PLAYER_ONE)
                Debug.Log(collision_down.collider.tag);
            
            if ((collision_down.collider.tag == "Ground" || collision_down.collider.tag == "Player") && inAir && rigidbody.velocity.y == 0)
            {
                inAir = false;
                return;
            }
            else
                inAir = true;
        }
        if(collision_leftDown.collider != null)
        {
            if ((collision_leftDown.collider.tag == "Ground" || collision_leftDown.collider.tag == "Player") && inAir && rigidbody.velocity.y == 0)
            {
                inAir = false;
                return;
            }
            else
                inAir = true;
        }
        if (collision_rightDown.collider != null)
        {
            if ((collision_rightDown.collider.tag == "Ground" || collision_rightDown.collider.tag == "Player") && inAir && rigidbody.velocity.y == 0)
            {
                inAir = false;
                return;
            }
            else
                inAir = true;
        }
        else//free falling
        {
            inAir = true;
        }
    }

    public virtual bool CheckIfIsHittingAWallInAir()
    {
        if (inAir)
        {
            collision_right = Physics2D.Raycast(transform.position, Vector3.right, player_local_sprite_size.x, layersToCheckAgainst);
            collision_left = Physics2D.Raycast(transform.position, Vector3.left, player_local_sprite_size.x, layersToCheckAgainst);
            collision_rightUp = Physics2D.Raycast(transform.position, new Vector3(0.5f,0.5f,0), player_sprite_across_length, layersToCheckAgainst);
            collision_leftUp = Physics2D.Raycast(transform.position, new Vector3(-0.5f, 0.5f, 0), player_sprite_across_length, layersToCheckAgainst);
            collision_rightDown = Physics2D.Raycast(transform.position, new Vector3(0.5f, -0.5f, 0), player_sprite_across_length, layersToCheckAgainst);
            collision_leftDown = Physics2D.Raycast(transform.position, new Vector3(-0.5f, -0.5f, 0), player_sprite_across_length, layersToCheckAgainst);
            //Debug.Log(collision.collider.tag);
            if (collision_right.collider != null)
            {
                //Debug.Log(collision.collider.tag);
                if ((collision_right.collider.tag == "Ground" || collision_right.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else if (collision_left.collider != null)
            {
                if ((collision_left.collider.tag == "Ground" || collision_left.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else if (collision_rightUp.collider != null)
            {
                if ((collision_rightUp.collider.tag == "Ground" || collision_rightUp.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else if (collision_leftUp.collider != null)
            {
                if ((collision_leftUp.collider.tag == "Ground" || collision_leftUp.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else if (collision_rightDown.collider != null)
            {
                if ((collision_rightDown.collider.tag == "Ground" || collision_rightDown.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else if (collision_leftDown.collider != null)
            {
                if ((collision_leftDown.collider.tag == "Ground" || collision_leftDown.collider.tag == "Player"))
                    return true;
                else
                    return false;
            }
            else//free falling
            {
                return false;
            }
        }
        else
            return false;
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
    //Usable Function next time if you want to transfer Block Check to here
    //public virtual int BlockCheck(int damage)
    //{
    //    int newDmg = damage;
    //    if (isBlocking)
    //    {
    //        newDmg = (int)(newDmg * character.GetBlockResist());
    //    }
    //    return newDmg;
    //}

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

    public void StopUpdate()
    {
        toUpdate = false;
    }

    public void StartUpdate()
    {
        toUpdate = true;
    }

    public void DestroySkillObjects()
    {
        SkillProfile[] temp = FindObjectsOfType<SkillProfile>();
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                Destroy(temp[i].gameObject);
            }

        }
    }
}