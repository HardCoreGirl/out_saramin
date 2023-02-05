using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsLGTKTalkBoxManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsLGTKTalkBoxManager _instance = null;

    public static CUIsLGTKTalkBoxManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsLGTKTalkBoxManagerr install null");

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

    public GameObject m_goContentChat;
    public GameObject m_goContentAnswer;

    public GameObject m_goScrollView;
    public GameObject m_goSBCTAnswer;

    private int m_nStage = 0;

    private string[] m_listChat;
    private string m_strQuiz;
    private int m_nAnswerCnt;
    private string[] m_listAnswer;

    private GameObject[] m_listAnswerObject;

    // Start is called before the first frame update
    void Start()
    {
        //InitLGTKTalkBoxMansger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKTalkBoxMansger()
    {
        DelContent();

        m_goSBCTAnswer.SetActive(false);
        m_goScrollView.SetActive(false);

        string strChatMsg = "";
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            if (m_nStage == 0)
                m_nAnswerCnt = 3;
            else if (m_nStage == 1)
                m_nAnswerCnt = 4;

            m_listAnswer = new string[30];

            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            if (m_nStage == 0)
            {
                strChatMsg = "Ž�����, �ȳ��ϼ���. �� ����Ⱓ�� ��ġ�� �����Ͻ� �� ȯ���մϴ�.\n���� (��)�������̽� ������������ �Ҽӵ� ���� ��Ʈ��, �� �ڸ��Դϴ�.\n���̾�Ͼ�κ��� Ž������� ���鿡�� ����� ���� �� �� ���� ���ۿ��� �ް� ��ôٴ� ������ �޾ҽ��ϴ�.\n������� �� �������Ű��� ?";
                //m_strQuiz = "������� �� �������Ű��� ?";

                m_listAnswer[0] = "���� ���������ϴ�. �����մϴ�.";
                m_listAnswer[1] = "������ ������ ������ �ֽ��ϴ�.";
                m_listAnswer[2] = "���� �� �𸣰����� ���� ����������.";

            }
            else if (m_nStage == 1)
            {
                strChatMsg = "���ƿ�. ����ó�� ��� ���� ��ȭ���ֽø� �˴ϴ�.\n��Ȥ ����Ž����鿡�� �̷� ���ۿ��� ��Ÿ���⵵ �ϴ�, �ʹ� �������� ������. ���� Ž������� �̼��� ���������� ��ĥ �� �ֵ��� ���͵帮�ڽ��ϴ�.\n�̹� �̼��� ���������� �ϼ��Ϸ��� ���� Ž����Կ��� �־��� 30���� �ð��� ȿ�������� Ȱ���ؾ� �մϴ�.\n���� �ý��� ��ܿ� ���� �ð��� ǥ��� �̴ϴ�. ���� �߰� �߰��� ����������� �����帮�ڽ��ϴ�.\n���� ���������� �̼��� �����Ͻ� �غ� �Ǽ̳���?";
                //m_strQuiz = "���� ���������� �̼��� �����Ͻ� �غ� �Ǽ̳���?";

                m_listAnswer[0] = "��, �غ�ƽ��ϴ�.";
                m_listAnswer[1] = "�����ϴ�";
                m_listAnswer[2] = "�ϴ� �����غ����?";
                m_listAnswer[3] = "�̼��� �����ϰڽ��ϴ�.";
            }
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            m_nAnswerCnt = quizLGTK.sets[m_nStage].questions[0].answers.Length;

            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            Debug.Log("Count : " + m_listAnswerObject.Length);

            strChatMsg = quizLGTK.sets[m_nStage].questions[0].qst_cnnt;

            for (int i = 0; i < m_nAnswerCnt; i++)
            {
                m_listAnswer[i] = quizLGTK.sets[m_nStage].questions[0].answers[i].anwr_cnnt;
            }

            // TODO : SET_GUDES
            //for(int i = 0; i < quizLGTK.set_gudes.Length; i++)
            //{
            //    Debug.Log(quizLGTK.set_gudes[i].gude_deth);
            //}


        }

        m_listChat = strChatMsg.Split("\n");

        StartCoroutine("ProcessQuiz");

        //for (int i = 0; i < m_listChat.Length; i++)
        //{
        //    GameObject goChat = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        //    goChat.transform.parent = m_goContentChat.transform;
        //    goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i]);
        //}

        //GameObject goQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        //goQuiz.transform.parent = m_goContentChat.transform;
        //goQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuiz, true);


        //// Answer 
        //m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (listAnswer.Length * 40) + ((listAnswer.Length - 1)* 10));
        //for (int i = 0; i < listAnswer.Length; i++)
        //{
        //    GameObject goAnswer = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
        //    goAnswer.transform.parent = m_goContentAnswer.transform;
        //    goAnswer.GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, listAnswer[i]);
        //}


    }

    IEnumerator ProcessQuiz()
    {
        for (int i = 0; i < m_listChat.Length; i++)
        {
            GameObject goChat = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);

            goChat.transform.parent = m_goContentChat.transform;

            if ( i == m_listChat.Length - 1)
                goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i], true);
            else
                goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i]);

            yield return new WaitForSeconds(2f);
        }

        //GameObject goQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        //goQuiz.transform.parent = m_goContentChat.transform;
        //goQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuiz, true);

        // Answer   
        if(CUIsLGTKManager.Instance.IsTutorial())
        {
            m_goSBCTAnswer.SetActive(false);
            m_goScrollView.SetActive(true);
            m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));
            for (int i = 0; i < m_nAnswerCnt; i++)
            {
                m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, m_listAnswer[i]);
            }
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            string strQuizType = quizLGTK.sets[m_nStage].questions[0].qst_ans_cd;

            if (strQuizType.Equals("SBCT"))
            {
                m_goSBCTAnswer.SetActive(true);
                m_goScrollView.SetActive(false);
            }
            else
            {
                m_goSBCTAnswer.SetActive(false);
                m_goScrollView.SetActive(true);
                m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));
                for (int i = 0; i < m_nAnswerCnt; i++)
                {
                    m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                    m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                    m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, m_listAnswer[i]);
                }

            }
        }
    }

    public void DelContent()
    {
        Component[] listChilds = m_goContentChat.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goContentChat.transform)
            {
                Destroy(iter.gameObject);
            }
        }

        listChilds = m_goContentAnswer.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goContentAnswer.transform)
            {
                Destroy(iter.gameObject);
            }
        }
    }

    public void ResetAnswer()
    {
        for(int i = 0; i < m_listAnswerObject.Length; i++)
        {
            m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().ResetAnswer();
        }
    }

    //        public void DelListAnswers()
    //{
    //    Component[] listChilds = m_goLeftContent.GetComponentsInChildren<Component>();

    //    foreach (Component iter in listChilds)
    //    {
    //        if (iter.transform != m_goLeftContent.transform)
    //        {
    //            Destroy(iter.gameObject);
    //        }
    //    }

    //    listChilds = m_goRightContent.GetComponentsInChildren<Component>();

    //    foreach (Component iter in listChilds)
    //    {
    //        if (iter.transform != m_goRightContent.transform)
    //        {
    //            Destroy(iter.gameObject);
    //        }
    //    }
    //}

    public void OnClickTalkBoxExit()
    {
        if(CUIsLGTKManager.Instance.IsTutorial())
        {
            // TODO : Ʃ�丮�󿡼� �ݱ� ��ư�� ������
            return;
        }
        CUIsLGTKManager.Instance.HideTalkBox();
    }

    public void OnClickTalkBoxSend()
    {
        Debug.Log("OnclickTalkBoxSend");
        StopCoroutine("ProcessQuiz");
        m_nStage++;
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            if (m_nStage == 2)
            {
                CUIsLGTKManager.Instance.SetTutorial(false);
                m_nStage = 0;
                CUIsLGTKManager.Instance.InitLGTK();
                //CUIsLGTKManager.Instance.HideTalkBox();
                return;
            }
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            if (m_nStage >= quizLGTK.sets.Length)
            {
                CUIsLGTKManager.Instance.HideAllPopup();
                CUIsLGTKManager.Instance.ShowPopupFinish();
                return;
            }
        }
        

        InitLGTKTalkBoxMansger();
    }
}
