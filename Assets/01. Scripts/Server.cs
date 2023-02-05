using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.Networking;
using Newtonsoft.Json;

public class Server : MonoBehaviour
{
    #region SingleTon
    public static Server _instance = null;

    public static Server Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Server install null");
                _instance = Instantiate(Resources.Load<Server>("Prefabs/Network/Server"));
            }

            _instance.cur_server = strServerLocal;

            if (CSpaceAppEngine.Instance.GetServerType().Equals("DEV2"))
                _instance.cur_server = strServerDev2;
            //_instance.cur_server = strServerLocal;

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        cur_server = strServerDev2;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    private const string strServerDev2 = "https://applier-api-dev2.indepth.thepllab.com/";
    private const string strServerLive = "https://ncdkeuehdl.execute-api.ap-northeast-2.amazonaws.com";
    private const string strServerLocal = "http://localhost:7575/";

    private string m_strToken = "";

    private bool m_bIsLoadDummy = false;

    [HideInInspector]
    public string cur_server = "";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void POST(string url, Dictionary<string, string> headerList, string jsonBody, UnityAction<string> method)
    {
        Debug.Log("POST  :  " + url);
        Debug.Log("POSTBody  :  " + jsonBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest www = new UnityWebRequest(url, "POST");

        www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        //   www.certificateHandler = new CertificateWhore();
        www.downloadHandler = new DownloadHandlerBuffer();

        foreach (KeyValuePair<string, string> header in headerList)
        {
            www.SetRequestHeader(header.Key, header.Value);
        }


        Server.Instance.NetCheck(WaitForRequest(www, method));


    }

    private void PUT(string url, Dictionary<string, string> headerList, string jsonBody, UnityAction<string> method)
    {
        Debug.Log("PUT  :  " + url);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest www = new UnityWebRequest(url, "PUT");

        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        //    www.certificateHandler = new CertificateWhore();
        www.downloadHandler = new DownloadHandlerBuffer();

        foreach (KeyValuePair<string, string> header in headerList)
        {
            www.SetRequestHeader(header.Key, header.Value);
        }

        Server.Instance.NetCheck(WaitForRequest(www, method));
    }

    private void GET(string url, Dictionary<string, string> headerList, UnityAction<string> method)
    {
        Debug.Log("GET  :  " + url);

        UnityWebRequest www = new UnityWebRequest(url, "GET");

        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //  www.certificateHandler = new CertificateWhore();

        foreach (KeyValuePair<string, string> header in headerList)
        {
            www.SetRequestHeader(header.Key, header.Value);
        }


        Server.Instance.NetCheck(WaitForRequest(www, method));
    }

    private IEnumerator WaitForRequest(UnityWebRequest www, UnityAction<string> del)
    {
        www.timeout = 5;

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            Debug.Log("error : " + www.error + " url : " + www.url);

        }
        else
        {
            string text = www.downloadHandler.text;
            Debug.Log("Handler : " + www.downloadHandler.data);
            Debug.Log("Text : " + text);
            if (del != null) del(text);
        }
    }

    public void NetCheck(IEnumerator request)
    {
        // StartCoroutine(CheckConnection(request));
        StartCoroutine(request);
    }

    public void SetToken(string strToken)
    {
        m_strToken = strToken;
    }


    public void RequestNoticePopups()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/applier/notice/popups";

        header.Add("Content-Type", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);
        //header.Add("Access-Control-Allow-Origin", "*");

        //header.Add("Access-Control-Allow-Credentials", "true");
        //header.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        //header.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        //header.Add("Access-Control-Allow-Origin", "*");



        GET(url, header, (string txt) =>
        {
            Debug.Log("txt : " + txt);

            //Aws_Rp_VersionCheck output = JsonConvert.DeserializeObject<Aws_Rp_VersionCheck>(txt);




            //if (output.errorMsg.Equals(string.Empty))
            //{



            //    ServerData.Instance.serverLevelData = output.levelData;

            //    GameDataOrder();

            //    PopupManager.Instance.Dismiss();

            //    //   if (method != null)
            //    //    method(output);
            //}
            //else
            //{
            //    PopupManager.Instance.ShowCommonPopup("NetworkError", output.errorMsg, null);
            //}
        });
    }

    #region 문항
    public void RequestGETQuestions(int nPartIdx)
    {
        PacketQuizPart stPacketQuiz = new PacketQuizPart();
        if ( CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (!m_bIsLoadDummy)
            {
                m_bIsLoadDummy = true;
                //stPacketQuiz.body = CQuizData.Instance.GetQuiz("RQT");
                //CQuizData.Instance.SetRQT(stPacketQuiz);

                
                TextAsset textAsset = Resources.Load<TextAsset>("Scripts/dummy_rqt");
                PacketQuizPart packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetRQT(packetQuiz);

                //Debug.Log("LGTK Dummy 00 : " + textAsset);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_aptd1");
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetAPTD1(packetQuiz);

                //Debug.Log("LGTK Dummy 01 : " + textAsset);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_aptd2");
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetAPTD2(packetQuiz);

                //Debug.Log("LGTK Dummy 02 : " + textAsset);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_cst");
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetCST(packetQuiz);

                //Debug.Log("LGTK Dummy 03 : " + textAsset);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_rat");
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetRAT(packetQuiz);

                //Debug.Log("LGTK Dummy 04 : " + textAsset);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_lgtk");
                //Debug.Log("LGTK Dummy : " + textAsset);
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetLGTK(packetQuiz);

                textAsset = Resources.Load<TextAsset>("Scripts/dummy_hpts");
                //Debug.Log("LGTK Dummy : " + textAsset);
                packetQuiz = JsonUtility.FromJson<PacketQuizPart>(textAsset.text);
                CQuizData.Instance.SetHPTS(packetQuiz);



                //CUIsSpaceManager.Instance.ShowLeftPage();

            }

            return;
        }

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/questions/" + nPartIdx.ToString();

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            Debug.Log("txt : " + txt);


            stPacketQuiz = JsonUtility.FromJson<PacketQuizPart>(txt);

            if (stPacketQuiz.code == 200)
            {
                if ( stPacketQuiz.body.qst_tp_cd.Equals("RQT") ) CQuizData.Instance.SetRQT(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("APTD1")) CQuizData.Instance.SetAPTD1(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("APTD2")) CQuizData.Instance.SetAPTD2(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("CST")) CQuizData.Instance.SetCST(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("RAT")) CQuizData.Instance.SetRAT(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("LGTK")) CQuizData.Instance.SetLGTK(stPacketQuiz);
                else if (stPacketQuiz.body.qst_tp_cd.Equals("HPTS")) CQuizData.Instance.SetHPTS(stPacketQuiz);
            }
            else
            {
                Debug.Log("ReqeustTestCheck Err : " + stPacketQuiz.code);
            }
        });
    }

    public void RequestPOSTQuestions(int nPartIdx)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/questions/" + nPartIdx.ToString();

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
            Debug.Log("txt : " + txt);
        });
    }

    public void RequestPUTQuestionsStatus(int nPartIdx, int nStatus)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);

        string strStatus = "TAE_FSH";
        if (nStatus == 1) strStatus = "TAE";

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/questions/" + nPartIdx.ToString() + "/" + strStatus;

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        PUT(url, header, jsonBody, (string txt) =>
        {
            Debug.Log("txt : " + txt);
        });
    }
    #endregion

    #region 영상
    #endregion

    #region 응시답안
    #endregion

    #region 정보안내
    #endregion

    #region 활동 이력
    #endregion

    #region FAQ
    #endregion

    #region 공지사항
    #endregion

    #region 언어
    #endregion

    #region 토큰
    #endregion

    #region 검사진입 
    public void RequestPOSTPartJoin(int nPartIdx)
    {
        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/part/" + nPartIdx.ToString() + "/join";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
            RequestGETQuestions(nPartIdx);
            Debug.Log("txt : " + txt);
        });
    }

    public void RequestGetPartJoin(int nPartIdx)
    {
        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/part/" + nPartIdx.ToString() + "/join";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            //RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);
            
            Debug.Log("txt : " + txt);
        });
    }

    public void RequestTestCheck()
    {
        //Aws_UserSign sign = new Aws_UserSign();
        //sign.email = email;
        //sign.password = m_pwd;
        //sign.nickname = m_name;

        //TempEmailID = email;

        //{ "code":200,"message":"OK","body":{ "rct_idx":1503,"part_idx":66975,"qst_tp_cd":"RQT","part_sort_seq":1,"last_qst_idx":0,"last_page_no":0,"finish_yn":"N","status":"WAITING","exm_cls_cd":"EXMS","part_list":[{ "part_idx":66975,"part_sort_seq":1},{ "part_idx":66968,"part_sort_seq":2},{ "part_idx":66969,"part_sort_seq":3},{ "part_idx":66970,"part_sort_seq":4},{ "part_idx":66971,"part_sort_seq":5},{ "part_idx":66972,"part_sort_seq":6},{ "part_idx":66973,"part_sort_seq":7},{ "part_idx":66974,"part_sort_seq":8}]} }

        STTestCheck stTestCheck = new STTestCheck();

        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Scripts/dummy_api_testcheck");
            stTestCheck = JsonUtility.FromJson<STTestCheck>(textAsset.text);
            Debug.Log(JsonUtility.ToJson(stTestCheck));
            return;
        } 

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/check";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
            //Debug.Log("txt : " + txt);
            stTestCheck = JsonUtility.FromJson<STTestCheck>(txt);

            if( stTestCheck.code == 200)
            {
                CQuizData.Instance.SetTestCheck(stTestCheck);
                for(int i = 0; i < stTestCheck.body.part_list.Length; i++)
                {
                    RequestGetPartJoin(stTestCheck.body.part_list[i].part_idx);
                    RequestPOSTPartJoin(stTestCheck.body.part_list[i].part_idx);
                    //RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);
                }
            } else
            {
                Debug.Log("ReqeustTestCheck Err : " + stTestCheck.code);
            }
        });
    }
    #endregion

    #region 전처리
    public void RequestTestInvest()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/invest";

        header.Add("Content-Type", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        GET(url, header, (string txt) =>
        {
            Debug.Log("txt : " + txt);
        });
    }

    public void RequestPOSTPrivacyAgree()
    {

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/privacy/agree/Y";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
            Debug.Log("txt : " + txt);
        });
    }
    #endregion

    public void RequestTest()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/applier/notice/popups";

        header.Add("Content-Type", "application/json");
        //header.Add("Authorization", "Bearer " + "aW5kZXB0aEFwcDojQClAIXRsYWNtZDEyKSM =");
        header.Add("Authorization", "Bearer " + "aW5kZXB0aEFwcDojQClAIXRsYWNtZDEyKSM =");
        //header.Add("Access-Control-Allow-Origin", "*");

        //header.Add("Access-Control-Allow-Credentials", "true");
        //header.Add("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        //header.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        //header.Add("Access-Control-Allow-Origin", "*");




        GET(url, header, (string txt) =>
        {
            Debug.Log("txt : " + txt);

            //Aws_Rp_VersionCheck output = JsonConvert.DeserializeObject<Aws_Rp_VersionCheck>(txt);




            //if (output.errorMsg.Equals(string.Empty))
            //{



            //    ServerData.Instance.serverLevelData = output.levelData;

            //    GameDataOrder();

            //    PopupManager.Instance.Dismiss();

            //    //   if (method != null)
            //    //    method(output);
            //}
            //else
            //{
            //    PopupManager.Instance.ShowCommonPopup("NetworkError", output.errorMsg, null);
            //}
        });
    }
}



