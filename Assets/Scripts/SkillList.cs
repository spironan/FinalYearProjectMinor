using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillList : MonoBehaviour {

    public List<GameObject> ListOfSkills = new List<GameObject>();

    public virtual GameObject GetSkill(int number)
    {
        if(ListOfSkills[number] != null)
        {
            return ListOfSkills[number];
        }
        else
        {
            Debug.Log("no such skill");
            return null;
        }
        
    }

    public virtual void AddSkill(GameObject skill)
    {
        ListOfSkills.Add(skill);
    }

    public virtual void swapSkillOrder(int firstSkillNumber,int secondSkillNumber)
    {
        GameObject temp = ListOfSkills[firstSkillNumber];
        ListOfSkills[firstSkillNumber] = ListOfSkills[secondSkillNumber];
        ListOfSkills[secondSkillNumber] = temp;
    }
}
