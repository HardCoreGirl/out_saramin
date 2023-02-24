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

    private int m_nAnswerIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKTalkBoxAnswer(int nIndex, int nAnswerIndex, string strAnswer)
    {
        m_nIndex = nIndex;
        m_txtAnswer.text = strAnswer;
        m_nAnswerIndex = nAnswerIndex;

        Vector2 rectBG = m_goAnswer.GetComponent<RectTransform>().sizeDelta;

        rectBG.x = m_txtAnswer.preferredWidth + 40;

        //Debug.Log("Width : " + rectBG.x);

        m_goAnswer.GetComponent<RectTransform>().sizeDelta = rectBG;

        m_goCheckBox.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color(0.937255f, 0.9647059f, 1f);
        m_txtAnswer.color = new Color(0, 0.5215687f, 1);
    }

    public void ResetAnswer()
    {
        m_goCheckBox.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color(0.937255f, 0.9647059f, 1f);
        m_txtAnswer.color = new Color(0, 0.5215687f, 1);
    }

    public bool IsSelected()
    {
        return m_goCheckBox.activeSelf;
    }

    public int GetAnswerIndex()
    {
        return m_nAnswerIndex;
    }

    public void OnClickAnswer()
    {
        if( CUIsLGTKTalkBoxManager.Instance.GetMultiAnswer() > 1 )  // 멀티 선택
        {
            if( m_goCheckBox.activeSelf )
            {
                ResetAnswer();
                CUIsLGTKTalkBoxManager.Instance.UpdateBtnSendAnswer();
            } else
            {
                CUIsLGTKTalkBoxManager.Instance.EnableBtnSendAnswer();
                m_goCheckBox.SetActive(true);
                gameObject.GetComponent<Image>().color = new Color(0, 0.5215687f, 1f);
                m_txtAnswer.color = new Color(1, 1, 1);
            }
        }
        else
        {
            CUIsLGTKTalkBoxManager.Instance.SetAnswerIndex(m_nAnswerIndex);
            CUIsLGTKTalkBoxManager.Instance.ResetAnswer();
            CUIsLGTKTalkBoxManager.Instance.EnableBtnSendAnswer();
            m_goCheckBox.SetActive(true);
            gameObject.GetComponent<Image>().color = new Color(0, 0.5215687f, 1f);
            m_txtAnswer.color = new Color(1, 1, 1);
        }
    }
}
