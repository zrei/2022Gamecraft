using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
public class SkillsControl : MonoBehaviour
{

    [SerializeField] private Sprite stunSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite poisonSprite;
    private Sprite normalSprite;
    private PlayerStats stats;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float poisonRadius;
    [SerializeField] private float stunRadius;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        this.normalSprite = GetComponent<SpriteRenderer>().sprite;
        this.stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // check skill 1
            GetSkill(0);
            Debug.Log("Y has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            // check skill 2
            GetSkill(1);
            Debug.Log("U has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // check skill 3
            GetSkill(2);
            Debug.Log("I has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            // check skill 4
            GetSkill(3);
            Debug.Log("O has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            // check skill 5
            GetSkill(4);
            Debug.Log("P has been pressed");
        }
    }

    private void GetSkill(int index) 
    {
        if (index < this.stats.GetNumberOfSkills())
        {
            UseSkill(stats.RetrieveSkill(index));
        }
    }

    private void UseSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.Explosion:
                Debug.Log("Explode :)");
                Explosion();
                break;
            case Skill.Stun:
                break;
            case Skill.Poison:
                break;
        }
    }

    private void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Debug.Log(colliders.Length);
        foreach (var hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("?");
                hitCollider.SendMessage("Damage", 1);
            }
        }
    }

}
