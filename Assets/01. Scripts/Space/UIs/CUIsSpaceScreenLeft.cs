using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsSpaceScreenLeft : MonoBehaviour
{
    #region SingleTon
    public static CUIsSpaceScreenLeft _instance = null;

    public static CUIsSpaceScreenLeft Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsSpaceScreenLeft install null");

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

    public GameObject[] m_listPage = new GameObject[2];

    public GameObject[] m_listQuiz = new GameObject[6];

    private int m_nOpenQuiz;

    // Start is called before the first frame update
    void Start()
    {
        ShowPage(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose()
    {
        CUIsSpaceManager.Instance.HideLeftPage();
    }

    public void OnClickMission(int nIndex)
    {
        ShowPage(1);
        HideAllQuiz();
        ShowQuiz(0);
    }

    public void HideAllPages()
    {
        for(int i = 0; i < m_listPage.Length; i++)
        {
            HidePage(i);
        }
    }

    public void ShowPage(int nPage)
    {
        HideAllPages();
        m_listPage[nPage].SetActive(true);
    }

    public void HidePage(int nPage)
    {
        m_listPage[nPage].SetActive(false);
    }

    public void HideAllQuiz()
    {
        for (int i = 0; i < m_listQuiz.Length; i++)
        {
            HideQuiz(i);
        }
    }

    public void ShowQuiz(int nIndex)
    {
        m_nOpenQuiz = nIndex;
        m_listQuiz[nIndex].SetActive(true);
    }

    public void HideQuiz(int nIndex)
    {
        m_listQuiz[nIndex].SetActive(false);
    }
}