//Aws_UserLogin_Kisti login = new Aws_UserLogin_Kisti();

//login.loginId = email;
//login.pasword = password;

//TempEmailID = email;

//string jsonBody = JsonConvert.SerializeObject(login);






//public void RequestJoinCodeInput(string code, UnityAction method)
//{
//    string uri = System.String.Format(url_join_Confirm, TempEmailID);


//    Aws_UserSignCode signcode = new Aws_UserSignCode();
//    signcode.code = code;


//    string jsonBody = JsonConvert.SerializeObject(signcode);

//    Dictionary<string, string> header = new Dictionary<string, string>();

//    header.Add("Content-Type", "application/json");

//    PATCH(cur_server + uri, header, jsonBody, (string txt) =>
//    {
//        Debug.Log("txt : " + txt);

//        Aws_Rp_Email output = JsonConvert.DeserializeObject<Aws_Rp_Email>(txt);

//        if (output.errorMsg.Equals(string.Empty))
//        {
//            PopupManager.Instance.ShowCommonPopup("Authorization", "CodeCorrect", null);
//            if (method != null)
//                method();

//            LoginWindow loginWindow = EnterPanel.Instance._Login_Window.GetComponent<LoginWindow>();
//            loginWindow._InputField_Id.text = TempEmailID;
//            loginWindow._InputField_Pass.text = "";
//        }
//        else
//        {
//            PopupManager.Instance.ShowCommonPopup("Authorization", output.errorMsg, null);
//            //  if (method != null)
//            //      method();
//        }
//    });
//}




//public void RequestLogin(string email, string password, UnityAction<Aws_Rp_UserLogin> method)
//{
//    string uri = System.String.Format(url_login, email);

//    Aws_UserLogin login = new Aws_UserLogin();
//    login.email = email;
//    login.password = password;

//    TempEmailID = email;


//    string jsonBody = JsonConvert.SerializeObject(login);


//    Dictionary<string, string> header = new Dictionary<string, string>();

//    header.Add("Content-Type", "application/json");

//    POST(cur_server + uri, header, jsonBody, (string txt) =>
//    {
//        EnterPanel.Instance.timerStart = false;

//        Debug.Log("txt : " + txt);
//        Aws_Rp_UserLogin output = JsonConvert.DeserializeObject<Aws_Rp_UserLogin>(txt);


//        if (output.errorMsg.Equals(string.Empty))
//        {
//            if (method != null)
//                method(output);
//        }
//        else if (output.errorMsg.Equals("AWS_Error_EmailCheck"))
//        {
//            EnterPanel.Instance.OpenJoinComplete();
//        }
//        else
//        {
//            PopupManager.Instance.ShowCommonPopup("LoginError", output.errorMsg, null);

//        }
//    });
//}