using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI
{
    public PlayerButton playerButton;
    public ChosenSkillImage chosenSkillImage;

    public PlayerUI(PlayerButton playerButton, ChosenSkillImage chosenSkillImage)
    {
        this.playerButton = playerButton;
        this.chosenSkillImage = chosenSkillImage;
    }
}
