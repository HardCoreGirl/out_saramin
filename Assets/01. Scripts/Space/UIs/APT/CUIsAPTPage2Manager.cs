using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAPTPage2Manager : MonoBehaviour
{
    #region SingleTon
    public static CUIsAPTPage2Manager _instance = null;

    public static CUIsAPTPage2Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsAPTPage2Manager install null");

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

    public GameObject[] m_listQuizBoard = new GameObject[9];

    public GameObject m_goQuizListContent;

    public GameObject m_goTutorialMsg;

    private GameObject[] m_listQuizList = new GameObject[29];

    public Text m_txtQuizTitle;

    public Text m_txtRemainTime;

    private bool m_bIsTutorial;
    private bool m_bIsTutorialWait;

    private int m_nQuizType; // 0 : APTD1, 1 : APTD2
    private int m_nQuizIndex;
    private int[] m_listAnswerIndex = new int[4];

    private int m_nRemainTime;

    // Start is called before the first frame update
    void Start()
    {
        HideTutorialMsg();
        //InitAPTPage2();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTPage2()
    {

        ShowQuizBoard(0);

        for (int i = 0; i < 29; i++)
        {
            m_listQuizList[i] = Instantiate(Resources.Load("Prefabs/APTQuizList02") as GameObject);
            m_listQuizList[i].transform.parent = m_goQuizListContent.transform;
            if( i == 0 )
                m_listQuizList[i].GetComponent<CObjectAPTQuizList2>().InitAPTQuizList2(i, 1);
            else
                m_listQuizList[i].GetComponent<CObjectAPTQuizList2>().InitAPTQuizList2(i);
        }

        ShowQuiz(0);
        SetTutorial(true);
        SetTutorialWait(false);

        m_txtRemainTime.text = "시작 전";
        m_nRemainTime = CQuizData.Instance.GetQuiz("APTD1").exm_time;
        //m_nRemainTime = 60;
        //+ CQuizData.Instance.GetQuiz("APTD2").exm_time;
    }

    public void InitAPTD2()
    {
        m_nQuizType = 1;
        m_nRemainTime = CQuizData.Instance.GetQuiz("APTD2").exm_time;
        DelQuizList();

        for (int i = 0; i < 21; i++)
        {
            m_listQuizList[i] = Instantiate(Resources.Load("Prefabs/APTQuizList02") as GameObject);
            m_listQuizList[i].transform.parent = m_goQuizListContent.transform;
            if (i == 0)
                m_listQuizList[i].GetComponent<CObjectAPTQuizList2>().InitAPTQuizList2(i, 1);
            else
                m_listQuizList[i].GetComponent<CObjectAPTQuizList2>().InitAPTQuizList2(i);
        }

        HideExQuizList();

        StartQuiz();
    }

    public void DelQuizList()
    {
        Component[] listChilds = m_goQuizListContent.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goQuizListContent.transform)
            {
                Destroy(iter.gameObject);
            }
        }
    }

    public void ShowQuiz(int nIndex)
    {
        Debug.Log("Show Quiz : " + nIndex);

        int nRealIndex = nIndex;
        if(nIndex == 0)
        {
            // 연습문제
            Debug.Log("연습문제");
            ShowQuizBoard(1);
            return;
        }
        nRealIndex--;

        string strKey = "APTD1";
        if (m_nQuizType == 1) strKey = "APTD2";

        Quiz quizAPT = CQuizData.Instance.GetQuiz(strKey);
        if (nRealIndex < quizAPT.sets.Length)
        {
            //m_nQuizType = 0;
            m_nQuizIndex = quizAPT.sets[nRealIndex].questions[0].test_qst_idx;

            for(int i = 0; i < quizAPT.sets[nRealIndex].questions[0].answers.Length; i++)
            {
                m_listAnswerIndex[i] = quizAPT.sets[nRealIndex].questions[0].answers[i].anwr_idx;
            }

            Debug.Log("APTD1 문제 Type : " + quizAPT.sets[nRealIndex].questions[0].qst_exos_cd);
            //m_txtQuizTitle.text = quizAPT.sets[nRealIndex].dir_cnnt;
            m_txtQuizTitle.text = quizAPT.sets[nRealIndex].questions[0].qst_cnnt;
            // APTD1 문제
            if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_A"))
            {
                ShowQuizBoard(1);
                m_listQuizBoard[1].GetComponent<CAPTQuizManager>().InitQuizType(m_nQuizType, nRealIndex, nIndex);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_B"))
            {
                ShowQuizBoard(2);
                m_listQuizBoard[2].GetComponent<CAPTQuizManager>().InitQuizType(m_nQuizType, nRealIndex, nIndex);

            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_C"))
            {
                ShowQuizBoard(3);
                m_listQuizBoard[3].GetComponent<CAPTQuizManager>().InitQuizType(m_nQuizType, nRealIndex, nIndex);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_D"))
            {
                ShowQuizBoard(4);
                m_listQuizBoard[4].GetComponent<CAPTQuizManager>().InitQuizType(m_nQuizType, nRealIndex, nIndex);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_E"))
            {
                ShowQuizBoard(5);
                m_listQuizBoard[5].GetComponent<CAPTQuizManager>().InitQuizType(m_nQuizType, nRealIndex, nIndex);
            } else {
                Debug.Log("알수 없는 타입");
            }
            return;
        }

        //nRealIndex -= quizAPT.sets.Length;

        //Quiz quizAPT2 = CQuizData.Instance.GetQuiz("APTD2");
        //if( nRealIndex < quizAPT2.sets.Length)
        //{
        //    m_nQuizType = 1;
        //    m_nQuizIndex = quizAPT.sets[nRealIndex].questions[0].test_qst_idx;
        //    m_txtQuizTitle.text = quizAPT2.sets[nRealIndex].dir_cnnt;
            
        //    //Debug.Log("APTD2 문제");
        //    if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_A"))
        //    {
        //        ShowQuizBoard(1);

        //        m_listQuizBoard[1].GetComponent<CAPTQuizManager>().InitQuizType(1, nRealIndex, nIndex);
        //    }
        //    else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_B"))
        //    {
        //        ShowQuizBoard(2);
        //        m_listQuizBoard[2].GetComponent<CAPTQuizManager>().InitQuizType(1, nRealIndex, nIndex);
        //    }
        //    else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_C"))
        //    {
        //        ShowQuizBoard(3);
        //    }
        //    else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_D"))
        //    {
        //        ShowQuizBoard(4);
        //    }
        //    else
        //    {
        //        Debug.Log("알수 없는 타입");
        //    }
        //    return;
        //}

        m_txtQuizTitle.text = "문제 데이터가 존재하지 않음";
        Debug.Log("해당데이터 없음");
    }

    public void UpdateQuizList(int nIndex)
    {
        m_listQuizList[nIndex].GetComponent<CObjectAPTQuizList2>().UpdateAPTQuizList2();
    }

    public void StartQuiz()
    {
        StartCoroutine("ProcessStartQuiz");
    }

    public void StopQuiz()
    {
        StopCoroutine("ProcessStartQuiz");
    }

    IEnumerator ProcessStartQuiz()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if (m_nRemainTime == 0)
                break;
        }

        if( m_nQuizType == 0 )
        {
            CUIsAPTManager.Instance.ShowPopupTimeOverAPTD1();
        } else
        {
            CUIsAPTManager.Instance.ShowPopupTimeOverAPTD2();
        }
    }

    public void OnClickExit()
    {
        CUIsAPTManager.Instance.ShowPopup(2);
        //CUIsSpaceManager.Instance.ScreenActive(false);
        //gameObject.SetActive(false);
    }

    public void OnClickSendAnswer()
    {
        if( m_nQuizType == 0 )
        {
            CUIsAPTManager.Instance.ShowPopupSendAnswerAPTD1();
        } else
        {
            CUIsAPTManager.Instance.ShowPopupSendAnswerAPTD2();
        }
        //CUIsAPTManager.Instance.ShowPopup(0);
    }

    public void SetTutorial(bool bIsTutorial)
    {
        m_bIsTutorial = bIsTutorial;
    }

    public bool IsTutorial()
    {
        return m_bIsTutorial;
    }

    public void SetTutorialWait(bool bIsTutorialWait)
    {
        m_bIsTutorialWait = bIsTutorialWait;
    }

    public bool IsTutorialWait()
    {
        return m_bIsTutorialWait;
    }

    public int GetQuizIndex()
    {
        return m_nQuizIndex;
    }

    public int GetAnswerIndex(int nIndex)
    {
        return m_listAnswerIndex[nIndex];
    }

    public void HideExQuizList()
    {
        m_listQuizList[0].SetActive(false);
    }

    public void ShowQuizBoard(int nIndex)
    {
        for (int i = 0; i < m_listQuizBoard.Length; i++)
            HideQuizBoard(i);

        m_listQuizBoard[nIndex].gameObject.SetActive(true);
    }

    public void HideQuizBoard(int nIndex)
    {
        m_listQuizBoard[nIndex].gameObject.SetActive(false);
    }

    public void ShowTutorialMsg()
    {
        StartCoroutine("ProcessShowTutorialMsg");
    }

    IEnumerator ProcessShowTutorialMsg()
    {
        m_goTutorialMsg.SetActive(true);
        yield return new WaitForSeconds(5f);
        m_goTutorialMsg.SetActive(false);
    }

    public void HideTutorialMsg()
    {
        m_goTutorialMsg.SetActive(false);
    }
}
