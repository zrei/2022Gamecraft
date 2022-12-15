using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class SkillsControl : MonoBehaviour
{
    private PlayerStats stats;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        this.stats = GetComponent<PlayerStats>();
    }

    public void UseSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.Explosion:
                Explosion(stats.GetSkillInfo(Skill.Explosion));
                break;
            case Skill.Stun:
                Stun(stats.GetSkillInfo(Skill.Stun));
                break;
            case Skill.Poison:
                Poison(stats.GetSkillInfo(Skill.Poison));
                break;
            case Skill.Healing:
                Heal(stats.GetSkillInfo(Skill.Healing));
                break;
        }
    }

    private void Explosion(Tuple<float, float, float> skillInfo)
    {
        StartCoroutine(ChangeSprite(SpriteStates.Explosion, 0.3f));
        //Debug.Log(skillInfo);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillInfo.Item2);
        //Debug.Log(colliders.Length);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                //Debug.Log("?");
                hitCollider.SendMessage("Damage", skillInfo.Item1);
            }
        }
    }

    private void Stun(Tuple<float, float, float> skillInfo)
    {
        StartCoroutine(ChangeSprite(SpriteStates.Stun, 0.3f));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillInfo.Item2);
        //Debug.Log(colliders.Length);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                //Debug.Log("?");
                hitCollider.SendMessage("Damage", skillInfo.Item1);
                hitCollider.SendMessage("Stun", skillInfo.Item3);
            }
        }
    }

    private void Heal(Tuple<float, float, float> skillInfo)
    {
        PlayerStats.RestoreHealthEvent(skillInfo.Item1);
    }

    private void Poison(Tuple<float, float, float> skillInfo)
    {
        StartCoroutine(ChangeSprite(SpriteStates.Poison, 0.3f));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillInfo.Item2);
        //Debug.Log(colliders.Length);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                //Debug.Log("?");
                hitCollider.SendMessage("Poison", new Tuple<float, float, float>(skillInfo.Item1, 1f, skillInfo.Item3));
            }
        }
    }

    private IEnumerator ChangeSprite(SpriteStates sprite, float time)
    {
        stats.ChangeSprite(sprite);
        yield return new WaitForSeconds(time);
        stats.ChangeSprite(SpriteStates.Normal);
    }
    
}