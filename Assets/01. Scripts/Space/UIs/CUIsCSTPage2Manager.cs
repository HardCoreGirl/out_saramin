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
    public Text m_txtLeftContent;
    public Text m_txtRightContent;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;

    private GameObject[] m_listLeftContents = new GameObject[25];
    private GameObject[] m_listRightContents = new GameObject[25];

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitCSTPage2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCSTPage2()
    {
        m_goTutorialMsg.SetActive(false);

        DelListAnswers();
        HideAllPopup();

        if( CUIsSpaceScreenLeft.Instance.IsCSTTutorial() )
        {
            m_txtSendAnswer.text = "본 퀴즈 시작하기";
            m_txtRemainTime.text = "시작전";
            m_txtMission.text = "{name}님,  플레이샵에 오신 것을 환영합니다! 첫 번째 라운드는 사고 유연성 테스트입니다. 안내문 내용을 참고하시어 아래 빈 칸에 알맞은 단어를 작성해 주세요.본 퀴즈는 연습이오니 부담 갖지 마시고 충분히 작성하셔도 됩니다.";
            m_txtLeftContent.text = "음식";
            m_txtRightContent.text = "사무용품";
        } else
        {
            m_txtSendAnswer.text = "답변 제출하기";
            
            m_txtMission.text = "과일, 가구에 속하는 단어를 번갈아 가며 최대한 많이 작성해 주시기 바랍니다.";
            Quiz quizData = CQuizData.Instance.GetQuiz("CST");
            m_nRemainTime = quizData.exm_time;
            //m_nRemainTime = 60;
            m_txtLeftContent.text = quizData.sets[0].questions[0].qst_cnnt;
            m_txtRightContent.text = quizData.sets[0].questions[1].qst_cnnt;

            StartCoroutine("ProcessPlayExam");
        }

        for(int i = 0; i < 25; i++)
        {
            m_listLeftContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            m_listLeftContents[i].transform.parent = m_goLeftContent.transform;
            m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(0, i);
            //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
        }

        m_listLeftContents[0].GetComponent<CUIsCSTListAnswer>().ActiveInputField();

        Vector2 vecSize = m_goLeftContent.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = 1120;
        m_goLeftContent.GetComponent<RectTransform>().sizeDelta = vecSize;


        for (int i = 0; i < 25; i++)
        {
            m_listRightContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            m_listRightContents[i].transform.parent = m_goRightContent.transform;
            m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(1, i);
            //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
        }

        vecSize = m_goRightContent.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = 1120;
        m_goRightContent.GetComponent<RectTransform>().sizeDelta = vecSize;

    }

    IEnumerator ProcessPlayExam()
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
        m_goTutorialMsg.SetActive(true);
    }

    public void HideTutorialMsg()
    {
        m_goTutorialMsg.SetActive(false);
    }

    public void DelListAnswers()
    {
        Component[] listChilds = m_goLeftContent.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goLeftContent.transform)
            {
                Destroy(iter.gameObject);
            }
        }

        listChilds = m_goRightContent.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goRightContent.transform)
            {
                Destroy(iter.gameObject);
            }
        }
    }

    public void ActiveInputField(int nSession, int nIndex)
    {
        if (nIndex >= 25)
            return;

        if( nSession == 0 )
        {
            m_listLeftContents[nIndex].GetComponent<CUIsCSTListAnswer>().ActiveInputField();
        } else
        {
            m_listRightContents[nIndex].GetComponent<CUIsCSTListAnswer>().ActiveInputField();
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
        m_goPopupToLobby.SetActive(true);

        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    }

    public void ShowPopupToLobbyOver()
    {
        m_goPopupToLobbyOver.SetActive(true);
    }

    public void OnClickPopupSendAnswerNext()
    {
        CUIsSpaceScreenLeft.Instance.HideAllPages();
        CUIsSpaceScreenLeft.Instance.ShowRATPage();
    }

    public void OnClickPopupSendAnswerContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupToLobby()
    {
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
        CUIsSpaceManager.Instance.ScreenActive(false);
    }
}
