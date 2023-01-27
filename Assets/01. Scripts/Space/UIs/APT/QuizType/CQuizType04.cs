using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuizType04 : MonoBehaviour
{
    private int m_nType;
    private int m_nIndex;
    private int m_nQuizListIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitQuizType(int nType, int nIndex, int nQuizListIndex)
    {
        m_nType = nType;
        m_nIndex = nIndex;
        m_nQuizListIndex = nQuizListIndex;

        string strKey = "APTD1";
        if (m_nType == 1)
            strKey = "APTD2";

        Quiz quizAPT = CQuizData.Instance.GetQuiz(strKey);
        //quizAPT.sets[nIndex].questions[0].qst_ans_cnt  // 문제 URL



        for (int i = 0; i < quizAPT.sets[nIndex].questions[0].answers.Length; i++)
        {
            // 정답 URL
            //quizAPT.sets[nIndex].questions[0].answers[i].anwr_cnnt
        }

        // CUIsRATManager 스프라이트 참조
    }

    public void OnClickAnswer(int nIndex)
    {
        Debug.Log("OnClickAnswer : " + nIndex);
        CUIsAPTManager.Instance.SetAnswerState(m_nQuizListIndex, 0);
        CUIsAPTPage2Manager.Instance.UpdateQuizList(m_nQuizListIndex);
    }
}
