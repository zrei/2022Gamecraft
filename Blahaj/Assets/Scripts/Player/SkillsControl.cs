using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class SkillsControl : MonoBehaviour
{
    private PlayerStats stats;

    private int timesHealedInLevel = 0;
    [SerializeField] private GameObject fireball;
    private Transform mouth = null;
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform);
        foreach (Transform child in transform) 
        {
            //Debug.Log("Hello!");
            if (mouth == null)
            {
                mouth = child;
                //Debug.Log(mouth);
            }
        }
    }

    private void Awake()
    {
        this.stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
    }

    public void UseSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.Fireball:
                Fireball(stats.GetSkillInfo(Skill.Fireball));
                break;
            case Skill.Stun:
                Stun(stats.GetSkillInfo(Skill.Stun));
                break;
            case Skill.Poison:
                Poison(stats.GetSkillInfo(Skill.Poison));
                break;
            case Skill.Healing:
                if (timesHealedInLevel < 1)
                {
                    Heal(stats.GetSkillInfo(Skill.Healing));
                    timesHealedInLevel += 1;
                }
                break;
        }
    }

    private void Fireball(Tuple<float, float, float> skillInfo)
    {
        //PlayerControls controls = gameObject.GetComponent<PlayerControls>();
        //Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, controls.getDirection());
        Quaternion bulletRotation = transform.rotation;
        //Debug.Log(mouth);
        StartCoroutine(ChangeSprite(SpriteStates.Fireball, 0.3f));
        GameObject bullet = Instantiate(fireball, mouth.position, bulletRotation);
        if (transform.localScale.x < 0)
        {
            bullet.GetComponent<FireballController>().SetMovingLeft();
        }
        //Debug.Log(skillInfo);
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillInfo.Item2);
        //Debug.Log(colliders.Length);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                //Debug.Log("?");
                hitCollider.SendMessage("Damage", skillInfo.Item1);
            }
        }*/
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
