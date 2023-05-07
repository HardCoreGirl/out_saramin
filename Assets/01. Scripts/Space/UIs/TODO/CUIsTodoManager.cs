using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

public class CUIsTodoManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsTodoManager _instance = null;

    public static CUIsTodoManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log(" CUIsTodoManager install null");

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

    public GameObject m_goTodo;

    public Image[] m_listTodoBG = new Image[4];
    public GameObject[] m_listNormalTitle = new GameObject[4];
    public GameObject[] m_listFinishTitle = new GameObject[4];

    public Text m_txtDummy;

    public Text m_txtTodoCnt;
    public GameObject[] m_listTodoContent = new GameObject[4];
    public Text[] m_txtTodoTitle = new Text[4];
    public Text[] m_txtTodoContent = new Text[4];

    private int m_nPozIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //m_txtDummy.text = "111111\n22222\n33333";
        m_txtDummy.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUIs()
    {

    }

    public void UpdateDummyTodo()
    {
        string strMsg = "";
        for(int i = 0; i < CQuizData.Instance.GetInfoMission().body.Length; i++)
        {
            strMsg = strMsg + CQuizData.Instance.GetInfoMission().body[i].title + "\n" + CQuizData.Instance.GetInfoMission().body[i].content + "\n";
        }

        m_txtDummy.text = strMsg;   
    }

    public void UpdateTodo()
    {
        m_txtTodoCnt.text = "(1/" + CQuizData.Instance.GetInfoMission().body.Length.ToString()+ ")";

        for(int i = 0; i < m_listTodoContent.Length; i++)
        {
            m_listTodoContent[i].SetActive(false);
        }

        for(int i=0; i < m_txtTodoTitle.Length; i++)
        {
            if (i >= CQuizData.Instance.GetInfoMission().body.Length)
                break;

            m_listTodoContent[i].SetActive(true);

            m_txtTodoTitle[i].text = CQuizData.Instance.GetInfoMission().body[i].title;
            m_txtTodoContent[i].text = CQuizData.Instance.GetInfoMission().body[i].content;
        }
    }

    public void UpdateTodoCnt(int nCnt)
    {
        m_txtTodoCnt.text = "(" + nCnt.ToString() + "/4)";
    }

    public void OnClickTitle()
    {
        if(m_nPozIndex == 0 )
        {
            m_nPozIndex = 1;

            Debug.Log("Top 01 : " + m_goTodo.GetComponent<RectTransform>().anchoredPosition.x);
            Debug.Log("Top 02 : " + m_goTodo.GetComponent<RectTransform>().anchoredPosition.y);
            //StartCoroutine("ProcessMoveUpTodo");
            // 1016

            //m_goTodo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30f, 390f);

            m_goTodo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(440f, -363.5004f), 1f);
            //m_goTodo.GetComponent<RectTransform>().DOLocalMove(new Vector3(-30f, 390f, 0f), 3f);
            //m_goTodo.transform.DOLocalMove(new Vector3(-30f, 390f, 0f), 3f);

        } else if(m_nPozIndex == 1)
        {
            m_nPozIndex = 0;

            m_goTodo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(440f, -676.5004f), 1f);

            //m_goTodo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30f, 1016f);

            //m_goTodo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-30f, 1016f), 3f);

            //m_goTodo.GetComponent<RectTransform>().DOLocalMove(new Vector3(-30f, 1016f, 0f), 3f);
        }
    }

    IEnumerator ProcessMoveUpTodo()
    {
        Vector2 vecPoz = m_goTodo.GetComponent<RectTransform>().anchoredPosition;

        while (true)
        {
            vecPoz = m_goTodo.GetComponent<RectTransform>().localPosition;
            vecPoz.y -= (Time.deltaTime * 5f);

            if (vecPoz.y <= 390)
                break;

            m_goTodo.GetComponent<RectTransform>().anchoredPosition = vecPoz;
            yield return new WaitForEndOfFrame();
        }

        m_goTodo.GetComponent<RectTransform>().anchoredPosition = new Vector3(-30f, 390f, 0);
    }

    IEnumerator ProcessMoveDownTodo()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void UpdateSlot(int nSlot, bool bIsFinish)
    {
        if(bIsFinish)
        {
            m_listTodoBG[nSlot].color = new Color(0.8627451f, 0.9176471f, 1);
            m_listNormalTitle[nSlot].SetActive(false);
            m_listFinishTitle[nSlot].SetActive(true);
        } else
        {
            m_listTodoBG[nSlot].color = new Color(1, 1, 1);
            m_listNormalTitle[nSlot].SetActive(true);
            m_listFinishTitle[nSlot].SetActive(false);
        }
    }

    public void OnClickSlot(int nSlot)
    {
        if(nSlot == 0)
        {
            if (CSpaceAppEngine.Instance.IsFinishLeft01())
                return;
            CUIsSpaceManager.Instance.OnClickLeftComputer();
        } else if (nSlot == 1)
        {
            if (CSpaceAppEngine.Instance.IsFinishLeft02())
                return;
            CUIsSpaceManager.Instance.OnClickCenterComputer();
        } else if (nSlot == 2)
        {
            CUIsSpaceManager.Instance.OnClickCenterComputer();
        } else if (nSlot == 3)
        {
            CUIsSpaceManager.Instance.OnClickRightComputer();
        }


        if (CSpaceAppEngine.Instance.IsFinishLeft01() && CSpaceAppEngine.Instance.IsFinishLeft02())
            return;
    }
}
