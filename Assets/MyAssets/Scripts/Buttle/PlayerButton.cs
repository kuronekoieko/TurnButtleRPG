using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class PlayerButton : MonoBehaviour
{
    RectTransform skillChoosePanel;
    public int partyMemberIndex;
    public Text buttonText;
    public Text hpText;
    public Text mpText;




    public void Init(RectTransform parent, Vector3 pos, RectTransform skillChoosePanel)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        //layer0の子にする
        rectTransform.SetParent(parent);
        //座標を移動
        rectTransform.anchoredPosition = pos;
        //スキル選択パネルを入れる
        this.skillChoosePanel = skillChoosePanel;
    }

    /// <summary>
    /// クリック時にスキル選択パネルを開く
    /// </summary>
    public void OnClick()
    {
        //スキル選択パネルを開く
        skillChoosePanel.gameObject.SetActive(true);

        //スキルボタンにプレイヤーごとにスキルをセットする
        for (int i = 0; i < BattleStatus.i.skillButtons.Length; i++)
        {
            string skillName = "";
            int skillID = 0;

            if (i == BattleStatus.i.skillButtons.Length - 1)
            {
                skillID = 1;
            }
            else
            {
                skillID = BattleStatus.i.partyMembers[partyMemberIndex].status.buttleSkills[i];
            }
            skillName = DataManager.skillList[skillID].name;

            BattleStatus.i.skillButtons[i].SetParam(skillID, partyMemberIndex);
        }
    }


}
