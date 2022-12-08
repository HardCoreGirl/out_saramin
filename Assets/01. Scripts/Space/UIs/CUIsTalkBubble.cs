using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class CUIsTalkBubble : MonoBehaviour
{
    public Text m_txtTalkBubble;

    public TMP_Text m_tmpTalkBubble;

    public RectTransform m_rectBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float InitUIs(string strTalk)
    {
        //m_tmpTalkBubble.text = strTalk;
        //Debug.Log("Talk : " + strTalk);
        //Debug.Log("Height : " + m_tmpTalkBubble.preferredHeight);
        m_txtTalkBubble.text = strTalk;

        var rectSize = m_rectBG.sizeDelta;
        //Debug.Log("Width : " + m_txtTalkBubble.preferredWidth);

        rectSize.x = m_txtTalkBubble.preferredWidth + 40;

        int nRow = 0;
        if (rectSize.x >= 1050)
        {
            nRow = (int)(rectSize.x / 1050);
            //Debug.Log("Row : " + nRow);
            rectSize.x = 1050;

        }


        rectSize.y = ((nRow + 1) * 32) + 4;

        

        //Debug.Log("Height : " + m_txtTalkBubble.preferredHeight);
        
        m_rectBG.sizeDelta = rectSize;

        return rectSize.y;
    }
}
