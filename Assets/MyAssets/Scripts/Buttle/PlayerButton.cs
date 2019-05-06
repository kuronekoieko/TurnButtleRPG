using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class PlayerButton : MonoBehaviour
{
    [NonSerialized] public RectTransform skillChoosePanel;
    [NonSerialized] public int playerNum;
    [NonSerialized] public SkillButton[] skillButtons;
    public Text buttonText;
    public void OnClick()
    {

        skillChoosePanel.gameObject.SetActive(true);
        for (int i = 0; i < skillButtons.Length; i++)
        {
            string skillName = "";
            int skillID = 0;

            try
            {
                skillID = CharactorStatus.i.list[playerNum].skillID[i];
                skillName = Skill.i.list[skillID].name;
            }
            catch (System.Exception)
            {
                skillName = "---";
            }


            skillButtons[i].buttonText.text = skillName;
            skillButtons[i].skillID = skillID;
            skillButtons[i].playerNum = playerNum;
        }
    }
}
