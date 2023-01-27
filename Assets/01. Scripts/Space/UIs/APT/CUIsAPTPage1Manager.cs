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

    public GameObject m_goAnswerContent;

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
        Quiz quizAPT = CQuizData.Instance.GetQuiz("APTD1");
        m_txtAPTD1Cnt.text = quizAPT.sets.Length.ToString() + " ����";
        int nMin = quizAPT.exm_time / 60;
        m_txtAPTD1Time.text = nMin.ToString() + " ��";

        quizAPT = CQuizData.Instance.GetQuiz("APTD2");
        m_txtAPTD2Cnt.text = quizAPT.sets.Length.ToString() + " ����";
        nMin = quizAPT.exm_time / 60;
        m_txtAPTD2Time.text = nMin.ToString() + " ��";

        for (int i = 0; i < 29; i++)
        {
            GameObject goList = Instantiate(Resources.Load("Prefabs/APTQuizList01") as GameObject);
            goList.transform.parent = m_goAnswerContent.transform;
            string strQuizName = "";
            string strQuizState = "";
            if (i == 0)
            {
                strQuizName = "���� ����";
            }
            else
            {
                strQuizName = i.ToString() + "�� ����";
            }

            if (CUIsAPTManager.Instance.GetAnswerState(i) == 0)
            {
                strQuizState = "Ȯ�� ���";
            } else if (CUIsAPTManager.Instance.GetAnswerState(i) == 1)
            {
                strQuizState = "������";
            }
            else
            {
                strQuizState = "�Ϸ�";
            }
            goList.GetComponent<CObjectAPTQuizList>().InitAPTQuizList(i, strQuizName, strQuizState);
        }
        
    }

    public void OnClickPlayQuiz()
    {
        CUIsAPTManager.Instance.ShowAPTPage(1);
    }

    public void OnClickExit()
    {
        CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);
    }
}
