using UnityEngine;
using Constants;
using UnityEngine.UI;

public class SkillSprite : MonoBehaviour
{
    [SerializeField] private Sprite skillSprite;
    [SerializeField] private Skill skill;
    private PlayerStats stats;
    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        if (stats.RetrieveSkillLevel(skill) > 0) {
            this.image.sprite = skillSprite;
        }
    }
}