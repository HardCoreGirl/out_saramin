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
    public ScrollRect m_srContentChat;
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

    private string m_strObjAnswer;

    private int m_nMultiAnswer;

    private float m_fChatHeight = 15f;

    private int m_nLastAnswerIndex = 0;
    private List<int> m_listAnswerIndex;
    // Start is called before the first frame update
    void Start()
    {
        //InitLGTKTalkBoxMansger();
        //m_listAnswerIndex = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKTalkBoxMansger()
    {
        //DelContent();

        DelContentAnswer();

        m_goSBCTAnswer.SetActive(false);
        m_goScrollView.SetActive(false);

        m_listAnswer = new string[30];

        string strChatMsg = "";
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            CUIsLGTKManager.Instance.ShowBlur();

            if (m_nStage == 0)
                m_nAnswerCnt = 3;
            else if (m_nStage == 1)
                m_nAnswerCnt = 4;

            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            if (m_nStage == 0)
            {
                //strChatMsg = "탐사원님, 안녕하세요. 긴 동면기간을 마치고 복귀하신 걸 환영합니다.<br />저는 (주)딥스페이스 항해지원팀에 소속된 항해 파트너, 딘 자린입니다.<br />파이어니어로부터 탐사원님이 동면에서 깨어나신 이후 알 수 없는 부작용을 겪고 계시다는 교신을 받았습니다.<br />간혹 우주탐사원들에게 이런 부작용이 나타나기도 하니, 너무 걱정하지 마세요. 제가 탐사원님이 미션을 성공적으로 마칠 수 있도록 도와드리겠습니다.";
                //m_strQuizMsg = "그럼 저와 함께 미션을 시작하실 준비가 되셨습니까?";
                ////m_strQuiz = "컨디션은 좀 괜찮으신가요 ?";

                //m_listAnswer[0] = "네, 준비됐습니다.";
                //m_listAnswer[1] = "좋습니다";
                //m_listAnswer[2] = "일단 시작해볼까요?";
                //m_listAnswer[3] = "미션을 시작하겠습니다.";
                strChatMsg = "탐사원님, 안녕하세요. 긴 동면기간을 마치고 복귀하신 걸 환영합니다.<br />저는 (주)딥스페이스 항해지원팀에 소속된 항해 파트너, 딘 자린입니다.<br />파이어니어로부터 탐사원님이 동면에서 깨어나신 이후 알 수 없는 부작용을 겪고 계시다는 교신을 받았습니다.";
                m_strQuizMsg = "컨디션은 좀 괜찮으신가요?";
                //m_strQuiz = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";

                m_listAnswer[0] = "많이 좋아졌습니다. 감사합니다.";
                m_listAnswer[1] = "열심히 적응해 나가고 있습니다.";
                m_listAnswer[2] = "아직 잘 모르겠지만 차차 나아지겠죠.";

            }
            else if (m_nStage == 1)
            {
                //strChatMsg = "탐사원님, 안녕하세요. 긴 동면기간을 마치고 복귀하신 걸 환영합니다.<br />저는 (주)딥스페이스 항해지원팀에 소속된 항해 파트너, 딘 자린입니다.<br />파이어니어로부터 탐사원님이 동면에서 깨어나신 이후 알 수 없는 부작용을 겪고 계시다는 교신을 받았습니다.";
                //m_strQuizMsg = "컨디션은 좀 괜찮으신가요?";
                ////m_strQuiz = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";

                //m_listAnswer[0] = "많이 좋아졌습니다. 감사합니다.";
                //m_listAnswer[1] = "열심히 적응해 나가고 있습니다.";
                //m_listAnswer[2] = "아직 잘 모르겠지만 차차 나아지겠죠.";
                strChatMsg = "좋아요. 지금처럼 계속 저와 대화해주시면 됩니다.<br />간혹 우주탐사원들에게 이런 부작용이 나타나기도 하니, 너무 걱정하지 마세요.<br />제가 탐사원님이 미션을 성공적으로 마칠 수 있도록 도와드리겠습니다.<br />이번 미션을 성공적으로 완수하려면 저와 탐사원님에게 주어진 30분의 시간을 효율적으로 활용해야 합니다.<br />메인 시스템 상단에 남은 시간이 표기될 겁니다. 제가 중간 중간에 ‘진행률’을 말씀드리겠습니다.";
                m_strQuizMsg = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";
                //m_strQuiz = "이제 본격적으로 미션을 시작하실 준비가 되셨나요?";

                m_listAnswer[0] = "네, 준비됐습니다.";
                m_listAnswer[1] = "좋습니다";
                m_listAnswer[2] = "일단 시작해볼까요?";
                m_listAnswer[3] = "미션을 시작하겠습니다.";
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
            CUIsLGTKManager.Instance.HideBlur();

            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");

            //if( !CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            //{
                if (m_nStage == 0)
                {
                    Debug.Log("InitLGTKTalkBoxMansger 00 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    m_listAnswerIndex = new List<int>();
                    for (int i = 0; i < quizLGTK.sets.Length; i++)
                    {
                        Debug.Log("InitLGTKTalkBoxMansger Answer Index : " + quizLGTK.sets[i].questions[0].test_answers[0].test_anwr_idx);
                        if ( quizLGTK.sets[i].questions[0].test_answers[0].test_anwr_idx == 0)
                        {
                            break;
                        } else
                        {
                            //m_nStage = i + 1;
                            m_nLastAnswerIndex = i + 1;
                            m_listAnswerIndex.Add(quizLGTK.sets[i].questions[0].qst_idx);
                            Debug.Log("InitLGTKTalkBoxMansger Answer Index 02 : " + quizLGTK.sets[i].questions[0].qst_idx);
                        }
                    }
                }
            //}

            Debug.Log("InitLGTKTalkBoxMansger Stage : " + m_nStage);

            m_nAnswerCnt = quizLGTK.sets[m_nStage].questions[0].answers.Length;
            m_listAnswerObject = new GameObject[m_nAnswerCnt];

            //if( !quizLGTK.sets[m_nStage].dir_cnnt.Equals("") )
            //{
            //    strSetMsg = quizLGTK.sets[m_nStage].dir_cnnt;
            //}

            //strChatMsg = quizLGTK.sets[m_nStage].questions[0].qst_cnnt;
            strChatMsg = quizLGTK.sets[m_nStage].dir_cnnt;

            if(quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("$$$") || quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("###")) 
            {
                m_strQuizMsg = quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Substring(3, quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Length - 3);
            } else
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
        bool bIsPass = false;

        if (m_listAnswerIndex != null)
        {
            for (int i = 0; i < m_listAnswerIndex.Count; i++)
            {
                Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
                if (m_listAnswerIndex[i] == quizLGTK.sets[m_nStage].questions[0].qst_idx)
                {
                    fWaitTime = 0.01f;
                    bIsPass = true;
                    break;
                }
            }
        }

        for (int i = 0; i < m_listChat.Length; i++)
        {
            if( !CUIsLGTKManager.Instance.IsQuizActive() )
            {
                while(true)
                {
                    if (CUIsLGTKManager.Instance.IsQuizActive())
                        break;

                    yield return new WaitForSeconds(0.2f);
                }
            }

            GameObject goChat = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);

            goChat.transform.parent = m_goContentChat.transform;

            //if ( i == m_listChat.Length - 1)
            //    goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i], true);
            //else
            goChat.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_listChat[i]);

            //Debug.Log("ProcessQuiz 00 : " + goChat.GetComponent<RectTransform>().sizeDelta.y);
            m_fChatHeight += (goChat.GetComponent<RectTransform>().sizeDelta.y + 5f);

            UpdateChatContentSize();

            //if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            //    fWaitTime = 0.2f;
            yield return new WaitForSeconds(fWaitTime);
        }

        if (!CUIsLGTKManager.Instance.IsQuizActive())
        {
            while (true)
            {
                if (CUIsLGTKManager.Instance.IsQuizActive())
                    break;

                yield return new WaitForSeconds(0.2f);
            }
        }


        Debug.Log("ProcessQuiz 01");
        GameObject goChatQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        goChatQuiz.transform.parent = m_goContentChat.transform;
        goChatQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuizMsg, true);

        m_fChatHeight += (goChatQuiz.GetComponent<RectTransform>().sizeDelta.y + 5f);
        UpdateChatContentSize();

        yield return new WaitForSeconds(fWaitTime);

        //GameObject goQuiz = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChat") as GameObject);
        //goQuiz.transform.parent = m_goContentChat.transform;
        //goQuiz.GetComponent<CObjecctLGTKTalkBoxChat>().UpdateChat(m_strQuiz, true);

        // Answer   
        if (CUIsLGTKManager.Instance.IsTutorial())
        {
            if (!CUIsLGTKManager.Instance.IsQuizActive())
            {
                while (true)
                {
                    if (CUIsLGTKManager.Instance.IsQuizActive())
                        break;

                    yield return new WaitForSeconds(0.2f);
                }
            }


            m_goSBCTAnswer.SetActive(false);
            m_goScrollView.SetActive(true);
            m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));
            for (int i = 0; i < m_nAnswerCnt; i++)
            {
                m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, 0, m_listAnswer[i]);
            }

            m_goScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
           
            //m_goContentAnswer.
        } else
        {
            if (!CUIsLGTKManager.Instance.IsQuizActive())
            {
                while (true)
                {
                    if (CUIsLGTKManager.Instance.IsQuizActive())
                        break;

                    yield return new WaitForSeconds(0.2f);
                }
            }

            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            if ( bIsPass )
            {
                string strQuizType = quizLGTK.sets[m_nStage].questions[0].qst_ans_cd;

                if (strQuizType.Equals("SBCT"))
                {
                    string strAnswer = quizLGTK.sets[m_nStage].questions[0].test_answers[0].test_anwr_sbct;

                    if (quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("$$$"))
                    {
                        CUIsLGTKManager.Instance.AddListPlanetAnswers(strAnswer);
                    }

                    if (quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("###"))
                    {
                        CUIsLGTKManager.Instance.AddListFairwayAnswers(strAnswer);
                    }

                    AddChatAnswer(strAnswer);
                } 
                else
                {
                    for(int x = 0; x < quizLGTK.sets[m_nStage].questions[0].test_answers.Length; x++)
                    {
                        for(int y = 0; y < quizLGTK.sets[m_nStage].questions[0].answers.Length; y++)
                        {
                            if (quizLGTK.sets[m_nStage].questions[0].answers[y].anwr_idx == quizLGTK.sets[m_nStage].questions[0].test_answers[x].test_anwr_idx)
                            {
                                string strAnswer = quizLGTK.sets[m_nStage].questions[0].answers[y].anwr_cnnt;
                                AddChatAnswer(strAnswer);

                                if(quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("$$$"))
                                {
                                    CUIsLGTKManager.Instance.AddListPlanetAnswers(strAnswer);
                                }

                                if (quizLGTK.sets[m_nStage].questions[0].qst_cnnt.Contains("###"))
                                {
                                    CUIsLGTKManager.Instance.AddListFairwayAnswers(strAnswer);
                                }

                                break;
                            }
                        }
                       ;
                    }
                }

                CUIsLGTKManager.Instance.UpdateDatabaseDynamic();

                m_nStage++;

                InitLGTKTalkBoxMansger();
            } else
            {
                
                string strQuizType = quizLGTK.sets[m_nStage].questions[0].qst_ans_cd;

                if (strQuizType.Equals("SBCT"))
                {
                    m_goSBCTAnswer.SetActive(true);
                    //m_goSBCTAnswer.GetComponentInChildren<InputField>().text = "";
                    //m_goSBCTAnswer.GetComponent<TMPro.TMP_InputField>().text = "";
                    m_goSBCTAnswer.GetComponentInChildren<TMPro.TMP_InputField>().text = "";
                    m_goScrollView.SetActive(false);
                }
                else
                {
                    SetMultiAnswer(quizLGTK.sets[m_nStage].questions[0].qst_ans_cnt);
                    m_goSBCTAnswer.SetActive(false);
                    m_goScrollView.SetActive(true);
                    m_goContentAnswer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60 + (m_nAnswerCnt * 40) + ((m_nAnswerCnt - 1) * 10));

                    int[] listAnswerSort = new int[m_nAnswerCnt];
                    for (int i = 0; i < listAnswerSort.Length; i++)
                    {
                        listAnswerSort[i] = quizLGTK.sets[m_nStage].questions[0].answers[i].anwr_idx;

                        //Debug.Log("ListAnswerSort : " + listAnswerSort[i]);
                    }

                    System.Array.Reverse(listAnswerSort);

                    int[] listSortIndex = new int[m_nAnswerCnt];

                    for (int i = 0; i < listAnswerSort.Length; i++)
                    {
                        //Debug.Log("ListAnswerSort After : " + listAnswerSort[i]);
                        for (int j = 0; j < listSortIndex.Length; j++)
                        {
                            if (listAnswerSort[i] == quizLGTK.sets[m_nStage].questions[0].answers[j].anwr_idx)
                            {
                                listSortIndex[i] = j;
                                break;
                            }
                        }
                    }

                    //for(int i = 0; i < listSortIndex.Length; i++)
                    //{
                    //    Debug.Log("Sort Index : " + listSortIndex[i]);
                    //}


                    for (int i = 0; i < m_nAnswerCnt; i++)
                    {
                        int nConvAnswerIndex = listSortIndex[i];
                        m_listAnswerObject[i] = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxAnswer") as GameObject);
                        m_listAnswerObject[i].transform.parent = m_goContentAnswer.transform;
                        //m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, quizLGTK.sets[m_nStage].questions[0].answers[i].anwr_idx, m_listAnswer[i]);
                        m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().InitLGTKTalkBoxAnswer(i, quizLGTK.sets[m_nStage].questions[0].answers[nConvAnswerIndex].anwr_idx, quizLGTK.sets[m_nStage].questions[0].answers[nConvAnswerIndex].anwr_cnnt);


                    }

                    m_goScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;

                }
            }
        }
    }

    public void AddChatAnswer(string strAnswer)
    {
        GameObject goChatAnswer = Instantiate(Resources.Load("Prefabs/LGTKTalkBoxChatAnswer") as GameObject);
        goChatAnswer.transform.parent = m_goContentChat.transform;
        goChatAnswer.GetComponent<CObjectLGTKTalkBoxChatAnswer>().UpdateChat(strAnswer);

        m_fChatHeight += (goChatAnswer.GetComponent<RectTransform>().sizeDelta.y + 5f);

        UpdateChatContentSize();
    }

    public void UpdateChatContentSize()
    {
        m_goContentChat.GetComponent<RectTransform>().sizeDelta = new Vector2(m_goContentChat.GetComponent<RectTransform>().sizeDelta.x, m_fChatHeight);
        //Vector3 vecPoz = m_goContentChat.GetComponent<RectTransform>().localPosition;
        //vecPoz.y = m_fChatHeight;
        //m_goContentChat.GetComponent<RectTransform>().localPosition = vecPoz;
        m_srContentChat.verticalNormalizedPosition = 0f;

    }

    public void DelContent()
    {
        m_fChatHeight = 15f;

        Component[] listChilds = m_goContentChat.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goContentChat.transform)
            {
                Destroy(iter.gameObject);
            }
        }

        DelContentAnswer();

        //listChilds = m_goContentAnswer.GetComponentsInChildren<Component>();

        //foreach (Component iter in listChilds)
        //{
        //    if (iter.transform != m_goContentAnswer.transform)
        //    {
        //        Destroy(iter.gameObject);
        //    }
        //}
    }

    public void DelContentAnswer()
    {
        Component[] listChilds = m_goContentAnswer.GetComponentsInChildren<Component>();

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
            CUIsLGTKManager.Instance.ShowPopupToLobbyTutorial();
            return;
        }
        //CUIsLGTKManager.Instance.HideTalkBox();

        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-999f, -999f, 0);
    }

    public void OnClickTalkBoxSend()
    {
        //Debug.Log("OnclickTalkBoxSend");
        StopCoroutine("ProcessQuiz");
        
        if( CUIsLGTKManager.Instance.IsTutorial() )
        {
            AddChatAnswer(GetObjAnswwer());
            m_nStage++;
            if (m_nStage == 2)
            {
                //DelContent();
                CUIsLGTKManager.Instance.SetTutorial(false);
                m_nStage = 0;
                //CUIsLGTKManager.Instance.InitLGTK();
                CUIsLGTKManager.Instance.PlayQuiz();
                InitLGTKTalkBoxMansger();
                //CUIsLGTKManager.Instance.HideTalkBox();
                CUIsLGTKManager.Instance.HideBlur();
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

                            // 데이터 베이스 선택적 표시를 위한 처리
                            //CUIsLGTKManager.Instance.AddListAnswers(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            if( CUIsLGTKManager.Instance.GetQuizPlanetIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                            {
                                Debug.Log("Add Planet Answer !!!!!!!!!!!!!!!!!!!!!!!! : " + m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                                CUIsLGTKManager.Instance.AddListPlanetAnswers(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            }

                            //if (CUIsLGTKManager.Instance.GetQuizFairwayIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                            //{
                            //    Debug.Log("Add Fairway Answer !!!!!!!!!!!!!!!!!!!!!!!! : " + m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            //    CUIsLGTKManager.Instance.AddListFairwayAnswers(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            //}

                            List<int> listQuizFairwayIndex = CUIsLGTKManager.Instance.GetListQuizFairwayIndex();
                            for (int j = 0; j < listQuizFairwayIndex.Count; j++)
                            {
                                if( listQuizFairwayIndex[j] == quizLGTK.sets[m_nStage].questions[0].set_dir_idx )
                                    CUIsLGTKManager.Instance.AddListFairwayAnswers(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            }

                            AddChatAnswer(m_listAnswerObject[i].GetComponent<CObjectLGTKTalkBoxAnswer>().GetAnswer());
                            Debug.Log("ListAnswer [" + i + "] : " + listAnswer[nAnswerIndex]);
                            nAnswerIndex++;
                        }
                    }
                    Server.Instance.RequestPUTAnswerObject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, listAnswer);
                } else
                {
                    //CUIsLGTKManager.Instance.AddListAnswers(GetObjAnswwer());
                    // 데이터 베이스 선택적 표시를 위한 처리
                    if (CUIsLGTKManager.Instance.GetQuizPlanetIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                    {
                        Debug.Log("Add Planet Answer !!!!!!!!!!!!!!!!!!!!!!!!");
                        CUIsLGTKManager.Instance.AddListPlanetAnswers(GetObjAnswwer());
                    }

                    //if (CUIsLGTKManager.Instance.GetQuizFairwayIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                    //{
                    //    Debug.Log("Add Fairway Answer !!!!!!!!!!!!!!!!!!!!!!!!");
                    //    CUIsLGTKManager.Instance.AddListFairwayAnswers(GetObjAnswwer());
                    //}

                    List<int> listQuizFairwayIndex = CUIsLGTKManager.Instance.GetListQuizFairwayIndex();
                    for (int j = 0; j < listQuizFairwayIndex.Count; j++)
                    {
                        if (listQuizFairwayIndex[j] == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                            CUIsLGTKManager.Instance.AddListFairwayAnswers(GetObjAnswwer());
                    }

                    AddChatAnswer(GetObjAnswwer());
                    Server.Instance.RequestPUTAnswerObject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, GetAnswerIndex());
                }
            }
            else
            {
                SetSBCTAnswer();

                Debug.Log("Send!!!!! Answer : " + GetSBCTAnswer());
                //CUIsLGTKManager.Instance.AddListAnswers(GetSBCTAnswer());
                //CUIsLGTKManager.Instance.AddListSBCTAnswer(GetSBCTAnswer());
                // 데이터 베이스 선택적 표시를 위한 처리
                if (CUIsLGTKManager.Instance.GetQuizPlanetIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                {
                    CUIsLGTKManager.Instance.AddListPlanetAnswers(GetSBCTAnswer());
                }

                //if (CUIsLGTKManager.Instance.GetQuizFairwayIndex() == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                //{
                //    string[] strAnswer = GetSBCTAnswer().Split(",");
                //    for(int i = 0; i < strAnswer.Length; i++)
                //    {
                //        string strConvAnswer = strAnswer[i].Replace(" ", "");

                //        if( !strConvAnswer.Equals(""))
                //        {
                //            CUIsLGTKManager.Instance.AddListFairwayAnswers(strConvAnswer);
                //        }
                //        //Debug.Log("OnClickTalkBoxSend : [" + strConvAnswer + "]");
                //    }
                //    //CUIsLGTKManager.Instance.AddListFairwayAnswers(GetSBCTAnswer());
                //}

                List<int> listQuizFairwayIndex = CUIsLGTKManager.Instance.GetListQuizFairwayIndex();
                for (int j = 0; j < listQuizFairwayIndex.Count; j++)
                {
                    if (listQuizFairwayIndex[j] == quizLGTK.sets[m_nStage].questions[0].set_dir_idx)
                        CUIsLGTKManager.Instance.AddListFairwayAnswers(GetSBCTAnswer());
                }

                AddChatAnswer(GetSBCTAnswer());
                Server.Instance.RequestPUTAnswerSubject(quizLGTK.sets[m_nStage].questions[0].test_qst_idx, quizLGTK.sets[m_nStage].questions[0].answers[0].anwr_idx, GetSBCTAnswer());
            }

            CUIsLGTKManager.Instance.UpdateDatabaseDynamic();

            m_nStage++;
            if (m_nStage >= quizLGTK.sets.Length)
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

    public void SetObjAnswer(string strAnswer)
    {
       m_strObjAnswer = strAnswer;
    }

    public string GetObjAnswwer()
    {
        return m_strObjAnswer;
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

        //Debug.Log("OnChangeSBCTAnswer : " + m_ifAnswerTMP.text);
        if ( m_ifAnswerTMP.text.Equals("")) DisableBtnSendAnswer();
        else EnableBtnSendAnswer();
    }
}
