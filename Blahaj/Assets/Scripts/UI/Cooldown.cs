using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;

    private bool isCooldown = false;
    private float cooldownTime;
    private float cooldownTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
     imageCooldown.fillAmount = 0.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0.0f) 
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }
        else 
        {
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }

    }

    public void useAttack() {
        if (!isCooldown) {
            isCooldown = true;
            cooldownTimer = cooldownTime;
        }
        
    }

    public void setCooldown(float cooldown) {
        this.cooldownTime = cooldown;
    }
}
