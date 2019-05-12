using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenSkillImage : MonoBehaviour
{
    public Text chosenSkillText;

    public void Init(RectTransform parent, Vector3 pos)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        //layer0の子にする
        rectTransform.SetParent(parent);
        //座標を移動
        rectTransform.anchoredPosition = pos;
    }

    public void setSkillText(int skillID)
    {
        chosenSkillText.text = DataManager.skillList[skillID].name;
    }
}
