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

    //private bool m_bIsDynamic = false;
    //private int m_nDynamicState = 0;

    private bool m_bIsPlanet = false;
    private bool m_bIsFairway = false;

    private int m_nFairwayDatabaseIndex = 0;
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
        //// SubIndex°¡ -1ÀÌ¸é µª½º ¿ø
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

        if (m_nDepth == 1)
        {
            m_listDepthContent[0].SetActive(true);
            m_listDepthContent[1].SetActive(false);

            m_listMainType[0].SetActive(false);
            m_listMainType[1].SetActive(false);

            if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].image_path.Equals(""))
            {
                m_nType = 0;
            }
            else
            {
                m_nType = 1;
                m_strImageURL = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].image_path;
            }

            m_listMainType[m_nType].SetActive(true);

            //    //if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 3).Equals("$$$"))
            if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("$"))
            {
                m_txtMainTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(1, CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Length - 1);
                m_bIsPlanet = true;
                gameObject.SetActive(false);
                //} else if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 3).Equals("###"))

            }
            else if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("#"))
            {
                m_txtMainTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(1, CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Length - 1);
                m_bIsFairway = true;
                m_nFairwayDatabaseIndex = m_nDatabaseIndex;
                gameObject.SetActive(false);
            }
            //if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("$"))
            //{
            //    m_txtMainTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(1, CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Length - 1);

            //    m_strTitle = m_txtMainTitle.text;

            //    Debug.Log("!!!!!!!!!!!!!! : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Length);

            //    if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Contains("Çà¼º"))
            //    {
            //        m_bIsPlanet = true;
            //    }

            //    if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Contains("Ç×·Î"))
            //    {
            //        m_bIsFairway = true;
            //        m_nFairwayDatabaseIndex = m_nDatabaseIndex;
            //    }

            //    gameObject.SetActive(false);
            //}
            else
            {
                m_txtMainTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title;
                m_strTitle = m_txtMainTitle.text;
            }
        }
        else
        {
            m_listDepthContent[0].SetActive(false);
            m_listDepthContent[1].SetActive(true);

            m_txtSubTitle.text = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title;
            m_strTitle = m_txtSubTitle.text;

            m_strImageURL = CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].image_path;

            if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("$"))
                //if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 9).Equals("$¼±ÅÃ Ç×·Î ¿¹Ãø"))
            {
                m_bIsPlanet = true;
            }
            else if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("#"))
            //else if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 9).Equals("$¼±ÅÃ Çà¼º ÀÚ·á"))
            {
                m_bIsFairway = true;
                m_nFairwayDatabaseIndex = m_nDatabaseIndex;
            }

            gameObject.SetActive(false);

            //if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("$"))
            //{
            //    // TODO DEL
            //    //m_bIsDynamic = true;

            //    if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Contains("Çà¼º"))
            //    {
            //        //Debug.Log("InitLGTkDatabase 02 Planet : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title);
            //        m_bIsPlanet = true;
            //    }

            //    if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Contains("Ç×·Î"))
            //    {
            //        //Debug.Log("InitLGTkDatabase 02 Fairway : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title);
            //        Debug.Log("InitLGTkDatabase 02 Fairway : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title);

            //        m_bIsFairway = true;
            //        m_nFairwayDatabaseIndex = m_nDatabaseIndex;
            //    }
            //}

            //gameObject.SetActive(false);
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

    public void UpdateFairway()
    {
        if( m_bIsFairway)
            CUIsLGTKManager.Instance.UpdateDatabaseChildren(m_nFairwayDatabaseIndex);
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

    public bool IsFairwayActive()
    {
        if (m_nType == 1)
        {
            if (m_bIsFairway)
            {
                return gameObject.activeSelf;
            }
        }

        return false;
    }

    public void ShowFairwayActive()
    {
        if (m_nType == 1)
        {
            if (m_bIsFairway)
            {
                for (int i = 0; i < CUIsLGTKManager.Instance.GetListFairwayAnswers().Count; i++)
                {
                    // Fix Fairway
                    //if (CUIsLGTKManager.Instance.GetListFairwayAnswers()[i].Equals(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title))
                    string strFairwayAnswer = RemoveSPString(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title);
                    if (CUIsLGTKManager.Instance.GetListFairwayAnswers()[i].Equals(strFairwayAnswer) )
                    {
                        gameObject.SetActive(true);
                        break;
                    }
                }
            }

        }
    }

    public void UpdateDatabase(int nParentIndex)
    {
        //Debug.Log("UpdateDatebase 0000 !!!!!");
        if (m_nParentIndex == nParentIndex)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            else
            {
                if( m_bIsPlanet )
                {
                    for(int i = 0; i < CUIsLGTKManager.Instance.GetListPlanetAnswers().Count; i++)
                    {
                        if (CUIsLGTKManager.Instance.GetListPlanetAnswers()[i].Equals(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title))
                        {
                            gameObject.SetActive(true);
                            break;
                        }
                    }

                    return;
                }

                if( m_bIsFairway )
                {
                    for (int i = 0; i < CUIsLGTKManager.Instance.GetListFairwayAnswers().Count; i++)
                    {
                        //Debug.Log("LGTKDatabase FairwayAnswer :  " + CUIsLGTKManager.Instance.GetListFairwayAnswers()[i]);
                        //if (CUIsLGTKManager.Instance.GetListFairwayAnswers()[i].Equals(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title))
                        string strFairwayAnswer = RemoveSPString(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title);
                        if (CUIsLGTKManager.Instance.GetListFairwayAnswers()[i].Equals(strFairwayAnswer))
                        {
                            gameObject.SetActive(true);
                            break;
                        }
                    }

                    return;
                }

                gameObject.SetActive(true);

                //if (m_bIsDynamic)
                // TODO DEL
                //{
                //    if (m_nDynamicState == 0)
                //    {
                //        Debug.Log("Dynamic 0004");
                //        for (int i = 0; i < CUIsLGTKManager.Instance.GetListAnswers().Count; i++)
                //        {
                //            Debug.Log("Dynamic 0005");
                //            if (CUIsLGTKManager.Instance.GetListAnswers()[i].Equals(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title))
                //            {
                //                Debug.Log("Dynamic 0006");
                //                gameObject.SetActive(true);
                //                break;
                //            }
                //        }
                //        return;
                //    }
                //    else
                //    {
                //        Debug.Log("Dynamic 0007");
                //        for (int i = 0; i < CUIsLGTKManager.Instance.GetListSBCTAnswer().Count; i++)
                //        {
                //            Debug.Log("Dynamic 0008");
                //            if (CUIsLGTKManager.Instance.GetListSBCTAnswer()[i].Equals(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[m_nSubIndex].title))
                //            {
                //                Debug.Log("Dynamic 0009");
                //                gameObject.SetActive(true);
                //                break;
                //            }
                //        }
                //        return;
                //    }
                //}

                //gameObject.SetActive(true);
            }
        }
    }

    public void UpdateDatabaseDynamic()
    {
        //Debug.Log("UpdateDatebaseDynamic 000 !!!!!");
        if (m_nDepth != 1)
            return;
        
        if (!(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("$") || CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].title.Substring(0, 1).Equals("#")))
            return;

        //Debug.Log("UpdateDatebaseDynamic !!!!!");
        bool bIsExist = false;
        for (int i = 0; i < CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children.Length; i++)
        {
            //Debug.Log("UpdateDatebaseDynamic !!!!! : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title);
            if (m_bIsPlanet)
            {
                //Debug.Log("UpdateDatebaseDynamic 01 - 00 !!!!! : " + CUIsLGTKManager.Instance.GetListPlanetAnswers().Count);
                for (int j = 0; j < CUIsLGTKManager.Instance.GetListPlanetAnswers().Count; j++)
                {
                    //Debug.Log("UpdateDatebaseDynamic !!!!! 02 : " + CUIsLGTKManager.Instance.GetListPlanetAnswers()[j]);
                    if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title.Equals(CUIsLGTKManager.Instance.GetListPlanetAnswers()[j]))
                    {
                        //Debug.Log("UpdateDatebaseDynamic !!!!! 03 : " + CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title);
                        //gameObject.SetActive(true);
                        bIsExist = true;
                        break;
                    }
                }
            }

            if (m_bIsFairway)
            {
                for (int j = 0; j < CUIsLGTKManager.Instance.GetListFairwayAnswers().Count; j++)
                {
                    //if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title.Equals(CUIsLGTKManager.Instance.GetListFairwayAnswers()[j]))
                    string strFairwayAnswer = RemoveSPString(CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title);
                    if (strFairwayAnswer.Equals(CUIsLGTKManager.Instance.GetListFairwayAnswers()[j]))
                    {
                        //gameObject.SetActive(true);
                        bIsExist = true;
                        break;
                    }
                }
            }

            // TODO DEL
            //if (m_nDynamicState == 0)
            //{
            //    for (int j = 0; j < CUIsLGTKManager.Instance.GetListAnswers().Count; j++)
            //    {
            //        Debug.Log("Dynamic 0001");
            //        if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title.Equals(CUIsLGTKManager.Instance.GetListAnswers()[j]))
            //        {
            //            Debug.Log("Dynamic 0002");
            //            bIsExist = true;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    for (int j = 0; j < CUIsLGTKManager.Instance.GetListSBCTAnswer().Count; j++)
            //    {
            //        Debug.Log("Dynamic 0011");
            //        if (CQuizData.Instance.GetGuides().body.contents[m_nMainIndex].children[i].title.Equals(CUIsLGTKManager.Instance.GetListSBCTAnswer()[j]))
            //        {
            //            Debug.Log("Dynamic 0012");
            //            bIsExist = true;
            //            break;
            //        }
            //    }
            //}
        }

        gameObject.SetActive(bIsExist);
    }

    public string RemoveSPString(string strValue)
    {
        string pattern = "[^0-9a-zA-Z°¡-ÆR]"; //¼ýÀÚ, ¿µ¹®ÀÚ, ÇÑ±Û ÀÌ¿ÜÀÇ ¹®ÀÚ¸¦ Ã£À½
        string resultString = System.Text.RegularExpressions.Regex.Replace(strValue, pattern, "");
        pattern = @"\s+";
        resultString = System.Text.RegularExpressions.Regex.Replace(resultString, pattern, "");

        return resultString;
    }
}

