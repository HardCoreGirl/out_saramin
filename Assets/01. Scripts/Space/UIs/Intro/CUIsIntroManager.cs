using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsIntroManager : MonoBehaviour
{
    public GameObject[] m_listIntroPage = new GameObject[4];

    public Text m_txtIntroMsg;
    public GameObject m_goBtnIntro;
    private string m_strIntroMsg;

    private int m_nPage;
    private float m_fTypingInterval = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitIntro()
    {
        m_strIntroMsg = "�ӳ��� ����, ����� ����Ž�缱 �����̾�Ͼ�� �Բ� ������ �����Ѵ�.\n��¼�� ��¼�� �� ���� �� �ֽ��ϴ�.���� ��� ������ ���� �׸��� �ϳ� �̿� �� �ֽ��ϴ�.�̱� ������ ���� ��, ������ ���ϴ�.\n���� ���� ���� �Ͼ ����� �� �� ���� ���ۿ��� �����µ�...";

        HideAllPage();
        m_nPage = 2;
        ShowPage(2);
    }

    public void HideAllPage()
    {
        for (int i = 0; i < m_listIntroPage.Length; i++)
            HidePage(i);
    }

    public void ShowPage(int nPage)
    {
        m_listIntroPage[nPage].SetActive(true);

        if (nPage == 2)
        {
            StartCoroutine("ProcessIntro");
        } else if( nPage == 3)
        {
            CSpaceAppEngine.Instance.StartTest();
            //CUIsSpaceManager.Instance.ShowTodo();
            m_listIntroPage[nPage].GetComponent<CUIsIntroSubpage>().InitIntroSubpage();
        }
    }

    IEnumerator ProcessIntro()
    {
        m_goBtnIntro.SetActive(false);
        if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL")) m_fTypingInterval = 0.01f;
        for(int i = 0; i < m_strIntroMsg.Length; i++)
        {
            m_txtIntroMsg.text = m_strIntroMsg.Substring(0, i);

            yield return new WaitForSeconds(m_fTypingInterval);
        }

        m_txtIntroMsg.text = m_strIntroMsg;
        m_goBtnIntro.SetActive(true);
    }

    public void HidePage(int nPage)
    {
        m_listIntroPage[nPage].SetActive(false);
    }

    public void OnClickNext()
    {
        HideAllPage();
        m_nPage++;
        if (m_nPage == 1)
            m_nPage = 2;
        ShowPage(m_nPage);
    }
}
