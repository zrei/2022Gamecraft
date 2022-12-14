using System;
using Constants;
using TMPro;
using UnityEngine;

public class DigestControls : MonoBehaviour
{
    private static float scaleFactor = 1f;

    public TMP_Text skillMade; 
    public TMP_Text redOrbs;
    private int redOrbs1;

    public TMP_Text yellowOrbs;
    private int yellowOrbs1;

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

    public delegate void ResetDisplayDelegate();
    public static ResetDisplayDelegate ResetDisplayEvent;

    #region Methods

    private void Start()
    {
        ResetDisplayEvent += ResetDisplay;
    }
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        //skillMade = GameObject.FindWithTag("SkillDisplay").GetComponent<TMP_Text>();
        ResetDisplay();
    }

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
        Debug.Log(skillMade.text);
        ResetDisplay();
        Debug.Log(skillMade.text);
    }

    public void Digest(int red, int purple, int yellow)
    {
        //Debug.Log(red);
        PlayerStats.ChangeOrbsEvent(-red, -yellow, -purple);
        //Debug.Log(stats.getPurpleOrbs());
        int skillValue = (int)Math.Round((red + purple + yellow) * scaleFactor);

        if (red == 0 && purple == 0 && yellow == 0 ) {
            skillMade.text = $"Input the number of orbs to digest by clicking the + and - buttons";
        } 
        else if (purple == 0 && red == 0 && yellow > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Stun, skillValue));
            skillMade.text = $"You have gained the skill: \n" + "Stun";
            //change sprite on UI
            //return new Tuple<Skill, int>(Skill.Stun, skillValue);
        }
        else if (yellow == 0 && purple == 0 && red > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Fireball, skillValue));
            skillMade.text = $"You have gained the skill: \n" + "Fireball";
            //return new Tuple<Skill, int>(Skill.Fireball, skillValue);
        }
        else if (yellow == 0 && red == 0 && purple > 0)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Poison, skillValue));
            skillMade.text = $"You have gained the skill: \n" + "Poison";
            //return new Tuple<Skill, int>(Skill.Poison, skillValue);
        }
        else if (purple > yellow && yellow > red)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.Healing, skillValue));
            skillMade.text = $"You have gained the skill: \n" + "Healing";
            //return new Tuple<Skill, int>(Skill.Healing, skillValue);
        }
        else if (yellow > red && red > purple)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.HpUp, skillValue)); //float it
            skillMade.text = $"Your max HP has increased";
            //return new Tuple<Skill, int>(Skill.HpUp, skillValue);
        }
        else if (red > purple && purple > yellow)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.AttackUp, skillValue));
            skillMade.text = $"Your attack has increased";
            //return new Tuple<Skill, int>(Skill.AttackUp, skillValue);
        }
        else if (red > yellow && yellow > purple)
        {
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.AttackSpeedUp, skillValue));    
            skillMade.text = $"Your attack cooldown has decreased";
            //return new Tuple<Skill, int>(Skill.AttackSpeedUp, skillValue);
        }
        else if (yellow > purple && purple > red)
        {
            //Debug.Log("Skill value is " + skillValue);
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(Skill.MovementSpeedUp, skillValue));
            skillMade.text = $"Your movement speed has increased";
            //return new Tuple<Skill, int>(Skill.MovementSpeedUp, skillValue);
        }
        else if (yellow == red && red == purple)
        {
            Array values = Enum.GetValues(typeof(Skill));
            System.Random random = new System.Random();
            Skill randomSkill = (Skill)values.GetValue(random.Next(values.Length));
            PlayerStats.GainSkillEvent(new Tuple<Skill, int>(randomSkill, skillValue));
            skillMade.text = $"You have gained the skill: \n" + $"{randomSkill}";
            //return new Tuple<Skill, int>(randomSkill, skillValue);
        }
        else
        {
            //return null;
            PlayerStats.ChangeOrbsEvent(red, yellow, purple);
            PlayerStats.GiveIndigestionEvent();
            skillMade.text = $"You have given your shark indigestion >:(";
        }
    }

    private void ResetDisplay()
    {
        redOrbs.text = $"{stats.getRedOrbs()}";
        yellowOrbs.text = $"{stats.getYellowOrbs()}";
        purpleOrbs.text = $"{stats.getPurpleOrbs()}";
        redOrbs1 = Int32.Parse(redOrbs.text);
        yellowOrbs1 = Int32.Parse(yellowOrbs.text);
        purpleOrbs1 = Int32.Parse(purpleOrbs.text);
        redOrbsC = 0; 
        redOrbsConsumed.text = "0";
        yellowOrbsC = 0;
        yellowOrbsConsumed.text = "0";
        purpleOrbsC = 0;
        purpleOrbsConsumed.text = "0";
    }

    private void OnDestroy()
    {
        ResetDisplayEvent -= ResetDisplay;
    }
    #endregion
}