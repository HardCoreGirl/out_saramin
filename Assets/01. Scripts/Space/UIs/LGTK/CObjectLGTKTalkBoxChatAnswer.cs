using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectLGTKTalkBoxChatAnswer : MonoBehaviour
{
    public Text m_txtChat;

    public RectTransform m_rectBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateChat(string strChat)
    {
        m_txtChat.text = strChat;

        var rectSize = m_rectBG.sizeDelta;
        rectSize.x = m_txtChat.preferredWidth + 46;
        int nRow = 0;
        if (rectSize.x >= 540)
        {

            nRow = (int)(rectSize.x / 540);
            //Debug.Log(rectSize.x + ", " + nRow);

            rectSize.x = 540;
        }

        m_rectBG.sizeDelta = rectSize;

        //rectSize.y = (nRow * 24) + 40;

        rectSize.y = m_txtChat.preferredHeight + 20;

        m_rectBG.sizeDelta = rectSize;

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, rectSize.y);
    }
}
