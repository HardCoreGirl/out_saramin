using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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

    public GameObject m_goUITitle;

    public GameObject m_goIntro;

    public GameObject m_goLobby;
    public GameObject m_goLeftPage;
    public GameObject m_goCenterPage;
    public GameObject m_goRightPage;

    public GameObject m_goOutro;

    public bool m_bIsActive = false;

    public GameObject m_goUICommonPopupsFinish;

    public Text m_txtAuthMsg;
    public GameObject m_goAuthFail;

    // Start is called before the first frame update
    void Start()
    {
        HideAllPage();
        HideAllCommonPopups();
        ShowTitle();
        ScreenActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideAllPage()
    {
        HideLobby();
        HideLeftPage();
        HideCenterPage();
        HideRightPage();
        HideIntro();
        HideOutro();
    }

    public void ShowLobby()
    {
        m_goLobby.SetActive(true);
    }

    public void HideLobby()
    {
        m_goLobby.SetActive(false);
    }

    public void ShowLeftPage()
    {
        m_goLeftPage.SetActive(true);
        m_goLeftPage.GetComponent<CUIsSpaceScreenLeft>().InitUIs();
    }

    public void HideLeftPage()
    {
        //ScreenActive(false);
        m_goLeftPage.SetActive(false);
    }

    public void ShowCenterPage()
    {
        m_goCenterPage.SetActive(true);

        m_goCenterPage.GetComponent<CUIsSpaceScreenCenter>().InitLGTKManager();


    }

    public void HideCenterPage()
    {
        //ScreenActive(false);
        m_goCenterPage.SetActive(false);
    }

    public void ShowRightPage()
    {
        m_goRightPage.SetActive(true);
        m_goRightPage.GetComponent<CUIsAPTManager>().InitAPTPage();
    }

    public void HideRightPage()
    {
        //ScreenActive(false);
        m_goRightPage.SetActive(false);
    }

    public void ShowOutro()
    {
        m_goOutro.SetActive(true);
        m_goOutro.GetComponent<CUIsOutroManager>().InitUIs();
    }

    public void HideOutro()
    {
        m_goOutro.SetActive(false);
    }

    public void ShowTitle()
    {
        HideAllPage();
        m_goUITitle.SetActive(true);
    }

    public void HideTitle()
    {
        m_goUITitle.SetActive(false);
    }

    public void ScreenActive(bool bActive, bool bDeley = false)
    {
        Debug.Log("ScreenActive : " + bActive);
        if( bActive == false && bDeley == true )
        {
            StartCoroutine("ProcessScreenActiveFalse");
            return;
        }    

        m_bIsActive = bActive;
    }

    IEnumerator ProcessScreenActiveFalse()
    {
        Debug.Log("ScreenActive False");
        yield return new WaitForSeconds(0.5f);
        m_bIsActive = false;
    }

    public bool IsScreenActive()
    {
        return m_bIsActive;
    }

    public void HideAllCommonPopups()
    {
        HideCommonPopupsFinish();
    }

    public void ShowCommonPopupsFinish(int nPartIndex, int nType = 0)
    {
        m_goUICommonPopupsFinish.SetActive(true);
        m_goUICommonPopupsFinish.GetComponent<CUIsCommonPopupFinish>().InitCommonPopupFinish(nPartIndex, nType);
    }

    public void HideCommonPopupsFinish()
    {
        m_goUICommonPopupsFinish.SetActive(false);
    }

    public void OnClickCommonPopupFinish()
    {
        HideCommonPopupsFinish();
        ScreenActive(false);
    }

    public void ShowIntro()
    {
        CSpaceAppEngine.Instance.StartIntro();
        m_goIntro.SetActive(true);
        m_goIntro.GetComponent<CUIsIntroManager>().InitIntro();
    }

    public void HideIntro()
    {
        m_goIntro.SetActive(false);
    }

    public void UpdateAuthMsg(string strMsg)
    {
        m_txtAuthMsg.text = strMsg;
    }    

    public void ShowAuthFail()
    {
        m_goAuthFail.SetActive(true);
    }

    public void HideAuthFail()
    {
        m_goAuthFail.SetActive(false);
    }
}
