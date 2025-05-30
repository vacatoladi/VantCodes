
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    public GameObject background;

    //RawImage background;
    TextMeshProUGUI nameText;
    TextMeshProUGUI talktext;

    public float speed = 10f;
    bool open = false;

    //float valor = 0;

    void Start()
    {
        //background = transform.GetChild(0).GetComponent<RawImage>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        talktext = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //if (open)
        //{
        //    //valor = Mathf.Lerp(0, 1, speed * Time.deltaTime);
        //    //background.uvRect = new Rect(0, 0, valor, 1);
        //}
        //else
        //{
        //    //valor = Mathf.Lerp(0, 0, speed * Time.deltaTime);
        //    //background.uvRect = new Rect(0, 0, valor, 1);
        //}

        background.SetActive(open);
    }

    public void SetName(string name)
    {
        Debug.Log(name);
        nameText.text = name;

    }

    public void Enable()
    {
        //valor = 1;
        open = true;
    }

    public void Disable()
    {
        //valor = 0;
        open = false;
        nameText.text = "";
        talktext.text = "";
    }
}
