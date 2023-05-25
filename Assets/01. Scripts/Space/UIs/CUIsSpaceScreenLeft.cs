using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

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

    public GameObject m_goBG;

    public GameObject[] m_listPage = new GameObject[2];
    public GameObject[] m_listToturialPage = new GameObject[2];
    public GameObject[] m_listMissionClear = new GameObject[2];

    public Toggle[] m_toggleAgree = new Toggle[2];
    public GameObject[] m_listBtnPlay = new GameObject[2];

    public GameObject m_goCSTPage;
    public GameObject m_goRATPage;
    public GameObject m_goHPTSPage;

    public Text[] m_listQuizCount = new Text[4];
    public Text[] m_listExmTime = new Text[4];

    public GameObject m_goTalkContents;

    public GameObject[] m_listQuiz = new GameObject[6];

    public GameObject m_goPopupFinish;
    
    public GameObject m_goPopupExit;

    public GameObject m_goScrollView;
    public GameObject m_goContents;

    public Text m_txtRemain;

    public GameObject m_goPoupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyRemainTime;
    public Text m_txtToLobbyRemainCnt;

    public GameObject m_goPopupToLobbyOver;
    public Text m_txtToLobbyOverRemainTime;
    public Text m_txtToLobbyOverRemainCnt;

    public GameObject m_goPopupToLobbyTutorial;

    private int m_nOpenQuiz;

    private int m_nRemainTime = 30;

    private float m_fScrollViewHeight = 0;

    private int m_nLastQuizIndex = 0;

    private bool m_bIsRQTTutorial = true;

    private bool m_bIsCSTTutorial = true;
    private bool m_bIsRATTutorial = true;
    private bool m_bIsHPTSTutorial = true;

    private int m_nRQTExitCount = 0;
    private int m_nRQTMaxExitCount = 5;

    private bool m_bIsLeftQuizActive = true;
    private bool m_bIsRightQuizActive = true;


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

        UpdateButtonPlay();

        if (CSpaceAppEngine.Instance.IsFinishLeft01()) ShowMissionClear(0);
        else HideMissionClear(0);

        if (CSpaceAppEngine.Instance.IsFinishLeft02()) ShowMissionClear(1);
        else HideMissionClear(1);

        m_goBG.SetActive(true);

        SetCSTTutorial(true);

        //gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-443f, -224f, 0);
        //gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 0);
        //gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 0, 0), CSpaceAppEngine.Instance.GetFadeTime());
        //gameObject.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), CSpaceAppEngine.Instance.GetFadeTime());

        ShowPage(0);
    }

    public void SetLeftQuizActive(bool bActive)
    {
        m_bIsLeftQuizActive = bActive;
    }

    public bool IsLeftQuizActive()
    {
        return m_bIsLeftQuizActive;
    }

    public void SetRightQuizActive(bool bActive)
    {
        m_bIsRightQuizActive = bActive;
    }

    public bool IsRightQuizActive()
    {
        return m_bIsRightQuizActive;
    }

    public void OnClickAgreeClose()
    {
        HideAllPopup();
        //PageFadeOut();
        HideAllPages();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }

    public void OnClickClose()
    {
        //CUIsSpaceManager.Instance.HideLeftPage();
        if (IsRQTTutorial())
        {
            //HideAllPopup();
            //HideAllPages();
            //CUIsSpaceManager.Instance.ScreenActive(false);
            ShowPopupToLobbyTutorial();
            return;
        }

        if (CQuizData.Instance.GetEnableExitCount() > 0)
            ShowPopupToLobby();
        else
            ShowPopupToLobbyOver();
    }

    public void InitRQTQuiz()
    {
        ShowPage(1, true);
        HideAllQuiz();
    }

    public void InitCSTQuiz()
    {
        HideAllPages();
        m_goBG.SetActive(true);
        ShowCSTPage();
    }

    public void InitRATQuiz()
    {
        HideAllPages();
        m_goBG.SetActive(true);
        ShowRATPage();
    }

    public void InitHPTSQuiz()
    {
        HideAllPages();
        m_goBG.SetActive(true);
        ShowHPTSPage();
    }

    public void OnClickMission(int nIndex)
    {
        string strKey = "RQT";
        if (nIndex == 1)
            strKey = "CST";

        if (!m_toggleAgree[nIndex].isOn)
            return;

        //Quiz quizData = CQuizData.Instance.GetQuiz(strKey);
        //Server.Instance.RequestPOSTQuestions(quizData.part_idx);
        //Server.Instance.RequestPOSTPartJoin(quizData.part_idx);
        //ShowPopupFinish();

        // 상태값 API 호출 -----------------------------
        if (nIndex == 0)
        {
            if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("WAITING"))
                {
                    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
                } else
                {
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RQT").idx, true);
                }
            } else
            {
                ShowPage(1, true);
                HideAllQuiz();
            }
        }
        else
        {
            CUIsSpaceScreenLeft.Instance.SetRightQuizActive(true);
            if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
                {
                    Debug.Log("CST WAITING");
                    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                } else if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("TAE"))
                {
                    Debug.Log("CST TAE");
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx, true);
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                } else if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("TAE"))
                {
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx, true);
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                } else if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("TAE"))
                {
                    Debug.Log("HPTS TAE");
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx, true);
                }

                    //if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
                    //{
                    //    Debug.Log("RAT WAITING");
                    //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                    //}
                    //else if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("TAE"))
                    //{
                    //    Debug.Log("RAT TAE");
                    //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                    //}

                    //if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
                    //{
                    //    Debug.Log("HPTS WAITING");
                    //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                    //}
                    //else if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("TAE"))
                    //{
                    //    Debug.Log("HPTS TAE");
                    //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                    //}

                    //HideAllPages();
                //if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("TAE") || CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
                //{
                //    ShowCSTPage();
                //} else if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("TAE") || CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
                //{
                //    ShowRATPage();
                //} else
                //{
                //    ShowHPTSPage();
                //}
            } else
            {
                HideAllPages();
                m_goBG.SetActive(true);
                ShowCSTPage();
            }

            // TODO 230216

            //if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
            //{
            //    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
            //}

            //if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
            //{
            //    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            //}

        }


        //ShowQuiz(nIndex);

        //InitQuiz();
    }

    public void OnChangeToggle(int nIndex)
    {
        UpdateButtonPlay();
    }

    public void UpdateButtonPlay()
    {
        for(int i = 0; i < m_listBtnPlay.Length; i++)
        {
            if (m_toggleAgree[i].isOn)
            {
                m_listBtnPlay[i].GetComponent<Button>().enabled = true;
                m_listBtnPlay[i].GetComponent<Image>().color = new Color(0, 0.5215687f, 1f);
            }
            else
            {
                m_listBtnPlay[i].GetComponent<Button>().enabled = false;
                m_listBtnPlay[i].GetComponent<Image>().color = new Color(0.7372549f, 0.8431373f, 1f);
            }
        }        
    }

    public void InitQuiz()
    {
        m_nRemainTime = 30;

        StartCoroutine("ProcessQuiz");
    }

    IEnumerator ProcessQuiz()
    {
        int nRequestTimer = 0;
        m_txtRemain.text = "00:" + m_nRemainTime.ToString("00");
        while(true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;
            m_txtRemain.text = "00:" + m_nRemainTime.ToString("00");

            if ((nRequestTimer % 5) == 0)
            {
                Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("CST").part_idx);
            }
            nRequestTimer++;

            if (m_nRemainTime <= 0)
                break;
        }

        Debug.Log("TimeOut");
        CUIsSpaceScreenLeft.Instance.HideAllPopup();
        CUIsSpaceScreenLeft.Instance.ShowPopupTimeover();

    }

    public void UpdateScrollView()
    {
        //Debug.Log(m_fScrollViewHeight);

        StartCoroutine("ProcessUpdateScrollView");
    }

    IEnumerator ProcessUpdateScrollView()
    {
        //yield return new WaitForSeconds(0.1f);

        //if (m_goContents.GetComponent<RectTransform>().sizeDelta.y > m_fScrollViewHeight)
        //{
        //    Vector3 vecPoz = m_goContents.transform.localPosition;
        //    vecPoz.y = (m_goContents.GetComponent<RectTransform>().sizeDelta.y - m_fScrollViewHeight);
        //    m_goContents.transform.localPosition = vecPoz;
        //}

        Debug.Log("ProcessUpdateScrollView");
        yield return new WaitForEndOfFrame();
         
        m_goScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    public void OnClickFinish()
    {
        ShowPopupFinish();
    }

    public void HideAllPages()
    {
        m_goBG.SetActive(false);
        for (int i = 0; i < m_listPage.Length; i++)
        {
            HidePage(i);
        }
    }

    public void ShowPage(int nPage, bool bTutorial = false)
    {
        HideAllPages();
        m_goBG.SetActive(true);
        m_listPage[nPage].SetActive(true);

        if (nPage == 0)
        {
            //Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT");
            //Quiz quizRQT = CQuizData.Instance.GetRQT().body;
            //m_listQuizCount[0].text = quizRQT.sets.Length.ToString() + " 문항";
            if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                m_listQuizCount[0].text = "150 문항";
                m_listExmTime[0].text = (CQuizData.Instance.GetExamInfoDetail("RQT").examTime / 60).ToString() + " 분";
                //Debug.Log("RQT Exm Time : " + quizRQT.exm_time);

                // TODO
                //m_listQuizCount[1].text = CQuizData.Instance.GetQuizTotalCount("CST").ToString() + " 문항";
                //m_listQuizCount[1].text = CQuizData.Instance.GetQuiz("CST").sets.Length.ToString()  + " 문항";
                m_listQuizCount[1].text = "1 문항";
                m_listExmTime[1].text = (CQuizData.Instance.GetExamInfoDetail("CST").examTime / 60).ToString() + " 분";
                //m_listQuizCount[2].text = CQuizData.Instance.GetQuiz("RAT").sets.Length.ToString() + " 문항";
                m_listQuizCount[2].text = "2 문항";
                m_listExmTime[2].text = (CQuizData.Instance.GetExamInfoDetail("RAT").examTime / 60).ToString() + " 분";
                //m_listQuizCount[3].text = CQuizData.Instance.GetQuiz("HPTS").sets.Length.ToString() + " 문항";
                m_listQuizCount[3].text = "2 문항";
                m_listExmTime[3].text = (CQuizData.Instance.GetExamInfoDetail("HPTS").examTime / 60).ToString() + " 분";
            }
        }
        else if (nPage == 1)
        {
            DelQuiz();

            if (CQuizData.Instance.GetQuiz("RQT").sets[0].questions[0].test_answers[0].test_anwr_idx != 0)
            {
                Debug.Log("InitRQT 01");
                CUIsSpaceScreenLeft.Instance.SetRQTTutorial(false);

                int nLastQuizIndex = 0;
                for (int i = 0; i < CQuizData.Instance.GetQuiz("RQT").sets.Length; i++)
                {
                    Debug.Log("InitRQT 02");
                    if (CQuizData.Instance.GetQuiz("RQT").sets[i].questions[0].test_answers[0].test_anwr_idx == 0)
                    {
                        Debug.Log("InitRQT 03");
                        nLastQuizIndex = i;
                        break;
                    }
                }

                Debug.Log("InitRQT 04");

                CUIsSpaceScreenLeft.Instance.InitRQTQuiz(false);
                CUIsSpaceScreenLeft.Instance.ShowQuiz(0, nLastQuizIndex, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
            }
            else
            {
                Debug.Log("InitRQT 05");
                CUIsSpaceScreenLeft.Instance.ShowQuiz(0, 0, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
            }

            //Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
            //Quiz quizRQT = CQuizData.Instance.GetRQT().body;
            //if (bTutorial)            {
            if( IsRQTTutorial()) { 
                Debug.Log("InitRQT 06");
                m_txtRemain.text = "시작 전";
            }
            //if (bTutorial )
            //{
            //    for (int i = 0; i < quizRQT.sets.Length; i++)
            //    {
            //        GameObject goTalk = Instantiate(Resources.Load("Prefabs/talk_list") as GameObject);
            //        goTalk.transform.parent = m_goTalkContents.transform;
            //        goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
            //    }
            //}
            //else
            //{
                //GameObject goTalk = Instantiate(Resources.Load("Prefabs/talk_list") as GameObject);
                //goTalk.transform.parent = m_goTalkContents.transform;
                //// TODO
                //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(0, "test", true);
            //}
            
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
        if (nIndex == 0)
        {
            SetLastQuizIndex(0);
            DelQuiz();
        }


        //Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);
        GameObject goTalk = Instantiate(Resources.Load("Prefabs/quizContent") as GameObject);
        goTalk.transform.parent = m_goContents.transform;
        Debug.Log("Show Quiz : " + bTutorial.ToString());
        goTalk.GetComponent<CUIsTalk>().InitUIs(nSetIndex, nIndex, bTutorial);

        UpdateScrollView();


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
        //HidePopupFinish();
        

        HidePopupSendAnswer();
        HidePopupTimeover();
        HidePopupToLobby();
        HidePopupToLobbyOver();
        HidePopupToLobbyTutorial();
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



    public void InitRQTQuiz(bool bIsTutorial)
    {
        SetRQTTutorial(bIsTutorial);
        SetLastQuizIndex(0);
        if (!bIsTutorial)
        {
            if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                Quiz quizData = CQuizData.Instance.GetQuiz("RQT");
                m_nRemainTime = quizData.exm_time;
                //m_nRemainTime = 60;
            } else
            {
                Quiz quizRQT = CQuizData.Instance.GetRQT().body;
                //m_nRemainTime = quizRQT.exm_time;               
                m_nRemainTime = quizRQT.progress_time;
            }
            StartCoroutine("ProcessRQTQuiz");
        } 
    }

    IEnumerator ProcessRQTQuiz()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        //Debug.Log("ProcessRQTQuiz 01 ReaminTime : " + m_nRemainTime);

        m_txtRemain.text = nMin.ToString("00") + ":" +  nSec.ToString("00");
        m_txtRemain.color = new Color(0, 0.5215687f, 1f);
        int m_nRemainTimeState = 0;

        int nRequestTimer = 0;
       
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            //Debug.Log("ProcessRQTQuiz 02 ReaminTime : " + m_nRemainTime);

            if (m_nRemainTimeState == 0)
            {
                if( m_nRemainTime <= 60 * 5 )
                {
                    m_nRemainTimeState = 1;
                    m_txtRemain.color = new Color(1f, 0.6588235f, 0);
                }
            } else if(m_nRemainTimeState == 1)
            {
                if (m_nRemainTime <= 60 * 1)
                {
                    m_nRemainTimeState = 2;
                    m_txtRemain.color = new Color(1f, 0, 0);
                }
            } 

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if ((nRequestTimer % 5) == 0)
            {
                Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("RQT").part_idx);
            }
            nRequestTimer++;

            if (m_nRemainTime <= 0)
                break;
        }

        Debug.Log("TimeOut");


        HideAllPopup();
        ShowPopupTimeover();
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

    // Popup Send Answer -------------------------------------------
    public void ShowPopupSendAnswer()
    {
        m_goPoupSendAnswer.SetActive(true);

        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    }

    public void HidePopupSendAnswer()
    {
        m_goPoupSendAnswer.SetActive(false);
    }

    public void OnClickSendAnswer()
    {
        StopCoroutine("ProcessRQTQuiz");
        HideAllPopup();
        //PageFadeOut();
        HideAllPages();
        CUIsSpaceManager.Instance.FadeOutComputer();
        //CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void OnClickSendAnswerContinue()
    {
        HidePopupSendAnswer();
    }
    // -------------------------------------------------------------

    // Popup Time Out -------------------------------------------
    public void ShowPopupTimeover()
    {
        Debug.Log("ShowPopupTimeover");
        m_goPopupTimeover.SetActive(true);
    }

    public void HidePopupTimeover()
    {
        m_goPopupTimeover.SetActive(false);
    }

    public void OnClickTimeoverLobby()
    {
        StopCoroutine("ProcessRQTQuiz");
        HideAllPopup();
        //PageFadeOut();
        HideAllPages();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("RQT").part_idx, 0);
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }
    // -------------------------------------------------------------

    // Popup To Lobby -------------------------------------------
    public void ShowPopupToLobby()
    {
        m_goPopupToLobby.SetActive(true);

        m_txtToLobbyRemainCnt.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";

        StartCoroutine("ProcessToLobbyRemainTime");
    }

    IEnumerator ProcessToLobbyRemainTime()
    {
        while(true)
        {
            int nMin = (int)(m_nRemainTime / 60);
            int nSec = (int)(m_nRemainTime % 60);

            //m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        m_goPopupToLobby.SetActive(false);
    }

    public void OnClickToLobby()
    {
        Server.Instance.RequestPUTActionExit();
        StopCoroutine("ProcessRQTQuiz");
        HideAllPopup();
        //PageFadeOut();
        HideAllPages();
        CUIsSpaceManager.Instance.FadeOutComputer();
        //CUIsSpaceManager.Instance.ScreenActive(false);

    }

    public void OnClickToLobbyContinue()
    {
        HideAllPopup();
    }
    // -------------------------------------------------------------

    // Popup To Lobby Over -------------------------------------------
    public void ShowPopupToLobbyOver()
    {
        m_goPopupToLobbyOver.SetActive(true);

        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        //m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        m_txtToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

        m_txtToLobbyOverRemainCnt.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";

        StartCoroutine("ProcessToLobbyOverRemainTime");

    }

    IEnumerator ProcessToLobbyOverRemainTime()
    {
        while (true)
        {
            int nMin = (int)(m_nRemainTime / 60);
            int nSec = (int)(m_nRemainTime % 60);

            //m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            m_txtToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupToLobbyOver()
    {
        StopCoroutine("ProcessToLobbyOverRemainTime");
        m_goPopupToLobbyOver.SetActive(false);
    }

    public void OnClickToLobbyOverContinue()
    {
        HideAllPopup();
    }
    // -------------------------------------------------------------

    // CST -------------------------------------------------------------
    public void HideRightAllPage()
    {
        m_goBG.SetActive(false);
        HideCSTPage();
        HideRATPage();
        HideHPTSPage();
    }


    public void ShowCSTPage()
    {
        m_goCSTPage.SetActive(true);
        m_goCSTPage.GetComponent<CUIsCSTPage2Manager>().InitCSTPage2();
    }

    public void HideCSTPage()
    {
        m_goCSTPage.SetActive(false);
    }

    public void OnClickPlayCST()
    {
        HideAllPages();
        // TODO : Text
        ShowCSTPage();
        //ShowRATPage();
        //ShowHPTSPage();
    }

    public void SetCSTTutorial(bool bIsTutorial)
    {
        m_bIsCSTTutorial = bIsTutorial;
    }    

    public bool IsCSTTutorial()
    {
        return m_bIsCSTTutorial;
    }

    // RAT -----------------------------------------------------
    public void ShowRATPage()
    {
        m_goRATPage.SetActive(true);
        m_goRATPage.GetComponent<CUIsRATManager>().InitRATPage();
        //m_goRATPage.GetComponent<CUIsRATManager>().PlayExam();
    }

    public void HideRATPage()
    {
        m_goRATPage.SetActive(false);
    }

    public void SetRATTutorial(bool bIsTutorial)
    {
        m_bIsRATTutorial = bIsTutorial;
    }

    public bool IsRATTutorial()
    {
        return m_bIsRATTutorial;
    }

    // HPTS -----------------------------------------------------
    public void ShowHPTSPage()
    {
        m_goHPTSPage.SetActive(true);
        m_goHPTSPage.GetComponent<CUIsHPTSManager>().InitHPTSPage();
        //InitHPTSPage();
    }

    public void HideHPTSPage()
    {
        m_goHPTSPage.SetActive(false);
    }

    public void SetHPTSTutorial(bool bIsTutorial)
    {
        m_bIsHPTSTutorial = bIsTutorial;
    }

    public bool IsHPTSTutorial()
    {
        return m_bIsHPTSTutorial;
    }

    // ------------------------------------------------------

    public void ShowMissionClear(int nIndex)
    {
        m_listMissionClear[nIndex].SetActive(true);
    }

    public void HideMissionClear(int nIndex)
    {
        m_listMissionClear[nIndex].SetActive(false);
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
        //PageFadeOut();
        HideAllPages();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }

    public void OnClickPopupToLobbyTutorialExit()
    {
        HidePopupToLobbyTutorial();
    }

    //-----------------------------------------------

    public void PageFadeOut()
    {
        Debug.Log("PageFadeOut");
        //gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-443f, -224f, 0);
        //gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 0);
        gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(-443, -224, 0), CSpaceAppEngine.Instance.GetFadeTime());
        gameObject.GetComponent<RectTransform>().DOScale(new Vector3(0.2f, 0.2f, 1), CSpaceAppEngine.Instance.GetFadeTime()).OnComplete(HideAllPages);
    }

    public void PageFadeOutRightPage()
    {
        gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(-443, -224, 0), CSpaceAppEngine.Instance.GetFadeTime());
        gameObject.GetComponent<RectTransform>().DOScale(new Vector3(0.2f, 0.2f, 1), CSpaceAppEngine.Instance.GetFadeTime()).OnComplete(HideRightAllPage);
    }


}

