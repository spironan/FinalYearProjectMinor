using UnityEngine;
using System.Collections;

//Script to initialize Read from Database once
public class SingletonInitScript : MonoBehaviour 
{
	void Awake () 
    {
        //Init Database
        Database db = DatabaseSystem.GetInstance().InitDataBase("FYPJ2Database", "FYPJ2Database.db");

	    //Init Managers,Order Matters!
        SpriteManager.GetInstance().Initialze(db, "Sprites");
        CharacterManager.GetInstance().InitCharacters(db, "Characters");
        MapManager.GetInstance().InitMaps(db, "Maps");
        PrefabManager.GetInstance().Initialze(db, "Prefabs");
        AudioClipManager.GetInstance().Initialze(db, "AudioClips");
	}
}
