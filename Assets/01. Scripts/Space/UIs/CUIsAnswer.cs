using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAnswer : MonoBehaviour
{
    public GameObject m_goSelector;
    public GameObject[] m_listBtnSelector = new GameObject[4];

    public GameObject m_goSelected;
    public GameObject m_goSelectedResult;

    public int m_nSetIndex;
    public int m_nQuizIndex;

    private Answers[] m_listAnswer;
    private int m_nSelectIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAnswer(int nSetIndex, int nQuizIndex, bool bTutorial = false)
    {
        m_nSetIndex = nSetIndex;
        m_nQuizIndex = nQuizIndex;

        Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", bTutorial);

        m_goSelected.SetActive(false);
        m_goSelector.SetActive(true);

        if (bTutorial)
            m_listAnswer = quizRQT.sets[nSetIndex].questions[nQuizIndex].answers;
        else
        {
            //m_listAnswer = quizRQT.sets[nQuizIndex].questions[0].answers;

            int[] listAnswerIndex = new int[quizRQT.sets[nQuizIndex].questions[0].answers.Length];

            for (int i = 0; i < listAnswerIndex.Length; i++)
            {
                listAnswerIndex[i] = quizRQT.sets[nQuizIndex].questions[0].answers[i].anwr_idx;
            }

            m_listAnswer = new Answers[listAnswerIndex.Length];

            System.Array.Sort(listAnswerIndex);
            System.Array.Reverse(listAnswerIndex);

            for (int i = 0; i < listAnswerIndex.Length; i++)
            {
                Debug.Log("RQT Init Answer : " + listAnswerIndex[i]);
                for(int j = 0; j < listAnswerIndex.Length; j++)
                {
                    if(listAnswerIndex[i] == quizRQT.sets[nQuizIndex].questions[0].answers[j].anwr_idx)
                    {
                        m_listAnswer[i] = quizRQT.sets[nQuizIndex].questions[0].answers[j];
                        break;
                    }
                }
            }
        }


        for (int i = 0; i < m_listBtnSelector.Length; i++)
        {
            if( i >= m_listAnswer.Length)
            {
                m_listBtnSelector[i].SetActive(false);
                continue;
            }

            //Debug.Log("Button : " + m_listAnswer[i].anwr_cnnt + ", " + m_listBtnSelector[i].GetComponentInChildren<Text>().preferredWidth);
            m_listBtnSelector[i].GetComponentInChildren<Text>().text = m_listAnswer[i].anwr_cnnt;
            var rectSize = m_listBtnSelector[i].GetComponent<RectTransform>().sizeDelta;
            rectSize.x = m_listBtnSelector[i].GetComponentInChildren<Text>().preferredWidth + 32;
            m_listBtnSelector[i].GetComponent<RectTransform>().sizeDelta = rectSize;
        }
    }

    public void OnClickAnswer(int nIndex)
    {
        if (!CUIsSpaceScreenLeft.Instance.IsRQTTutorial())
        {
            Quiz quizRQT = CQuizData.Instance.GetQuiz("RQT", CUIsSpaceScreenLeft.Instance.IsRQTTutorial());

            // TODO 활동로그 남기기
            CSpaceAppEngine.Instance.SetPage("RQT");
            Server.Instance.RequestPostAnswerUpdateTime(quizRQT.sets[m_nQuizIndex].questions[0].test_qst_idx, CUIsSpaceScreenLeft.Instance.GetRemainTime());

            // TODO 로그 확장
            //Server.Instance.RequestPUTAnswerObject(quizRQT.sets[m_nQuizIndex].questions[0].test_qst_idx, m_listAnswer[nIndex].anwr_idx);
            Server.Instance.RequestPUTAnswerObject(quizRQT.sets[m_nQuizIndex].questions[0].test_qst_idx, m_listAnswer[nIndex].anwr_idx, quizRQT.sets[m_nQuizIndex].questions[0].qst_idx);

            Debug.Log("Exam Cnt : " + quizRQT.sets.Length + ", QuizIndex : " + m_nQuizIndex);

            if (m_nQuizIndex >= quizRQT.sets.Length - 1)
            {
                // TODO : 시험 종료
                //Debug.Log("Finish Exam");
                CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("RQT").part_idx);
                CUIsSpaceManager.Instance.HideLeftPage();
                CUIsSpaceManager.Instance.FadeOutComputer();
                return;
             }
        }

        m_nSelectIndex = nIndex;
        m_goSelected.SetActive(true);
        m_goSelector.SetActive(false);

        m_goSelectedResult.GetComponentInChildren<Text>().text = m_listAnswer[m_nSelectIndex].anwr_cnnt;
        var rectSize = m_goSelectedResult.GetComponent<RectTransform>().sizeDelta;
        rectSize.x = m_goSelectedResult.GetComponentInChildren<Text>().preferredWidth + 32;
        m_goSelectedResult.GetComponent<RectTransform>().sizeDelta = rectSize;

        if( CUIsSpaceScreenLeft.Instance.IsRQTTutorial() )
        {
            if (m_nQuizIndex >= 3)
            {
                CUIsSpaceScreenLeft.Instance.DelQuiz();
                CUIsSpaceScreenLeft.Instance.InitRQTQuiz(false);
                CUIsSpaceScreenLeft.Instance.ShowQuiz(0, 0, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
                return;
            }
            else if (m_nQuizIndex == 2)
            {
                if (nIndex == 0)
                {
                    CUIsSpaceScreenLeft.Instance.DelQuiz();
                    CUIsSpaceScreenLeft.Instance.InitRQTQuiz(false);
                    CUIsSpaceScreenLeft.Instance.ShowQuiz(0, 0, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
                    return;
                }
            }
        }

        if (CUIsSpaceScreenLeft.Instance.GetLastQuizIndex() <= m_nQuizIndex)
        {
            CUIsSpaceScreenLeft.Instance.SetLastQuizIndex(m_nQuizIndex + 1);
            int nNextQuizIndex = m_nQuizIndex + 1;
            if( (nNextQuizIndex % 4) == 0 )
            {
                CUIsSpaceScreenLeft.Instance.DelQuiz();
            }
            CUIsSpaceScreenLeft.Instance.ShowQuiz(m_nSetIndex, m_nQuizIndex + 1, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
        }
    }

    public void OnClickReset()
    {
        m_goSelected.SetActive(false);
        m_goSelector.SetActive(true);
    }
}
