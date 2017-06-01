using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    //private Dictionary<CHARACTERS,CharacterBase> characterList = new Dictionary<CHARACTERS,CharacterBase>();
    //public CharacterBase GetCharacterByID(int charID)
    //{
    //    if (HasCharacter(charID))
    //    {
    //        foreach (CHARACTERS key in characterList.Keys)
    //        {
    //            if (key == (CHARACTERS)charID)
    //            {
    //                CharacterBase avatar = new CharacterBase(characterList[key]);
    //                return avatar;
    //            }
    //        }
    //    }
    //    Debug.Log("No Such Character of ID : " + charID + " Exist, Please Create It First");
    //    return null;
    //}
    //public CharacterBase GetCharacterByName(CHARACTERS character)
    //{
    //    if (HasCharacter(character))
    //    {
    //        foreach (CHARACTERS key in characterList.Keys)
    //        {
    //            if (key == character)
    //            {
    //                CharacterBase avatar = new CharacterBase(characterList[key]);
    //                return avatar;
    //            }
    //        }
    //    }
    //    Debug.Log("No Such Character : " + character.ToString() + " Exist, Please Create It First");
    //    return null;
    //}

    //public bool HasCharacter(int charID)
    //{
    //    return characterList.ContainsKey((CHARACTERS)charID);
    //}
    //public bool HasCharacter(CHARACTERS charName)
    //{
    //    return characterList.ContainsKey(charName);
    //}
    //public bool HasCharacter(string charName)
    //{ 
    //    CHARACTERS go = (CHARACTERS)System.Enum.Parse(typeof(CHARACTERS), charName);
    //    return characterList.ContainsKey(go);
    //}

    private Dictionary<string, CharacterBase> characterList = new Dictionary<string, CharacterBase>();

    public CharacterManager() { } 

    public void Clear(){ characterList.Clear(); }

    public void InitCharacters(Database database, string tableName)
    {
        if (!RanBefore())
        {
            //Create All Unique Characters Data Here through local database server,Should Only Be Ran Once On Initalization
            database.dbConnection.Open();
            database.dbCmd = database.dbConnection.CreateCommand();
            string sqlQuery = "SELECT * FROM " + tableName;
            database.dbCmd.CommandText = sqlQuery;
            database.reader = database.dbCmd.ExecuteReader();
            while (database.reader.Read())
            {
                string name = database.reader.GetString(1);
                if (!HasCharacter(name))
                {
                    CharacterBase charBase = new CharacterBase();
                    charBase.SetName(database.reader.GetString(0));
                    //TODO Convert to enum
                    //charBase.SetType(database.reader.GetString(1));
                    charBase.SetMaxHealth(database.reader.GetInt32(2));
                    charBase.SetJumpForce(database.reader.GetInt32(3));
                    charBase.SetMoveSpeed(database.reader.GetInt32(4));
                    charBase.SetUltiMax(database.reader.GetInt32(5));
                    charBase.SetBlockResistance(database.reader.GetFloat(6));
                    charBase.SetStunResistance(database.reader.GetFloat(7));
                    charBase.SetStunDuration(database.reader.GetFloat(8));
                    charBase.SetCharArt(SpriteManager.GetInstance().GetSprite(database.reader.GetString(9)));
                    charBase.SetCharIcon(SpriteManager.GetInstance().GetSprite(database.reader.GetString(10)));
                    charBase.SetChar(SpriteManager.GetInstance().GetSprite(database.reader.GetString(11)));
                }
            }
            database.SoftReset();
        }
    }

    public int GetCharCount()
    {
        return characterList.Count;
    }

    public int GetCharacterIndex(string charName)
    {
        if (HasCharacter(charName))
        {
            int counter = 0;
            foreach (string key in characterList.Keys)
            {
                if (key == charName)
                {
                    return counter;
                }
                counter++;
            }
        }
        Debug.Log("Character of name : " + charName +"cant be found");
        return -1;
    }

    public CharacterBase GetCharacterByName(string charName)
    {
        if (HasCharacter(charName))
        {
            foreach (string key in characterList.Keys)
            {
                if (key == charName)
                {
                    CharacterBase avatar = new CharacterBase(characterList[key]);
                    return avatar;
                }
            }
        }
        Debug.Log("No Such Character : " + charName + " Exist, Please Create It First");
        return null;
    }

    public bool HasCharacter(string charName)
    {
        return characterList.ContainsKey(charName);
    }

}

    