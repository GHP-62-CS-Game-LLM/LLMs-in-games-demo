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
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string text;
    public string newText;
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
        textComponent.text = text;
    }

    public void StartDialogue(IEnumerable<Message> msg)
    {
        Reset();
        List<Message> msgList = new List<Message>
        {
            msg.ElementAt(msg.Count() - 2),
            msg.ElementAt(msg.Count() - 1)
        };
        IEnumerable<Message> newMessages = msgList;
        StartCoroutine(TypeDialogue(newMessages));
    }
    public IEnumerator TypeDialogue(IEnumerable<Message> msg)
    {
        newText = FullString(msg);
        foreach (Message m in msg)
        {
            string mMsg = m.Content;
            textComponent.text += m.Role.ToString() + ": ";
            foreach (char c in mMsg.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            textComponent.text += "\n\n";
        }
        text = text + newText;
    }
    string FullString(IEnumerable<Message> msg)
    {
        string finalMsg = "";
        foreach (Message m in msg)
        {
            finalMsg += m.Role.ToString() + ": " + m.Content + "\n\n";
        }

        return finalMsg;
    }
}
