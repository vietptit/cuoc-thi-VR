using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;
    public List<AudioClip> Aututorial;
    int idx = 0;
    public AudioSource audioSource;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject panel;
    [TextArea(2, 5)]
    public List<string> lines = new List<string>();
    private Queue<string> messages = new Queue<string>();

    void Start()
    {
        Instance = this;
        panel.SetActive(false);
    }
    public void ShowDialogue(List<string> lines)
    {
        // audioSource.PlayOneShot(Aututorial[idx]);
        // idx++;
        // if (idx > Aututorial.Count) idx = 0;
        messages.Clear();
        foreach (string line in lines)
            messages.Enqueue(line);

        panel.SetActive(true);
        DisplayNextMessage();
    }

    public void DisplayNextMessage()
    {
        SoundEffect.Instance.PlaySound(SoundEffect.Instance.audioSource.clip);
         audioSource.PlayOneShot(Aututorial[idx]);
        idx++;
        if (idx > Aututorial.Count)
        {
            return;
            idx = 0;
        }
        if (messages.Count == 0)
            {
                EndDialogue();
                return;
            }
        
        string message = messages.Dequeue();
        dialogueText.text = message;
    }

    private void EndDialogue()
    {
        panel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowDialogue(lines);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            DisplayNextMessage();
        }
    }
}
