using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypeAnimation : MonoBehaviour
{

    public Action TypeFinished;

    public TextMeshProUGUI text;    

    public string fullText;

    float typeDelay = 0.05f;
    
    int i = 0;

    //Função utilizada fora do script que ativa a Corrotina (Sei que dava para iniciar ela la fora, mas fiz assim mesmo)
    public void StartAnimation()
    {
        StartCoroutine(TypeText());
    }

    //Corrotina
    IEnumerator TypeText()
    {
        text.text = fullText;
        text.maxVisibleCharacters = 0;

        for (i = 0; i <= text.text.Length; i++)
        {
            text.maxVisibleCharacters++;

            yield return new WaitForSeconds(typeDelay);
        }

        if (i > 0)
        {
            TypeFinished?.Invoke();
        }

    }

    public void Skip()
    {
        StopCoroutine(TypeText());
        text.maxVisibleCharacters = text.text.Length;
        TypeFinished?.Invoke();
    }

}
