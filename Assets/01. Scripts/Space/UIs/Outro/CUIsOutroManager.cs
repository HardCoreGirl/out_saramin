using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsOutroManager : MonoBehaviour
{
    public GameObject[] m_listOutroPage = new GameObject[2];
    public Text[] m_listTxtMsg = new Text[2];
    public GameObject[] m_listBtnNext = new GameObject[2];

    private string[] m_listMsg = new string[2];
    
    

    private int m_nPage = 0;

    private float m_fTypingInterval = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitUIs()
    {
        //CSpaceAppEngine.Instance.PlayMoveCenter();

        Debug.Log("OutroManager !!!!!!!!!!!!!!!!!!!");

        m_listMsg[0] = "축하드립니다! 주어진 미션을 완수하셨군요. 옆에서 함께한 저도 기쁘네요.\n아직 마지막 절차가 남아있어요. 저는 여기서 인사드리지만, 끝까지 탐사원님을 응원할게요!";
        m_listMsg[1] = "머나먼 심우주에서 주어진 미션에 최선을 다한 당신은 목표 탐사 지점으로 항해를 계속합니다.\n많은 우여곡절과 위기의 순간이 있었지만 마침내 성공적으로 파이어니어 탐사 미션을 수행해냈습니다.\n탐사 미션을 완수하고 지구로 귀환한 당신의 여정은 우주 개발 시대의 기념비적인 업적으로 기억될 것입니다.";

        m_listOutroPage[0].SetActive(true);
        m_listOutroPage[1].SetActive(false);

        m_listBtnNext[0].SetActive(false);

        StartCoroutine("ProcessMsg");
    }

    public void OnClickNext(int nIndex)
    {
        if(nIndex == 0)
        {
            m_nPage = 1;
            m_listOutroPage[0].SetActive(false);
            m_listOutroPage[1].SetActive(true);

            m_listBtnNext[1].SetActive(false);

            StartCoroutine("ProcessMsg");
        } else
        {
            //Application.OpenURL("www.naver.com");

            string strExUrl;
            if( CSpaceAppEngine.Instance.IsFaceTest() )
            {
                strExUrl = "/exams_setting";
            } else
            {
                strExUrl = "/exam_end";
            }

            //string url = Server.Instance.GetFaceTestCurURL() + strExUrl;
            string url = Server.Instance.GetPLLabCurURL() + strExUrl;
            Application.ExternalEval("window.location.href='" + url + "'");
        }
    }

    IEnumerator ProcessMsg()
    {
        for (int i = 0; i < m_listMsg[m_nPage].Length; i++)
        {
            m_listTxtMsg[m_nPage].text = m_listMsg[m_nPage].Substring(0, i);

            yield return new WaitForSeconds(m_fTypingInterval);
        }

        m_listTxtMsg[m_nPage].text = m_listMsg[m_nPage];

        m_listBtnNext[m_nPage].SetActive(true);
    }
}
