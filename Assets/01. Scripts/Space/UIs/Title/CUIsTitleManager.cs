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
    void Start()
    {
        //m_ifToken.text = "DEV2";
        //CSpaceAppEngine.Instance.SetServerType(m_ifToken.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlayTest()
    {
        if (m_ifToken.text.Equals(""))
        {
            CSpaceAppEngine.Instance.SetServerType("DEV2");
        }

        if(CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            Server.Instance.RequestGETQuestions(0);
            CUIsSpaceManager.Instance.HideTitle();
            CUIsSpaceManager.Instance.ScreenActive(false);
            return;
        }

        //string strURL = Application.absoluteURL;
        //string[] listURL = strURL.Split("?token=");
        string strToken = getToken("accessToken");
        if( strToken.Equals("") )
        {
            string strURL = Application.absoluteURL;
            string[] listURL = strURL.Split("?token=");
            if(listURL.Length > 1)
            {
                setToken("accessToken", listURL[1]);
            }
            showAlert("Auth Fail..");
            return;
        }

        Server.Instance.SetToken(strToken);
        Server.Instance.RequestTestCheck();
        Server.Instance.RequestTestInvest();
        Server.Instance.RequestGETInfoExams();
        Server.Instance.ReuquestGETInfoMissions();
        CUIsSpaceManager.Instance.HideTitle();
        CUIsSpaceManager.Instance.ScreenActive(false, true);
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
}
