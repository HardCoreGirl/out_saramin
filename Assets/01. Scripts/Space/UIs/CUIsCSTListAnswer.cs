using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsCSTListAnswer : MonoBehaviour
{
    public InputField m_ifAnswer;
    public Text m_txtAnswer;

    private int m_nSession;
    private int m_nIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitListAnswer(int nSesstion, int nIndex)
    {
        m_nSession = nSesstion;
        m_nIndex = nIndex;
        string strAnswer = m_nIndex.ToString() + ". 답변을 입력해 주세요";
        UpdateAnswer(strAnswer);
        m_ifAnswer.interactable = false;
    }

    public void UpdateAnswer(string strAnswer)
    {
        m_txtAnswer.text = strAnswer;
    }

    public void OnClickAnswer()
    {
        Debug.Log("Session : " + m_nSession + ", Index : " + m_nIndex);

        CUIsSpaceScreenLeft.Instance.ShowCSTPage();
    }

    public void OnChangeAnswer()
    {
        if( m_nSession == 0 )
        {
            CUIsCSTPage2Manager.Instance.ActiveInputField(1, m_nIndex);
        } else
        {
            CUIsCSTPage2Manager.Instance.ActiveInputField(0, m_nIndex + 1);
        }
    }

    public void ActiveInputField()
    {
        m_ifAnswer.interactable = true;
    }
}
