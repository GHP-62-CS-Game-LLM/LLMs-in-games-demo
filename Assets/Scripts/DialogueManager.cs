using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string text;
    public float textSpeed;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            textComponent.text = text;
            StopAllCoroutines();
        }
    }

    public void StartDialogue(string msg)
    {
        index = 0;

        text = msg;
        StartCoroutine(TypeDialogue(msg));

    }
    public IEnumerator TypeDialogue(string msg)
    {
        foreach (char c in msg.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}