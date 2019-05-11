using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtleManager : MonoBehaviour
{
    [SerializeField] PlayerButton playerButtonPrefab;
    [SerializeField] RectTransform layer0;
    [SerializeField] RectTransform skillChoosePanel;
    [SerializeField] SkillButton skillButtonPrefab;
    [SerializeField] RectTransform buttleUI;



    /// <summary>
    /// start()で実行
    /// </summary>
    public void Init()
    {
        buttleUI.gameObject.SetActive(false);
        skillChoosePanel.gameObject.SetActive(false);
        SkillButtonGenerator();
        PlayerButtleUIGenerator();
        ButtleStatus.i.partyMember = new OwnedCharactor[4];
    }

    /// <summary>
    /// バトル開始時
    /// </summary>
    public void ButtleInitialize()
    {
        buttleUI.gameObject.SetActive(true);

        SetButtlePartyMember();

        SetDefaultSkill();
    }

    void SetButtlePartyMember()
    {
        for (int i = 0; i < ButtleStatus.i.partyMember.Length; i++)
        {
            int index = DataManager.userData.partyMembers[i];
            ButtleStatus.i.partyMember[i] = DataManager.userData.ownedCharactorList[index];
        }
    }

    /// <summary>
    /// バトル終了時
    /// </summary>
    public void ButtleFinalize()
    {
        skillChoosePanel.gameObject.SetActive(false);
        buttleUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// バトル中
    /// </summary>
    public void ButtleUpdate()
    {
        //ボタン以外をタッチしたらスキルパネルを閉じる
        if (!IsButtonTap())
        {
            skillChoosePanel.gameObject.SetActive(false);
        }
    }

    void SetDefaultSkill()
    {
        //デフォルトの攻撃状態をセット
        for (int i = 0; i < ButtleStatus.i.playerButtons.Length; i++)
        {
            //キャラクターの0番目に設定されているスキルIDを取得
            int skillID = ButtleStatus.i.partyMember[i].status.buttleSkills[0];
            string charaName = ButtleStatus.i.partyMember[i].charactorSpec.name;
            ButtleInfo info = new ButtleInfo(skillID);
            ButtleStatus.i.players.Add(info);
            //スキルテキストを変更
            ButtleStatus.i.playerButtons[i].setSkillText(skillID);
            //キャラクター名を表示
            ButtleStatus.i.playerButtons[i].buttonText.text = charaName;
        }
    }



    bool IsButtonTap()
    {
        //タップ検知したGameObjectを取得。
        GameObject tapObj = EventSystem.current.currentSelectedGameObject;
        //取得したGameObjectがnullならばfalseを返す。
        if (!tapObj) { return false; }
        //取得したオブジェクトからButtonを取得。
        Button btn = tapObj.GetComponent<Button>();
        //ボタンが存在するかどうかを返す。
        return btn;
    }

    void SkillButtonGenerator()
    {
        Vector3 pos = new Vector3(-500, 0, 0);
        ButtleStatus.i.skillButtons = new SkillButton[5];
        for (int i = 0; i < ButtleStatus.i.skillButtons.Length; i++)
        {
            ButtleStatus.i.skillButtons[i] = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity);
            ButtleStatus.i.skillButtons[i].Init(pos, skillChoosePanel);
            pos += new Vector3(250, 0, 0);
        }
    }

    void PlayerButtleUIGenerator()
    {
        Vector3 pos = new Vector3(-560, -300, 0);
        ButtleStatus.i.playerButtons = new PlayerButton[4];
        for (int i = 0; i < ButtleStatus.i.playerButtons.Length; i++)
        {
            ButtleStatus.i.playerButtons[i] = Instantiate(playerButtonPrefab, Vector3.zero, Quaternion.identity);
            ButtleStatus.i.playerButtons[i].Init(layer0, pos, skillChoosePanel, i);
            //座標をずらす
            pos += new Vector3(180, 0, 0);
        }
    }


}
