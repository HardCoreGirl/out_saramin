using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectAPTQuizList2 : MonoBehaviour
{
    public GameObject[] m_listQuizList = new GameObject[3];
    public Text[] m_listName = new Text[3];

    private int m_nIndex = 0;
    private int m_nState = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTQuizList2(int nIndex, int nState = 2)
    {
        m_nIndex = nIndex;
        m_nState = nState;
        string strName = "";
        for (int i = 0; i < m_listQuizList.Length; i++)
        {
            m_listQuizList[i].SetActive(true);

            if (nIndex == 0)
                strName = "연습문제";
            else
                strName = nIndex.ToString() + "번 문제";

            m_listName[i].text = strName;
        }

        ShowQuizList(m_nState);
    }

    public void UpdateAPTQuizList2()
    {
        m_nState = CUIsAPTManager.Instance.GetAnswerState(m_nIndex);

        Debug.Log("UpdateAPTQuizList2 : " + m_nIndex + ", " + m_nState);

        ShowQuizList(m_nState);
    }

    public void OnClickQuizList()
    {
        if (CUIsAPTPage2Manager.Instance.IsTutorial() && !CUIsAPTPage2Manager.Instance.IsTutorialWait())
            return;

        if (m_nIndex == 0)
            return;

        if (CUIsAPTPage2Manager.Instance.IsTutorial())
        {
            Debug.Log("일반 모드로 전환");
            CUIsAPTPage2Manager.Instance.SetTutorial(false);
            CUIsAPTPage2Manager.Instance.HideExQuizList();
            CUIsAPTPage2Manager.Instance.StartQuiz();
            // 기본 모드로 시작
        } 
            

        if (m_nState == 2)
            CUIsAPTManager.Instance.SetAnswerState(m_nIndex, 1);

        UpdateAPTQuizList2();

        CUIsAPTPage2Manager.Instance.ShowQuiz(m_nIndex);
    }

    public void ShowQuizList(int nIndex)
    {
        for (int i = 0; i < m_listQuizList.Length; i++)
        {
            m_listQuizList[i].SetActive(false);
        }

        m_listQuizList[nIndex].SetActive(true);
    }
}
