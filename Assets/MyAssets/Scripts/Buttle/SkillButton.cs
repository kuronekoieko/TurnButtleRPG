using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    RectTransform skillChoosePanel;
    int skillID;
    int playerButtonNum;

    public void Init(Vector3 pos, RectTransform skillChoosePanel)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        //子オブジェクトにする
        rectTransform.SetParent(skillChoosePanel);
        //位置を設定
        rectTransform.anchoredPosition = pos;
        //スキルパネルを設定
        this.skillChoosePanel = skillChoosePanel;

    }

    public void SetParam(int skillID, int playerButtonNum)
    {
        this.skillID = skillID;
        this.playerButtonNum = playerButtonNum;
        buttonText.text = DataManager.skillList[skillID].name;
    }


    public void OnClick()
    {
        //スキルなしとチェンジの場合は何もしない
        if (skillID == 0 || skillID == 1) return;
        //スキルパネルを閉じる
        skillChoosePanel.gameObject.SetActive(false);
        //攻撃するスキルIDをセット
        ButtleStatus.i.players[playerButtonNum].skillID = skillID;
        //スキルテキストを変更
        ButtleStatus.i.playerButtons[playerButtonNum].setSkillText(skillID);
    }
}
