using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class CUIsCommonPopupFinish : MonoBehaviour
{
    public Text m_txtMainMsg;
    public Text m_txtSubMsg;

    private int m_nPartIndex;
    private int m_nFinishType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCommonPopupFinish(int nPartIdex, int nType = 0, string strMainMsg = "�̼� ����Ϸ�", string strSubMsg = "�ش� �̼��� ���������� �����Ͽ����ϴ�.")
    {
        m_nPartIndex = nPartIdex;
        m_txtMainMsg.text = strMainMsg;
        m_txtSubMsg.text = strSubMsg;
        m_nFinishType = nType;
    }

    public void OnClickOK()
    {
        Server.Instance.RequestPUTQuestionsStatus(m_nPartIndex, 1);
        if (m_nFinishType == 0) CSpaceAppEngine.Instance.SetFinishLeft01(true);
        else if (m_nFinishType == 1) CSpaceAppEngine.Instance.SetFinishLeft02(true);
        else if (m_nFinishType == 2) CSpaceAppEngine.Instance.SetFinishCenter(true);
        else if (m_nFinishType == 3) CSpaceAppEngine.Instance.SetFinishRight(true);

        CSpaceAppEngine.Instance.UpdateMissionClear();

        //Server.Instance.RequestPUTQuestionsStatus(m_nPartIndex, 1);
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideCommonPopupsFinish();
    }
}
