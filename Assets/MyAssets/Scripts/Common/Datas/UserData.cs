using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MyGame/Create UserData", fileName = "UserData")]
public class UserData : ScriptableObject
{

    //所有しているキャラのうち、パーティーメンバーにしているキャラのインデックスを格納
    public int[] partyMemberIndexces;
    //所有しているキャラクターのリスト
    public List<OwnedCharactor> ownedCharactorList;
}
