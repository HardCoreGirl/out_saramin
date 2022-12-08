using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsSpaceScreenLeft : MonoBehaviour
{
    #region SingleTon
    public static CUIsSpaceScreenLeft _instance = null;

    public static CUIsSpaceScreenLeft Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsSpaceScreenLeft install null");

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    public GameObject[] m_listPage = new GameObject[2];

    public GameObject[] m_listToturialPage = new GameObject[2];

    public Text[] m_listQuizCount = new Text[4];
    public Text[] m_listExmTime = new Text[4];

    public GameObject m_goTalkContents;

    public GameObject[] m_listQuiz = new GameObject[6];

    public GameObject m_goPopupFinish;
    public GameObject m_goPopupTimeover;
    public GameObject m_goPopupExit;

    public GameObject m_goScrollView;
    public GameObject m_goContents;

    public Text m_txtRemain;

    private int m_nOpenQuiz;

    private int m_nRemainTime = 30;

    private float m_fScrollViewHeight = 0;

    private int m_nLastQuizIndex = 0;

    private bool m_bIsRQTTutorial = true;

    // Start is called before the first frame update
    void Start()
    {
        InitUIs();

        m_fScrollViewHeight = m_goScrollView.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUIs()
    {
        HideAllPopup();
        ShowPage(0);
    }

    public void OnClickClose()
    {
        CUIsSpaceManager.Instance.HideLeftPage();
    }

    public void OnClickMission(int nIndex)
    {
        //ShowPopupFinish();
        ShowPage(1, true);
        HideAllQuiz();
        //ShowQuiz(nIndex);

        //InitQuiz();
    }

    public void InitQuiz()
    {
        m_nRemainTime = 30;

        StartCoroutine("ProcessQuiz");
    }

    IEnumerator ProcessQuiz()
    {
        m_txtRemain.text = "00:" + m_nRemainTime.ToString("00");
        while(true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;
            m_txtRemain.text = "00:" + m_nRemainTime.ToString("00");

            if (m_nRemainTime == 0)
                break;
        }

        Debug.Log("TimeOut");
        CUIsSpaceScreenLeft.Instance.HideAllPopup();
        CUIsSpaceScreenLeft.Instance.ShowPopupTimeover();

    }

    public void UpdateScrollView()
    {
        Debug.Log(m_fScrollViewHeight);

        StartCoroutine("ProcessUpdateScrollView");
    }

    IEnumerator ProcessUpdateScrollView()
    {
        yield return new WaitForSeconds(0.1f);

        if (m_goContents.GetComponent<RectTransform>().sizeDelta.y > m_fScrollViewHeight)
        {
            Vector3 vecPoz = m_goContents.transform.localPosition;
            vecPoz.y = (m_goContents.GetComponent<RectTransform>().sizeDelta.y - m_fScrollViewHeight);
            m_goContents.transform.localPosition = vecPoz;
        }

    }

    public void OnClickFinish()
    {
        ShowPopupFinish();
    }

    public void HideAllPages()
    {
        for(int i = 0; i < m_listPage.Length; i++)
        {
            HidePage(i);
        }
    }

    public void ShowPage(int nPage, bool bTutorial = false)
    {
        HideAllPages();
        m_listPage[nPage].SetActive(true);

        if (nPage == 0)
        {
            Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT");
            m_listQuizCount[0].text = quizRQT.sets.Length.ToString() + " 문항";
            m_listExmTime[0].text = quizRQT.exm_time.ToString() + " 초";
            Debug.Log("Exm Time : " + quizRQT.exm_time);

            m_listQuizCount[1].text = CQuizData.Instance.GetQuizTotalCount("CST").ToString() + " 문항";
            m_listExmTime[1].text = quizRQT.exm_time.ToString() + " 분";
            m_listQuizCount[2].text = CQuizData.Instance.GetQuizTotalCount("RAT").ToString() + " 문항";
            m_listExmTime[2].text = quizRQT.exm_time.ToString() + " 분";
            m_listQuizCount[3].text = CQuizData.Instance.GetQuizTotalCount("HPTS").ToString() + " 문항";
            m_listExmTime[3].text = quizRQT.exm_time.ToString() + " 분";
        }
        else if (nPage == 1)
        {
            Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
            Debug.Log(quizRQT.sets.Length);
            for(int i = 0; i < quizRQT.sets.Length; i++)
            {
                GameObject goTalk = Instantiate(Resources.Load("Prefabs/talk_list") as GameObject);
                goTalk.transform.parent = m_goTalkContents.transform;
                goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
            }
        }
    }


    public void HidePage(int nPage)
    {
        m_listPage[nPage].SetActive(false);
    }

    public void HideAllQuiz()
    {
        for (int i = 0; i < m_listQuiz.Length; i++)
        {
            HideQuiz(i);
        }
    }

    public void ShowQuiz(int nSetIndex, int nIndex, bool bTutorial = false)
    {
        Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
        GameObject goTalk = Instantiate(Resources.Load("Prefabs/quizContent") as GameObject);
        goTalk.transform.parent = m_goContents.transform;
        goTalk.GetComponent<CUIsTalk>().InitUIs(nSetIndex, nIndex, bTutorial);


        //string[] listQuiz = quizRQT.sets[0].questions[0].qst_cnnt.Split("\n");

        //for (int i = 0; i < listQuiz.Length; i++)
        //{
        //    Debug.Log(listQuiz[i]);

        //}



        //goTalk.transform.parent = m_goTalkContents.transform;
        //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(0, quizRQT.sets[0].dir_cnnt);
        /*
        m_nOpenQuiz = nIndex;
        m_listQuiz[nIndex].SetActive(true);
        if (nIndex < m_listQuiz.Length - 1 )
            m_listQuiz[nIndex].GetComponent<CUIsDummyTalk>().InitUIs();

        UpdateScrollView();
        */
    }

    public void HideQuiz(int nIndex)
    {
        m_listQuiz[nIndex].SetActive(false);
    }

    public void DelQuiz()
    {
        Component[] listChilds = m_goContents.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if ( iter.transform != m_goContents.transform )
            {
                Destroy(iter.gameObject);
            }
        }
    }

    public void HideAllPopup()
    {
        HidePopupFinish();
        HidePopupTimeover();
    }
    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
    }

    public void HidePopupFinish()
    {
        Debug.Log("HidePopupFinish");
        m_goPopupFinish.SetActive(false);
    }

    public void ShowPopupTimeover()
    {
        m_goPopupTimeover.SetActive(true);
    }

    public void HidePopupTimeover()
    {
        m_goPopupTimeover.SetActive(false);
    }

    public void InitRQTQuiz(bool bIsTutorial)
    {
        SetRQTTutorial(bIsTutorial);
        SetLastQuizIndex(0);
        if (!bIsTutorial)
        {
            Quiz quizData = CQuizData.Instance.GetQuiz("RQT", true);
            m_nRemainTime = quizData.exm_time;
            StartCoroutine("ProcessRQTQuiz");
        }
    }

    IEnumerator ProcessRQTQuiz()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtRemain.text = nMin.ToString("00") + ":" +  nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if (m_nRemainTime == 0)
                break;
        }

        Debug.Log("TimeOut");
        CUIsSpaceScreenLeft.Instance.HideAllPopup();
        CUIsSpaceScreenLeft.Instance.ShowPopupTimeover();

    }

    public void SetLastQuizIndex(int nIndex)
    {
        m_nLastQuizIndex = nIndex;
    }

    public int GetLastQuizIndex()
    {
        return m_nLastQuizIndex;
    }

    public void SetRQTTutorial(bool bIsTutorial)
    {
        m_bIsRQTTutorial = bIsTutorial;
    }

    public bool IsRQTTutorial()
    {
        return m_bIsRQTTutorial;
    }
}

