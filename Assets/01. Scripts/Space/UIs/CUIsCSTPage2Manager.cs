using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsCSTPage2Manager : MonoBehaviour
{
    #region SingleTon
    public static CUIsCSTPage2Manager _instance = null;

    public static CUIsCSTPage2Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsCSTPage2Manager install null");

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

    public GameObject m_goTutorialMsg;

    public Text m_txtSendAnswer;
    public Text m_txtRemainTime;

    public GameObject m_goLeftContent;
    public GameObject m_goRightContent;

    public Text m_txtMission;
    public Text m_txtMissionContent;
    public Text m_txtLeftContent;
    public Text m_txtRightContent;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyMsg;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;
    public Text m_txtToLobbyOverMsg;
    public Text m_txtToLobbyOverRemainTime;

    public GameObject[] m_listLeftContents = new GameObject[25];
    public GameObject[] m_listRightContents = new GameObject[25];

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    //private List<InputField> m_listInputField;
    private List<TMPro.TMP_InputField> m_listInputFieldTmp;
    private int m_nCurPos;

    // Start is called before the first frame update
    void Start()
    {
        InitCSTPage2();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            MovePrev();
        } else if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveNext();
        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            MoveNext();
        }
    }

    public void MovePrev()
    {
        GetCurrentPos();
        //Debug.Log("Move Prev : " + GetCurrentPos());
        if( m_nCurPos > 0 )
        {
            --m_nCurPos;

            //m_listInputField[m_nCurPos].Select();

            m_listInputFieldTmp[m_nCurPos].Select();
        }
    }

    public void MoveNext()
    {
        GetCurrentPos();
        //Debug.Log("Move Prev : " + GetCurrentPos());
        //if (m_nCurPos < m_listInputField.Count - 1)
        //{
        //    if (!m_listInputField[m_nCurPos + 1].IsInteractable())
        //        return;
        //    ++m_nCurPos;

        //    m_listInputField[m_nCurPos].Select();
        //}
        if (m_nCurPos < m_listInputFieldTmp.Count - 1)
        {
            if (!m_listInputFieldTmp[m_nCurPos + 1].IsInteractable())
                return;
            ++m_nCurPos;

            m_listInputFieldTmp[m_nCurPos].Select();
        }

    }

    private int GetCurrentPos()
    {
        //for (int i = 0; i < m_listInputField.Count; ++i)
        //{
        //    if(m_listInputField[i].isFocused == true)
        //    {
        //        m_nCurPos = i;
        //        break;
        //    }
        //}

        for (int i = 0; i < m_listInputFieldTmp.Count; ++i)
        {
            if (m_listInputFieldTmp[i].isFocused == true)
            {
                m_nCurPos = i;
                break;
            }
        }

        return m_nCurPos;
    }

    public void InitCSTPage2()
    {
        m_goTutorialMsg.SetActive(false);

        m_nCurPos = 0;

        m_listInputFieldTmp = new List<TMPro.TMP_InputField>();
        m_listInputFieldTmp.Clear();

        if ( m_listInputFieldTmp.Count == 0 )
        {
            Debug.Log("InitCSTPage2 InputFiled Init");
            for(int i = 0; i < 25; i++)
            {
                m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().InitListAnswer(0, i);
                m_listInputFieldTmp.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().m_ifAnswer);

                m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().InitListAnswer(1, i);
                m_listInputFieldTmp.Add(m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().m_ifAnswer);
            }
            m_listLeftContents[0].GetComponent<CUIsCSTListAnswerTmp>().ActiveInputField();
            m_listInputFieldTmp[0].Select();
        }

        DelListAnswers();
        HideAllPopup();

        Debug.Log("InitCSTPage2 01");

        Quiz quizData = CQuizData.Instance.GetQuiz("CST");

        if( quizData.exm_time != quizData.progress_time)
        {
            CUIsSpaceScreenLeft.Instance.SetCSTTutorial(false);
        }


        if ( CUIsSpaceScreenLeft.Instance.IsCSTTutorial() )
        {
            Debug.Log("InitCSTPage2 02");
            m_txtSendAnswer.text = "본 퀴즈 시작하기";
            m_txtRemainTime.text = "시작전";
            m_txtMission.text = CQuizData.Instance.GetUserName() + "님,  플레이샵에 오신 것을 환영합니다! 첫 번째 라운드는 사고 유연성 테스트입니다. 안내문 내용을 참고하시어 아래 빈 칸에 알맞은 단어를 작성해 주세요.본 퀴즈는 연습이오니 부담 갖지 마시고 충분히 작성하셔도 됩니다.";
            m_txtLeftContent.text = "음식";
            m_txtRightContent.text = "사무용품";

            m_txtMissionContent.text = "‘음식’ 범주에는 일반적으로 사람이 먹거나 마실 수 있는 모든 것(김치, 사과 등)을 작성해 주세요.\n-\n사무용품 범주에는 문구류, 사무기기, 종이류 등을 포함한 물품(연필, 스테인플러 등)을 작성해 주세요.입니다.\n-\n각 범주에 속하는 단어를 번갈아가며 순서대로 입력해 주세요.\n-\n최대한 빠르고 오탈자 없이 입력해 주세요.\n-\n부정 행위를 하실 경우 불이익이 생길 수 있습니다.";
        } else
        {
            Debug.Log("InitCSTPage2 03");
            m_txtSendAnswer.text = "답변 제출하기";


            
            //m_txtMission.text = "과일, 가구에 속하는 단어를 번갈아 가며 최대한 많이 작성해 주시기 바랍니다.";
            //Quiz quizData = CQuizData.Instance.GetQuiz("CST");

            Debug.Log("InitCSTPage2 03 - 01 : " + quizData.sets[0].dir_cnnt);
            Debug.Log("InitCSTPage2 03 - 00 : " + quizData.progress_time);

            m_txtMission.text = quizData.sets[0].dir_cnnt;
            //m_txtMissionContent.text = quizData.sets[0].dir_cnnt;
            m_txtMissionContent.text = quizData.sets[0].qst_brws_cnnt;
            //m_nRemainTime = quizData.exm_time;
            m_nRemainTime = quizData.progress_time;
            m_txtLeftContent.text = quizData.sets[0].questions[0].qst_cnnt;
            m_txtRightContent.text = quizData.sets[1].questions[0].qst_cnnt;

            StartCoroutine("ProcessPlayExam");
        }

        Debug.Log("InitCSTPage2 04");

        /*

        //int nSelectableIdx = 0;
        //selectables = new Selectable[50];

        //m_listInputField = new List<InputField>();
        //m_listInputField.Clear();
        m_listInputFieldTmp = new List<TMPro.TMP_InputField>();
        m_listInputFieldTmp.Clear();
        m_nCurPos = 0;

        Debug.Log("InitCSTPage2 05");

        for (int i = 0; i < 25; i++)
        {
            //m_listLeftContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            //m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(0, i);
            //m_listInputField.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().m_ifAnswer);
            //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);

            //m_listRightContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            //m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(1, i);
            //m_listInputField.Add(m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().m_ifAnswer);


            m_listLeftContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswerTmp") as GameObject);
            Debug.Log("InitCSTPage2 06 - 02");
            m_listLeftContents[i].transform.parent = m_goLeftContent.transform;
            Debug.Log("InitCSTPage2 06 - 03");
            m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().InitListAnswer(0, i);
            Debug.Log("InitCSTPage2 06 - 04");
            m_listInputFieldTmp.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().m_ifAnswer);
            Debug.Log("InitCSTPage2 06 - 05");

            m_listRightContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswerTmp") as GameObject);
            Debug.Log("InitCSTPage2 06 - 06");
            m_listRightContents[i].transform.parent = m_goRightContent.transform;
            Debug.Log("InitCSTPage2 06 - 07");
            m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().InitListAnswer(1, i);
            Debug.Log("InitCSTPage2 06 - 08");
            m_listInputFieldTmp.Add(m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().m_ifAnswer);
            Debug.Log("InitCSTPage2 06 - 09");
        }

        Debug.Log("InitCSTPage2 07");

        //m_listLeftContents[0].GetComponent<CUIsCSTListAnswer>().ActiveInputField();
        m_listLeftContents[0].GetComponent<CUIsCSTListAnswerTmp>().ActiveInputField();

        Debug.Log("InitCSTPage2 08");
        //m_listInputField[0].Select();
        m_listInputFieldTmp[0].Select();

        Debug.Log("InitCSTPage2 09");

        Vector2 vecSize = m_goLeftContent.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = 1120;
        m_goLeftContent.GetComponent<RectTransform>().sizeDelta = vecSize;

        Debug.Log("InitCSTPage2 10");

        //for (int i = 0; i < 25; i++)
        //{
        //    m_listRightContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
        //    m_listRightContents[i].transform.parent = m_goRightContent.transform;
        //    m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(1, i);
        //    //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
        //}

        vecSize = m_goRightContent.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = 1120;
        m_goRightContent.GetComponent<RectTransform>().sizeDelta = vecSize;

        Debug.Log("InitCSTPage2 11");
        */
    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        int nRequestTimer = 0;

        m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if ((nRequestTimer % 5) == 0)
            {
                Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("CST").part_idx);
            }
            nRequestTimer++;


            if (m_nRemainTime <= 0)
                break;
        }

        for (int i = 0; i < 25; i++)
        {
            //m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().DisableInputField();
            //m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().DisableInputField();

            m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().DisableInputField();
            m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().DisableInputField();

        }
        ShowPopupTimeOver();
    }

    public void OnClickSendAnswer()
    {
        if(CUIsSpaceScreenLeft.Instance.IsCSTTutorial())
        {
            if( m_nTutorialStep == 0)
            {
                m_nTutorialStep++;
                ShowTutorialMsg();
            } else
            {
                CUIsSpaceScreenLeft.Instance.SetCSTTutorial(false);
                InitCSTPage2();
            }
            return;
        }

        ShowPopupSendAnswer();
    }

    public void ShowTutorialMsg()
    {
        //m_goTutorialMsg.SetActive(true);
        StartCoroutine("ProcessTutorialMsg");
    }

    IEnumerator ProcessTutorialMsg()
    {
        m_goTutorialMsg.SetActive(true);
        yield return new WaitForSeconds(5f);
        HideTutorialMsg();
    }

    public void HideTutorialMsg()
    {
        m_goTutorialMsg.SetActive(false);
    }

    public void DelListAnswers()
    {
        //Component[] listChilds = m_goLeftContent.GetComponentsInChildren<Component>();

        //foreach (Component iter in listChilds)
        //{
        //    if (iter.transform != m_goLeftContent.transform)
        //    {
        //        Destroy(iter.gameObject);
        //    }
        //}

        //listChilds = m_goRightContent.GetComponentsInChildren<Component>();

        //foreach (Component iter in listChilds)
        //{
        //    if (iter.transform != m_goRightContent.transform)
        //    {
        //        Destroy(iter.gameObject);
        //    }
        //}
    }

    public void ActiveInputField(int nSession, int nIndex)
    {
        if (nIndex >= 25)
            return;

        if( nSession == 0 )
        {
            //m_listLeftContents[nIndex].GetComponent<CUIsCSTListAnswer>().ActiveInputField();
            m_listLeftContents[nIndex].GetComponent<CUIsCSTListAnswerTmp>().ActiveInputField();
        } else
        {
            //m_listRightContents[nIndex].GetComponent<CUIsCSTListAnswer>().ActiveInputField();
            m_listRightContents[nIndex].GetComponent<CUIsCSTListAnswerTmp>().ActiveInputField();
        }
    }

    public void OnClickExit()
    {
        ShowPopupToLobby();
    }

    public void HideAllPopup()
    {
        m_goPopupSendAnswer.SetActive(false);
        m_goPopupTimeover.SetActive(false);
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
    }

    public void ShowPopupSendAnswer()
    {
        m_goPopupSendAnswer.SetActive(true);
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    }

    public void ShowPopupTimeOver()
    {
        m_goPopupTimeover.SetActive(true);
    }

    public void ShowPopupToLobby()
    {
        if (CQuizData.Instance.GetEnableExitCount() > 0)
        {
            m_goPopupToLobby.SetActive(true);
            m_txtToLobbyMsg.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";

            int nMin = (int)(m_nRemainTime / 60);
            int nSec = (int)(m_nRemainTime % 60);

            m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        } else
        {
            m_goPopupToLobbyOver.SetActive(true);
            m_txtToLobbyOverMsg.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";
        }

        StartCoroutine("ProcessToLobbyRemainTime");
    }

    IEnumerator ProcessToLobbyRemainTime()
    {
        while (true)
        {
            int nRemainTime = m_nRemainTime;
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            if (m_goPopupToLobby.activeSelf)
                m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if (m_goPopupToLobbyOver.activeSelf)
                m_txtToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void ShowPopupToLobbyOver()
    {
        m_goPopupToLobbyOver.SetActive(true);
    }

    public void OnClickPopupSendAnswerNext()
    {
        List<string> listLeftAnswer = new List<string>();
        for (int i = 0; i < 25; i++)
        {
            //if (m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString().Equals("")) break;
            if (m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().GetAnswerString().Equals("")) break;

            //listLeftAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString());
            listLeftAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().GetAnswerString());
        }

        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("CST").sets[0].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("CST").sets[0].questions[0].answers[0].anwr_idx, listLeftAnswer.ToArray());

        List<string> listRightAnswer = new List<string>();
        for (int i = 0; i < 25; i++)
        {
            //if (m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString().Equals("")) break;
            //listRightAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString());

            if (m_listRightContents[i].GetComponent<CUIsCSTListAnswerTmp>().GetAnswerString().Equals("")) break;
            listRightAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswerTmp>().GetAnswerString());

        }

        // 상태값 API 호출 -----------------------------
        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("CST").sets[1].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("CST").sets[1].questions[0].answers[0].anwr_idx, listRightAnswer.ToArray());

        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
            }

            Server.Instance.RequestPUTQuestionsStatus(CQuizData.Instance.GetQuiz("CST").part_idx, 1);
        }

        CUIsSpaceScreenLeft.Instance.HideAllPages();
        CUIsSpaceScreenLeft.Instance.ShowRATPage();
    }

    public void OnClickPopupSendAnswerContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        Server.Instance.RequestPUTActionExit();
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void OnClickPopupToLobbyContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupTimeover()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("CST").part_idx, 1);
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
    }
}
