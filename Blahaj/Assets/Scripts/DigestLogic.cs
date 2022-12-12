using System;
using Constants;

static class DigestLogic
{
    private static float scaleFactor = 1f;
    public static Tuple<Skill, int> Digest(int red, int purple, int yellow)
    {
        PlayerStats.ChangeOrbsEvent(-red, -yellow, -purple);
        int skillValue = (int)Math.Round((red + purple + yellow) * scaleFactor);
        if (purple == 0 && red == 0 && yellow > 0)
        {
            return new Tuple<Skill, int>(Skill.Stun, skillValue);
        }
        else if (yellow == 0 && purple == 0 && red > 0)
        {
            return new Tuple<Skill, int>(Skill.Explosion, skillValue);
        }
        else if (yellow == 0 && red == 0 && purple > 0)
        {
            return new Tuple<Skill, int>(Skill.Poison, skillValue);
        }
        else if (purple > yellow && yellow > red)
        {
            return new Tuple<Skill, int>(Skill.Healing, skillValue);
        }
        else if (yellow > red && red > purple)
        {
            return new Tuple<Skill, int>(Skill.HpUp, skillValue);
        }
        else if (red > purple && purple > yellow)
        {
            return new Tuple<Skill, int>(Skill.AttackUp, skillValue);
        }
        else if (red > yellow && yellow > purple)
        {
            return new Tuple<Skill, int>(Skill.AttackSpeedUp, skillValue);
        }
        else if (yellow > purple && purple > red)
        {
            return new Tuple<Skill, int>(Skill.MovementSpeedUp, skillValue);
        }
        else if (yellow == red && red == purple)
        {
            Array values = Enum.GetValues(typeof(Skill));
            Random random = new Random();
            Skill randomSkill = (Skill)values.GetValue(random.Next(values.Length));
            return new Tuple<Skill, int>(randomSkill, skillValue);
        }
        else
        {
            return null;
        }
    }
    
}
