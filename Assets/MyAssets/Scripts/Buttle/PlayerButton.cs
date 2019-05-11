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
                skillID = DataManager.charactorStatusList[playerNum].skillID[i];
                skillName = DataManager.skillList[skillID].name;
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
