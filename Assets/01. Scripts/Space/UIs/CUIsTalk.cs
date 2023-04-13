using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsTalk : MonoBehaviour
{
    public int m_nQuizIndex;
    public int m_nSetIndex;

    public GameObject[] m_listSelect = new GameObject[2];
    

    public GameObject[] m_listSelected = new GameObject[4];
    public GameObject[] m_listBtnSelector = new GameObject[4];
    public Text[] m_listTxtSelector = new Text[4];

    public GameObject m_goBtnReset;

    public GameObject m_goSelector;

    private int m_nSelectIndex;

    private string[] m_listQuiz;
    private bool m_bIsTutorial;

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
        m_nSetIndex = nSetIndex;
        m_nQuizIndex = nQuizIndex;
        m_bIsTutorial = bTutorial;
        //Debug.Log("00000 SetIndex : " + nSetIndex + ", QuizIndex : " + nQuizIndex);
        Quiz quizRQT = CQuizData.Instance.GetRQT().body;

        if (bTutorial)
        {
            //Debug.Log("1111 Tutorial : " + bTutorial);
            quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
        }
        //Debug.Log("22222 Question Cnt : " + quizRQT.sets[nSetIndex].questions.Length);
        //Debug.Log("Question Total : " + quizRQT.sets[nSetIndex].questions[nQuizIndex].test_prg_time);
        //Debug.Log("Question : " + quizRQT.sets[nSetIndex].questions[nQuizIndex].qst_cnnt);
        //string[] listQuiz;
        if (bTutorial)
            m_listQuiz = quizRQT.sets[nSetIndex].questions[nQuizIndex].qst_cnnt.Split("\n");
        else
            m_listQuiz = quizRQT.sets[nQuizIndex].questions[0].qst_cnnt.Split("\n");

        StartCoroutine("ProcessDisplayQuiz");
        //string[] listQuiz = strQuiz.Split("\n");
        //float fTotalHeight = 20;
        //for (int i = 0;i<m_listQuiz.Length;i++)
        //{
        //    Debug.Log("List Quiz : [" + i.ToString() + "]" + m_listQuiz[i]);
        //    GameObject goTalk = Instantiate(Resources.Load("Prefabs/quizTalk") as GameObject);
        //    goTalk.transform.parent = transform;
        //    Vector2 vecPoz2 = goTalk.GetComponent<RectTransform>().anchoredPosition;
        //    //Debug.Log("Before!!!! Y : " + vecPoz2.y);
        //    //Vector3 vecPoz = goTalk.transform.localPosition;
        //    //Debug.Log("Before Y : " + vecPoz.y);
            
        //    ////goTalk.GetComponent<RectTransform>().localPosition = vecPoz;
        //    //goTalk.transform.localPosition = vecPoz;
        //    //Debug.Log("After Y : " + vecPoz.y);
        //    float fHeight = goTalk.GetComponent<CUIsTalkBubble>().InitUIs(m_listQuiz[i]);
        //    vecPoz2.y = (-8 * i) - fTotalHeight + 16;
        //    //Debug.Log("Height : " + vecPoz2.y);
        //    //Debug.Log("Height : " + fHeight);
        //    goTalk.GetComponent<RectTransform>().anchoredPosition = vecPoz2;
        //    fTotalHeight += (fHeight + 10);
        //}

        ////Vector2 vecSelector = m_goSelector.GetComponent<RectTransform>().anchoredPosition;
        ////vecSelector.y = (fTotalHeight + 16) * -1;
        ////m_goSelector.GetComponent<RectTransform>().anchoredPosition = vecSelector;

        //GameObject goAnswer = Instantiate(Resources.Load("Prefabs/quizAnswer") as GameObject);
        //goAnswer.transform.parent = transform;
        //goAnswer.GetComponent<CUIsAnswer>().InitAnswer(nSetIndex, nQuizIndex, bTutorial);

        //fTotalHeight += (40 + 10);

        //Vector2 vecSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        //vecSize.y = fTotalHeight;
        //gameObject.GetComponent<RectTransform>().sizeDelta = vecSize;

        //Debug.Log("Total Height : " + fTotalHeight);
    }

    IEnumerator ProcessDisplayQuiz()
    {
        float fTotalHeight = 20;
        for (int i = 0; i < m_listQuiz.Length; i++)
        {
            if( i != 0)
                yield return new WaitForSeconds(2f);
            //Debug.Log("List Quiz : [" + i.ToString() + "]" + m_listQuiz[i]);
            //yield return new WaitForSeconds(2f);
            //if( i != 0 )
            //{
            //    if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            //        yield return new WaitForSeconds(0.2f);
            //    else
            //        yield return new WaitForSeconds(2f);
            //}

            GameObject goTalk = Instantiate(Resources.Load("Prefabs/quizTalk") as GameObject);
            goTalk.transform.parent = transform;
            Vector2 vecPoz2 = goTalk.GetComponent<RectTransform>().anchoredPosition;
            //Debug.Log("Before!!!! Y : " + vecPoz2.y);
            //Vector3 vecPoz = goTalk.transform.localPosition;
            //Debug.Log("Before Y : " + vecPoz.y);

            ////goTalk.GetComponent<RectTransform>().localPosition = vecPoz;
            //goTalk.transform.localPosition = vecPoz;
            //Debug.Log("After Y : " + vecPoz.y);
            float fHeight = goTalk.GetComponent<CUIsTalkBubble>().InitUIs(m_listQuiz[i]);
            //vecPoz2.y = (-8 * i) - fTotalHeight + 16;
            vecPoz2.y = (-26 * i) - fTotalHeight + 16;
            //Debug.Log("Height : " + vecPoz2.y);
            //Debug.Log("Height : " + fHeight);

            fTotalHeight += (fHeight + 10);

            goTalk.GetComponent<RectTransform>().anchoredPosition = vecPoz2;


            
        }

        //Vector2 vecSelector = m_goSelector.GetComponent<RectTransform>().anchoredPosition;
        //vecSelector.y = (fTotalHeight + 16) * -1;
        //m_goSelector.GetComponent<RectTransform>().anchoredPosition = vecSelector;

        GameObject goAnswer = Instantiate(Resources.Load("Prefabs/quizAnswer") as GameObject);
        goAnswer.transform.parent = transform;
        goAnswer.GetComponent<CUIsAnswer>().InitAnswer(m_nSetIndex, m_nQuizIndex, m_bIsTutorial);

        fTotalHeight += (40 + 10);

        Vector2 vecSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = fTotalHeight;
        gameObject.GetComponent<RectTransform>().sizeDelta = vecSize;
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
