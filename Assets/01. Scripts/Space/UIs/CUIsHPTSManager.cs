using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsHPTSManager : MonoBehaviour
{
    // TODO 활동로그 남기기
    #region SingleTon
    public static CUIsHPTSManager _instance = null;

    public static CUIsHPTSManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsHPTSManager install null");

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

    public Text m_txtBtnSendAnswer;
    public Text m_txtRemainTime;

    public Text m_txtTitleMsg;
    public Text m_txtContentMsg;

    public GameObject m_goContents;

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    private int m_nQuizIndex = 0;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyMsg;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;
    public Text m_txtToLobbyOverMsg;
    public Text m_txtToLobbyOverRemainTime;

    public GameObject m_goPopupToLobbyTutorial;

    // Start is called before the first frame update
    void Start()
    {
        //InitHPTSPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHPTSPage()
    {
        HideTutorialMsg();

        DelListAnswers();

        Quiz quizData = CQuizData.Instance.GetQuiz("HPTS");

        if (quizData.exm_time != quizData.progress_time)
        {
            CUIsSpaceScreenLeft.Instance.SetHPTSTutorial(false);
        }

        if (CUIsSpaceScreenLeft.Instance.IsHPTSTutorial())
        {
            m_txtBtnSendAnswer.text = "본 퀴즈 시작하기";
            m_txtRemainTime.text = "시작전";

            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(null, "평소 명상을 많이 하는 사람은 그렇지 않은 사람보다", "", "", "", "");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(null, "엔돌핀 수준이", "높을", "낮을", "", "것이다. 동일한 양의 초콜렛을 먹는다고 가정할 때,");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(null, "초콜렛을 좋아하는 정도가 엔돌핀 양에 영향을", "줄 것이다", "주지 않을 것이다", "", ". 매우 슬픈 사람과");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(null, "매우 기쁜 사람의 엔돌핀 수치를 비교했을 때, 그 결과를 예상할 수", "있다", "없다", "", ".");
        }
        else
        {
            //Quiz quizData = CQuizData.Instance.GetQuiz("HPTS");
            if (m_nQuizIndex == 0)
            {
                Debug.Log("InitHPTS 01");

                m_txtBtnSendAnswer.text = "다음문제 (1/2)";
                //m_nRemainTime = 120;
                //m_nRemainTime = quizData.exm_time;
                m_nRemainTime = quizData.progress_time;
                StopCoroutine("ProcessPlayExam");
                StartCoroutine("ProcessPlayExam");

                m_txtTitleMsg.text = quizData.sets[0].dir_cnnt;
                m_txtContentMsg.text = quizData.sets[0].qst_brws_cnnt;

                for (int i = 0; i < quizData.sets[0].questions.Length; i++)
                {
                    //if( i == 0 )
                    //{
                    //    string[] listQuiz = quizData.sets[0].questions[i].qst_cnnt.Split(',');
                    //    if (listQuiz.Length > 1)
                    //    {
                    //        for (int j = 0; j < listQuiz.Length; j++)
                    //        {
                    //            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                    //            goQuiz.transform.parent = m_goContents.transform;

                    //            if (listQuiz[j].Contains("{{answers}}"))
                    //            {
                    //                string[] listAnswer = listQuiz[j].Split("{{answers}}");
                    //                string[] listSelector = new string[3];
                    //                for (int k = 0; k < listSelector.Length; k++)
                    //                {
                    //                    listSelector[k] = "";
                    //                }
                    //                for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                    //                {
                    //                    listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                    //                }

                    //                if (listAnswer.Length > 1)
                    //                {
                    //                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                    //                }
                    //                else
                    //                {
                    //                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], "", "", "", "");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listQuiz[j], "", "", "", "");
                    //            }
                    //        }
                    //    }
                    //} else
                    //{
                    //    GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                    //    goQuiz.transform.parent = m_goContents.transform;

                    //    if (quizData.sets[0].questions[i].qst_cnnt.Contains("{{answers}}"))
                    //    {
                    //        string[] listAnswer = quizData.sets[0].questions[i].qst_cnnt.Split("{{answers}}");
                    //        string[] listSelector = new string[3];
                    //        for (int k = 0; k < listSelector.Length; k++)
                    //        {
                    //            listSelector[k] = "";
                    //        }
                    //        for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                    //        {
                    //            listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                    //        }

                    //        if (listAnswer.Length > 1)
                    //        {
                    //            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                    //        }
                    //        else
                    //        {
                    //            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], "", "", "", "");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], quizData.sets[0].questions[i].qst_cnnt, "", "", "", "");
                    //    }
                    //}
                    string[] listQuiz = quizData.sets[0].questions[i].qst_cnnt.Split("<br />");
                    if (listQuiz.Length > 1)
                    {
                        for (int j = 0; j < listQuiz.Length; j++)
                        {
                            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                            goQuiz.transform.parent = m_goContents.transform;

                            if (listQuiz[j].Contains("{{answers}}"))
                            {
                                string[] listAnswer = listQuiz[j].Split("{{answers}}");
                                string[] listSelector = new string[3];
                                for (int k = 0; k < listSelector.Length; k++)
                                {
                                    listSelector[k] = "";
                                }
                                for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                                {
                                    listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                                }

                                if (listAnswer.Length > 1)
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                                }
                                else
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], "", "", "", "");
                                }
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listQuiz[j], "", "", "", "");
                            }
                        }
                    }
                    else
                    {
                        GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                        goQuiz.transform.parent = m_goContents.transform;

                        if (listQuiz[0].Contains("{{answers}}"))
                        {
                            string[] listAnswer = listQuiz[0].Split("{{answers}}");
                            string[] listSelector = new string[3];
                            for (int k = 0; k < listSelector.Length; k++)
                            {
                                listSelector[k] = "";
                            }
                            for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                            {
                                listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                            }

                            if (listAnswer.Length > 1)
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listAnswer[0], "", "", "", "");
                            }
                        }
                        else
                        {
                            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[0].questions[i], listQuiz[0], "", "", "", "");
                        }
                    }
                }
            }
            else
            {
                StopCoroutine("ProcessPlayExam");
                StartCoroutine("ProcessPlayExam");

                m_txtBtnSendAnswer.text = "답변 제출하기";

                m_txtTitleMsg.text = quizData.sets[m_nQuizIndex].dir_cnnt;
                m_txtContentMsg.text = quizData.sets[m_nQuizIndex].qst_brws_cnnt;

                for (int i = 0; i < quizData.sets[m_nQuizIndex].questions.Length; i++)
                {
                    string[] listQuiz = quizData.sets[m_nQuizIndex].questions[i].qst_cnnt.Split("<br />");
                    if (listQuiz.Length > 1)
                    {
                        for (int j = 0; j < listQuiz.Length; j++)
                        {
                            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                            goQuiz.transform.parent = m_goContents.transform;

                            if (listQuiz[j].Contains("{{answers}}"))
                            {
                                string[] listAnswer = listQuiz[j].Split("{{answers}}");
                                string[] listSelector = new string[3];
                                for (int k = 0; k < listSelector.Length; k++)
                                {
                                    listSelector[k] = "";
                                }
                                for (int k = 0; k < quizData.sets[m_nQuizIndex].questions[i].answers.Length; k++)
                                {
                                    listSelector[k] = quizData.sets[m_nQuizIndex].questions[i].answers[k].anwr_cnnt;
                                }

                                if (listAnswer.Length > 1)
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                                }
                                else
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listAnswer[0], "", "", "", "");
                                }
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listQuiz[j], "", "", "", "");
                            }
                        }
                    }
                    else
                    {
                        GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                        goQuiz.transform.parent = m_goContents.transform;

                        if (listQuiz[0].Contains("{{answers}}"))
                        {
                            string[] listAnswer = listQuiz[0].Split("{{answers}}");
                            string[] listSelector = new string[3];
                            for (int k = 0; k < listSelector.Length; k++)
                            {
                                listSelector[k] = "";
                            }
                            for (int k = 0; k < quizData.sets[m_nQuizIndex].questions[i].answers.Length; k++)
                            {
                                listSelector[k] = quizData.sets[m_nQuizIndex].questions[i].answers[k].anwr_cnnt;
                            }

                            if (listAnswer.Length > 1)
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listAnswer[0], "", "", "", "");
                            }
                        }
                        else
                        {
                            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(quizData.sets[m_nQuizIndex].questions[i], listQuiz[0], "", "", "", "");
                        }
                    }
                }
            }
        }
    }

    public void DelListAnswers()
    {
        Component[] listChilds = m_goContents.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goContents.transform)
            {
                Destroy(iter.gameObject);
            }
        }

    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        int nRequestTimer = 0;

        m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            if( !CUIsSpaceScreenLeft.Instance.IsRightQuizActive() )
            {
                yield return new WaitForSeconds(0.1f);
            } else
            {
                //Debug.Log("ProcessPlayExam 01");
                yield return new WaitForSeconds(1f);

                m_nRemainTime--;

                nMin = (int)(m_nRemainTime / 60);
                nSec = (int)(m_nRemainTime % 60);

                m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

                if ((nRequestTimer % 5) == 0)
                {
                    Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("HPTS").part_idx);
                }
                nRequestTimer++;

                if (m_nRemainTime <= 0)
                    break;

            }
        }

        ShowPopupTimeOver();
    }

    public void OnClickExit()
    {
        if (CUIsSpaceScreenLeft.Instance.IsHPTSTutorial())
            ShowPopupToLobbyTutorial();
        else
            ShowPopupToLobby();
    }

    //public void ShowTutorialMsg()
    //{
    //    m_goTutorialMsg.SetActive(true);
    //}

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

    public void OnClickSendAnswer()
    {
        //CUIsSpaceScreenLeft.Instance.SetHPTSTutorial(false);

        if (CUIsSpaceScreenLeft.Instance.IsHPTSTutorial())
        {
            if (m_nTutorialStep == 0)
            {
                m_nTutorialStep++;
                ShowTutorialMsg();
            }
            else
            {
                CUIsSpaceScreenLeft.Instance.SetHPTSTutorial(false);
                InitHPTSPage();
            }
            return;
        }

        if (m_nQuizIndex < 1)
        {
            m_nQuizIndex++;
            InitHPTSPage();
            return;
        }

        OnClickPopupSendAnswerNext();
        //ShowPopupSendAnswer();
    }

    public void HideAllPopup()
    {
        m_goPopupSendAnswer.SetActive(false);
        m_goPopupTimeover.SetActive(false);
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
        HidePopupToLobbyTutorial();
    }

    public void ShowPopupSendAnswer()
    {
        m_goPopupSendAnswer.SetActive(true);
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

        StartCoroutine("ProcessToLobbySendAnswer");
    }

    IEnumerator ProcessToLobbySendAnswer()
    {
        while (true)
        {
            int nRemainTime = m_nRemainTime;
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
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
        }
        else
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
        StopCoroutine("ProcessPlayExam");
        StopCoroutine("ProcessToLobbySendAnswer");
        HideAllPopup();

        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("HPTS").part_idx, 1);
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        CUIsSpaceManager.Instance.FadeOutComputer();
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
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
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
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("HPTS").part_idx, 1);
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }

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
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }

    public void OnClickPopupToLobbyTutorialClose()
    {
        HidePopupToLobbyTutorial();
    }

    // TODO 활동로그 남기기
    public int GetRemainTime()
    {
        return m_nRemainTime;
    }
}
