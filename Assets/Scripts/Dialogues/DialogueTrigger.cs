using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;

    public void TriggerDialogue()
    {
        if (DialogueManager.dialogueCounter < dialogue.Length && dialogue[DialogueManager.dialogueCounter].sentences.Length > 0)
            GetComponent<DialogueManager>().StartDialogue();
        else
            Debug.Log("No dialogue found");
    }
}
