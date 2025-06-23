using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Rendering.HighDefinition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OllamaSharp;
using OllamaSharp.Models.Chat;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string text;
    public float textSpeed;



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

    public void Reset()
    {
        textComponent.text = string.Empty;
    }

    public void StartDialogue(IEnumerable<Message> msg)
    {
        Reset();
        StartCoroutine(TypeDialogue(msg));

    }
    public IEnumerator TypeDialogue(IEnumerable<Message> msg)
    {
        foreach (Message m in msg)
        {
            string mMsg = m.Content;
            foreach (char c in mMsg.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            textComponent.text += "\n";
        }

    }
}
