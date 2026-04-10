using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public Animator dialogueAnimator;

    private DialogueSystem dialogueSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueAnimator.SetBool("isDialogueOpen", false);
    }

    //these are calling from other scripts
    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueSystem>().StartDialogue(dialogue);
    }

     public void EndDialogue()
    {
        FindFirstObjectByType<DialogueSystem>().EndDialogue();
    }


    //these are if player enters or exits NPC's radius
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player enter NPC radius. Can start convo. ");
            dialogueAnimator.SetBool("isDialogueOpen", true);

            TriggerDialogue();
        }
    }

     public void OnTriggerExit(Collider other)
     {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player exit NPC radius. ");
            dialogueAnimator.SetBool("isDialogueOpen", false);

            EndDialogue();
        }
    }
}
