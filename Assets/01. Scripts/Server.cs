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

    public void RequestTestCheck()
    {
        //Aws_UserSign sign = new Aws_UserSign();
        //sign.email = email;
        //sign.password = m_pwd;
        //sign.nickname = m_name;

        //TempEmailID = email;

        //{ "code":200,"message":"OK","body":{ "rct_idx":1503,"part_idx":66975,"qst_tp_cd":"RQT","part_sort_seq":1,"last_qst_idx":0,"last_page_no":0,"finish_yn":"N","status":"WAITING","exm_cls_cd":"EXMS","part_list":[{ "part_idx":66975,"part_sort_seq":1},{ "part_idx":66968,"part_sort_seq":2},{ "part_idx":66969,"part_sort_seq":3},{ "part_idx":66970,"part_sort_seq":4},{ "part_idx":66971,"part_sort_seq":5},{ "part_idx":66972,"part_sort_seq":6},{ "part_idx":66973,"part_sort_seq":7},{ "part_idx":66974,"part_sort_seq":8}]} }


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
        });
    }

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
