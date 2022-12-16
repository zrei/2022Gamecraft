using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider; 
    [SerializeField] Gradient gradient; 
    [SerializeField] Image fill; 

    private PlayerStats stats;

    public delegate void SetDelegate(float amount);
    public static SetDelegate SetMaxHealthEvent;
    public static SetDelegate SetHealthEvent;

    private void Start()
    {
        SetMaxHealthEvent += SetMaxHealth;
        SetHealthEvent += SetHealth;
    }

    private void Awake()
    {
        stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        SetMaxHealth(stats.getMaxHealth());
        SetHealth(stats.getHealth());
    }
    private void SetMaxHealth(float health) {
        //Debug.Log("Max health is " + health);
        slider.maxValue = health; 
        slider.value = health; 
        fill.color = gradient.Evaluate(1f);
    }

    private void SetHealth(float health) {
        slider.value = health;
        fill.color =  gradient.Evaluate(slider.normalizedValue);
    }

    private void OnDestroy()
    {
        SetMaxHealthEvent -= SetMaxHealth;
        SetHealthEvent -= SetHealth;
    }
}
