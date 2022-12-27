using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectLGTKTalkBoxAnswer : MonoBehaviour
{
    public GameObject m_goAnswer;
    public Text m_txtAnswer;

    public GameObject m_goCheckBox;

    private int m_nIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKTalkBoxAnswer(int nIndex, string strAnswer)
    {
        m_nIndex = nIndex;
        m_txtAnswer.text = strAnswer;

        Vector2 rectBG = m_goAnswer.GetComponent<RectTransform>().sizeDelta;

        rectBG.x = m_txtAnswer.preferredWidth + 40;

        Debug.Log("Width : " + rectBG.x);

        m_goAnswer.GetComponent<RectTransform>().sizeDelta = rectBG;

        m_goCheckBox.SetActive(false);
    }

    public void ResetAnswer()
    {
        m_goCheckBox.SetActive(false);
    }

    public void OnClickAnswer()
    {
        CUIsLGTKTalkBoxManager.Instance.ResetAnswer();
        m_goCheckBox.SetActive(true);
        Debug.Log("OnClick Answer : " + m_nIndex);        
    }
}
