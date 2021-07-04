using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI text elements")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("References to other scripts")]
    [SerializeField] References refs;
    [SerializeField] SceneLoader sceneLoader;
    private DialogueTrigger dialogueTrigger;

    [Header("Dialogue")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource typeSound;
    [SerializeField] bool isPlayingTimeMachineEffect;
    [SerializeField] bool isEndGameDialogue;
    private Dialogue[] dialogue;
    private Queue<string> sentences;
    private byte typing;
    public static int dialogueCounter;

    private void Awake()
    {
        dialogueCounter = 0;
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void Start()
    {
        dialogue = dialogueTrigger.dialogue;

        typeSound = GetComponent<AudioSource>();
        sentences = new Queue<string>();

        sceneLoader = refs.GetSceneLoader();
    }

    public void StartDialogue()
    {
        if (dialogueCounter < dialogue.Length)
        {
            PlayerMovement.playerHasControl = false;
            GameMusic.TurnDownVolume();
            animator.SetBool("IsOpen", true);
            
            sentences.Clear();

            foreach (string sentence in dialogue[dialogueCounter].sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
        else
        {
            return;
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        nameText.text = dialogueTrigger.dialogue[dialogueCounter].characterName;

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if (typing >= 2) // This plays typing sound every second char typed
            {
                typeSound.Play();
                typing = 0;
            }
            dialogueText.text += letter;
            for(int i = 0; i < 2; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            typing++;
        }
    }

    private void EndDialogue()
    {
        dialogueCounter++;

        if (dialogueCounter >= dialogueTrigger.dialogue.Length)
        {
            animator.SetBool("IsOpen", false);
            GameMusic.TurnVolumeBack();

            StopAllCoroutines();
  
            if (isPlayingTimeMachineEffect)
            {
                dialogueCounter = 0;
                StartCoroutine(sceneLoader.LoadNextSceneWithTimeMachineAnimation());
            }
            else if(isEndGameDialogue)
            {
                StartCoroutine(sceneLoader.EndGameDramatically());
            }
            else if (gameObject.CompareTag("Endlevel dialogue"))
            {
                StartCoroutine(sceneLoader.InitiateWinning());
            }
            else
            {
                dialogueCounter = 0;
                Invoke("GivePlayerControl", 0.3f);
            }
        }
        else
        {
            StartDialogue();
        }
    }

    private void GivePlayerControl()
    {
        PlayerMovement.playerHasControl = true;
    }
}
