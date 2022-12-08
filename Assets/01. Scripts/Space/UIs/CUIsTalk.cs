using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsTalk : MonoBehaviour
{
    public int m_nQuizIndex;

    public GameObject[] m_listSelect = new GameObject[2];
    

    public GameObject[] m_listSelected = new GameObject[4];
    public GameObject[] m_listBtnSelector = new GameObject[4];
    public Text[] m_listTxtSelector = new Text[4];

    public GameObject m_goBtnReset;

    public GameObject m_goSelector;

    private int m_nSelectIndex;

    // Start is called before the first frame update
    void Start()
    {
        //InitUIs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitUIs(int nSetIndex, int nQuizIndex, bool bTutorial = false)
    {
        //Debug.Log("00000 QuizIndex : " + nQuizIndex);
        Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
        //Debug.Log("11111 Question Cnt : " + quizRQT.sets[nSetIndex].questions.Length);
        //Debug.Log("Question Total : " + quizRQT.sets[nSetIndex].questions[nQuizIndex].test_prg_time);
        //Debug.Log("Question : " + quizRQT.sets[nSetIndex].questions[nQuizIndex].qst_cnnt);
        string[] listQuiz = quizRQT.sets[nSetIndex].questions[nQuizIndex].qst_cnnt.Split("\n");
        //string[] listQuiz = strQuiz.Split("\n");
        float fTotalHeight = 20;
        for (int i = 0;i<listQuiz.Length;i++)
        {
            //Debug.Log(listQuiz[i]);
            GameObject goTalk = Instantiate(Resources.Load("Prefabs/quizTalk") as GameObject);
            goTalk.transform.parent = transform;
            Vector2 vecPoz2 = goTalk.GetComponent<RectTransform>().anchoredPosition;
            //Debug.Log("Before!!!! Y : " + vecPoz2.y);
            //Vector3 vecPoz = goTalk.transform.localPosition;
            //Debug.Log("Before Y : " + vecPoz.y);
            
            ////goTalk.GetComponent<RectTransform>().localPosition = vecPoz;
            //goTalk.transform.localPosition = vecPoz;
            //Debug.Log("After Y : " + vecPoz.y);
            float fHeight = goTalk.GetComponent<CUIsTalkBubble>().InitUIs(listQuiz[i]);
            vecPoz2.y = (-8 * i) - fTotalHeight + 16;
            //Debug.Log("Height : " + vecPoz2.y);
            //Debug.Log("Height : " + fHeight);
            goTalk.GetComponent<RectTransform>().anchoredPosition = vecPoz2;
            fTotalHeight += (fHeight + 10);
        }

        //Vector2 vecSelector = m_goSelector.GetComponent<RectTransform>().anchoredPosition;
        //vecSelector.y = (fTotalHeight + 16) * -1;
        //m_goSelector.GetComponent<RectTransform>().anchoredPosition = vecSelector;

        GameObject goAnswer = Instantiate(Resources.Load("Prefabs/quizAnswer") as GameObject);
        goAnswer.transform.parent = transform;
        goAnswer.GetComponent<CUIsAnswer>().InitAnswer(nSetIndex, nQuizIndex, bTutorial);

        fTotalHeight += (40 + 10);

        Vector2 vecSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = fTotalHeight;
        gameObject.GetComponent<RectTransform>().sizeDelta = vecSize;

        //Debug.Log("Total Height : " + fTotalHeight);
    }

    public void OnClickSelector(int nIndex)
    {
        m_nSelectIndex = nIndex;
        ShowSelect(1);
        ShowSelected(m_nSelectIndex);

        CUIsSpaceScreenLeft.Instance.ShowQuiz(0, m_nQuizIndex + 1);

        if (m_nSelectIndex == 1 || m_nSelectIndex == 2)
        {
            m_goBtnReset.transform.localPosition = new Vector3(445, 0, 0);
        }
        else
        {
            m_goBtnReset.transform.localPosition = new Vector3(410, 0, 0);
        }
    }

    public void OnClickReset()
    {
        ShowSelect(0);
    }

    public void HideAllSelect()
    {
        for (int i = 0; i < m_listSelect.Length; i++)
        {
            HideSelect(i);
        }
    }

    public void ShowSelect(int nIndex)
    {
        HideAllSelect();
        m_listSelect[nIndex].SetActive(true);
    }

    public void HideSelect(int nIndex)
    {
        m_listSelect[nIndex].SetActive(false);
    }

    public void HideAllSelected()
    {
        for (int i = 0; i < m_listSelected.Length; i++)
        {
            HideSelected(i);
        }
    }

    public void ShowSelected(int nIndex)
    {
        HideAllSelected();
        m_listSelected[nIndex].SetActive(true);
    }

    public void HideSelected(int nIndex)
    {
        m_listSelected[nIndex].SetActive(false);
    }
}
