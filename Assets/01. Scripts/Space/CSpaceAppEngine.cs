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

    //private string m_strServerType = "LOCAL";
    private string m_strServerType = "DEV2";

    //public GameObject[] m_listObjectOutline = new GameObject[3];

    Vector3 m_vecMouseDownPos;


    private bool m_bIsQuizLoaded = false;


    
        // Start is called before the first frame update
    void Start()
    {
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
        m_txtDebug.text = "Width : " + Screen.width.ToString() + ", Hight : " + Screen.height.ToString();

        m_vecMouseDownPos = Input.mousePosition;

        Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if( !CUIsSpaceManager.Instance.IsScreenActive() )
                {
                    Debug.Log("GetMouseButtonDown : " + hit.collider.name);
                    if( !m_bIsQuizLoaded)
                    {
                        Debug.Log("GetMouseButtonDown Quiz Loaded");
                        m_bIsQuizLoaded = true;
                        CUIsSpaceManager.Instance.ScreenActive(true);

                        if (GetServerType().Equals("LOCAL"))
                        {
                            //Debug.Log("Screen Left - LOCAL LOADED");
                            Server.Instance.RequestGETQuestions(0);
                            //CUIsSpaceManager.Instance.ShowLeftPage();
                            //return;
                        } else
                        {
                            STTestCheck stTestCheck = CQuizData.Instance.GetTestCheck();
                            for (int i = 0; i < stTestCheck.body.part_list.Length; i++)
                            {
                                Server.Instance.RequestGETQuestions(stTestCheck.body.part_list[i].part_idx);
                            }
                        }
                    }
                    if (hit.collider.name.Equals("screen_left"))
                    {
                        CUIsSpaceManager.Instance.ScreenActive(true);
                        //if (GetServerType().Equals("LOCAL"))
                        //{
                        //    //Debug.Log("Screen Left - LOCAL LOADED");
                        //    //Server.Instance.RequestGETQuestions(0);
                        //}

                        CUIsSpaceManager.Instance.ShowLeftPage();
                        //CUIsSpaceManager.Instance.ShowLeftPage();
                    }
                    else if (hit.collider.name == "screen_main")
                    {
                        CUIsSpaceManager.Instance.ScreenActive(true);
                        if (GetServerType().Equals("LOCAL"))
                        {
                            //Server.Instance.RequestGETQuestions(0);
                            CUIsSpaceManager.Instance.ShowCenterPage();
                            return;
                        }

                        CUIsSpaceManager.Instance.ShowCenterPage();
                    }
                    else if (hit.collider.name == "screen_right")
                    {
                        CUIsSpaceManager.Instance.ScreenActive(true);
                        if (GetServerType().Equals("LOCAL"))
                        {
                            Server.Instance.RequestGETQuestions(0);
                        }

                        CUIsSpaceManager.Instance.ShowRightPage();
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
}
