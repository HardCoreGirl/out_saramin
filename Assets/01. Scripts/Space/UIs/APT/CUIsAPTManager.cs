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
        ShowAPTPage(0);
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
}
