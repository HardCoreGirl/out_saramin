using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsChat : MonoBehaviour
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
        rectSize.x = m_txtChat.preferredWidth;
        m_rectBG.sizeDelta = rectSize;
    }
}
