using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitGameScript : MonoBehaviour 
{    
    public static void ExitApplication()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            Debug.Log("Can Only Exit From Main Menu Safety Check");
            return;
        }

        //Save all The Neccesary Data Needed to be Saved Here
        SaveVolume();

        //LoopThrough All DataBase and Clear Them up
        DatabaseSystem.GetInstance().Clear();

        //Do Check to Quit correctly on Android
        Debug.Log("Quitting Successful");
        Application.Quit();
    }

    public static void SaveVolume()
    {
        Database database = DatabaseSystem.GetInstance().GetDataBase("FYPJ2Database");
        if (database != null)
        {
            database.UpdateLine(
                "VolumeData",
                "BGMVolume = " + SoundSystem.Instance.GetAudioManagerByType(AUDIO_TYPE.BACKGROUND_MUSIC).GetVolume() +
                ", SFXVolume = " + SoundSystem.Instance.GetAudioManagerByType(AUDIO_TYPE.SOUND_EFFECTS).GetVolume()
                );
        }
    }

    //public static void SavePlayerData(int playerID)
    //{
    //    Database database = DatabaseSystem.GetInstance().GetDataBase("SP4DataBase");
    //    if ( database != null)
    //    {
    //        database.UpdateLine(
    //            "VolumeData",
    //            "BGMVolume = " + ,
    //            "SFXVolume = "+
    //            )
    //    }
    //    Player player = PlayerManager.GetInstance().GetCurrentPlayer();
    //    if (player != null && database != null)
    //    {
    //        //Save Player Data
    //        database.UpdateLine(
    //            "PlayerData",
    //            "PlayerCurrentStage = " + player.GetStage().ToString() +
    //            ", PlayerSkillPoints = " + player.GetSkillPoint().ToString(),
    //            "PlayerID = " + playerID.ToString());

    //        //Save Player Class
    //        database.UpdateLine(
    //           "PlayerClass",
    //           "ClassID = " + player.GetClassID(),
    //           "PlayerID = " + playerID.ToString());

    //        //Save Player Buff
    //        Debug.Log("Player Buff Count : " + player.GetBuffCount());
    //        for (int i = 0; i < player.GetBuffCount(); ++i)
    //        {
    //            database.SelectTable("PlayerBuff");
    //            bool exist = false;
    //            while (database.reader != null && database.reader.Read())
    //            {
    //                if (database.reader.GetInt64(0) == Int64.Parse(player.GetUserID()) && i == database.reader.GetInt32(1))
    //                {
    //                    database.UpdateLine("PlayerBuff",
    //                    "BuffLevel = " + player.GetBuffByID(i).GetLvl(),
    //                    "PlayerID = " + playerID.ToString() +
    //                    " AND BuffID = " + database.reader.GetInt32(1));
    //                    exist = true;
    //                    break;
    //                }
    //            }
    //            //INSERT NEW BUFF IF PLAYER DONT HAVE THEM
    //            if (!exist)
    //            {
    //                Debug.Log("adding New Buff : " +
    //                player.GetUserID() +
    //                ","
    //                + i +
    //                ","
    //                + player.GetBuffByID(i).GetLvl()
    //                );
    //                database.AddToDataBase("PlayerBuff",
    //                player.GetUserID() +
    //                ","
    //                + i +
    //                ","
    //                + player.GetBuffByID(i).GetLvl());
    //            }
    //            database.SoftReset();
    //        }

    //        Debug.Log("Item Count : " + InventoryManager.GetInstance().Items.Count);
    //        //Save Player Equipment
    //        for (int i = 0; i < InventoryManager.GetInstance().Items.Count; ++i)
    //        {
    //            ItemData item = InventoryManager.GetInstance().Items[i];

    //            Debug.Log("Item Name while Saving :" + item.itemName);
    //            Debug.Log("Item Index while Saving :" + item.GetInstanceIndex());
    //            if (item.type == ItemType.NOTYPE)
    //                continue;

    //            database.SelectTable("PlayerEquipment");
    //            bool exist = false;
    //            while (database.reader != null && database.reader.Read())
    //            {
    //                Debug.Log("item Index Vs database Index : " + item.GetInstanceIndex() + database.reader.GetInt32(1));
    //                if (database.reader.GetInt64(0) == Int64.Parse(player.GetUserID()))
    //                {
    //                    if (item.GetInstanceIndex() == database.reader.GetInt32(1))
    //                    {
    //                        database.UpdateLine("PlayerEquipment",
    //                        "Equipped = " + Convert.ToInt32(item.Equipped()) +
    //                        ", AttackModifier = " + item.GetAttack() +
    //                        ", DefenseModifier = " + item.GetDefence() +
    //                        ", FuseModifier = " + item.GetFuseCounter(),
    //                        "PlayerID = " + playerID.ToString());
    //                        exist = true;
    //                        break;
    //                    }
    //                }
    //            }

    //            //INSERT NEW BUFF IF PLAYER DONT HAVE THEM
    //            if (!exist)
    //            {
    //                Debug.Log("Creating new Item");
    //                database.AddToDataBase("PlayerEquipment",
    //                player.GetUserID() +
    //                ","
    //                + item.GetInstanceIndex() +
    //                ","
    //                + Convert.ToInt32(item.Equipped()) +
    //                ","
    //                + item.GetAttack() +
    //                ","
    //                + item.GetDefence() +
    //                ","
    //                + item.GetFuseCounter()
    //                );
    //            }
    //            database.SoftReset();
    //        }

    //        Debug.Log("EQuip item Count : " + InventoryManager.GetInstance().EQ.Count);
    //        //Save Player Equipment
    //        for (int i = 0; i < InventoryManager.GetInstance().EQ.Count; ++i)
    //        {
    //            ItemData item = InventoryManager.GetInstance().EQ[i];
    //            if (item.type == ItemType.NOTYPE)
    //                continue;

    //            database.SelectTable("PlayerEquipment");
    //            bool exist = false;
    //            while (database.reader != null && database.reader.Read())
    //            {
    //                if (database.reader.GetInt64(0) == Int64.Parse(player.GetUserID()))
    //                {
    //                    if (item.GetInstanceIndex() == database.reader.GetInt32(1))
    //                    {
    //                        database.UpdateLine("PlayerEquipment",
    //                        "Equipped = " + Convert.ToInt32(item.Equipped()) +
    //                        ", AttackModifier = " + item.GetAttack() +
    //                        ", DefenseModifier = " + item.GetDefence() +
    //                        ", FuseModifier = " + item.GetFuseCounter(),
    //                        "PlayerID = " + playerID.ToString());
    //                        exist = true;
    //                        break;
    //                    }
    //                }
    //            }

    //            //INSERT NEW BUFF IF PLAYER DONT HAVE THEM
    //            if (!exist)
    //            {
    //                database.AddToDataBase("PlayerEquipment",
    //                player.GetUserID() +
    //                ","
    //                + item.GetInstanceIndex() +
    //                ","
    //                + Convert.ToInt32(item.Equipped()) +
    //                ","
    //                + item.GetAttack() +
    //                ","
    //                + item.GetDefence() +
    //                ","
    //                + item.GetFuseCounter()
    //                );
    //            }
    //            database.SoftReset();
    //        }

    //    }
    //    Debug.Log("Saved Character Successful!!");
    //}

}
