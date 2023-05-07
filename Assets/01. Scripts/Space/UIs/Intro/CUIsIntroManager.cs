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
        m_strIntroMsg = "머나먼 우주, 당신은 우주탐사선 ‘파이어니어’와 함께 여정을 시작한다.\n어쩌고 저쩌고 된 마디씩 별 있습니다.내일 언덕 무성할 마디씩 그리워 하나 이웃 별 있습니다.이국 마리아 같이 잠, 흙으로 봅니다.\n오랜 동면 끝에 일어난 당신은 알 수 없는 부작용을 느끼는데...";

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
