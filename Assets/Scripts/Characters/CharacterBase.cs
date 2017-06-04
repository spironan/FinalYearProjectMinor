using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Holds the Static Information Of The Character That Doesnt Change Throughout the Course of the Game,Loaded Once From Database and Reusable
public class CharacterBase 
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
    //protected float blockResist;    //Resistnace when Blocking,Receive lesser damage by %
    //protected float stunResistance; //The Resistance to Stun that reduces the stun you take by %
    protected float stunDuration;   //The Duration your character Gets Stunned For
    protected Sprite characterArt;  //The Art of the Character Used in Character Select
    protected Sprite characterIcon; //The Icon of the Character used to display the mini icons in Character Select
    protected Sprite character;     //The Actual Art of the Character in the BattleScene

    //Getter(s)
    //public CHARACTERS GetName() { return name; }
    public string GetName() { return name; }
    public ATTACKTYPE GetType() { return type; }
    public int GetMaxHp() { return maxHp; }
    public int GetHealth() { return health; }
    public int GetJumpForce() { return jumpForce; }
    public int GetMoveSpeed() { return moveSpeed; }
    public int GetUltiMax() { return ultiMax; }
    //public float GetBlockResist() { return blockResist; }
    //public float GetStunResistance() { return stunResistance; }
    public float GetStunDuration() { return stunDuration; }
    public Sprite GetCharArt() { return characterArt; }
    public Sprite GetIcon() { return characterIcon; }
    public Sprite GetChar() { return character; }

    //Setter(s)
    //public void SetName(CHARACTERS name) { this.name = name; }
    public void SetName(string name) { this.name = name; }
    public void SetType(ATTACKTYPE type) { this.type = type; }
    public void SetMaxHealth(int health) { this.maxHp = this.health = health; }
    public void SetJumpForce(int force) { this.jumpForce = force; }
    public void SetMoveSpeed(int movespeed) { this.moveSpeed = movespeed; }
    public void SetUltiMax(int ultiMax) { this.ultiMax = ultiMax; }
    //public void SetBlockResistance(float blockResist) { Mathf.Clamp(blockResist, 0.0f, 1.0f); this.blockResist = blockResist; } // value range should be from 0 - 1
    //public void SetStunResistance(float stunResistance) { Mathf.Clamp(stunResistance, 0.0f, 1.0f); this.stunResistance = stunResistance; } // value range should be from 0 - 1
    public void SetStunDuration(float stunDuration) { this.stunDuration = stunDuration; }
    public void SetCharArt(Sprite charArt) { this.characterArt = charArt; }
    public void SetCharIcon(Sprite charIcon) { this.characterIcon = charIcon; }
    public void SetChar(Sprite chara) { this.character = chara; }

    //Constructor(s)
    public CharacterBase()
    {
        //name = CHARACTERS.MAX_CHARACTER;
        name = "";
        type = ATTACKTYPE.MAX_ATTACKTYPE;
        maxHp = health = 0;
        moveSpeed = jumpForce = 0;
        ultiMax = 0;
        //stunResistance = stunDuration = 0.0f;
        stunDuration = 0.0f;
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
        //stunResistance = copy.stunResistance;
        stunDuration = copy.stunDuration;
        characterArt = copy.characterArt;
        characterIcon = copy.characterIcon;
        character = copy.character;
    }

    //Basic Function(s)
    public void ResetHealth()
    {
        health = maxHp;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public float GetHealthPercentage() {  return (float)(health / maxHp); }
}
