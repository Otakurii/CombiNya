using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue Texts")]
    //variable to keep track of all sentences
    private Queue<string> sentences;

    //public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Animations")]
    //public Animator animatorDialogue;
    //public Animator animatorName;




    [Header("Input (assign InputActionReferences)")]
    public InputActionReference leftClickUI;             //mouse's delta cursor

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (leftClickUI?.action != null && leftClickUI.action.IsPressed())
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //textbox comes in
        //animatorDialogue.SetBool("isOpened", true);
        //animatorName.SetBool("isOpenedName", true);
        //AudioManager.Instance.PlaySFX("DialogueOpen");

        //Debug.Log("Starting convo with " + dialogue.name);
    
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            if (!string.IsNullOrEmpty(sentence))
            {
                sentences.Enqueue(sentence);
            }
        }

        // Start the first sentence immediately
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Debug.Log("Displaying next sentence triggered. ");
        if(sentences.Count == 0)     //end of queue, no more dialogue
        {
            EndDialogue();
            return;
        }
        else
        {
            //if got more sentences, continue
            string sentence = sentences.Dequeue();
            //Debug.Log("Displaying sentence of: " + sentence);
            
            //show the sentences one shot
            //dialogueText.text = sentence;

            //to let previous animation of texts stop before playing second line of sentences
            //is for those players who dont have patience n just skip dialogue lol
            StopAllCoroutines();
            //then only show the sentences, letter by letter
            StartCoroutine(TypeSentence(sentence));
        }
        
    }

    //show sentences letter by letter
    IEnumerator TypeSentence(string sentence)
    {
        //default is nothing displayed
        dialogueText.text = "";

        //each frame add one letter of the sentence into the text's string
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; 
            yield return null;
        }

    }

    public void EndDialogue()
    {
        //Debug.Log("End of sentences. Or player exits NPC radius");
        //AudioManager.Instance.PlaySFX("DialogueClose");

        //textbox goes out
        //animatorDialogue.SetBool("isOpened", false);
        //animatorName.SetBool("isOpenedName", false);
    }

}