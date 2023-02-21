using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAPTPage1Manager : MonoBehaviour
{
    public Text m_txtAPTD1Cnt;
    public Text m_txtAPTD1Time;
    public Text m_txtAPTD2Cnt;
    public Text m_txtAPTD2Time;

    public Text m_txtAnswerCnt;

    public GameObject m_goAnswerContent;

    public Toggle m_toggleAgree;
    public GameObject m_goBtnPlay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitAPTPage()
    {
        UpdateButtonPlay();

        Quiz quizAPT = CQuizData.Instance.GetQuiz("APTD1");
        m_txtAPTD1Cnt.text = quizAPT.sets.Length.ToString() + " 문항";
        int nMin = quizAPT.exm_time / 60;
        m_txtAPTD1Time.text = nMin.ToString() + " 분";

        quizAPT = CQuizData.Instance.GetQuiz("APTD2");
        m_txtAPTD2Cnt.text = quizAPT.sets.Length.ToString() + " 문항";
        nMin = quizAPT.exm_time / 60;
        m_txtAPTD2Time.text = nMin.ToString() + " 분";

        int nFinishAnswerCnt = 0;

        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING") || CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("TAE"))
            {
                //Debug.Log("OnClickPlayQuiz Index : " + i + ", Answer : " + CQuizData.Instance.GetQuiz("APTD1").sets[i].questions[0].test_answers[0].test_anwr_idx);
                for (int i = 0; i < CQuizData.Instance.GetQuiz("APTD1").sets.Length; i++)
                {
                    if (CQuizData.Instance.GetQuiz("APTD1").sets[i].questions[0].test_answers[0].test_anwr_idx != 0)
                    {
                        CUIsAPTManager.Instance.SetAnswerState(i, 0);
                        nFinishAnswerCnt++;
                    }
                }
            }
        }

        m_txtAnswerCnt.text = "적응\n테스트\n" + nFinishAnswerCnt.ToString() + "/28";

        for (int i = 0; i < 28; i++)
        {
            GameObject goList = Instantiate(Resources.Load("Prefabs/APTQuizList01") as GameObject);
            goList.transform.parent = m_goAnswerContent.transform;
            string strQuizName = "";
            string strQuizState = "";
            //if (i == 0)
            //{
            //    strQuizName = "연습 문제";
            //    strQuizState = "확인 요망";
            //}
            //else
            //{
                strQuizName = (i + 1).ToString() + "번 문제";

                if (CUIsAPTManager.Instance.GetAnswerState(i) == 0)
                {
                    strQuizState = "완료";
                }
                else if (CUIsAPTManager.Instance.GetAnswerState(i) == 1)
                {
                    strQuizState = "진행중";
                }
                else
                {
                    strQuizState = "확인요망";
                }
            //}

            goList.GetComponent<CObjectAPTQuizList>().InitAPTQuizList(i, strQuizName, strQuizState);
        }
        
    }

    public void OnClickPlayQuiz()
    {
        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
            }

            if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING") || CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("TAE"))
            {
                Debug.Log("APTD1 Active");
                //Debug.Log("OnClickPlayQuiz Index : " + i + ", Answer : " + CQuizData.Instance.GetQuiz("APTD1").sets[i].questions[0].test_answers[0].test_anwr_idx);
                for (int i = 0; i < CQuizData.Instance.GetQuiz("APTD1").sets.Length; i++)
                {
                    if (CQuizData.Instance.GetQuiz("APTD1").sets[i].questions[0].test_answers[0].test_anwr_idx != 0)
                    {
                        CUIsAPTManager.Instance.SetAnswerState(i, 0);
                    }
                }
            }
            else if (CQuizData.Instance.GetExamInfoDetail("APTD2").status.Equals("WAITING") || CQuizData.Instance.GetExamInfoDetail("APTD2").status.Equals("TAE"))
            {
                Debug.Log("APTD2 Active");
                for (int i = 0; i < CQuizData.Instance.GetQuiz("APTD2").sets.Length; i++)
                {
                    if (CQuizData.Instance.GetQuiz("APTD2").sets[i].questions[0].test_answers[0].test_anwr_idx != 0)
                    {
                        CUIsAPTManager.Instance.SetAnswerState(i, 0);
                    }
                }
            }
        }

        CUIsAPTManager.Instance.ShowAPTPage(1);
    }

    public void OnClickExit()
    {
        CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);
    }

    public void OnChangeToggle()
    {
        UpdateButtonPlay();
    }

    public void UpdateButtonPlay()
    {
        if (m_toggleAgree.isOn)
        {
            m_goBtnPlay.GetComponent<Button>().enabled = true;
            m_goBtnPlay.GetComponent<Image>().color = new Color(0, 0.5215687f, 1f);
        }
        else
        {
            m_goBtnPlay.GetComponent<Button>().enabled = false;
            m_goBtnPlay.GetComponent<Image>().color = new Color(0.7372549f, 0.8431373f, 1f);
        }
    }
}
