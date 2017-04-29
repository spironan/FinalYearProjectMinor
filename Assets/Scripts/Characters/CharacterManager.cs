using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    private Dictionary<CHARACTERS,CharacterBase> characterList = new Dictionary<CHARACTERS,CharacterBase>();

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

        return null;
    }

    public CharacterBase GetCharacterByName(CHARACTERS character)
    {

        return null;
    }


}
