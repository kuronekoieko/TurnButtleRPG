using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtleManager : MonoBehaviour
{
    [SerializeField] PlayerButtleUI playerButtleUIPrefab;
    [SerializeField] GameObject layer0;
    [SerializeField] RectTransform skillChoosePanel;
    [SerializeField] SkillButton skillButtonPrefab;
    PlayerButtleUI[] playerButtleUIs;
    SkillButton[] skillButtons;




    public void Init()
    {
        skillChoosePanel.gameObject.SetActive(false);


        SkillButtonGenerator();
        PlayerButtleUIGenerator();


        // Debug.Log(CharactorStatus.i == null);


        for (int i = 0; i < 4; i++)
        {
            ButtleInfo info = new ButtleInfo(CharactorStatus.i.list[i].skillID[0]);
            ButtleStatus.i.players.Add(info);
        }


    }

    public void ButtleUpdate()
    {

        if (!IsButtonTap())
        {
            skillChoosePanel.gameObject.SetActive(false);
        }
        for (int i = 0; i < playerButtleUIs.Length; i++)
        {
            int skillID = ButtleStatus.i.players[i].skillID;
            playerButtleUIs[i].chosenSkillText.text = Skill.i.list[skillID].name;
        }

        string a = "";
        for (int i = 0; i < ButtleStatus.i.players.Count; i++)
        {

            a += ButtleStatus.i.players[i].skillID + ":";
        }
        Debug.Log(a);
    }

    bool IsButtonTap()
    {
        //タップ検知したオブジェクトを取得
        GameObject tapObj = EventSystem.current.currentSelectedGameObject;
        //nullチェック
        if (!tapObj) { return false; }
        //ボタンをもっているかどうかを判定
        Button btn = tapObj.GetComponent<Button>();
        if (btn) { return true; }
        //ボタンをもっていないならfalseを返す
        return false;
    }

    void SkillButtonGenerator()
    {
        Vector3 pos = new Vector3(-500, 0, 0);
        skillButtons = new SkillButton[5];
        for (int i = 0; i < 5; i++)
        {
            skillButtons[i] = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity);
            skillButtons[i].transform.parent = skillChoosePanel.transform;
            skillButtons[i].GetComponent<RectTransform>().anchoredPosition = pos;
            skillButtons[i].buttonText.text = "スキル" + i;
            skillButtons[i].skillChoosePanel = skillChoosePanel;
            pos += new Vector3(250, 0, 0);
        }

    }

    void PlayerButtleUIGenerator()
    {
        Vector3 pos = Vector3.zero;
        playerButtleUIs = new PlayerButtleUI[4];
        for (int i = 0; i < playerButtleUIs.Length; i++)
        {
            PlayerButtleUI player = playerButtleUIs[i];
            playerButtleUIs[i] = Instantiate(playerButtleUIPrefab, Vector3.zero, Quaternion.identity);
            playerButtleUIs[i].transform.parent = layer0.transform;
            playerButtleUIs[i].GetComponent<RectTransform>().anchoredPosition = pos;
            playerButtleUIs[i].playerButton.skillChoosePanel = skillChoosePanel;
            playerButtleUIs[i].playerButton.buttonText.text = "Player" + i;
            playerButtleUIs[i].playerButton.playerNum = i;
            playerButtleUIs[i].playerButton.skillButtons = skillButtons;
            pos += new Vector3(160, 0, 0);
        }

    }


}
