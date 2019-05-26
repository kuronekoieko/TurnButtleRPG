using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerButton playerButtonPrefab;
    [SerializeField] ChosenSkillImage chosenSkillImagePrefab;
    [SerializeField] RectTransform layer0;
    [SerializeField] RectTransform skillChoosePanel;
    [SerializeField] SkillButton skillButtonPrefab;
    [SerializeField] RectTransform BattleUI;



    /// <summary>
    /// start()で実行
    /// </summary>
    public void Init()
    {
        BattleUI.gameObject.SetActive(false);
        skillChoosePanel.gameObject.SetActive(false);
        SkillButtonGenerator();
        PlayerBattleUIGenerator();
        int partyMemberNum = DataManager.userData.partyMemberIndexces.Length;
        BattleStatus.i.partyMembers = new OwnedCharactor[partyMemberNum];
    }

    /// <summary>
    /// バトル開始時
    /// </summary>
    public void BattleInitialize()
    {
        BattleUI.gameObject.SetActive(true);

        SetBattlePartyMember();

        SetDefaultStatus();
    }

    /// <summary>
    /// バトル終了時
    /// </summary>
    public void BattleFinalize()
    {
        skillChoosePanel.gameObject.SetActive(false);
        BattleUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// バトル中
    /// </summary>
    public void BattleUpdate()
    {
        //ボタン以外をタッチしたらスキルパネルを閉じる
        if (!IsButtonTap())
        {
            skillChoosePanel.gameObject.SetActive(false);
        }
    }

    void SetBattlePartyMember()
    {
        for (int i = 0; i < BattleStatus.i.partyMembers.Length; i++)
        {
            //パーティメンバーの所有インデックスを取得
            int ownedCharactorIndex = DataManager.userData.partyMemberIndexces[i];
            //バトルに参加するパーティメンバーの配列にキャラクター情報を格納する
            BattleStatus.i.partyMembers[i] = DataManager.userData.ownedCharactorList[ownedCharactorIndex];
            //ボタンにパーティメンバーのインデックスを割り当てる
            BattleStatus.i.playerUIs[i].playerButton.partyMemberIndex = i;
        }
    }

    void SetDefaultStatus()
    {
        //デフォルトの攻撃状態をセット
        for (int i = 0; i < BattleStatus.i.playerUIs.Length; i++)
        {
            //キャラクターの0番目に設定されているスキルIDを取得
            OwnedCharactor partyMember = BattleStatus.i.partyMembers[i];
            int skillID = partyMember.status.buttleSkills[0];
            string charaName = partyMember.charactorSpec.name;
            BattleInfo info = new BattleInfo(skillID);
            BattleStatus.i.players.Add(info);
            PlayerUI playerUI = BattleStatus.i.playerUIs[i];
            //スキルテキストを変更
            playerUI.chosenSkillImage.setSkillText(skillID);
            //キャラクター名を表示
            playerUI.playerButton.buttonText.text = charaName;
            playerUI.playerButton.hpText.text = "HP:" + partyMember.status.hp;
            playerUI.playerButton.mpText.text = "MP:" + partyMember.status.mp;
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

        Vector3 pos = new Vector3(Constants.FIRST_SKILL_BUTTON_X, 0, 0);
        BattleStatus.i.skillButtons = new SkillButton[Constants.SKILL_BUTTON_NUM];
        for (int i = 0; i < BattleStatus.i.skillButtons.Length; i++)
        {
            BattleStatus.i.skillButtons[i] = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity);
            BattleStatus.i.skillButtons[i].Init(pos, skillChoosePanel);
            pos += new Vector3(Constants.SKILL_BUTTON_OFFSET_X, 0, 0);
        }
    }

    void PlayerBattleUIGenerator()
    {
        float x = Constants.FIRST_PLAYER_UI_X;

        BattleStatus.i.playerUIs = new PlayerUI[Constants.PLAYER_UI_NUM];
        for (int i = 0; i < BattleStatus.i.playerUIs.Length; i++)
        {
            PlayerButton btn = Instantiate(playerButtonPrefab, Vector3.zero, Quaternion.identity);
            ChosenSkillImage image = Instantiate(chosenSkillImagePrefab, Vector3.zero, Quaternion.identity);
            BattleStatus.i.playerUIs[i] = new PlayerUI(btn, image);
            PlayerUI playerUI = BattleStatus.i.playerUIs[i];
            playerUI.playerButton.Init(layer0, new Vector3(x, Constants.PLAYER_UI_BUTTON_Y, 0), skillChoosePanel);
            playerUI.chosenSkillImage.Init(layer0, new Vector3(x, Constants.PLAYER_UI_IMAGE_Y, 0));

            //座標をずらす
            x += Constants.PLAYER_UI_OFFSET_X;
        }
    }
}
