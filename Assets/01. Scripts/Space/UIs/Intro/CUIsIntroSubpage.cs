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
        m_listMsg[0] = "Ž�����!";
        m_listMsg[1] = "Ž�����, ����̱���. ������ 2053�� 3�� 17���Դϴ�. ���� �����ð� �������ο�. Ž������� ���鿡 �����\n ��Ȯ�� 1,058��° �Ǵ� ���̿���. �������� �ƴ� �ݰ��׿�. ������� ��Ű���?";
        m_listMsg[2] = "���� Ž������� Ÿ�� ��� <���̾�Ͼ�> Ž�缱�� AI�κ��Դϴ�. Ž������� �̼� ������ ���� �־��.\n...�ƹ� ����� �� ���ó���?";
        m_listMsg[3] = "��...�׷��ٸ� �������� ���� ���ۿ��� ������ �� �մϴ�. Ž������� �̸��� ��ﳪ�ó���?";
        m_listMsg[4] = "";
        m_listMsg[5] = "������ �̸��� ��Ȯ�� ����ϰ� ��ó׿�, {$NAME$} Ž�����!\n���鿡�� ����� �� üũ�� ��ü ����Ż ����� ��� �������� Ȯ�εƽ��ϴ�. �ƹ����� ��� ���� ������ɿ�\n���ۿ��� �߻��� �� �����ϴ�.";
        m_listMsg[6] = "�ʹ� ����������. ���� {$NAME$} Ž�������\n����� ��ã�� ����Ž�缱 ��Ȱ�� �����Ͻ� �� �ֵ��� ���͵帱�Կ�.";
        m_listMsg[7] = "�� ��������! �׷� ���� ���� Ž������� �̼Ǽ��� ������ ���ؼ� �˷��帮�ڽ��ϴ�.";
        m_listMsg[8] = "Ž������� ����ũ ������ �Ʒ��� ���ø�, [TO DO LIST]�� �ֽ��ϴ�. �־��� �ð����� ���� ���� �̼ǵ���\n ��� �ϼ����ּž� �մϴ�. � �̼��� �ϼ��ؾ� �ϴ��� Ȯ���غ��ðھ��?";
        m_listMsg[9] = "����ũ�� �� ���� �ý����� ���̽���? ������ [����Ž��� ���� �ý���], �������� [��ü ���� �ý���],\n �׸��� ����� [���̾�Ͼ� ���� �ý���]�Դϴ�. �� �ý��ۿ��� �ʿ��� �̼��� �����Ͻ� �� �־��.\n�̼� ������ �ý��� ȭ�鿡 ��Ÿ�� ���� �̴ϴ�. �ѹ� Ȯ���غ��ðھ��?";
        m_listMsg[10] = "���� �̼� ���� ����� �����Ͻ� �� ���׿�. � �̼��� ���� ���������� Ž������� �����Ӱ� ���Ͻø� �˴ϴ�.\n�̼��� �����Ͻô� ���� ���� �� Ž����� �翡 �����Կ�. �ٽ� �ȳ��� �ʿ��Ͻø� ���� �ҷ��ּ���.";

        m_listBtnMsg[0] = "...?";
        m_listBtnMsg[1] = "...�� ������?";
        m_listBtnMsg[2] = "...�� �𸣰ھ�.";
        m_listBtnMsg[3] = "�� �̸���...";
        m_listBtnMsg[4] = "";
        m_listBtnMsg[5] = "�׷� �����?";
        m_listBtnMsg[6] = "����.";
        m_listBtnMsg[7] = "��Ź��.";
        m_listBtnMsg[8] = "�˰ھ�.";
        m_listBtnMsg[9] = "�˰ھ�.";
        m_listBtnMsg[10] = "����. ���� �̼��� �����Ұ�.";


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
