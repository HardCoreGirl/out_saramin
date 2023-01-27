using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsAPTManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsAPTManager _instance = null;

    public static CUIsAPTManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsAPTManager install null");

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

    public GameObject[] m_listAPTPage = new GameObject[2];

    public GameObject[] m_listPopup = new GameObject[4];

    private int[] m_listAnswerState = new int[29];

    private int m_nAPT1Cnt = 0;
    private int m_nAPT2Cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InitAPTPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTPage()
    {
        HideAllPopup();

        ShowAPTPage(0);

        for(int i = 0; i < m_listAnswerState.Length; i++)
        {
            m_listAnswerState[i] = 2;
        }
    }

    public void SetAnswerState(int nIndex, int nState)
    {
        m_listAnswerState[nIndex] = nState;
    }

    public int GetAnswerState(int nIndex)
    {
        return m_listAnswerState[nIndex];
    }

    public void ShowAPTPage(int nIndex)
    {
        HideAllAPTPage();
        m_listAPTPage[nIndex].SetActive(true);

        if(nIndex == 0)
        {
            m_listAPTPage[nIndex].GetComponent<CUIsAPTPage1Manager>().InitAPTPage();
        }
    }

    public void HideAllAPTPage()
    {
        for(int i = 0; i < m_listAPTPage.Length; i++)
        {
            m_listAPTPage[i].SetActive(false);
        }
    }

    public int GetAPTCnt(int nType)
    {
        string strKey = "APTD1";
        if (nType == 1)
            strKey = "APTD2";
        Quiz quizAPT = CQuizData.Instance.GetQuiz(strKey);

        return quizAPT.sets.Length;


    }

    public void HideAllPopup()
    {
        for (int i = 0; i < m_listPopup.Length; i++)
            HidePopup(i);
    }

    public void ShowPopup(int nIndex)
    {
        m_listPopup[nIndex].SetActive(true);
    }

    public void HidePopup(int nIndex)
    {
        m_listPopup[nIndex].SetActive(false);
    }


    public void OnClickPopupSendAnswerSend()
    {
        HideAllPopup();
        CUIsSpaceManager.Instance.ScreenActive(false);
        gameObject.SetActive(false);
    }
    public void OnClickPopupSendAnswerExit()
    {
        HideAllPopup();
    }

    public void OnClickPopupToLobbyToLobby()
    {
        HideAllPopup();
        CUIsSpaceManager.Instance.ScreenActive(false);
        gameObject.SetActive(false);
    }

    public void OnClickPopupToLobbyExit()
    {
        HideAllPopup();
    }
}
