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
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;

    private GameObject[] m_listLeftContents = new GameObject[25];
    private GameObject[] m_listRightContents = new GameObject[25];

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    private List<InputField> m_listInputField;
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

            m_listInputField[m_nCurPos].Select();
        }
    }

    public void MoveNext()
    {
        GetCurrentPos();
        //Debug.Log("Move Prev : " + GetCurrentPos());
        if (m_nCurPos < m_listInputField.Count - 1)
        {
            if (!m_listInputField[m_nCurPos + 1].IsInteractable())
                return;
            ++m_nCurPos;

            m_listInputField[m_nCurPos].Select();
        }
    }

    private int GetCurrentPos()
    {
        for (int i = 0; i < m_listInputField.Count; ++i)
        {
            if(m_listInputField[i].isFocused == true)
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

        DelListAnswers();
        HideAllPopup();

        if( CUIsSpaceScreenLeft.Instance.IsCSTTutorial() )
        {
            m_txtSendAnswer.text = "�� ���� �����ϱ�";
            m_txtRemainTime.text = "������";
            m_txtMission.text = CQuizData.Instance.GetUserName() + "��,  �÷��̼��� ���� ���� ȯ���մϴ�! ù ��° ����� ��� ������ �׽�Ʈ�Դϴ�. �ȳ��� ������ �����Ͻþ� �Ʒ� �� ĭ�� �˸��� �ܾ �ۼ��� �ּ���.�� ����� �����̿��� �δ� ���� ���ð� ����� �ۼ��ϼŵ� �˴ϴ�.";
            m_txtLeftContent.text = "����";
            m_txtRightContent.text = "�繫��ǰ";

            m_txtMissionContent.text = "�����ġ� ���ֿ��� �Ϲ������� ����� �԰ų� ���� �� �ִ� ��� ��(��ġ, ��� ��)�� �ۼ��� �ּ���.\n-\n�繫��ǰ ���ֿ��� ������, �繫���, ���̷� ���� ������ ��ǰ(����, �������÷� ��)�� �ۼ��� �ּ���.�Դϴ�.\n-\n�� ���ֿ� ���ϴ� �ܾ �����ư��� ������� �Է��� �ּ���.\n-\n�ִ��� ������ ��Ż�� ���� �Է��� �ּ���.\n-\n���� ������ �Ͻ� ��� �������� ���� �� �ֽ��ϴ�.";
        } else
        {
            m_txtSendAnswer.text = "�亯 �����ϱ�";
            
            //m_txtMission.text = "����, ������ ���ϴ� �ܾ ������ ���� �ִ��� ���� �ۼ��� �ֽñ� �ٶ��ϴ�.";
            Quiz quizData = CQuizData.Instance.GetQuiz("CST");
            m_txtMission.text = quizData.sets[0].dir_cnnt;
            m_txtMissionContent.text = quizData.sets[0].dir_cnnt;
            m_nRemainTime = quizData.exm_time;
            m_txtLeftContent.text = quizData.sets[0].questions[0].qst_cnnt;
            m_txtRightContent.text = quizData.sets[0].questions[1].qst_cnnt;

            StartCoroutine("ProcessPlayExam");
        }

        //int nSelectableIdx = 0;
        //selectables = new Selectable[50];

        m_listInputField = new List<InputField>();
        m_listInputField.Clear();
        m_nCurPos = 0;
        
        for(int i = 0; i < 25; i++)
        {
            m_listLeftContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            m_listLeftContents[i].transform.parent = m_goLeftContent.transform;
            m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(0, i);
            m_listInputField.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().m_ifAnswer);
            //goTalk.GetComponent<CUIsRQTTalkChat>().InitObject(i, quizRQT.sets[i].dir_cnnt, true);
            //selectables[nSelectableIdx] = m_listLeftContents[i].GetComponent<Selectable>();
            //nSelectableIdx++;

            m_listRightContents[i] = Instantiate(Resources.Load("Prefabs/cstListAnswer") as GameObject);
            m_listRightContents[i].transform.parent = m_goRightContent.transform;
            m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().InitListAnswer(1, i);
            m_listInputField.Add(m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().m_ifAnswer);

            //selectables[nSelectableIdx] = m_listRightContents[i].GetComponent<Selectable>();
            //nSelectableIdx++;
        }

        m_listLeftContents[0].GetComponent<CUIsCSTListAnswer>().ActiveInputField();

        m_listInputField[0].Select();

        Vector2 vecSize = m_goLeftContent.GetComponent<RectTransform>().sizeDelta;
        vecSize.y = 1120;
        m_goLeftContent.GetComponent<RectTransform>().sizeDelta = vecSize;


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


            if (m_nRemainTime == 0)
                break;
        }

        for (int i = 0; i < 25; i++)
        {
            m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().DisableInputField();
            m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().DisableInputField();

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
        List<string> listLeftAnswer = new List<string>();
        for (int i = 0; i < 25; i++)
        {
            if (m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString().Equals("")) break;

            listLeftAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString());
        }

        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("CST").sets[0].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("CST").sets[0].questions[0].answers[0].anwr_idx, listLeftAnswer.ToArray());

        List<string> listRightAnswer = new List<string>();
        for (int i = 0; i < 25; i++)
        {
            if (m_listRightContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString().Equals("")) break;
            listRightAnswer.Add(m_listLeftContents[i].GetComponent<CUIsCSTListAnswer>().GetAnswerString());
        }

        // ���°� API ȣ�� -----------------------------
        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("CST").sets[0].questions[1].test_qst_idx, CQuizData.Instance.GetQuiz("CST").sets[0].questions[1].answers[0].anwr_idx, listRightAnswer.ToArray());

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
