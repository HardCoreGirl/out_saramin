using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsSpaceManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsSpaceManager _instance = null;

    public static CUIsSpaceManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsSpaceManager install null");

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

    public GameObject m_goLeftPage;
    public GameObject m_goCenterPage;
    public GameObject m_goRightPage;

    public bool m_bIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        HideAllPage();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllPage()
    {
        HideLeftPage();
        HideCenterPage();
        HideRightPage();
    }

    public void ShowLeftPage()
    {
        m_goLeftPage.SetActive(true);
        m_goLeftPage.GetComponent<CUIsSpaceScreenLeft>().InitUIs();
    }

    public void HideLeftPage()
    {
        ScreenActive(false);
        m_goLeftPage.SetActive(false);
    }

    public void ShowCenterPage()
    {
        m_goCenterPage.SetActive(true);
    }

    public void HideCenterPage()
    {
        ScreenActive(false);
        m_goCenterPage.SetActive(false);
    }

    public void ShowRightPage()
    {
        m_goRightPage.SetActive(true);
    }

    public void HideRightPage()
    {
        ScreenActive(false);
        m_goRightPage.SetActive(false);
    }

    public void ScreenActive(bool bActive)
    {
        m_bIsActive = bActive;
    }

    public bool IsScreenActive()
    {
        return m_bIsActive;
    }
}
