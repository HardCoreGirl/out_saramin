using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAPTPage2Manager : MonoBehaviour
{
    #region SingleTon
    public static CUIsAPTPage2Manager _instance = null;

    public static CUIsAPTPage2Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsAPTPage2Manager install null");

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

    public GameObject[] m_listQuizBoard = new GameObject[9];

    public GameObject m_goQuizListContent;

    private GameObject[] m_listQuizList = new GameObject[29];

    public Text m_txtQuizTitle;

    // Start is called before the first frame update
    void Start()
    {
        InitAPTPage2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTPage2()
    {
        ShowQuizBoard(0);

        for (int i = 0; i < 29; i++)
        {
            m_listQuizList[i] = Instantiate(Resources.Load("Prefabs/APTQuizList02") as GameObject);
            m_listQuizList[i].transform.parent = m_goQuizListContent.transform;
            m_listQuizList[i].GetComponent<CObjectAPTQuizList2>().InitAPTQuizList2(i);
        }
    }

    public void ShowQuiz(int nIndex)
    {
        Debug.Log("Show Quiz : " + nIndex);

        int nRealIndex = nIndex;
        if(nIndex == 0)
        {
            // 연습문제
            Debug.Log("연습문제");
            return;
        }
        nRealIndex--;
        Quiz quizAPT = CQuizData.Instance.GetQuiz("APTD1");
        if (nRealIndex < quizAPT.sets.Length)
        {
            Debug.Log("APTD1 문제 Type : " + quizAPT.sets[nRealIndex].questions[0].qst_exos_cd);
            m_txtQuizTitle.text = quizAPT.sets[nRealIndex].dir_cnnt;
            // APTD1 문제
            if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_A"))
            {
                ShowQuizBoard(1);
                m_listQuizBoard[1].GetComponent<CAPTQuizManager>().InitQuizType(0, nRealIndex, nIndex);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_B"))
            {
                ShowQuizBoard(2);
                m_listQuizBoard[2].GetComponent<CAPTQuizManager>().InitQuizType(0, nRealIndex, nIndex);

            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_C"))
            {
                ShowQuizBoard(3);
                m_listQuizBoard[3].GetComponent<CQuizType04>().InitQuizType(0, nRealIndex, nIndex);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_D"))
            {
                ShowQuizBoard(4);
            } else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_E"))
            {
                ShowQuizBoard(5);
            } else {
                Debug.Log("알수 없는 타입");
            }
            return;
        }
        nRealIndex -= quizAPT.sets.Length;

        Quiz quizAPT2 = CQuizData.Instance.GetQuiz("APTD2");
        if( nRealIndex < quizAPT2.sets.Length)
        {
            m_txtQuizTitle.text = quizAPT2.sets[nRealIndex].dir_cnnt;
            //Debug.Log("APTD2 문제");
            if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_A"))
            {
                ShowQuizBoard(1);

                m_listQuizBoard[1].GetComponent<CAPTQuizManager>().InitQuizType(1, nRealIndex, nIndex);
            }
            else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_B"))
            {
                ShowQuizBoard(2);
                m_listQuizBoard[2].GetComponent<CAPTQuizManager>().InitQuizType(1, nRealIndex, nIndex);
            }
            else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_C"))
            {
                ShowQuizBoard(3);
            }
            else if (quizAPT.sets[nRealIndex].questions[0].qst_exos_cd.Equals("FORM_D"))
            {
                ShowQuizBoard(4);
            }
            else
            {
                Debug.Log("알수 없는 타입");
            }
            return;
        }

        m_txtQuizTitle.text = "문제 데이터가 존재하지 않음";
        Debug.Log("해당데이터 없음");
    }

    public void UpdateQuizList(int nIndex)
    {
        m_listQuizList[nIndex].GetComponent<CObjectAPTQuizList2>().UpdateAPTQuizList2();
    }

    public void OnClickExit()
    {
        CUIsAPTManager.Instance.ShowPopup(2);
        //CUIsSpaceManager.Instance.ScreenActive(false);
        //gameObject.SetActive(false);
    }

    public void OnClickSendAnswer()
    {
        CUIsAPTManager.Instance.ShowPopup(0);
    }

    public void ShowQuizBoard(int nIndex)
    {
        for (int i = 0; i < m_listQuizBoard.Length; i++)
            HideQuizBoard(i);

        m_listQuizBoard[nIndex].gameObject.SetActive(true);
    }

    public void HideQuizBoard(int nIndex)
    {
        m_listQuizBoard[nIndex].gameObject.SetActive(false);
    }
}
