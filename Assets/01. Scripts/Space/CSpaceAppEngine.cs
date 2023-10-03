using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

using System.Runtime.InteropServices;

public class CSpaceAppEngine : MonoBehaviour
{
    #region SingleTon
    public static CSpaceAppEngine _instance = null;

    public static CSpaceAppEngine Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CSpaceAppEngine install null");

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

    public TMPro.TMP_Text m_txtAuthInfo;
    public Text m_txtDebug;

    public GameObject m_goLogout;

    public GameObject[] m_listInGameBoard = new GameObject[3];

    public GameObject[] m_goMissionClear = new GameObject[4];

    public GameObject[] m_goMissionActive = new GameObject[3];

    //private string m_strServerType = "LOCAL";
    private string m_strServerType = "DEV2";

    //public GameObject[] m_listObjectOutline = new GameObject[3];

    Vector3 m_vecMouseDownPos;

    public Animator m_aniRobo;


    private bool m_bIsQuizLoaded = false;

    private bool m_bIsFinishLeft01 = false;
    private bool m_bIsFinishLeft02 = false;
    private bool m_bIsFinishCenter = false;
    private bool m_bIsFinishRight = false;

    private bool m_bIsActiveLeft = false;
    private bool m_bIsActiveCenter = false;
    private bool m_bIsActiveRight = false;

    private bool m_bIsIntro = true;
    //private bool m_bIsIntro = false;

    private int m_nBuildType = 1;   // 0 : Debug, 1 : DEV2
    private bool m_bIsSkipIntro = false;

    private string m_strToken = "9ea8510d-8482-45c1-8103-53b454bea584";

    private int m_nBoardIndex = 0;

    private string m_strVer = "230904.01";
    private int m_nAuthOverDay = 1360;
    //private int m_nAuthOverDay = 1310;

    private bool m_bIsFaceTest = false;

    private float m_fFadeTime = 0.5f;

    private float m_fNoneInputTime = 0;

    private bool m_bIsLogout = false;

    // TODO 활동로그 남기기
    private int m_nPlayExamTime = 0;
    private string m_strCategory = "";
    private string m_strPage = "";

    [System.Serializable]
    public class HostConfig
    {
        public string APP_API_HOST;
        public string APP_PLLAB_HOST;
    }

 //   {
	//"APP_API_HOST":"1111",
	//"APP_PLLAB_HOST":"2222"
 //   }

// Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ProcessLoadServerInfo");
        //Debug.Log(Server.Instance.GetCurURL());
        System.DateTime currentDate = System.DateTime.Now;
        System.DateTime yearStartDate = new System.DateTime(2020, 1, 1);

        int dayOfYear = (currentDate - yearStartDate).Days + 1;

        Debug.Log("Today : " + dayOfYear);

        //UpdateMissionClear();
        m_listInGameBoard[0].SetActive(true);
        m_listInGameBoard[1].SetActive(false);
        m_listInGameBoard[2].SetActive(false);

        CUIsSpaceManager.Instance.UpdateAuthMsg("");
        CUIsSpaceManager.Instance.HideAuthFail();
        // TODO 인증 제거
        //Server.Instance.RequestPOSTTRAuth("saraminxx", 1000);

        //int[] test = new int[] { 23, 43 };

        //string strTest = "";
        //for(int i = 0; i < test.Length; i++)
        //{
        //    if (i != 0)
        //        strTest += ",";
        //    strTest += test[i];
        //}

        //Debug.Log(strTest);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsLogout)
            return;

        m_fNoneInputTime += Time.deltaTime;
        if( m_fNoneInputTime >= (60 * 5) )
        {
            m_bIsLogout = true;
            Time.timeScale = 0;
            m_goLogout.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_fNoneInputTime = 0;
        }

        if( Input.anyKey )
        {
            m_fNoneInputTime = 0;
        }

        return;

        if (m_nBoardIndex != 2)
            return;
        //m_txtDebug.text = "Width : " + Screen.width.ToString() + ", Height : " + Screen.height.ToString();

