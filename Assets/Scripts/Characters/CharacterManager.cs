using UnityEngine;
using UnityEngine.UI;
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

    public void InitCharacters()
    {
        //Create All Unique Characters Data Here Through Database through cloud server,Should Only Be Ran Once On Initalization
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


}
