using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAPTManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsAPTManager _instance = null;

    public static CUIsAPTManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsAPTManager install null");

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

    public GameObject[] m_listAPTPage = new GameObject[2];

    public GameObject[] m_listPopup = new GameObject[4];

    public GameObject m_goPopupTimeOverAPTD1;
    public GameObject m_goPopupSendAnswerAPTD1;
    public Text m_txtPopupSendAnswerAPTD1RemainTime;
    public GameObject m_goPopupTimeOverAPTD2;
    public GameObject m_goPopupSendAnswerAPTD2;
    public Text m_txtPopupSendAnswerAPTD2RemainTime;
    public GameObject m_goPopupToLobby;
    public Text m_txtPopupToLobbyMsg;
    public Text m_txtPopupToLobbyRemainTime;
    public GameObject m_goPopupOverExit;
    public Text m_txtPopupOverExityMsg;
    public Text m_txtPopupOverExitRemainTime;

    public GameObject m_goPopupToLobbyTutorial;

    private int[] m_listAnswerState = new int[29];

    private int m_nAPT1Cnt = 0;
    private int m_nAPT2Cnt = 0;

    private bool m_bIsTutorial = true;
    private bool m_bIsQuizActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //InitAPTPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTPage()
    {
        HideAllPopup();

        for (int i = 0; i < m_listAnswerState.Length; i++)
        {
            m_listAnswerState[i] = 2;
        }

        ShowAPTPage(0);
    }

    public void InitAnswerState()
    {
        for (int i = 0; i < m_listAnswerState.Length; i++)
        {
            m_listAnswerState[i] = 2;
        }
    }

    public void SetAnswerState(int nIndex, int nState)
    {
        m_listAnswerState[nIndex] = nState;
    }

    public int GetAnswerState(int nIndex)
    {
        return m_listAnswerState[nIndex];
    }

    public int GetFinishAnswerCount()
    {
        int nCount = 0;
        for(int i = 0; i < m_listAnswerState.Length; i++)
        {
            if (GetAnswerState(i) == 0)
                nCount++;
        }
        return nCount;
    }

    public void ShowAPTPage(int nIndex)
    {
        HideAllAPTPage();
        m_listAPTPage[nIndex].SetActive(true);

        if(nIndex == 0)
        {
            m_listAPTPage[nIndex].GetComponent<CUIsAPTPage1Manager>().InitAPTPage();
        } else if (nIndex == 1)
        {
            if(!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING") || CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("TAE"))
                {
                    m_listAPTPage[nIndex].GetComponent<CUIsAPTPage2Manager>().InitAPTPage2();
                }
                else
                {
                    m_listAPTPage[nIndex].GetComponent<CUIsAPTPage2Manager>().InitAPTD2();
                }
            } else
            {
                m_listAPTPage[nIndex].GetComponent<CUIsAPTPage2Manager>().InitAPTPage2();
            }
        }
    }

    public void HideAllAPTPage()
    {
        for(int i = 0; i < m_listAPTPage.Length; i++)
        {
            m_listAPTPage[i].SetActive(false);
        }
    }

    public int GetAPTCnt(int nType)
    {
        string strKey = "APTD1";
        if (nType == 1)
            strKey = "APTD2";
        Quiz quizAPT = CQuizData.Instance.GetQuiz(strKey);

        return quizAPT.sets.Length;


    }

    public void ShowPage()
    {
        m_listAPTPage[0].SetActive(true);
        this.gameObject.transform.localPosition = new Vector3(0, 0, 0f);
    }

    public void HideAgreePage()
    {
        m_listAPTPage[0].SetActive(false);
    }

    public void HidePage()
    {
        SetQuizActive(false);
        this.gameObject.transform.localPosition = new Vector3(999f, 999f, 999f);
    }

    public void SetQuizActive(bool bIsActive)
    {
        m_bIsQuizActive = bIsActive;
    }

    public bool IsQuizActive()
    {
        return m_bIsQuizActive;
    }

    // Popup ---------------------------------------------------
    public void HideAllPopup()
    {
        HidePopupTimeOverAPTD1();
        HidePopupSendAnswerAPTD1();
        HidePopupToLobbyTutorial();
        for (int i = 0; i < m_listPopup.Length; i++)
            HidePopup(i);
    }

    public void ShowPopup(int nIndex)
    {
        m_listPopup[nIndex].SetActive(true);
    }

    public void HidePopup(int nIndex)
    {
        m_listPopup[nIndex].SetActive(false);
    }

    public void ShowPopupTimeOverAPTD1()
    {
        HideAllPopup();
        m_goPopupTimeOverAPTD1.SetActive(true);
    }

    public void HidePopupTimeOverAPTD1()
    {
        m_goPopupTimeOverAPTD1.SetActive(false);
    }

    public void OnClickPopupTimeOverAPTD1Next()
    {
        Debug.Log("OnClickPopupTimeoverAPTD1Next");

        FinishAPTD1();
        HidePopupTimeOverAPTD1();
    }

    public void FinishAPTD1()
    {
        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            Server.Instance.RequestPUTQuestionsStatus(CQuizData.Instance.GetExamInfoDetail("APTD1").idx, 1);
            if (CQuizData.Instance.GetExamInfoDetail("APTD2").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
            }
        }

        m_listAPTPage[1].GetComponent<CUIsAPTPage2Manager>().StopQuiz();
        m_listAPTPage[1].GetComponent<CUIsAPTPage2Manager>().InitAPTD2();
    }


    public void ShowPopupSendAnswerAPTD1()
    {
        m_goPopupSendAnswerAPTD1.SetActive(true);

        StartCoroutine("ProcessSendAnswerAPTD1RemainTime");
    }

    IEnumerator ProcessSendAnswerAPTD1RemainTime()
    {
        while (true)
        {
            int nRemainTime = CUIsAPTPage2Manager.Instance.GetRemainTime();
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            m_txtPopupSendAnswerAPTD1RemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupSendAnswerAPTD1()
    {
        StopCoroutine("ProcessSendAnswerAPTD1RemainTime");
        m_goPopupSendAnswerAPTD1.SetActive(false);
    }

    public void OnClickPopupSendAnswerAPTD1Exit()
    {
        HidePopupSendAnswerAPTD1();
    }

    public void OnClickPopupSendAnswerAPTD1Send()
    {
        FinishAPTD1();
        HidePopupSendAnswerAPTD1();
    }


    // --------------------------------------------------------------------

    public void ShowPopupTimeOverAPTD2()
    {
        HideAllPopup();
        m_goPopupTimeOverAPTD2.SetActive(true);
    }

    public void HidePopupTimeOverAPTD2()
    {
        m_goPopupTimeOverAPTD2.SetActive(false);
    }

    public void OnClickPopupTimeOverAPTD2Finish()
    {
        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("APTD2").part_idx, 3);
        CUIsSpaceManager.Instance.HideRightPage();
    }

    public void ShowPopupSendAnswerAPTD2()
    {
        m_goPopupSendAnswerAPTD2.SetActive(true);
        StartCoroutine("ProcessSendAnswerAPTD2RemainTime");
    }

    IEnumerator ProcessSendAnswerAPTD2RemainTime()
    {
        while (true)
        {
            int nRemainTime = CUIsAPTPage2Manager.Instance.GetRemainTime();
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            m_txtPopupSendAnswerAPTD2RemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupSendAnswerAPTD2()
    {
        StopCoroutine("ProcessSendAnswerAPTD2RemainTime");
        m_goPopupSendAnswerAPTD2.SetActive(false);
    }

    public void OnClickPopupSendAnswerAPTD2Exit()
    {
        HidePopupSendAnswerAPTD2();
    }

    public void OnClickPopupSendAnswerAPTD2Send()
    {
        
        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("APTD2").part_idx, 3);
        CUIsSpaceManager.Instance.HideRightPage();
    }



    // ----------------------------------------------------------
    public void ShowPopupToLobby()
    {
        if (CQuizData.Instance.GetEnableExitCount() > 0)
        {
            m_goPopupToLobby.SetActive(true);
            m_txtPopupToLobbyMsg.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";
        }
        else
        {
            m_goPopupOverExit.SetActive(true);
            m_txtPopupOverExityMsg.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";
        }

        StartCoroutine("ProcessToLobbyRemainTime");
    }

    IEnumerator ProcessToLobbyRemainTime()
    {
        while(true)
        {
            int nRemainTime = CUIsAPTPage2Manager.Instance.GetRemainTime();
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            if( m_goPopupToLobby.activeSelf )
                m_txtPopupToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if ( m_goPopupOverExit.activeSelf)
                m_txtPopupOverExitRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        m_goPopupToLobby.SetActive(false);
        m_goPopupOverExit.SetActive(false);
    }

    public void OnClickPopupToLobbyToLobby()
    {
        Server.Instance.RequestPUTActionExit();
        HideAllPopup();
        //CUIsSpaceManager.Instance.ScreenActive(false);
        //gameObject.SetActive(false);

        HidePage();
    }

    public void OnClickPopupToLobbyExit()
    {
        HidePopupToLobby();
    }

    //------------------------------------------------------------


    public void OnClickPopupSendAnswerSend()
    {
        HideAllPopup();
        CUIsSpaceManager.Instance.ScreenActive(false);
        gameObject.SetActive(false);
    }
    public void OnClickPopupSendAnswerExit()
    {
        HideAllPopup();
    }

    // Popup ToLobby Tutorail ------------------------------------
    public void ShowPopupToLobbyTutorial()
    {
        m_goPopupToLobbyTutorial.SetActive(true);
    }

    public void HidePopupToLobbyTutorial()
    {
        m_goPopupToLobbyTutorial.SetActive(false);
    }

    public void OnClickPopupToLobbyTutorialToLobby()
    {
        HideAllPopup();
        HidePage();
    }

    public void OnClickPopupToLobbyTutorialExit()
    {
        HidePopupToLobbyTutorial();
    }

    //-----------------------------------------------
}
