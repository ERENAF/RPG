using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] Dialog[] dialogs;
    [SerializeField] Text text;

    public IEnumerator Dialog(int index)
    {
        char[] dialogChars = dialogs[index].line.ToCharArray();
        foreach (char c in dialogChars)
        {
            TypeLine(c,dialogs[index].speedText);
        }
        yield return new WaitForSeconds(0);
    }

    private IEnumerator TypeLine(char c, int speedText)
    {
        text.text += c;
        yield return new WaitForSeconds(speedText);
    }

    public void SkipDialog()
    {
        StopAllCoroutines();
    }
}
