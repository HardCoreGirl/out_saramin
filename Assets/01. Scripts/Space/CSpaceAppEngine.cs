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

    public Text m_txtDebug;

    public GameObject[] m_goMissionClear = new GameObject[4];

    private string m_strServerType = "LOCAL";
    //private string m_strServerType = "DEV2";

    //public GameObject[] m_listObjectOutline = new GameObject[3];

    Vector3 m_vecMouseDownPos;

    public Animator m_aniRobo;


    private bool m_bIsQuizLoaded = false;

    private bool m_bIsFinishLeft01 = false;
    private bool m_bIsFinishLeft02 = false;
    private bool m_bIsFinishCenter = false;
    private bool m_bIsFinishRight = false;

    private bool m_bIsIntro = true;
    //private bool m_bIsIntro = false;

    private int m_nBuildType = 1;   // 0 : Debug, 1 : DEV2
    private bool m_bIsSkipIntro = false;

    private string m_strToken = "f34667ff-fca0-4b64-bab9-7b4e1183c5d8";


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("CSpaceAppEngine 01");
        UpdateMissionClear();
        //HideAllObjectOutline();
        //Server.Instance.GetComponent()
        //Server.Instance.RequestTest();
        //Server.Instance.SetToken("aW5kZXB0aEFwcDojQClAIXRsYWNtZDEyKSM=");
        //Server.Instance.RequestTestCheck();
        //foreach(var s in WWW.GetResopseHeader())
        //{
        //    Debug.Log(s);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //m_txtDebug.text = "Width : " + Screen.width.ToString() + ", Height : " + Screen.height.ToString();

        m_vecMouseDownPos = Input.mousePosition;

        Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if( !CUIsSpaceManager.Instance.IsScreenActive() )
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
                                Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
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

    public void SetServerType(string strServerType)
    {
        m_strServerType = strServerType;
    }
    public string GetServerType()
    {
        return m_strServerType;
    }

    public void UpdateMissionClear()
    {
        if (m_bIsFinishLeft01) m_goMissionClear[0].SetActive(true);
        else if (!m_bIsFinishLeft01) m_goMissionClear[0].SetActive(false);

        if (m_bIsFinishLeft02) m_goMissionClear[1].SetActive(true);
        else if (!m_bIsFinishLeft02) m_goMissionClear[1].SetActive(false);

        if (m_bIsFinishCenter) m_goMissionClear[2].SetActive(true);
        else if (!m_bIsFinishCenter) m_goMissionClear[2].SetActive(false);

        if (m_bIsFinishRight) m_goMissionClear[3].SetActive(true);
        else if (!m_bIsFinishRight) m_goMissionClear[3].SetActive(false);
    }

    public void SetFinishLeft01(bool bIsFinish) { m_bIsFinishLeft01 = bIsFinish; }
    public bool IsFinishLeft01() { return m_bIsFinishLeft01; }
    public void SetFinishLeft02(bool bIsFinish) { m_bIsFinishLeft02 = bIsFinish; }
    public bool IsFinishLeft02() { return m_bIsFinishLeft02; }
    public void SetFinishCenter(bool bIsFinish) { m_bIsFinishCenter = bIsFinish; }
    public void SetFinishRight(bool bIsFinish) { m_bIsFinishRight = bIsFinish; }

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
        m_aniRobo.Play("Robot03");
    }

    public string GetToken()
    {
        return m_strToken;
    }
}
