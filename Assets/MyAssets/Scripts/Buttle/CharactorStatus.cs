using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create CharactorStatus", fileName = "CharactorStatus")]
public class CharactorStatus : ScriptableObject
{

    public List<Parametor> list = new List<Parametor>();

    public static CharactorStatus i;
    void OnEnable()
    {
        i = this;
    }

}

[System.Serializable]
public class Parametor
{
    public int CharacterID;
    public string name;
    public int HP;
    public int level;
    public int speed;

    public List<int> skillID = new List<int>();
}



