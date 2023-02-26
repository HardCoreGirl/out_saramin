using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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

    public GameObject m_goBtnSendAnswer;

    public InputField m_ifAnswer;

    public TMPro.TMP_InputField m_ifAnswerTMP;

    private int m_nStage = 0;

    private string[] m_listChat;
    private string m_strQuiz;
    private int m_nAnswerCnt;
    private string[] m_listAnswer;

    private GameObject[] m_listAnswerObject;

    private int m_nAnswerIndex;
    private string m_strAnswer;
    private string m_strQuizMsg;

    private int m_nMultiAnswer;
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

        m_listAnswer = new string[30];

        string strChatMsg = "";
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            if (m_nStage == 0)
                m_nAnswerCnt = 3;
            else if (m_nStage == 1)
                m_nAnswerCnt = 4;

            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            if (m_nStage == 0)
            {
                strChatMsg = "탐사원님, 안녕하세요. 긴 동면기간을 마치고 복귀하신 걸 환영합니다.<br />저는 (주)딥스페이스 항해지원팀에 소속된 항해 파트너, 딘 자린입니다.<br />파이어니어로부터 탐사원님이 동면에서 깨어나신 이후 알 수 없는 부작용을 겪고 계시다는 교신을 받았습니다.<br />간혹 우주탐사원들에게 이런 부작용이 나타나기도 하니, 너무 걱정하지 마세요. 제가 탐사원님이 미션을 성공적으로 마칠 수 있도록 도와드리겠습니다.";
                m_strQuizMsg = "그럼 저와 함께 미션을 시작하실 준비가 되셨습니까?";
                //m_strQuiz = "컨디션은 좀 괜찮으신가요 ?";

                m_listAnswer[0] = "네, 준비됐습니다.";
                m_listAnswer[1] = "좋습니다";
                m_listAnswer[2] = "일단 시작해볼까요?";
                m_listAnswer[3] = "미션을 시작하겠습니다.";


            }
            else if (m_nStage == 1)
            {
                strChatMsg = "탐사원님, 안녕하세요. 긴 동면기간을 마치고 복귀하신 걸 환영합니다.<br />저는 (주)딥스페이스 항해지원팀에 소속된 항해 파트너, 딘 자린입니다.<br />파이어니어로부터 탐사원님이 동면에서 깨어나신 이후 알 수 없는 부작용을 겪고 계시다는 교신을 받았습니다.";
                m_strQuizMsg = "컨디션은 좀 괜찮으신가요?";
                //m_strQuiz = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";

                m_listAnswer[0] = "많이 좋아졌습니다. 감사합니다.";
                m_listAnswer[1] = "열심히 적응해 나가고 있습니다.";
                m_listAnswer[2] = "아직 잘 모르겠지만 차차 나아지겠죠.";
            }
            else if (m_nStage == 2)
            {
                strChatMsg = "좋아요. 지금처럼 계속 저와 대화해주시면 됩니다.<br />간혹 우주탐사원들에게 이런 부작용이 나타나기도 하니, 너무 걱정하지 마세요.<br />제가 탐사원님이 미션을 성공적으로 마칠 수 있도록 도와드리겠습니다.<br />이번 미션을 성공적으로 완수하려면 저와 탐사원님에게 주어진 30분의 시간을 효율적으로 활용해야 합니다.<br />메인 시스템 상단에 남은 시간이 표기될 겁니다. 제가 중간 중간에 ‘진행률’을 말씀드리겠습니다.";
                m_strQuizMsg = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";
                //m_strQuiz = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";

                m_listAnswer[0] = "네, 준비됐습니다.";
                m_listAnswer[1] = "좋습니다";
                m_listAnswer[2] = "일단 시작해볼까요?";
                m_listAnswer[3] = "미션을 시작하겠습니다.";
            }
        } else
        {


            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");

            if( !CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                if (m_nStage == 0)
                {
                    for (int i = 0; i < quizLGTK.sets.Length; i++)
                    {
                        if( quizLGTK.sets[i].questions[0].test_answers[0].test_anwr_idx == 0)
                        {
                            break;
                        } else
                        {
                            m_nStage = i;
                        }
                    }
                }
            }

            m_nAnswerCnt = quizLGTK.sets[m_nStage].questions[0].answers.Length;
            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            //if( !quizLGTK.sets[m_nStage].dir_cnnt.Equals("") )
            //{
            //    strSetMsg = quizLGTK.sets[m_nStage].dir_cnnt;
            //}

            //strChatMsg = quizLGTK.sets[m_nStage].questions[0].qst_cnnt;
            strChatMsg = quizLGTK.sets[m_nStage].dir_cnnt;
            m_strQuizMsg = quizLGTK.sets[m_nStage].questions[0].qst_cnnt;
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
        //List<string> listTotalMsg = new List<string>();
        //if ( !strSetMsg.Equals("") )
        //{
        //    string[] listSetMsg = strSetMsg.Split("<br />");

        //    for (int i = 0; i < listSetMsg.Length; i++)
        //    {
        //        Debug.Log("Set Msg [" + i + "]" + listSetMsg[i]);
        //        if(!listSetMsg[i].Equals("")) listTotalMsg.Add(listSetMsg[i]);
        //    }
        //}

        //string[] listChatMsg = strChatMsg.Split("\n");

        //for (int i = 0; i < listChatMsg.Length; i++)
        //    listTotalMsg.Add(listChatMsg[i]);

        //m_listChat = listTotalMsg.ToArray();
        //m_listChat = strChatMsg.Split("\n");

        if (strChatMsg.Equals("")) m_listChat = new string[] { };
        else m_listChat = strChatMsg.Split("<br />");
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

        DisableBtnSendAnswer();
    }

    IEnumerator ProcessQuiz()
    {
        float fWaitTime = 2.0f;
        for (int i = 0; i < m_listChat.Length; i++)
        {
            GameObject goChat = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);

            goChat.transform.parent = m_goContentChat.transform;

            //if ( i == m_listChat.Length - 1)
            //    goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i], true);
            //else
            goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i]);

            if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
                fWaitTime = 0.2f;
            yield return new WaitForSeconds(fWaitTime);
        }

        GameObject goChatQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        goChatQuiz.transform.parent = m_goContentChat.transform;
        goChatQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuizMsg, true);

        yield return new WaitForSeconds(fWaitTime);

        //GameObject goQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        //goQuiz.transform.parent = m_goContentChat.transform;
        //goQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuiz, true);

        // Answer   
        if (CUIsLGTKManager.Instance.IsTutorial())
        {
            m_goSBCTAnswer.SetActive(false);
            m_goScrollView.SetActive(true);
            m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));
            for (int i = 0; i < m_nAnswerCnt; i++)
            {
                m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, 0, m_listAnswer[i]);
            }
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            string strQuizType = quizLGTK.sets[m_nStage].questions[0].qst_ans_cd;

            if (strQuizType.Equals("SBCT"))
            {
                m_goSBCTAnswer.SetActive(true);
                //m_goSBCTAnswer.GetComponentInChildren<InputField>().text = "";
                m_goSBCTAnswer.GetComponent<TMPro.TMP_InputField>().text = "";
                m_goScrollView.SetActive(false);
            }
            else
            {
                SetMultiAnswer(quizLGTK.sets[m_nStage].questions[0].qst_ans_cnt);
                m_goSBCTAnswer.SetActive(false);
                m_goScrollView.SetActive(true);
                m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));
                for (int i = 0; i < m_nAnswerCnt; i++)
                {
                    m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                    m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                    m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, quizLGTK.sets[m_nStage].questions[0].answers[i].anwr_idx, m_listAnswer[i]);
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
            // TODO : 튜토리얼에서 닫기 버튼을 누를때
            return;
        }
        //CUIsLGTKManager.Instance.HideTalkBox();

        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-999f, -999f, 0);
    }

    public void OnClickTalkBoxSend()
    {
        Debug.Log("OnclickTalkBoxSend");
        StopCoroutine("ProcessQuiz");
        
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            m_nStage++;
            if (m_nStage == 3)
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
            if( quizLGTK.sets[m_nStage].questions[0].qst_ans_cd.Equals("OBJ") ) // 객관식
            {
                if( GetMultiAnswer() > 1 )  // 멀티
                {
                    int nMultiAnswerCnt = 0;
                    for (int i = 0; i < m_listAnswerObject.Length; i++)
                    {
                        if(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().IsSelected()) nMultiAnswerCnt++;
                    }

                    int[] listAnswer = new int[nMultiAnswerCnt];
                    int nAnswerIndex = 0;
                    for (int i = 0; i < m_listAnswerObject.Length; i++)
                    {
                        if (m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().IsSelected())
                        {
                            listAnswer[nAnswerIndex] = m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswerIndex();
                            Debug.Log("ListAnswer [" + i + "] : " + listAnswer[nAnswerIndex]);
                            nAnswerIndex++;
                        }
                    }
                    Server.Instance.RequestPUTAnswerObject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, listAnswer);
                } else
                {
                    Server.Instance.RequestPUTAnswerObject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, GetAnswerIndex());
                }
            }
            else
            {
                SetSBCTAnswer();
                Debug.Log("Send!!!!! Answer : " + GetSBCTAnswer());
                Server.Instance.RequestPUTAnswerSubject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, quizLGTK.sets[m_nStage].questions[0].answers[0].anwr_idx, GetSBCTAnswer());
            }

            m_nStage++;
            if (m_nStage >= quizLGTK.sets.Length - 1)
            {
                Debug.Log("Finish!!!!");
                CUIsLGTKManager.Instance.HideAllPopup();
                //CUIsLGTKManager.Instance.ShowPopupFinish();
                CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("LGTK").part_idx, 2);
                CUIsSpaceManager.Instance.HideCenterPage();
                return;
            }
        }
        

        InitLGTKTalkBoxMansger();
    }

    public void SetAnswerIndex(int nAnswerIndex)
    {
        m_nAnswerIndex = nAnswerIndex;
    }

    public int GetAnswerIndex()
    {
        return m_nAnswerIndex;
    }

    public void SetSBCTAnswer()
    {
        //m_strAnswer = m_ifAnswer.text;

        m_strAnswer = m_ifAnswerTMP.text;
    }

    public string GetSBCTAnswer()
    {
        return m_strAnswer;
    }

    public void SetMultiAnswer(int nAnswerCnt)
    {
        m_nMultiAnswer = nAnswerCnt;
    }

    public int GetMultiAnswer()
    {
        return m_nMultiAnswer;
    }

    public void EnableBtnSendAnswer()
    {
        m_goBtnSendAnswer.GetComponent<Button>().interactable = true;
    }

    public void DisableBtnSendAnswer()
    {
        m_goBtnSendAnswer.GetComponent<Button>().interactable = false;
    }

    public void UpdateBtnSendAnswer()
    {
        bool bIsSelected = false;
        for (int i = 0; i < m_listAnswerObject.Length; i++)
        {
            if (m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().IsSelected())
            {
                bIsSelected = true;
                break;
            }
        }

        if (bIsSelected) EnableBtnSendAnswer();
        else DisableBtnSendAnswer(); 
    }

    public void OnChangeSBCTAnswer()
    {
        //Debug.Log("OnChangeSBCTAnswer : " + m_ifAnswer.text);
        //if (m_ifAnswer.text.Equals("")) DisableBtnSendAnswer();
        //else EnableBtnSendAnswer();

        Debug.Log("OnChangeSBCTAnswer : " + m_ifAnswerTMP.text);
        if ( m_ifAnswerTMP.text.Equals("")) DisableBtnSendAnswer();
        else EnableBtnSendAnswer();
    }
}
