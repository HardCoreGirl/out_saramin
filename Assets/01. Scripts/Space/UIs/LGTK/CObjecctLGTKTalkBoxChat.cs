using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjecctLGTKTalkBoxChat : MonoBehaviour
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

    public void UpdateChat(string strChat, bool bIsQuiz = false)
    {
        m_txtChat.text = strChat;

        Color clrOutline = gameObject.GetComponent<Outline>().effectColor;
        clrOutline.a = 0f;

        //gameObject.GetComponent<Outline>().gameObject.SetActive(false);
        if (bIsQuiz)
        {
            m_txtChat.fontStyle = FontStyle.Bold;
            //gameObject.GetComponent<Outline>().gameObject.SetActive(true);
            
            clrOutline.a = 1f;
        }

        gameObject.GetComponent<Outline>().effectColor = clrOutline;

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

    }
}
