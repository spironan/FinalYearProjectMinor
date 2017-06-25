using UnityEngine;
using System.Collections;
public enum WORDING_TYPES
{
    STUNNED,
    NOMANA,
    TOTAL,
};
public class WordingsHolder : MonoBehaviour {
    public Wordings[] wordings;

    public void showAndSetTiming(WORDING_TYPES types, float lifeTime)
    {
        wordings[(int)types].gameObject.SetActive(true);
        wordings[(int)types].lifeTime = lifeTime;
    }

    public void resetWordings()
    {
        foreach(Wordings wording in wordings)
        {
            wording.lifeTime = 0f;
        }
    }
	//// Use this for initialization
	//void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}
}