        m_vecMouseDownPos = Input.mousePosition;

        Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if(Input.GetMouseButtonDown(0))
        {
            
            if (hit.collider != null)
            {
                if ( !CUIsSpaceManager.Instance.IsScreenActive() )
                {
                    //Debug.Log("GetMouseButtonDown : " + hit.collider.name);
                    //if( !m_bIsQuizLoaded)
                    //{
                    //    Debug.Log("GetMouseButtonDown Quiz Loaded");
                    //    m_bIsQuizLoaded = true;
                    //    CUIsSpaceManager.Instance.ScreenActive(true);

                    //    if (GetServerType().Equals("LOCAL"))
                    //    {
                    //        //Debug.Log("Screen Left - LOCAL LOADED");
                    //        Server.Instance.RequestGETQuestions(0);
                    //        //CUIsSpaceManager.Instance.ShowLeftPage();
                    //        //return;
                    //    } else
                    //    {
                    //        STTestCheck stTestCheck = CQuizData.Instance.GetTestCheck();
                    //        for (int i = 0; i < stTestCheck.body.part_list.Length; i++)
                    //        {
                    //            Server.Instance.RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);
                    //        }
                    //    }
                    //}
                    if (hit.collider.name.Equals("screen_left"))
                    {
                        if (m_bIsFinishLeft01 && m_bIsFinishLeft02)
                            return;

                        CUIsSpaceManager.Instance.ScreenActive(true);

                        if (!GetServerType().Equals("LOCAL"))
                        {
                            if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("WAITING"))
                                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
                            if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
                                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                            if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
                                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
                                Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);

                            Server.Instance.RequestGETInfoExams();
                            //if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("WAITING") || CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("TAE"))
                            //{
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);

                            //    if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("TAE"))
                            //    {
                            //        Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
                            //        Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                            //        Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            //        Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                            //    }
                            //}


                            //if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
                            //{
                            //    Debug.Log("Screen Left CST WAITING");
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                            //} else if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("TAE"))
                            //{
                            //    Debug.Log("Screen Left CST TAE");
                            //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx);
                            //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            //    //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                            //}
                            ////Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("CST").idx);

                            //if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
                            //{
                            //    Debug.Log("Screen Left RAT WAITING");
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            //} else if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("TAE"))
                            //{
                            //    Debug.Log("Screen Left RAT TAE");
                            //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
                            //}
                            ////Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("RAT").idx);

                            //if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
                            //{
                            //    Debug.Log("Screen Left HPTS WAITING");
                            //    Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                            //} else if(CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("TAE"))
                            //{
                            //    Debug.Log("Screen Left HPTS TAE");
                            //    Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                            //}
                            //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
                        }

                        CUIsSpaceManager.Instance.ShowLeftPage();
                    }
                    else if (hit.collider.name == "screen_main")
                    {
                        if (m_bIsFinishCenter)
                            return;                               
                            
                        CUIsSpaceManager.Instance.ScreenActive(true);

                        if (GetServerType().Equals("LOCAL"))
                        {
                            //Server.Instance.RequestGETQuestions(0);
                            CUIsSpaceManager.Instance.ShowCenterPage();
                            return;
                        } else
                        {
                            if (!CQuizData.Instance.GetExamInfoDetail("LGTK").status.Equals("WAITING"))
                                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
                        }

                        Server.Instance.RequestGETInfoExams();

                        CUIsSpaceManager.Instance.ShowCenterPage();
                    }
                    else if (hit.collider.name == "screen_right")
                    {
                        if (m_bIsFinishRight)
                            return;

                        CUIsSpaceManager.Instance.ScreenActive(true);
                        if (GetServerType().Equals("LOCAL"))
                        {
                            Server.Instance.RequestGETQuestions(0);
                            CUIsSpaceManager.Instance.ShowRightPage();
                        } else
                        {
                            if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING"))
                            {
                                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
                                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                            } else
                            {
                                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
                                //Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
                            }

                        }

                        //CUIsSpaceManager.Instance.ShowRightPage();
                    }
                }
                //else if (hit.collider.name == "Screen_spaceship")
                //{
                //    //ShowObjectOutline(1);
                //    Debug.Log("22222222222222");
                //    CUIsSpaceUIManager.Instance.ShowScreen(1);
                //}
                //else if (hit.collider.name == "Screen_comunication")
                //{
                //    //ShowObjectOutline(2);
                //    Debug.Log("333333333333333");
                //} 
            }
            else
            {
                //HideAllObjectOutline();
            }
        } else
        {
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Screen_main")
                {
                    //ShowObjectOutline(0);
                }
                else if (hit.collider.name == "Screen_spaceship")
                {
                    //ShowObjectOutline(1);
                }
                else if (hit.collider.name == "Screen_comunication")
                {
                    //ShowObjectOutline(2);
                }
            }
            else
            {
                //HideAllObjectOutline();
            }

        }

        //Ray ray = Camera.main.ScreenPointToRay(m_vecMouseDownPos);
        //RaycastHit hit;

        //if(Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log(hit.collider.name);
        //    //if(hit.collider.name)
        //}
    }

    public int GetAuthOverDay()
    {
        return m_nAuthOverDay;
    }

    //public void ShowObjectOutline(int nIndex)
    //{
    //    HideAllObjectOutline();
    //    m_listObjectOutline[nIndex].SetActive(true);
    //}

    //public void HideAllObjectOutline()
    //{
    //    for (int i = 0; i < m_listObjectOutline.Length; i++)
    //    {
    //        m_listObjectOutline[i].SetActive(false);
    //    }
    //}

    IEnumerator ProcessLoadServerInfo()
    {
        m_txtAuthInfo.text = "";

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "config.txt");

        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        string strUrl = "";

        if (www.result == UnityWebRequest.Result.Success)
        {
            string fileContents = www.downloadHandler.text;
            HostConfig hcData = JsonUtility.FromJson<HostConfig>(fileContents);

            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 02 : " + hcData.APP_API_HOST);
            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 03 : " + hcData.APP_PLLAB_HOST);

            Server.Instance.SetCurURL(hcData.APP_API_HOST);
            Server.Instance.SetPLLabCurURL(hcData.APP_PLLAB_HOST);
            strUrl = hcData.APP_API_HOST;
        }

        // TODO AUTH
        //m_txtAuthInfo.text = "Dev Build (Ver." + m_strVer + ") API_HOST_URL : " + strUrl;
        m_txtAuthInfo.text = "";
    }

    public void SetServerType(string strServerType)
    {
        m_strServerType = strServerType;
    }
    public string GetServerType()
    {
        return m_strServerType;
    }

    public void HideMissionActive()
    {
        m_goMissionActive[0].SetActive(false);
        m_goMissionActive[1].SetActive(false);
        m_goMissionActive[2].SetActive(false);
    }

    public void UpdateMissionActive()
    {
        HideMissionActive();
        //if ( !IsActiveLeft() )
        //{
        //    m_goMissionActive[0].SetActive(true);
        //}

        //if (!IsActiveCenter())
        //{
        //    m_goMissionActive[1].SetActive(true);
        //}

        //if (!IsActiveRight())
        //{
        //    m_goMissionActive[2].SetActive(true);
        //}
    }

    public void UpdateMissionClear()
    {
        bool bIsAllClear = true;
        int nTodoCnt = 0;
        if (m_bIsFinishLeft01)
            m_goMissionClear[0].SetActive(true);
        else if (!m_bIsFinishLeft01)
        {
            if (IsActiveLeft())
            {
                bIsAllClear = false;
                nTodoCnt++;
                m_goMissionClear[0].SetActive(false);
            }
        }

        if (m_bIsFinishLeft02)
            m_goMissionClear[1].SetActive(true);
        else if (!m_bIsFinishLeft02)
        {
            if (IsActiveLeft())
            {
                bIsAllClear = false;
                nTodoCnt++;
                m_goMissionClear[1].SetActive(false);
            }
        }

        if (m_bIsFinishCenter)
        {
            m_goMissionClear[2].SetActive(true);
        }
        else if (!m_bIsFinishCenter)
        {
            if (IsActiveCenter())
            {
                bIsAllClear = false;
                nTodoCnt++;
                m_goMissionClear[2].SetActive(false);
            }
        }

        if (m_bIsFinishRight) m_goMissionClear[3].SetActive(true);
        else if (!m_bIsFinishRight)
        {
            if (IsActiveRight())
            {
                bIsAllClear = false;
                nTodoCnt++;
                m_goMissionClear[3].SetActive(false);
            }
        }


        //CUIsTodoManager.Instance.UpdateTodoCnt(nTodoCnt);
        //CUIsTodoManager.Instance.UpdateSlot(0, m_bIsFinishLeft01);
        //CUIsTodoManager.Instance.UpdateSlot(1, m_bIsFinishLeft02);
        //CUIsTodoManager.Instance.UpdateSlot(2, m_bIsFinishCenter);
        //CUIsTodoManager.Instance.UpdateSlot(3, m_bIsFinishRight);

        if (bIsAllClear)
        {
            CUIsSpaceManager.Instance.ShowOutro();
        }
    }

    public void SetFinishLeft01(bool bIsFinish) { m_bIsFinishLeft01 = bIsFinish; }
    public bool IsFinishLeft01() { return m_bIsFinishLeft01; }
    public void SetFinishLeft02(bool bIsFinish) { m_bIsFinishLeft02 = bIsFinish; }
    public bool IsFinishLeft02() { return m_bIsFinishLeft02; }
    public void SetFinishCenter(bool bIsFinish) { m_bIsFinishCenter = bIsFinish; }
    public bool IsFinishCenter() { return m_bIsFinishCenter; }
    public void SetFinishRight(bool bIsFinish) { m_bIsFinishRight = bIsFinish; }
    public bool IsFinishRight() { return m_bIsFinishRight; }

    public void SetActiveLeft(bool bIsActive) {  m_bIsActiveLeft = bIsActive; }
    public bool IsActiveLeft() { return m_bIsActiveLeft; }
    public void SetActiveCenter(bool bIsActive) { m_bIsActiveCenter = bIsActive; }
    public bool IsActiveCenter() { return m_bIsActiveCenter; }
    public void SetActiveRight(bool bIsActive) { m_bIsActiveRight = bIsActive; }
    public bool IsActiveRight() { return m_bIsActiveRight; }

    public void SetIsIntro(bool bIsIntro) { m_bIsIntro = bIsIntro; }
    public bool IsIntro() { return m_bIsIntro; }

    public int GetBuildType() { return m_nBuildType; }
    public bool IsSkipIntro() { return m_bIsSkipIntro; }

    public void PlayAniRobo()
    {
        m_aniRobo.Play("Robot00");
    }

    public void PlayFinishRobo()
    {
        m_aniRobo.Play("Robot01");
    }

    public void PlayLookatCenter()
    {
        m_aniRobo.Play("Robot02");
    }

    public void PlayLookatCenterTalk()
    {
        m_aniRobo.Play("Robot02_talk");
    }

    public void PlayTalk()
    {
        m_aniRobo.Play("Robot00_talk");
    }


    public void PlayLookatRight()
    {
        m_aniRobo.Play("Robot03");
    }

    public void PlayLookatRightTalk()
    {
        m_aniRobo.Play("Robot03_talk");
    }

    public void PlayMoveCenter()
    {
        m_aniRobo.Play("Robot04");
    }

    public string GetToken()
    {
        return m_strToken;
    }

    public void StartTest()
    {
        m_listInGameBoard[0].SetActive(false);
        m_listInGameBoard[1].SetActive(false);
        m_listInGameBoard[2].SetActive(true);

        m_nBoardIndex = 2;
    }

    public void StartIntro()
    {
        m_listInGameBoard[0].SetActive(false);
        m_listInGameBoard[1].SetActive(true);
        m_listInGameBoard[2].SetActive(false);

        m_nBoardIndex = 1;
    }

    public void SetFactTest(bool bIsFaceTest)
    {
        m_bIsFaceTest = bIsFaceTest;
    }

    public bool IsFaceTest()
    {
        return m_bIsFaceTest;
    }

    public float GetFadeTime()
    {
        return m_fFadeTime;
    }

    public void OnClickLogout()
    {
        //string url = "https://applier-dev2.indepth.thepllab.com/";
        string url = Server.Instance.GetPLLabCurURL() + "/";
        Application.ExternalEval("window.location.href='" + url + "'");
    }

    // TODO 활동로그 남기기
    public void PlayExamTime()
    {
        StartCoroutine("ProcessPlayExamTime");
    }

    IEnumerator ProcessPlayExamTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            m_nPlayExamTime++;
        }
    }

    public int GetPlayExamTime()
    {
        return m_nPlayExamTime;
    }

    public void SetCaterogy(string strCaterogy)
    {
        m_strCategory = strCaterogy;
    }

    public string GetCaterogy()
    {
        return m_strCategory;
    }

    public void SetPage(string strPage)
    {
        m_strPage = strPage;
    }

    public string GetPage()
    {
        return m_strPage;
    }
}
