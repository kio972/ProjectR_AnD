using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speakerField;
    [SerializeField]
    private TextMeshProUGUI textField;
    private Coroutine currentDisplay = null;
    
    public Image illurst;

    public void CallDialog(int index)
    {
        if (currentDisplay != null)
            StopCoroutine(currentDisplay);

        List<string[]> messages = DialogData.Instance.LoadDialog(index);
        currentDisplay = StartCoroutine(DisplayText(messages));
    }

    public void CallDialog(List<string[]> messages, System.Action callback = null)
    {
        if (currentDisplay != null)
            StopCoroutine(currentDisplay);

        currentDisplay = StartCoroutine(DisplayText(messages, callback));
    }

    private IEnumerator DisplayText(List<string[]> messages, System.Action callback = null)
    {
        int currentMessageIndex = 0;

        while (currentMessageIndex < messages.Count)
        {
            speakerField.text = messages[currentMessageIndex][0]; // 0 = speakerKey;
            textField.text = messages[currentMessageIndex][1]; // 1 = dialogKey;

            while (true)
            {
                bool isBreak = false;
                foreach(KeyCode key in InputManager.Instance.nextMessageKeys)
                {
                    if(Input.GetKeyDown(key))
                    {
                        isBreak = true;
                        break;
                    }
                }

                if (isBreak)
                    break;

                yield return null;
            }

            currentMessageIndex++;
            yield return null;
        }

        callback?.Invoke();
    }
}
