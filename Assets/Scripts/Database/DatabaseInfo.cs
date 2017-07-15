using UnityEngine;
using System.Collections;

//This Class is used to Store Information which can be used everywhere globally like a singleton but the values should never be Changed
public class DatabaseInfo
{
    public string fileStored, databaseName, databasePath, fullfilePath;
    #if UNITY_ANDROID
    public string databasePersistentPath, newDatabasePath_Android;
    #endif

    public DatabaseInfo(string databaseName, string fileStored = "StreamingAssets")
    {
        this.fileStored = fileStored;
        this.databaseName = databaseName;
        databasePath = databaseName + ".db";
#if UNITY_ANDROID
        databasePersistentPath = Application.persistentDataPath + "/" + databasePath;
        fullfilePath = "URI=file:" + databasePersistentPath;
        newDatabasePath_Android = "jar:file://" + Application.dataPath + "!/assets/" + databasePath;
#elif UNITY_STANDALONE
        fullfilePath =  "URI=file:" + Application.dataPath + "/" + fileStored + "/" + databasePath;
#endif
    }



//SAMPLE
//    public string fileStored = "StreamingAssets";
//    public string databaseName = "LSEDatabase";
//    public string databasePath = databaseName + ".db";
//#if UNITY_ANDROID
//    public string databasePersistentPath = Application.persistentDataPath + "/" + databasePath;
//    public string fullfilePath = "URI=file:" + databasePath;
//    public string newDatabasePath_Android = "jar:file://" + Application.dataPath + "!/assets/" + database;
//#elif UNITY_WINDOWS
//    public string fullfilePath = "URI=file:" + Application.dataPath + "/" + fileStored + "/" + dataBase;
//#endif
}
