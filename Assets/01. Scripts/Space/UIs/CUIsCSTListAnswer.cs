using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class CUIsCSTListAnswer : MonoBehaviour
{
    public InputField m_ifAnswer;
    public Text m_txtAnswer;

    public Image m_imgBG;
    public Text m_txtQuiz;
    public Text m_txtRealAnswer;

    public Image m_imgSelected;

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
        m_imgBG.color = new Color(0.7372549f, 0.8431373f, 1);
        Color clrSelected = m_imgSelected.color;
        clrSelected.a = 0;
        m_imgSelected.color = clrSelected;
        //gameObject.GetComponent<Image>().color = new Color(0.7372549f, 0.8431373f, 1);
    }

    public void UpdateAnswer(string strAnswer)
    {
        m_txtAnswer.text = strAnswer;
    }

    public void OnSelectAnswer()
    {
        Color clrAnswer = m_txtRealAnswer.color;
        clrAnswer.a = 1f;
        m_txtRealAnswer.color = clrAnswer;
        m_txtRealAnswer.fontStyle = FontStyle.Bold;

        Color clrSelected = m_imgSelected.color;
        clrSelected.a = 1f;
        m_imgSelected.color = clrSelected;
    }

    public void OnClickAnswer()
    {
        Debug.Log("Session : " + m_nSession + ", Index : " + m_nIndex);

        CUIsSpaceScreenLeft.Instance.ShowCSTPage();
    }

    public void OnChangeAnswer()
    {
        //Debug.Log("OnChangeAnswer");
        //gameObject.GetComponent<Image>().color = new Color(0.8588236f, 0.9215687f, 1);

        if ( m_nSession == 0 )
        {
            CUIsCSTPage2Manager.Instance.ActiveInputField(1, m_nIndex);
        } else
        {
            CUIsCSTPage2Manager.Instance.ActiveInputField(0, m_nIndex + 1);
        }
    }

    public void OnEndEditAnswer()
    {
        Debug.Log("OnEndEdit");
        Color clrAnswer = m_txtRealAnswer.color;
        clrAnswer.a = 0.5f;
        m_txtRealAnswer.color = clrAnswer;
        m_txtRealAnswer.fontStyle = FontStyle.Normal;

        Color clrSelected = m_imgSelected.color;
        clrSelected.a = 0;
        m_imgSelected.color = clrSelected;
    }

    public void ActiveInputField()
    {
        m_ifAnswer.interactable = true;
        m_imgBG.color = new Color(0.8588236f, 0.9215687f, 1);
        Color clrAnswer = m_txtAnswer.color;
        clrAnswer.a = 1;
        m_txtAnswer.color = clrAnswer;
        m_txtAnswer.fontStyle = FontStyle.Bold;
        m_txtRealAnswer.fontStyle = FontStyle.Bold;
    }

    public void DisableInputField()
    {
        OnEndEditAnswer();
        m_ifAnswer.interactable = false;
    }

    public string GetAnswerString()
    {
        return m_txtRealAnswer.text;
    }
}
