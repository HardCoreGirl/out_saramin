using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectLGTKDatabase : MonoBehaviour
{
    public GameObject[] m_listDepthContent = new GameObject[2];
    public GameObject[] m_listMainType = new GameObject[2];
    public Text m_txtMainTitle;
    public Text m_txtSubTitle;
    public Text m_txtRegData;
    public Text m_txtSecurityGrade;
    private int m_nMainIndex;
    private int m_nSubIndex;

    private int m_nDepth;
    private int m_nType;
    private int m_nDatabaseIndex;
    private int m_nParentIndex = -1;

    private string m_strTitle;
    private string m_strImageURL;
    //private STGuidesBodyContents m_stGuideContent;
    //private STGuidesBodyContents m_stGuideContentChirend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTkDatabase(int nMainIndex, int nSubIndex)
    {
        m_nMainIndex = nMainIndex; 
        m_nSubIndex = nSubIndex;

        //m_stGuideContent = new STGuidesBodyContents();
        //// SubIndex가 -1이면 뎁스 원
        if (m_nSubIndex == -1)
        {
            m_nDatabaseIndex = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].idx;
            m_nDepth = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].depth;
            m_txtRegData.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].reg_date;
            m_txtSecurityGrade.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].security_grade;
        } else
        {
            m_nType = 1;
            m_nDatabaseIndex = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].idx;
            m_nParentIndex = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].parent_idx;
            m_nDepth = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].depth;
            m_txtRegData.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].reg_date;
            m_txtSecurityGrade.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].security_grade;
        }

        if(m_nDepth == 1)
        {
            m_listDepthContent[0].SetActive(true);
            m_listDepthContent[1].SetActive(false);

            m_listMainType[0].SetActive(false);
            m_listMainType[1].SetActive(false);

            if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].image_path.Equals(""))
            {
                m_nType = 0;
            } else
            {
                m_nType = 1;
                m_strImageURL = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].image_path;
            }

            m_listMainType[m_nType].SetActive(true);

            m_txtMainTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title;
            m_strTitle = m_txtMainTitle.text;
        } else
        {
            m_listDepthContent[0].SetActive(false);
            m_listDepthContent[1].SetActive(true);

            m_txtSubTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title;
            m_strTitle = m_txtSubTitle.text;

            m_strImageURL = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].image_path;

            gameObject.SetActive(false);
        }
        //    m_stGuideContent = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex];
        //else
        //    m_stGuideContent = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex];
            //m_stGuideContent = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex];
    }

    public void OnClickContent()
    {
        if(m_nType == 0)
        {
            CUIsLGTKManager.Instance.UpdateDatabaseChildren(m_nDatabaseIndex);
        } else
        {
            CUIsLGTKManager.Instance.UpdateDatabaseDetail(m_strTitle, m_strImageURL);
        }
    }

    public string GetRegData()
    {
        return m_txtRegData.text;
    }

    public void UpdateDatabaseStatus(int nParentIndex)
    {
        Debug.Log("UpdateDatabaseStatus");
    }

    public void ShowDatabase(int nParentIndex)
    {
        if (m_nParentIndex == nParentIndex)
            gameObject.SetActive(true);
    }

    public void UpdateDatabase(int nParentIndex)
    {
        if (m_nParentIndex == nParentIndex)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }
}
