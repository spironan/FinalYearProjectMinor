using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : Singleton<SpriteManager>
{
    public SpriteManager() { }

    private Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>();
    string fullFilePath;

    public void ClearMap()
    {
        spriteMap.Clear();
    }

    public bool HasSprite(string fileName)
    {
        return spriteMap.ContainsKey(fileName);
    }

    public Sprite GetSprite(string fileName)
    {
        if (HasSprite(fileName))
        {
            foreach (string key in spriteMap.Keys)
            {
                if (key == fileName)
                {
                    return spriteMap[key];
                }
            }
        }

        Debug.Log("No Such Sprite: " + fileName + " Exist, Please Create It First");
        return null;
    }

    public Sprite GenerateSprite(string fileName, string filePath)
    {
        if (HasSprite(fileName))
        {
            Debug.Log("FileName Already Have an existing Sprite, returning the existing Sprite");
            GetSprite(fileName);
        }
        fullFilePath = "Images/" + filePath;
        Sprite Sprite = Resources.Load<Sprite>(filePath);
        if (Sprite != null)
        {
            Debug.Log("SuccessFully Loaded Sprite File :" + fileName + "at FilePath : " + filePath);
            spriteMap.Add(fileName, Sprite);
            return Sprite;
        }

        Debug.Log("No Such FilePath :" + filePath + " Have you loaded the right file?");
        return null;
    }

}
