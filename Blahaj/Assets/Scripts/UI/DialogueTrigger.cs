using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
public class DialogueTrigger : MonoBehaviour
{
    private Dialogue dialogue = new Dialogue();
    private DialogueManager dialogueManager;
    private bool isOpen = false;
    private PlayerStats stats;

    private void Awake()
    {
        this.stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        string[] heal = new string[2] {
        "This is the heal skill. Press the Y key to use it.",
        "You can only use the skill once per level.", };

        string[] fireball = new string[2] {
        "This is the fireball skill. Press the I key to use it.", 
        "Ever eaten something just a little too spicy? Yeah."};

        string[] poison = new string[2] {
        "This is the poison skill. Press the U key to use it.",
        "Think of it like a fart so bad it's poisonous.", };

        string[] stun = new string[2] {
        "This is the stun skill. Press the O key to use it.",
        "You might say that this skill makes you - electrifying.", };

        string[] indigestion = new string[1] {
        "Eating the wrong things will give your shark indigestion."};


        if (gameObject.CompareTag("Heal")) {
            dialogue.setSentences(heal);
        } else if (gameObject.CompareTag("Fireball")) {
            dialogue.setSentences(fireball);
        } else if (gameObject.CompareTag("Poison")) {
            dialogue.setSentences(poison);
        } else if (gameObject.CompareTag("Stun")) {
            dialogue.setSentences(stun);
        } else {
            dialogue.setSentences(indigestion);
        }

        dialogue.setName("(=^-w-^=)");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue() {
        if (!isOpen) {
            if (gameObject.CompareTag("Heal") && stats.RetrieveSkillLevel(Skill.Healing) > 0)
            {
                dialogueManager.StartDialogue(dialogue);
                isOpen = true;
            } else if (gameObject.CompareTag("Fireball") && stats.RetrieveSkillLevel(Skill.Fireball) > 0)
            {
                dialogueManager.StartDialogue(dialogue);
                isOpen = true;
            } else if (gameObject.CompareTag("Poison") && stats.RetrieveSkillLevel(Skill.Poison) > 0)
            {
                dialogueManager.StartDialogue(dialogue);
                isOpen = true;
            } else if (gameObject.CompareTag("Stun") && stats.RetrieveSkillLevel(Skill.Stun) > 0)
            {
                dialogueManager.StartDialogue(dialogue);
                isOpen = true;
            }
        } else {
            dialogueManager.EndDialogue();
            isOpen = false;
        }
    }
}
