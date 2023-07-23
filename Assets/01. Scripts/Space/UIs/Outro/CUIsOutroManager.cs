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

        m_listMsg[0] = "���ϵ帳�ϴ�! �־��� �̼��� �ϼ��ϼ̱���. ������ �Բ��� ���� ��ڳ׿�.\n���� ������ ������ �����־��. ���� ���⼭ �λ�帮����, ������ Ž������� �����ҰԿ�!";
        m_listMsg[1] = "�ӳ��� �ɿ��ֿ��� �־��� �̼ǿ� �ּ��� ���� ����� ��ǥ Ž�� �������� ���ظ� ����մϴ�.\n���� �쿩������ ������ ������ �־����� ��ħ�� ���������� ���̾�Ͼ� Ž�� �̼��� �����س½��ϴ�.\nŽ�� �̼��� �ϼ��ϰ� ������ ��ȯ�� ����� ������ ���� ���� �ô��� �������� �������� ���� ���Դϴ�.";

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
