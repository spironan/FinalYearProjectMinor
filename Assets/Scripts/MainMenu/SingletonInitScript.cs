﻿using UnityEngine;
using System.Collections;

//Script to initialize Read from Database once
public class SingletonInitScript : MonoBehaviour 
{
	void Awake () 
    {
        //Init Database
        Database db = DatabaseSystem.GetInstance().InitDataBase("FYPJ2Database", "FYPJ2Database.db");

	    //Init Universal Managers here
        SpriteManager.GetInstance().Initialze(db, "Sprites");
        PrefabManager.GetInstance().Initialze(db, "Prefabs");
        AudioClipManager.GetInstance().Initialze(db, "AudioClips");

        //Init Game Specific DB here,Order Matters!
        CharacterManager.GetInstance().InitCharacters(db, "Characters");
        MapManager.GetInstance().InitMaps(db, "Maps");
	}
}
