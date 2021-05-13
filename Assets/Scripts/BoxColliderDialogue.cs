using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderDialogue : MonoBehaviour
{
    DialogueTrigger dialogueSCR;
    DialogueManager dialogueManagerSCR;
    public float distanceToActiveDialogue;
    public GameObject playerGO;
    bool dialogue;

    private void Awake()
    {
        playerGO = FindObjectOfType<Player>().gameObject;
        dialogueManagerSCR = FindObjectOfType<DialogueManager>();
        dialogueSCR = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        CheckToActiveDialogue();
    }

    public void CheckToActiveDialogue()
    {
        if (Vector3.Distance(transform.position, playerGO.transform.position) <= distanceToActiveDialogue)
        {
            if (!dialogue)
            {
                ActiveDialogue();
            }
        }
    }

    public void ActiveDialogue()
    {
        dialogueSCR.TriggerDialogue();
        dialogue = true;
        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        Invoke("NextSentence", 3);
    }

    public void NextSentence()
    {
        dialogueManagerSCR.DisplayNextSentence();
        StartCoroutine(Next());
    }


}
