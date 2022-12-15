using System;
using Constants;
using TMPro;
using UnityEngine;

public class DigestControls : MonoBehaviour
{
    private static float scaleFactor = 1f;

    
    public TMP_Text redOrbs;
    private int redOrbs1;

    public TMP_Text yellowOrbs;
    public int yellowOrbs1;

    public TMP_Text purpleOrbs; 
    private int purpleOrbs1;

    public TMP_Text redOrbsConsumed;
    private int redOrbsC = 0;

    public TMP_Text yellowOrbsConsumed;
    private int yellowOrbsC = 0;

    public TMP_Text purpleOrbsConsumed;
    private int purpleOrbsC = 0;

    private GameObject player;
    private PlayerStats stats;

    #region Methods
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        redOrbs.text = $"{stats.getRedOrbs()}";
        yellowOrbs.text = $"{stats.getYellowOrbs()}";
        purpleOrbs.text = $"{stats.getPurpleOrbs()}";
        redOrbs1 = Int32.Parse(redOrbs.text);
        yellowOrbs1 = Int32.Parse(yellowOrbs.text);
        purpleOrbs1 = Int32.Parse(purpleOrbs.text);
    }

    /*
    private void Start() {
        redOrbs.text = $"{stats.getRedOrbs()}";
        yellowOrbs.text = $"{stats.getYellowOrbs()}";
        purpleOrbs.text = $"{stats.getPurpleOrbs()}";
    }*/

    public void Plus(string color) {
        if (color.Equals("red") && redOrbsC < redOrbs1) {
            redOrbsC++;
            redOrbsConsumed.text = (redOrbsC).ToString(); 

        } else if (color.Equals("yellow") && yellowOrbsC < yellowOrbs1) {
            yellowOrbsC++;
            yellowOrbsConsumed.text = (yellowOrbsC).ToString(); 

        } else if (color.Equals("purple") && purpleOrbsC < purpleOrbs1) {
            purpleOrbsC++;
            purpleOrbsConsumed.text = (purpleOrbsC).ToString(); 

        } else { 
            Debug.Log("Not enough " + color + " orbs");
        } 
    }

    public void Minus(string color) {
        if (color.Equals("red") && redOrbsC > 0) {
            redOrbsC--;
            redOrbsConsumed.text = (redOrbsC).ToString(); 
        } else if (color.Equals("yellow") && yellowOrbsC > 0) {
            yellowOrbsC--;
            yellowOrbsConsumed.text = (yellowOrbsC).ToString();
        } else if (color.Equals("purple") && purpleOrbsC > 0) {
            purpleOrbsC--;
            purpleOrbsConsumed.text = (purpleOrbsC).ToString(); 
        } else { 
            Debug.Log("Cannot have negative num of " + color + " orbs");
        } 
    }

    public void DigestButton()
    {
        Digest(Int32.Parse(redOrbsConsumed.text), Int32.Parse(purpleOrbsConsumed.text), Int32.Parse(yellowOrbsConsumed.text));
    }

    public void Digest(int red, int purple, int yellow)
    {
        Debug.Log(red);
        PlayerStats.ChangeOrbsEvent(-red, -yellow, -purple);
        Debug.Log(stats.getPurpleOrbs());
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
    #endregion
}