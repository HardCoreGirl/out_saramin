using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using System.Runtime.InteropServices;


public class CUIsTitleManager : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern string getCookie(string cname);
    [DllImport("__Internal")]
    public static extern void setToken(string strKey, string strValue);
    [DllImport("__Internal")]
    public static extern string getToken(string strKey);
    [DllImport("__Internal")]

    public static extern void setTokenTest(string strKey, string strValue);
    [DllImport("__Internal")]
    public static extern string getTokenTest(string strKey);

    [DllImport("__Internal")]
    public static extern void showAlert(string strMsg);

    public TMPro.TMP_InputField m_ifToken;
    // Start is called before the first frame update

    public TMPro.TMP_InputField m_ifTest;

    public GameObject m_goDebug;

    void Start()
    {
        //m_ifToken.text = "DEV2";
        //CSpaceAppEngine.Instance.SetServerType(m_ifToken.text);

        //if( CSpaceAppEngine.Instance.GetBuildType() != 0 )
        //{
        //    m_goDebug.GetComponent<RectTransform>().localPosition = new Vector3(9999f, 9999f, 1f);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlayTest()
    {
        //if (m_ifToken.text.Equals(""))
        //{
        //    CSpaceAppEngine.Instance.SetServerType("DEV2");
        //}

        
        //if(PlayerPrefs.GetInt("FinishIntro", 0) == 1)
        //{
        //    CSpaceAppEngine.Instance.SetIsIntro(false);
        //}

        Debug.Log("OnClickPlayTest : " + CSpaceAppEngine.Instance.GetServerType());

        Debug.Log("FinishIntro : " + PlayerPrefs.GetInt("FinishIntro", 0));

        

        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            //Application.Quit();


            CSpaceAppEngine.Instance.StartTest();
            Server.Instance.RequestGETQuestions(0);
            Server.Instance.RequestGETGuides(0);
            CUIsSpaceManager.Instance.HideTitle();
            CUIsSpaceManager.Instance.ShowLobby();
            //CUIsSpaceManager.Instance.ShowTodo();
            CUIsSpaceManager.Instance.ShowComputers();

            if (CSpaceAppEngine.Instance.IsIntro())
            {
                CUIsSpaceManager.Instance.ShowIntro();
                //CUIsSpaceManager.Instance.ShowOutro();
            }
            else
                CUIsSpaceManager.Instance.ScreenActive(false, true);
            return;
        }

        //string strURL = Application.absoluteURL;
        //string[] listURL = strURL.Split("?token=");

#if UNITY_EDITOR
        string strToken = CSpaceAppEngine.Instance.GetToken();
#else
        string strToken = getToken("accessToken");
        if( strToken.Equals("") )
        {
            strToken = CSpaceAppEngine.Instance.GetToken();
            //string strURL = Application.absoluteURL;
            //string[] listURL = strURL.Split("?token=");
            //if (listURL.Length > 1)
            //{
            //    setToken("accessToken", listURL[1]);
            //}
            //showAlert("Auth Fail..");
            //return;
        }
#endif
        Server.Instance.SetToken(strToken);
        Server.Instance.RequestTestCheck();
        //Server.Instance.RequestTestInvest();
        Server.Instance.RequestGETInfoExams(true);
        Server.Instance.ReuquestGETInfoMissions();
        //CUIsSpaceManager.Instance.HideTitle();

        //if (CSpaceAppEngine.Instance.IsIntro())
        //    CUIsSpaceManager.Instance.ShowIntro();
        //else
        //    CUIsSpaceManager.Instance.ScreenActive(false, true);
    }

    public void OnClickServer(int nIndex)
    {
        if(nIndex == 0)
        {
            m_ifToken.text = "LOCAL";

            //CSpaceAppEngine.Instance.SetS
        } else if (nIndex == 1)
        {
            m_ifToken.text = "DEV2";
        }

        CSpaceAppEngine.Instance.SetServerType(m_ifToken.text);
    }


    public void OnClickSetToken()
    {
        setToken("testToken", "test_gurrendan");
        //setTokenTest("testToken", "test_gurrendan");
    }

    public void OnClickGetToken()
    {
        Debug.Log("Get Token : " + getToken("testToken"));
        //getTokenTest("testToken");
    }

    //public void OnChangeInputFieldTest()
    //{
    //    if(m_ifTest.text != null)
    //        Debug.Log("OnChangeInputFieldTest Test Input Field : " + m_ifTest.text);
    //}
}
