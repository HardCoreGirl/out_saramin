using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Networking;

public class CUIsLGTKManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsLGTKManager _instance = null;

    public static CUIsLGTKManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsLGTKManager install null");

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

    public GameObject m_goTalkBox;

    public GameObject m_goPopupFinish;
    public GameObject m_goPopupTimeOver;
    public GameObject m_goPopupToLobby;
    public Text m_txtPopupToLobbyMsg;
    public Text m_txtPopupToLobbyRemainTime;
    public GameObject m_goPopupToLobbyOver;
    public Text m_txtPopupToLobbyOverMsg;
    public Text m_txtPopupToLobbyOverRemainTime;

    public Text m_txtRemain;

    public GameObject m_goDatabaseContent;

    public Text m_txtDatabaseDetailTitle;
    public GameObject m_goDatabaseDetail;
    public Image m_imgDatabaseDetail;

    private bool m_bIsTutorial = true;
    private int m_nRemainTime;

    private bool m_bIsFirstOpen = true;

    private Vector3 m_vecTalkBoxPoz;

    private bool m_bIsLoadDatabases = false;

    private List<GameObject> m_listDatabase;

    private string m_strDatabaseDetailURL;

    private List<string> m_listAnswers;

    private List<string> m_listSBCTAnswer;



    // Start is called before the first frame update
    void Start()
    {
        //InitLGTK();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTK()
    {
        //StartCoroutine("ProcessTestImage");

        HideTalkBox();

        m_listAnswers = new List<string>();
        m_listSBCTAnswer = new List<string>();

        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            Debug.Log("InitLGTK 01");
            if (!m_bIsLoadDatabases)
            {
                Debug.Log("InitLGTK 02");
                //m_bIsLoadDatabases = true;
                InitDatabase();
            }
        }


        //if (!m_bIsLoadDatabases)
        //{
        //    m_bIsLoadDatabases = true;

        //    m_listDatabase = new List<GameObject>();

        //    for (int i = 0; i < CQuizData.Instance.GetGuides().body.contents.Length; i++)
        //    {
        //        GameObject goDatabase = Instantiate(Resources.Load("Prefabs/LGTKDatabase") as GameObject);
        //        goDatabase.transform.parent = m_goDatabaseContent.transform;
        //        goDatabase.GetComponent<CObjectLGTKDatabase>().InitLGTkDatabase(i, -1);
        //        m_listDatabase.Add(goDatabase);



        //        for(int j = 0; j < CQuizData.Instance.GetGuides().body.contents[i].children.Length; j++)
        //        {
        //            GameObject goDatabaseChildren = Instantiate(Resources.Load("Prefabs/LGTKDatabase") as GameObject);
        //            goDatabaseChildren.transform.parent = m_goDatabaseContent.transform;
        //            goDatabaseChildren.GetComponent<CObjectLGTKDatabase>().InitLGTkDatabase(i, j);
        //            m_listDatabase.Add(goDatabaseChildren);
        //        }
        //        //goDropdown.GetComponent<CObjectLGTKDropdown>().InitLGTKDropdown(quizLGTK.set_gudes[i].gude_nm, quizLGTK.set_gudes[i].gude_seur_grd, quizLGTK.set_gudes[i].gude_reg_dtm);
        //    }

        //    UpdateDatabase();
        //}


        if ( m_bIsTutorial)
        {
            m_txtRemain.text = "시작전";
            if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
                OnClickTalkBox();
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            //m_nRemainTime = quizLGTK.exm_time;
            m_nRemainTime = quizLGTK.progress_time;
            //m_nRemainTime = 30;
            Debug.Log("LGTK Remain Time : " + m_nRemainTime);

            // TODO : Set_Gudes
            //for(int i= 0; i < quizLGTK.set_gudes.Length; i++)
            //{
            //    GameObject goDropdown = Instantiate(Resources.Load("Prefabs/LGTKDropdown") as GameObject);
            //    goDropdown.transform.parent = m_goDropdownContent.transform;
            //    goDropdown.GetComponent<CObjectLGTKDropdown>().InitLGTKDropdown(quizLGTK.set_gudes[i].gude_nm, quizLGTK.set_gudes[i].gude_seur_grd, quizLGTK.set_gudes[i].gude_reg_dtm);
            //}

            StartCoroutine("ProcessPlayExam");
        }
    }

    public void InitDatabase()
    {
        if (m_bIsLoadDatabases)
            return;

        m_bIsLoadDatabases = true;

        m_listDatabase = new List<GameObject>();

        for (int i = 0; i < CQuizData.Instance.GetGuides().body.contents.Length; i++)
        {
            GameObject goDatabase = Instantiate(Resources.Load("Prefabs/LGTKDatabase") as GameObject);
            goDatabase.transform.parent = m_goDatabaseContent.transform;
            goDatabase.GetComponent<CObjectLGTKDatabase>().InitLGTkDatabase(i, -1);
            m_listDatabase.Add(goDatabase);



            for (int j = 0; j < CQuizData.Instance.GetGuides().body.contents[i].children.Length; j++)
            {
                GameObject goDatabaseChildren = Instantiate(Resources.Load("Prefabs/LGTKDatabase") as GameObject);
                goDatabaseChildren.transform.parent = m_goDatabaseContent.transform;
                goDatabaseChildren.GetComponent<CObjectLGTKDatabase>().InitLGTkDatabase(i, j);
                m_listDatabase.Add(goDatabaseChildren);
            }
            //goDropdown.GetComponent<CObjectLGTKDropdown>().InitLGTKDropdown(quizLGTK.set_gudes[i].gude_nm, quizLGTK.set_gudes[i].gude_seur_grd, quizLGTK.set_gudes[i].gude_reg_dtm);
        }

        UpdateDatabase();
    }

    public void UpdateDatabase()
    {
        int nRealSize = 0;
        for(int i =0; i < m_listDatabase.Count; i++)
        {
            //Debug.Log("UpdateDataBase : " + m_listDatabase[i].GetComponent<CObjectLGTKDatabase>().GetRegData());

            if ( m_listDatabase[i].activeSelf )
            {
                nRealSize++;
            } 
        }

        m_goDatabaseContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, nRealSize * 40);
    }

    public void UpdateDatabaseChildren(int nParentIndx)
    {
        for (int i = 0; i < m_listDatabase.Count; i++)
        {
            Debug.Log("ShowDatabaseChildren!!");
            m_listDatabase[i].GetComponent<CObjectLGTKDatabase>().UpdateDatabase(nParentIndx);
        }

        UpdateDatabase();

    }

    public void UpdateDatabaseDynamic()
    {
        for (int i = 0; i < m_listDatabase.Count; i++)
        {
            m_listDatabase[i].GetComponent<CObjectLGTKDatabase>().UpdateDatabaseDynamic();
        }

        UpdateDatabase();
    }

    public void UpdateDatabaseDetail(string strTitle, string strImageURL)
    {
        StopCoroutine("ProcessDatabaseDetail");
        Debug.Log("UpdateDatabaseDetail URL : " + strImageURL);
        m_txtDatabaseDetailTitle.text = strTitle;
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            m_strDatabaseDetailURL = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjEwMDlfMjA4%2FMDAxNjY1Mjg5NjkwNDcw.PU1zLsWkwUFVqasfKdg3isaQrWZWu6tKRbxYcgvtKJ0g.oXT70SvfyeTxN1y_bY2__QQF8tciooCjZMGuzjouCjYg.JPEG.dedoeoh%2FFejY9GNaAAEvJSc.jpeg&type=sc960_832";
            m_strDatabaseDetailURL = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA3MTdfNTMg%2FMDAxNjU4MDY2Njc4OTcz.OYw-Tbuc6c-0r1NqzEfLfA7tLURKm8W1-miahUnJNw4g.-KAfdfOpX7gPZBeD6LRJkqFKX6uCs2XOK5WBRXJinTog.JPEG.dltpdud03%2Fd147c230d461afdebf293d31ca8928150c6db88e%25A3%25DFs2%25A3%25DFn2.jpg&type=sc960_832";
        }
        else
            m_strDatabaseDetailURL = Server.Instance.GetCurURL() + strImageURL;
        StartCoroutine("ProcessDatabaseDetail");
    }

    IEnumerator ProcessDatabaseDetail()
    {
        m_imgDatabaseDetail.color = new Color(1, 1, 1, 0);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(m_strDatabaseDetailURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //Debug.Log("Width : " + myTexture.width + ", Height : " + myTexture.height);
            
            // Width : 760
            float fRate = (float)760 / (float)myTexture.width;
            //Debug.Log("Database Rate : " + fRate);
            m_imgDatabaseDetail.GetComponent<RectTransform>().sizeDelta = new Vector2(760, myTexture.height * fRate);
            Rect rect = new Rect(0, 0, myTexture.width, myTexture.height);
            //Rect rect = new Rect(0, 0, 760, myTexture.height * fRate);
            m_imgDatabaseDetail.sprite = Sprite.Create(myTexture, rect, new Vector2(760, myTexture.height * fRate));
            m_goDatabaseDetail.GetComponent<RectTransform>().sizeDelta = new Vector2(0, myTexture.height * fRate);
            m_imgDatabaseDetail.color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        int nRequestTimer = 0;

        m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if ((nRequestTimer % 5) == 0)
            {
                Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("LGTK").part_idx);
            }
            nRequestTimer++;


            if (m_nRemainTime <= 0)
                break;
        }

        HideAllPopup();
        ShowPopupTimeOver();
    }
    //IEnumerator ProcessQuiz()
    //{

    //}
    public void OnClickAgreeExit()
    {
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideCenterPage();
    }

    public void OnClickExit()
    {
        ShowPopupToLobby();
    }

    public void ShowTalkBox()
    {
        m_goTalkBox.SetActive(true);
    }

    public void HideTalkBox()
    {
        m_goTalkBox.SetActive(false);
    }

    public void HideAllPopup()
    {
        HidePopupFinish();
        HidePopupTimeOver();
    }

    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
    }

    public void HidePopupFinish()
    {
        m_goPopupFinish.SetActive(false);
    }

    public void OnClickPopupFinishOK()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void ShowPopupTimeOver()
    {
        m_goPopupTimeOver.SetActive(true);
    }

    public void HidePopupTimeOver()
    {
        m_goPopupTimeOver.SetActive(false);
    }

    public void OnClickPopupTimeOverLobby()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);

        Debug.Log("OnClickPopupFinishOK");
        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("LGTK").part_idx, 2);
        CUIsSpaceManager.Instance.HideCenterPage();


    }

    // Popup To Lobby -------------------------------
    public void ShowPopupToLobby()
    {
        //if( m_bIsTutorial )
        //{
        //    CUIsSpaceManager.Instance.ScreenActive(false, true);
        //    CUIsSpaceManager.Instance.HideCenterPage();
        //    return;
        //}

        if (CQuizData.Instance.GetEnableExitCount() > 0)
        {
            m_goPopupToLobby.SetActive(true);
            m_txtPopupToLobbyMsg.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";
        }
        else
        {
            m_goPopupToLobbyOver.SetActive(true);
            m_txtPopupToLobbyOverMsg.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";
        }

        StartCoroutine("ProcessToLobbyRemainTime");
    }

    IEnumerator ProcessToLobbyRemainTime()
    {
        while (true)
        {
            int nRemainTime = m_nRemainTime;
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            if (m_goPopupToLobby.activeSelf)
                m_txtPopupToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if (m_goPopupToLobbyOver.activeSelf)
                m_txtPopupToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
    }

    public void OnClickPopupToLobbyToLobby()
    {
        Server.Instance.RequestPUTActionExit();
        HidePopupToLobby();
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideCenterPage();
    }

    public void OnClickPopupToLobbyExit()
    {
        HidePopupToLobby();
    }
    // -----------------------------------------------------------

    public void OnClickTalkBox()
    {
        
        if (m_bIsFirstOpen)
        {
            ShowTalkBox();

            m_goTalkBox.GetComponent<CUIsLGTKTalkBoxManager>().InitLGTKTalkBoxMansger();

            m_vecTalkBoxPoz = m_goTalkBox.GetComponent<RectTransform>().localPosition;

            if( !m_bIsTutorial)
                m_bIsFirstOpen = false;
        } else
        {
            m_goTalkBox.GetComponent<RectTransform>().localPosition = m_vecTalkBoxPoz;
        }
        
    }

    public void SetTutorial(bool bIsTutorial)
    {
        m_bIsTutorial = bIsTutorial;
    }

    public bool IsTutorial()
    {
        return m_bIsTutorial;
    }

    public void AddListAnswers(string strAnswer)
    {
        m_listAnswers.Add(strAnswer);
    }

    public List<string> GetListAnswers()
    {
        return m_listAnswers;
    }

    public void AddListSBCTAnswer(string strAnswer)
    {
        m_listSBCTAnswer.Add(strAnswer);
    }

    public List<string> GetListSBCTAnswer()
    {
        return m_listSBCTAnswer;
    }
}
