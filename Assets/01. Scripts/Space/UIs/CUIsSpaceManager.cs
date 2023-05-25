using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

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

    public GameObject m_goLobbyObject;
    public GameObject m_goUITitle;

    public GameObject m_goIntro;

    public GameObject m_goLobby;
    public GameObject m_goLeftPage;
    public GameObject m_goCenterPage;
    public GameObject m_goRightPage;

    public GameObject m_goTodo;
    public GameObject m_goComputers;

    public GameObject m_goOutro;

    public bool m_bIsActive = false;

    public GameObject m_goUICommonPopupsFinish;

    public Text m_txtAuthMsg;
    public GameObject m_goAuthFail;

    private bool m_bIsCenterFirst = true;
    private bool m_bIsRightFirst = true;

    private bool m_bIsPlayFadein = false;

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
        HideTodo();
        HideComputers();
    }

    public void HideAllPageFadeOut()
    {
        HideAllPage();
        FadeOutComputer();
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
        m_bIsPlayFadein = false;
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
        FadeOutComputer();
    }

    public void ShowRightPage()
    {
        m_goRightPage.SetActive(true);
        m_bIsRightFirst = false;
        m_goRightPage.GetComponent<CUIsAPTManager>().InitAPTPage();
    }

    public void HideRightPage()
    {
        //ScreenActive(false);
        m_goRightPage.SetActive(false);
    }

    public void SetRightFirst(bool bIsRightFirst)
    {
        m_bIsRightFirst=bIsRightFirst; 
    }

    public bool IsRightFirst()
    {
        return m_bIsRightFirst;
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

    public void ShowTodo()
    {
        m_goTodo.SetActive(true);
        m_goTodo.GetComponent<CUIsTodoManager>().InitUIs();

    }

    public void HideTodo()
    {
        m_goTodo.SetActive(false);
    }

    public void ShowComputers()
    {
        m_goComputers.SetActive(true);
    }

    public void HideComputers()
    {
        m_goComputers.SetActive(false);
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

    public void OnClickLeftComputer()
    {
        if (!CSpaceAppEngine.Instance.IsActiveLeft())
            return;

        if (CSpaceAppEngine.Instance.IsFinishLeft01() && CSpaceAppEngine.Instance.IsFinishLeft02())
            return;

        if (m_bIsPlayFadein)
            return;

        m_bIsPlayFadein = true;

        CUIsSpaceManager.Instance.ScreenActive(true);

        //Debug.Log("OnClickLeftComputer 00");

        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("WAITING"))
            {
                //Debug.Log("OnClickLeftComputer 01");
                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
            }
            if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
            {
                //Debug.Log("OnClickLeftComputer 02");
                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
            }
            if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
            {
                //Debug.Log("OnClickLeftComputer 03");
                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
            }
            if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
            {
                //Debug.Log("OnClickLeftComputer 04");
                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            }

            Server.Instance.RequestGETInfoExams();
        }

        //CUIsSpaceManager.Instance.ShowLeftPage();
        FadeInLeftComputer();
    }

    public void FadeInLeftComputer()
    {
        m_goLobbyObject.transform.DOMove(new Vector3(32.9f, 16.6f, 0), 1f);
        m_goLobbyObject.transform.DOScale(new Vector3(5f, 5f, 5f), 1f).OnComplete(ShowLeftPage);
    }

    public void OnClickCenterComputer()
    {
        if (!CSpaceAppEngine.Instance.IsActiveCenter())
            return;

        if (CSpaceAppEngine.Instance.IsFinishCenter())
            return;

        if (m_bIsPlayFadein)
            return;

        m_bIsPlayFadein = true;
        //CUIsSpaceManager.Instance.ScreenActive(true);

        //if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        //{
        //    //Server.Instance.RequestGETQuestions(0);
        //    CUIsSpaceManager.Instance.ShowCenterPage();
        //    return;
        //}
        //else
        //{
        //    if (!CQuizData.Instance.GetExamInfoDetail("LGTK").status.Equals("WAITING"))
        //        Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
        //}

        //Server.Instance.RequestGETInfoExams();

        FadeInCenterComputer();
        //CUIsSpaceManager.Instance.ShowCenterPage();
    }

    public void FadeInCenterComputer()
    {
        m_goLobbyObject.transform.DOMove(new Vector3(-0.03f, 5.31f, 0), 1f);
        m_goLobbyObject.transform.DOScale(new Vector3(2.58f, 2.58f, 2.58f), 1f).OnComplete(ShowCenterComputer);
    }

    public void ShowCenterComputer()
    {
        m_bIsPlayFadein = false;

        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            //Server.Instance.RequestGETQuestions(0);
            CUIsSpaceManager.Instance.ShowCenterPage();
            return;
        }
        else
        {
            if (!CQuizData.Instance.GetExamInfoDetail("LGTK").status.Equals("WAITING"))
                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
        }

        Server.Instance.RequestGETInfoExams();

        CUIsSpaceManager.Instance.ShowCenterPage();
    }

    public void OnClickRightComputer()
    {
        //FadeInRightComputer();
        //return;

        if (!CSpaceAppEngine.Instance.IsActiveRight())
            return;

        if (CSpaceAppEngine.Instance.IsFinishRight())
            return;

        if (m_bIsPlayFadein)
            return;
        m_bIsPlayFadein = true;

        //CUIsSpaceManager.Instance.ScreenActive(true);

        if (m_bIsRightFirst)
        {
            if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                Server.Instance.RequestGETQuestions(0);
                CUIsSpaceManager.Instance.ShowRightPage();
            }
            else
            {
                if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING"))
                {
                    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
                    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                } else if(CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("TAE_FSH") )
                {
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                }
                else
                {
                    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
                    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                }

            }
        } else
        {
            //CUIsAPTManager.Instance.ShowPage();
            FadeInRightComputer();
        }
    }

    public void FadeInRightComputer()
    {
        m_goLobbyObject.transform.DOMove(new Vector3(-33.2f, 16.7f, 0), 1f);
        m_goLobbyObject.transform.DOScale(new Vector3(5f, 5f, 5f), 1f).OnComplete(ShowRightComputer);
    }

    public void FadeOutComputer()
    {
        m_goLobbyObject.transform.DOMove(new Vector3(0, 0, 0), 1f);
        m_goLobbyObject.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
    }

    public void ShowRightComputer()
    {
        m_bIsPlayFadein = false;

        if (m_bIsRightFirst)
        {
            CUIsSpaceManager.Instance.ShowRightPage();
        }
        else
        {
            CUIsAPTManager.Instance.ShowPage();
        }
    }
}
