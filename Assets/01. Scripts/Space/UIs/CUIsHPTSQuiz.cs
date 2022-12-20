using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsHPTSQuiz : MonoBehaviour
{
    public GameObject[] m_listGoWord = new GameObject[5];
    public GameObject[] m_listGoText = new GameObject[3];
    public Text[] m_listTxtWord = new Text[5];

    private Color m_clrDefalutClr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHPTSQuiz(string strQuiz01, string strQuiz02, string strQuiz03, string strQuiz04, string strQuiz05)
    {
        m_clrDefalutClr = m_listGoWord[1].GetComponent<Image>().color;

        m_listTxtWord[0].text = strQuiz01;
        var rectSize = m_listGoWord[0].GetComponent<RectTransform>().sizeDelta;
        rectSize.x = m_listTxtWord[0].preferredWidth;
        m_listGoWord[0].GetComponent<RectTransform>().sizeDelta = rectSize;

        if (strQuiz02.Equals(""))
        {
            m_listGoWord[1].SetActive(false);
        } else
        {
            m_listGoWord[1].SetActive(true);
            m_listTxtWord[1].text = strQuiz02;
            rectSize = m_listGoWord[1].GetComponent<RectTransform>().sizeDelta;
            rectSize.x = m_listTxtWord[1].preferredWidth + 24;
            m_listGoWord[1].GetComponent<RectTransform>().sizeDelta = rectSize;
            m_listGoText[0].GetComponent<RectTransform>().sizeDelta = rectSize;
        }

        if (strQuiz03.Equals(""))
        {
            m_listGoWord[2].SetActive(false);
        } else
        {
            m_listGoWord[2].SetActive(true);
            m_listTxtWord[2].text = strQuiz03;
            rectSize = m_listGoWord[2].GetComponent<RectTransform>().sizeDelta;
            rectSize.x = m_listTxtWord[2].preferredWidth + 24;
            m_listGoWord[2].GetComponent<RectTransform>().sizeDelta = rectSize;
            m_listGoText[1].GetComponent<RectTransform>().sizeDelta = rectSize;
        }

        if (strQuiz04.Equals(""))
        {
            m_listGoWord[3].SetActive(false);
        } else
        {
            m_listGoWord[3].SetActive(true);
            m_listTxtWord[3].text = strQuiz04;
            rectSize = m_listGoWord[3].GetComponent<RectTransform>().sizeDelta;
            rectSize.x = m_listTxtWord[3].preferredWidth + 24;
            m_listGoWord[3].GetComponent<RectTransform>().sizeDelta = rectSize;
            m_listGoText[2].GetComponent<RectTransform>().sizeDelta = rectSize;
        }

        if (strQuiz05.Equals(""))
        {
            m_listGoWord[4].SetActive(false);
        } else
        {
            m_listGoWord[4].SetActive(true);
            m_listTxtWord[4].text = strQuiz05;
            rectSize = m_listGoWord[4].GetComponent<RectTransform>().sizeDelta;
            rectSize.x = m_listTxtWord[4].preferredWidth;
            m_listGoWord[4].GetComponent<RectTransform>().sizeDelta = rectSize;
        }

    }

    public void OnClickAnswer(int nIndex)
    {
        Color clrDefault;
        ColorUtility.TryParseHtmlString("#F0F6FF", out clrDefault);

        Color clrBlue;
        ColorUtility.TryParseHtmlString("#0085FF", out clrBlue);

        m_listGoWord[1].GetComponent<Image>().color = clrDefault;
        m_listGoWord[2].GetComponent<Image>().color = clrDefault;
        m_listGoWord[3].GetComponent<Image>().color = clrDefault;

        m_listTxtWord[1].color = clrBlue;
        m_listTxtWord[2].color = clrBlue;
        m_listTxtWord[3].color = clrBlue;

        m_listGoWord[nIndex].GetComponent<Image>().color = clrBlue;
        m_listTxtWord[nIndex].color = Color.white;
    }
}
