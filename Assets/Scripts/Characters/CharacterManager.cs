using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    private Dictionary<CHARACTERS,CharacterBase> characterList = new Dictionary<CHARACTERS,CharacterBase>();

    public CharacterManager() { } 

    public void Clear()
    {
        characterList.Clear();
    }

    public void InitCharacters(string database, string tableName)
    {
        //Create All Unique Characters Data Here through local database server,Should Only Be Ran Once On Initalization
        Database dataBase = DatabaseSystem.GetInstance().GetDataBase(database);
        dataBase.dbConnection.Open();
        dataBase.dbCmd = dataBase.dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tableName;
        dataBase.dbCmd.CommandText = sqlQuery;
        dataBase.reader = dataBase.dbCmd.ExecuteReader();
        while (dataBase.reader.Read())
        {
            string name = dataBase.reader.GetString(1);
            if (!HasCharacter(name))
            {
                //characterList.Add(dataBase.reader.GetInt32(0),
                //    new CharacterBase(name, dataBase.reader.GetInt32(2),
                //                 dataBase.reader.GetInt32(3), dataBase.reader.GetInt32(4), dataBase.reader.GetString(5),
                //                 dataBase.reader.GetString(6), dataBase.reader.GetString(7)));
            }
        }
        dataBase.SoftReset();

    }

    public CharacterBase GetCharacterByID(int charID)
    {
        if (HasCharacter(charID))
        {
            foreach (CHARACTERS key in characterList.Keys)
            {
                if (key == (CHARACTERS)charID)
                {
                    CharacterBase avatar = new CharacterBase(characterList[key]);
                    return avatar;
                }
            }
        }

        Debug.Log("No Such Character of ID : " + charID + " Exist, Please Create It First");
        return null;
    }

    public CharacterBase GetCharacterByName(CHARACTERS character)
    {
        if (HasCharacter(character))
        {
            foreach (CHARACTERS key in characterList.Keys)
            {
                if (key == character)
                {
                    CharacterBase avatar = new CharacterBase(characterList[key]);
                    return avatar;
                }
            }
        }

        Debug.Log("No Such Character : " + character.ToString() + " Exist, Please Create It First");
        return null;
    }

    public bool HasCharacter(int charID)
    {
        return characterList.ContainsKey((CHARACTERS)charID);
    }

    public bool HasCharacter(CHARACTERS charName)
    {
        return characterList.ContainsKey(charName);
    }

    public bool HasCharacter(string charName)
    { 
        CHARACTERS go = (CHARACTERS)System.Enum.Parse(typeof(CHARACTERS), charName);
        return characterList.ContainsKey(go);
    }
    
}

    