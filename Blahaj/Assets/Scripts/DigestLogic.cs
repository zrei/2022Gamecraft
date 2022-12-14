using System;
//using System.Collections;
//using System.Collections.Generic;
using Constants;
//using TMPro;
using UnityEngine;

public class DigestLogic : MonoBehaviour
{
    private static float scaleFactor = 1f;

    /*
    public TMP_Text redOrbs;
    public TMP_Text yellowOrbs;
    public TMP_Text purpleOrbs; 
    private GameObject player = gameObject.FindGameObjectsWithTag("Player");
    private PlayerStats stats = player.GetComponent<PlayerStats>();

    private void Start() {
        redOrbs.text = $"{stats.getRedOrbs()}";
        yellowOrbs.text = $"{stats.getYellowOrbs()}";
        purpleOrbs.text = $"{stats.getPurpleOrbs()}";
    }
    */

    public void Digest(int red, int purple, int yellow)
    {
        PlayerStats.ChangeOrbsEvent(-red, -yellow, -purple);
        int skillValue = (int)Math.Round((red + purple + yellow) * scaleFactor);
        if (purple == 0 && red == 0 && yellow > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Stun, skillValue));
            //change sprite on UI
            //return new Tuple<Skill, int>(Skill.Stun, skillValue);
        }
        else if (yellow == 0 && purple == 0 && red > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Explosion, skillValue));
            //return new Tuple<Skill, int>(Skill.Explosion, skillValue);
        }
        else if (yellow == 0 && red == 0 && purple > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Poison, skillValue));
            //return new Tuple<Skill, int>(Skill.Poison, skillValue);
        }
        else if (purple > yellow && yellow > red)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Healing, skillValue));
            //return new Tuple<Skill, int>(Skill.Healing, skillValue);
        }
        else if (yellow > red && red > purple)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.HpUp, skillValue)); //float it
            //return new Tuple<Skill, int>(Skill.HpUp, skillValue);
        }
        else if (red > purple && purple > yellow)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.AttackUp, skillValue));
            //return new Tuple<Skill, int>(Skill.AttackUp, skillValue);
        }
        else if (red > yellow && yellow > purple)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.AttackSpeedUp, skillValue));      
            //return new Tuple<Skill, int>(Skill.AttackSpeedUp, skillValue);
        }
        else if (yellow > purple && purple > red)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.MovementSpeedUp, skillValue));
            //return new Tuple<Skill, int>(Skill.MovementSpeedUp, skillValue);
        }
        else if (yellow == red && red == purple)
        {
            Array values = Enum.GetValues(typeof(Skill));
            System.Random random = new System.Random();
            Skill randomSkill = (Skill)values.GetValue(random.Next(values.Length));
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(randomSkill, skillValue));
            //return new Tuple<Skill, int>(randomSkill, skillValue);
        }
        else
        {
            //return null;
        }
    }
    
}
