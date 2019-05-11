﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillButton : MonoBehaviour
{
    public Text buttonText;
    [NonSerialized] public RectTransform skillChoosePanel;
    [NonSerialized] public int skillID;
    [NonSerialized] public int playerNum;

    public void OnClick()
    {
        skillChoosePanel.gameObject.SetActive(false);
        //スキルなしの場合はバトルステータスを変更しない
        if (skillID == 0) return;
        ButtleStatus.i.players[playerNum].skillID = skillID;
    }
}
