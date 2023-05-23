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

            //_instance.cur_server = strServerLocal;

            //if (CSpaceAppEngine.Instance.GetServerType().Equals("DEV2"))
            //    _instance.cur_server = strServerDev2;
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
    private const string strServerDev2FaceTest = "https://applier-dev2.indepth.thepllab.com/";
    private const string strServerLive = "https://ncdkeuehdl.execute-api.ap-northeast-2.amazonaws.com";
    private const string strServerLocal = "http://localhost:7575/";

    private const string strServerTRAuth = "https://9dakv9e6p5.execute-api.ap-northeast-2.amazonaws.com/auth";

    private string m_strToken = "";

    private bool m_bIsLoadDummy = false;

    private int m_nRetryCnt = 0;

    [HideInInspector]
    public string cur_server = "";
    public string cur_pllab_server = "";

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
        www.timeout = 50000;

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log(www.error);

            if(www.url.Equals(strServerTRAuth))
            {
                // 인증서버 접속 오류처리
                //Debug.Log("Auth Error !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                CUIsSpaceManager.Instance.UpdateAuthMsg("인증서버 접속에 실패했습니다. 인터넷 연결을 확인해 주세요.");

                System.DateTime currentDate = System.DateTime.Now;
                System.DateTime yearStartDate = new System.DateTime(2020, 1, 1);

                int dayOfYear = (currentDate - yearStartDate).Days + 1;

                if (dayOfYear > CSpaceAppEngine.Instance.GetAuthOverDay() )
                {
                    CUIsSpaceManager.Instance.ShowAuthFail();
                }
            }
            //Debug.Log("error : " + www.error + " url : " + www.url);
            string text = www.downloadHandler.text;
            Debug.Log("ERROR : [" + www.url + "] " + www.method + "MSG : " + www.error + " Text : " + text);
        }
        else
        {
            string text = www.downloadHandler.text;
            //Debug.Log("Handler : " + www.downloadHandler.data);
            Debug.Log("[" + www.url + "] " + www.method + " Text : " + text);
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

    public void SetCurURL(string strURL)
    {
        cur_server = strURL + "/";
        Debug.Log("SetCurURL : " + cur_server);
    }

    public string GetCurURL()
    {
        Debug.Log("GetCurURL : " + cur_server);
        return cur_server.Substring(0, cur_server.Length - 1);
    }

    public void SetPLLabCurURL(string strURL)
    {
        cur_pllab_server = strURL + "/";
    }

    public string GetPLLabCurURL()
    {
        return cur_pllab_server.Substring(0, cur_pllab_server.Length - 1);
    }



    public string GetFaceTestCurURL()
    {
        if(CSpaceAppEngine.Instance.GetServerType().Equals("DEV2"))
        {
            return strServerDev2FaceTest.Substring(0, strServerDev2FaceTest.Length - 1);
        } else
        {
            return strServerDev2FaceTest.Substring(0, strServerDev2FaceTest.Length - 1);
        }
    }

    // TR Auth ---------------
    public class STTRAuth
    {
        public string packageName;
        public int ver;
    }

    public class STTRAuthResponse
    {
        public int resultCode;
        public int clientAuthKey;
        public string authToken;
    }

    public void RequestPOSTTRAuth(string strPackageName, int nVer)
    {

        STTRAuth stTRAuth = new STTRAuth();
        stTRAuth.packageName = strPackageName;
        stTRAuth.ver = nVer;


        string jsonBody = JsonConvert.SerializeObject(stTRAuth);



        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = strServerTRAuth;

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
            STTRAuthResponse stTRAuthResponse = JsonUtility.FromJson<STTRAuthResponse>(txt);
            if(stTRAuthResponse.resultCode == 0)
            {

            } 
            else
            {
                CUIsSpaceManager.Instance.ShowAuthFail();
            }
        });
    }
    // -----------------------

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

    #region 가이드
    public void RequestGETGuides(int nGuideIdx)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Scripts/dummy_guides");
            Debug.Log("LOCAL Guides : " + textAsset.text);
            STGuides stGuides= JsonUtility.FromJson<STGuides>(textAsset.text);
            CQuizData.Instance.SetGuides(stGuides);
            return;
        }

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/guides/" + nGuideIdx.ToString();

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            STGuides stGuides = new STGuides();
            stGuides = JsonUtility.FromJson<STGuides>(txt);

            if (stGuides.code == 200)
            {
                //RequestPOSTPartJoin(nPartIdx);
                CQuizData.Instance.SetGuides(stGuides);
                CUIsLGTKManager.Instance.InitDatabase();
            }
        });
    }
    #endregion

    #region 문항
    public void RequestGETQuestions(int nPartIdx, bool bIsShowPage = false)
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
            //RequestGETQuestionsRetry(nPartIdx);

            stPacketQuiz = JsonUtility.FromJson<PacketQuizPart>(txt);

            if (stPacketQuiz.code == 200)
            {
                m_nRetryCnt = 0;

                if (stPacketQuiz.body.qst_tp_cd.Equals("RQT"))
                {
                    CQuizData.Instance.SetRQT(stPacketQuiz);
                    if (bIsShowPage)
                        CUIsSpaceScreenLeft.Instance.InitRQTQuiz();
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("APTD1"))
                {
                    //if (CQuizData.Instance.GetAPTD1().body.sets.Length > 0)
                    //{
                    //    Debug.Log("APTD1 Init ....");
                    //    return;
                    //}

                    if(CQuizData.Instance.GetAPTD1() != null)
                    {
                        return;
                    }

                    CQuizData.Instance.SetAPTD1(stPacketQuiz);
                    //RequestGETQuestions()
                    //CUIsSpaceManager.Instance.ShowRightPage();
                    RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("APTD2"))
                {
                    CQuizData.Instance.SetAPTD2(stPacketQuiz);
                    //CUIsSpaceManager.Instance.ShowRightPage();
                    CUIsSpaceManager.Instance.FadeInRightComputer();
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("CST"))
                {
                    CQuizData.Instance.SetCST(stPacketQuiz);
                    if(bIsShowPage)
                        CUIsSpaceScreenLeft.Instance.InitCSTQuiz();
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("RAT"))
                {
                    CQuizData.Instance.SetRAT(stPacketQuiz);
                    if (bIsShowPage)
                        CUIsSpaceScreenLeft.Instance.InitRATQuiz();
                    for (int i = 0; i < stPacketQuiz.body.sets.Length; i++)
                    {
                        for (int j = 0; j < stPacketQuiz.body.sets[i].questions.Length; j++)
                        {
                            RequestGETAnswerDictionaries(stPacketQuiz.body.sets[i].questions[j].qst_dics);
                        }
                    }
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("HPTS"))
                {
                    CQuizData.Instance.SetHPTS(stPacketQuiz);
                    if (bIsShowPage)
                        CUIsSpaceScreenLeft.Instance.InitHPTSQuiz();
                }
                else if (stPacketQuiz.body.qst_tp_cd.Equals("LGTK"))
                {
                    CQuizData.Instance.SetLGTK(stPacketQuiz);
                    Server.Instance.RequestGETGuides(CQuizData.Instance.GetLGTK().body.guide_idx);
                }
                
            }
            else
            {
                Debug.Log("ReqeustTestCheck Err : " + stPacketQuiz.code + ", " + stPacketQuiz.message);
                //m_nRetryCnt++;

                //if (m_nRetryCnt < 10)
                //{
                //    RequestGETQuestionsRetry(nPartIdx);
                //}
            }
        });
    }

    public void RequestGETQuestionsRetry(int nPartIdx)
    {
        StartCoroutine(ProcessRequestGETQuestionsRetry(nPartIdx));
    }

    IEnumerator ProcessRequestGETQuestionsRetry(int nPartIdx)
    {
        yield return new WaitForSeconds(0.3f);
        RequestGETQuestions(nPartIdx);
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
            //Debug.Log("txt : " + txt);
        });
    }

    public void RequestPUTQuestionsStatus(int nPartIdx, int nStatus)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);

        string strStatus = "TAE_FSH";
        if (nStatus == 0) strStatus = "TAE";

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/questions/" + nPartIdx.ToString() + "/" + strStatus;

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);
        header.Add("Content-Type", "application/json");

        //POST(url, header, jsonBody, (string txt) =>
        PUT(url, header, jsonBody, (string txt) =>
        {
            STPacketQuestionStatus stPacketQuestionStatus;
            stPacketQuestionStatus = JsonUtility.FromJson<STPacketQuestionStatus>(txt);

            if ( stPacketQuestionStatus.code == 200 )
            {
                if (stPacketQuestionStatus.body.video_part_yn.Equals("Y"))
                {
                    CSpaceAppEngine.Instance.SetFactTest(true);
                }
                else
                {
                    CSpaceAppEngine.Instance.SetFactTest(false);
                }
            }
            //Debug.Log("txt : " + txt);
        });
    }

    //public void RequestPUTQuestionsStatus(int nPartIdx, int nStatus)
    //{
    //    if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

    //    string jsonBody = JsonConvert.SerializeObject(null);

    //    string strStatus = "TAE_FSH";
    //    if (nStatus == 0) strStatus = "TAE";

    //    Dictionary<string, string> header = new Dictionary<string, string>();
    //    string url = cur_server + "api/v1/questions/" + nPartIdx.ToString() + "/" + strStatus;

    //    //header.Add("Content-Type", "application/json");
    //    header.Add("accept", "application/json");
    //    header.Add("Authorization", "Bearer " + m_strToken);
    //    header.Add("Content-Type", "application/json");

    //    //POST(url, header, jsonBody, (string txt) =>
    //    PUT(url, header, jsonBody, (string txt) =>
    //    {

    //        //Debug.Log("txt : " + txt);
    //    });
    //}
    #endregion

    #region 사전
    public void RequestGETAnswerDictionaries(int nDicCateNo)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/answerDictionaries/" + nDicCateNo.ToString();

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            STPacketAnswerDictionaries packetAnswerDictionaries = new STPacketAnswerDictionaries();
            packetAnswerDictionaries = JsonUtility.FromJson<STPacketAnswerDictionaries>(txt);
            CQuizData.Instance.AddAnswerDictionaries(packetAnswerDictionaries);
        });
    }
    #endregion

    #region 영상
    #endregion

    #region 응시답안
    public void RequestPUTAnswerObject(int nQuestIndex, int nAnswer)
    {
        Debug.Log("RequestPUTAnswerObject Single - QuestIdx : " + nQuestIndex + ", Answer : " + nAnswer);
        RequestPUTAnswerObject(nQuestIndex, new int[] { nAnswer });
    }

    public void RequestPUTAnswerObject(int nQuestIndex, int[] listAnswer)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        STPacketAnswerObject stPacketAnswer = new STPacketAnswerObject();
        stPacketAnswer.answer_idx = nQuestIndex;
        stPacketAnswer.answer_type = "OBJ";
        stPacketAnswer.answers = listAnswer;

        string jsonBody = JsonConvert.SerializeObject(stPacketAnswer);

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/answer";

        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);
        header.Add("Content-Type", "application/json");

        PUT(url, header, jsonBody, (string txt) =>
        {
        });
    }

    public void RequestPUTAnswerSubject(int nQuestIndex, int nAnswerIndex, string strContent, float fScore = 0f)
    {
        RequestPUTAnswerSubject(nQuestIndex, nAnswerIndex, new string[] { strContent }, fScore);
    }

    public void RequestPUTAnswerSubject(int nQuestIndex, int nAnswerIndex, string[] listContent, float fSocre = 0f)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        STPacketAnswerSubject stPacketAnswer = new STPacketAnswerSubject();
        stPacketAnswer.answer_type = "SBCT";
        stPacketAnswer.answer_idx = nQuestIndex;
        stPacketAnswer.answers = new int[] { nAnswerIndex };
        stPacketAnswer.contents = listContent;
        stPacketAnswer.demerit_score = fSocre;

        string jsonBody = JsonConvert.SerializeObject(stPacketAnswer);

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/answer";

        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);
        header.Add("Content-Type", "application/json");

        //POST(url, header, jsonBody, (string txt) =>
        PUT(url, header, jsonBody, (string txt) =>
        {
            //Debug.Log("txt : " + txt);
        });
    }
    #endregion

    #region 정보안내
    public void RequestGETInfoExams(bool bIsFirstRequest = false)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/info/exams";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            STPacketExamInfo packetExamInfo = new STPacketExamInfo();
            packetExamInfo = JsonUtility.FromJson<STPacketExamInfo>(txt);
            CQuizData.Instance.SetExamInfo(packetExamInfo);

            if(bIsFirstRequest)
            {
                bool bIsFirst = true;
                for (int i = 0; i < CQuizData.Instance.GetExamInfo().body.Length; i++)
                {
                    if (!CQuizData.Instance.GetExamInfo().body[i].status.Equals("WAITING"))
                    {
                        bIsFirst = false;
                        break;
                    }
                }

                for (int i = 0; i < CQuizData.Instance.GetExamInfo().body.Length; i++)
                {
                    Debug.Log("Active!!!!!!!!!!!!!!! 00 : " + CQuizData.Instance.GetExamInfo().body[i].qstTpCd);
                    if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("RQT"))
                    {
                        Debug.Log("Active!!!!!!!!!!!!!!! 01");
                        CSpaceAppEngine.Instance.SetActiveLeft(true);
                    }

                    if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("LGTK"))
                    {
                        Debug.Log("Active!!!!!!!!!!!!!!! 02");
                        CSpaceAppEngine.Instance.SetActiveCenter(true);
                    }

                    if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("APTD1"))
                    {
                        Debug.Log("Active!!!!!!!!!!!!!!! 03");
                        CSpaceAppEngine.Instance.SetActiveRight(true);
                    }
                }

                CUIsSpaceManager.Instance.HideTitle();
                CUIsSpaceManager.Instance.ShowLobby();
                CUIsSpaceManager.Instance.ShowComputers();

                if (CSpaceAppEngine.Instance.IsSkipIntro())
                {
                    CUIsSpaceManager.Instance.ScreenActive(false, true);
                }
                else
                {
                    if (bIsFirst)
                    {
                        CSpaceAppEngine.Instance.StartIntro();
                        CUIsSpaceManager.Instance.ShowIntro();
                        CSpaceAppEngine.Instance.PlayAniRobo();
                    }
                    else
                    {
                        RequestPUTActionExit();

                        CSpaceAppEngine.Instance.StartTest();
                        //CUIsSpaceManager.Instance.ShowTodo();
                        //CUIsSpaceManager.Instance.ScreenActive(false, true);
                        CSpaceAppEngine.Instance.PlayLookatCenter();
                    }
                }


                for (int i = 0; i < CQuizData.Instance.GetExamInfo().body.Length; i++)
                {

                    if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("RQT"))
                    {
                        if (CQuizData.Instance.GetExamInfo().body[i].status.Equals("TAE_FSH")) CSpaceAppEngine.Instance.SetFinishLeft01(true);
                    }
                    else if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("HPTS"))
                    {
                        if (CQuizData.Instance.GetExamInfo().body[i].status.Equals("TAE_FSH")) CSpaceAppEngine.Instance.SetFinishLeft02(true);
                    }
                    else if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("LGTK"))
                    {
                        if (CQuizData.Instance.GetExamInfo().body[i].status.Equals("TAE_FSH")) CSpaceAppEngine.Instance.SetFinishCenter(true);
                    }
                    else if (CQuizData.Instance.GetExamInfo().body[i].qstTpCd.Equals("APTD2"))
                    {
                        if (CQuizData.Instance.GetExamInfo().body[i].status.Equals("TAE_FSH")) CSpaceAppEngine.Instance.SetFinishRight(true);
                    }
                }

                CSpaceAppEngine.Instance.UpdateMissionClear();
                CSpaceAppEngine.Instance.UpdateMissionActive();
            }
        });
    }

    public void ReuquestGETInfoMissions()
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/info/missions";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            STPacketInfoMission packetInfoMission = new STPacketInfoMission();
            packetInfoMission = JsonUtility.FromJson<STPacketInfoMission>(txt);
            CQuizData.Instance.SetInfoMission(packetInfoMission);

            //CUIsTodoManager.Instance.UpdateDummyTodo();
            // DEL : 230419
            //CUIsTodoManager.Instance.UpdateTodo();
            //RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);

            //Debug.Log("txt : " + txt);
        });
    }
    #endregion

    #region 활동 이력
    public void RequestGETActionExit()
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/action/exit";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        GET(url, header, (string txt) =>
        {
            STPacketActionExit packetActionExit = new STPacketActionExit();
            packetActionExit = JsonUtility.FromJson<STPacketActionExit>(txt);
            CQuizData.Instance.SetExitCount(packetActionExit.body);
            //RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);

            //Debug.Log("txt : " + txt);
        });
    }

    public void RequestPUTActionExit()
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);

        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/action/exit";

        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        PUT(url, header, jsonBody, (string txt) =>
        {
            STPacketActionExit packetActionExit = new STPacketActionExit();
            packetActionExit = JsonUtility.FromJson<STPacketActionExit>(txt);

            if(packetActionExit.code == 200)
            {
                CQuizData.Instance.SetExitCount(packetActionExit.body);
                // TODO : 나가기 기능
            } else if (packetActionExit.code == 400)
            {
                // TODO : 나가기 실패
            }
            
            //Debug.Log("txt : " + txt);
        });
    }
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
            if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("CST").idx)
            {
                //Debug.Log("RequestPOSTPartJoin RequestQuestion CST");
                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx, true);
                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
            }
            //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("RAT").idx)
            //{
            //    Debug.Log("RequestPOSTPartJoin RequestQuestion RAT");
            //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
            //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
            //}
            //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("HPTS").idx)
            //{
            //    Debug.Log("RequestPOSTPartJoin RequestQuestion HPTS");
            //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            //}

            if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("LGTK").idx)
            {
                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
            }

            if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("APTD1").idx)
            {
                RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
                //RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
            }
            //Debug.Log("txt : " + txt);

            RequestGETInfoExams();
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
            STPacketBasic stPacket = new STPacketBasic();
            stPacket = JsonUtility.FromJson<STPacketBasic>(txt);
            if (stPacket.code == 200)
            {
                //RequestPOSTPartJoin(nPartIdx);
                //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("CST").idx)
                //{
                //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                //    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                //}
                //else if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("RAT").idx)
                //{
                //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                //    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                //}
                //else if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("HPTS").idx)
                //{
                //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                //    Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                //}
                //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("CST").idx)
                //{
                //    Debug.Log("RequestGETPartJoin RequestQuestion CST");
                //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                //}
                //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("RAT").idx)
                //{
                //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                //}
                //if (nPartIdx == CQuizData.Instance.GetExamInfoDetail("HPTS").idx)
                //{
                //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                //    //Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                //}
            }
        });
    }

    public void RequestTestCheck()
    {
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
                Server.Instance.RequestTestInvest();
                // TODO 230216
                //RequestGetPartJoin(stTestCheck.body.part_idx);

                //for (int i = 0; i < stTestCheck.body.part_list.Length; i++)
                //{
                //    RequestGetPartJoin(stTestCheck.body.part_list[i].part_idx);
                //    //RequestPOSTPartJoin(stTestCheck.body.part_list[i].part_idx);
                //}

                //CQuizData.Instance.SetTestCheck(stTestCheck);
                //for (int i = 0; i < stTestCheck.body.part_list.Length; i++)
                //{
                //    RequestGetPartJoin(stTestCheck.body.part_list[i].part_idx);
                //    // TODO 230215 : RequestPOSTPartJoin
                //    RequestPOSTPartJoin(stTestCheck.body.part_list[i].part_idx);
                //    RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);
                //}
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
            STPacketTestInvest stPacketTestInvest = new STPacketTestInvest();
            stPacketTestInvest = JsonUtility.FromJson<STPacketTestInvest>(txt);

            CQuizData.Instance.SetUserName(stPacketTestInvest.body.applier.username);

            RequestGETActionExit();
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
        });
    }
    #endregion

    #region 검사 응시
    public void RequestPOSTPartTimer(int nPartIndex)
    {
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) return;

        string jsonBody = JsonConvert.SerializeObject(null);


        Dictionary<string, string> header = new Dictionary<string, string>();
        string url = cur_server + "api/v1/test/part/" + nPartIndex.ToString() + "/timer";

        //header.Add("Content-Type", "application/json");
        header.Add("accept", "application/json");
        header.Add("Authorization", "Bearer " + m_strToken);

        //POST(url, header, jsonBody, (string txt) =>
        POST(url, header, jsonBody, (string txt) =>
        {
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
