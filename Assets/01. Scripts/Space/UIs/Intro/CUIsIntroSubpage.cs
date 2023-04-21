using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsIntroSubpage : MonoBehaviour
{
    public Text m_txtMsg;
    public GameObject m_goBtnNext;
    public GameObject m_goBtnLast;

    public GameObject m_goBlack;

    public GameObject m_goName;
    public Text m_txtName;
    public TMPro.TMP_InputField m_ifName;

    private string[] m_listMsg = new string[11];
    private string[] m_listBtnMsg = new string[11];

    private int m_nSubpage = 0;

    private float m_fTypingInterval = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitIntroSubpage()
    {
        m_listMsg[0] = "탐사원님!";
        m_listMsg[1] = "탐사원님, 깨어나셨군요. 오늘은 2053년 3월 17일입니다. 물론 지구시간 기준으로요. 탐사원님이 동면에 드신지\n 정확히 1,058일째 되는 날이에요. 오랜만에 뵈니 반갑네요. 컨디션은 어떠신가요?";
        m_listMsg[2] = "저는 탐사원님이 타고 계신 <파이어니어> 탐사선의 AI로봇입니다. 탐사원님의 미션 수행을 돕고 있어요.\n...아무 기억이 안 나시나요?";
        m_listMsg[3] = "흠...그렇다면 동면으로 인한 부작용이 있으신 듯 합니다. 탐사원님의 이름은 기억나시나요?";
        m_listMsg[4] = "";
        m_listMsg[5] = "다행히 이름은 정확히 기억하고 계시네요, {$NAME$} 탐사원님!\n동면에서 깨어나실 때 체크한 신체 바이탈 기능은 모두 정상으로 확인됐습니다. 아무래도 몇몇 기억과 인지기능에\n부작용이 발생한 것 같습니다.";
        m_listMsg[6] = "너무 걱정마세요. 제가 {$NAME$} 탐사원님이\n기억을 되찾고 우주탐사선 생활에 적응하실 수 있도록 도와드릴게요.";
        m_listMsg[7] = "별 말씀을요! 그럼 제일 먼저 탐사원님의 미션수행 절차에 대해서 알려드리겠습니다.";
        m_listMsg[8] = "탐사원님의 데스크 오른쪽 아래를 보시면, [TO DO LIST]가 있습니다. 주어진 시간동안 여기 적힌 미션들을\n 모두 완수해주셔야 합니다. 어떤 미션을 완수해야 하는지 확인해보시겠어요?";
        m_listMsg[9] = "데스크에 세 개의 시스템이 보이시죠? 왼쪽은 [우주탐사원 관리 시스템], 오른쪽은 [기체 점검 시스템],\n 그리고 가운데는 [파이어니어 메인 시스템]입니다. 각 시스템에서 필요한 미션을 수행하실 수 있어요.\n미션 내용은 시스템 화면에 나타나 있을 겁니다. 한번 확인해보시겠어요?";
        m_listMsg[10] = "이제 미션 수행 방법을 숙지하신 것 같네요. 어떤 미션을 먼저 시작할지는 탐사원님이 자유롭게 정하시면 됩니다.\n미션을 진행하시는 동안 저는 늘 탐사원님 곁에 있을게요. 다시 안내가 필요하시면 저를 불러주세요.";

        m_listBtnMsg[0] = "...?";
        m_listBtnMsg[1] = "...넌 누구야?";
        m_listBtnMsg[2] = "...잘 모르겠어.";
        m_listBtnMsg[3] = "내 이름은...";
        m_listBtnMsg[4] = "";
        m_listBtnMsg[5] = "그럼 어떡하지?";
        m_listBtnMsg[6] = "고마워.";
        m_listBtnMsg[7] = "부탁해.";
        m_listBtnMsg[8] = "알겠어.";
        m_listBtnMsg[9] = "알겠어.";
        m_listBtnMsg[10] = "고마워. 이제 미션을 시작할게.";


        m_goBlack.SetActive(true);
        m_goName.SetActive(false);
        StartCoroutine("ProcessPage");
    }

    public void OnClickNextSubpage()
    {
        m_goBlack.SetActive(false);
        StopCoroutine("ProcessPage");

        //if (m_nSubpage == 4)
        //{
        //    if (m_ifName.text.Equals("")) return;

        //    CQuizData.Instance.SetUserName(m_ifName.text);
        //    PlayerPrefs.SetString("UserName", m_ifName.text);
        //}

        m_nSubpage++;

        if( m_nSubpage == 4 )
        {
            m_goName.SetActive(true);
            m_txtName.text = "";
            StartCoroutine("ProcessDisplayUserName");
        } 
        else
        {
            m_goName.SetActive(false);
        }

        StartCoroutine("ProcessPage");
    }

    IEnumerator ProcessDisplayUserName()
    {
        for(int i = 0; i <= CQuizData.Instance.GetUserName().Length; i++)
        {
            m_txtName.text = CQuizData.Instance.GetUserName().Substring(0, i);
            yield return new WaitForSeconds(m_fTypingInterval);
        }
    }

    public void OnClickFinishSubpage()
    {
        //CSpaceAppEngine.Instance.PlayFinishRobo();
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideIntro();
    }

    IEnumerator ProcessPage()
    {
        m_goBtnNext.SetActive(false);
        m_goBtnLast.SetActive(false);

        if( m_nSubpage == 1 )
        {
            m_txtMsg.text = "";
            CSpaceAppEngine.Instance.PlayAniRobo();
            yield return new WaitForSeconds(2f);
        }

        if( m_nSubpage == 5 || m_nSubpage == 6 )
        {
            m_listMsg[m_nSubpage] = m_listMsg[m_nSubpage].Replace("{$NAME$}", CQuizData.Instance.GetUserName());

            //Debug.Log("MSG : " + m_listMsg[m_nSubpage]);
        }

        if( m_nSubpage == 9 )
        {
            CSpaceAppEngine.Instance.PlayFinishRobo();
            for (int i = 0; i < m_listMsg[m_nSubpage].Length; i++)
            {
                m_txtMsg.text = m_listMsg[m_nSubpage].Substring(0, i);

                if (i == 40)
                {
                    Debug.Log(m_txtMsg.text);
                    CUIsLobbyManager.Instance.PlayIntroOutline(0);
                    yield return new WaitForSeconds(5f);
                }
                else if (i == 58)
                {
                    CSpaceAppEngine.Instance.PlayLookatRight();
                    Debug.Log(m_txtMsg.text);
                    CUIsLobbyManager.Instance.PlayIntroOutline(2);
                    yield return new WaitForSeconds(5f);
                }
                else if (i == 87)
                {
                    Debug.Log(m_txtMsg.text);
                    CUIsLobbyManager.Instance.PlayIntroOutline(1);
                    yield return new WaitForSeconds(5f);

                    CUIsLobbyManager.Instance.HideIntroOutlineAll();
                    CSpaceAppEngine.Instance.PlayLookatCenter();
                }

                yield return new WaitForSeconds(m_fTypingInterval);
            }
        }
        else
        {
            for (int i = 0; i < m_listMsg[m_nSubpage].Length; i++)
            {
                m_txtMsg.text = m_listMsg[m_nSubpage].Substring(0, i);

                yield return new WaitForSeconds(m_fTypingInterval);
            }
        }

        m_txtMsg.text = m_listMsg[m_nSubpage];

        if ( m_nSubpage == 10 )
        {
            m_goBtnLast.SetActive(true);
        }
        else
        {
            m_goBtnNext.SetActive(true);
            m_goBtnNext.GetComponentInChildren<Text>().text = m_listBtnMsg[m_nSubpage];
        }
        
    }
}
