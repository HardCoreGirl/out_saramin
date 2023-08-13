using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class STTestCheckBodyPartList
{
    public int part_idx;
    public int part_sort_seq;
}

[Serializable]
public class STTestCheckBody
{
    public int rct_idx;
    public int part_idx;
    public string qst_tp_cd;
    public int part_sort_seq;
    public int last_qst_idx;
    public int last_page_no;
    public string finish_yn;
    public string status;
    public string exm_cls_cd;
    public STTestCheckBodyPartList[] part_list;
}

[Serializable]
public class STTestCheck
{
    public int code;
    public string message;
    public STTestCheckBody body;
}

[Serializable]
public class Answers
{
    public int anwr_idx;
    public string anwr_brws_cd;
    public string anwr_cnnt;
}

[Serializable]
public class QSTDics
{
    public string dic_tp_cd;
    public string dic_wrd_nm;
    public int dic_scre;
}

[Serializable]
public class STTestAnswer
{
    public int test_anwr_idx;
    public string test_anwr_sbct;
}

[Serializable]
public class Questions
{
    public int set_dir_idx;
    public int test_qst_idx;
    //public int[] test_anwr_idx;
    //public int[] test_anwr_sbct;
    public STTestAnswer[] test_answers;
    public int test_prg_time;
    public int qst_idx;
    public string qst_exos_cd;
    public string qst_sove_cd;
    public string qst_ans_cd;
    public int qst_ans_cnt;
    //public QSTDics[] qst_dics;
    public int qst_dics;
    public string qst_brws_cnnt;
    public string qst_brws_cd;
    public string qst_cnnt;
    public Answers[] answers;
}

[Serializable]
public class Sets
{
    public string dir_tp_cd;
    public string dir_cnnt;
    public string qst_brws_cnnt;
    public string qst_brws_cd;
    public Questions[] questions;
}

[Serializable]
public class SetGudes
{
    public string gude_deth;
    public string gude_tp_cd;
    public string gude_nm;
    public string gude_seur_grd;
    public string gude_reg_dtm;
    public string gude_cnnt;
    public string gude_img;
}

[Serializable]
public class Quiz
{
    public int part_idx;    // 문항유형번호
    public int exm_time;    // 문항유형 검사시간
    public int progress_time;   // 문항유형 검사 남은 시간
    public string prg_st_cd;
    public string set_dir_tp_cd;
    public string set_dir_cnnt;
    public int guide_idx;
    public SetGudes[] set_gudes;
    public string qst_tp_cd;
    public int last_qst_idx;
    public int last_page_no;
    public Sets[] sets;
}

public class PacketQuiz
{
    public int code;
    public string message;
    public Quiz[] body;
}

public class PacketQuizPart
{
    public int code;
    public string message;
    public Quiz body;
}

[Serializable]
public class STPacketAnswerObject
{
    public string answer_type;
    public int answer_idx;
    public int[] answers;
}

[Serializable]
public class STPacketAnswerSubject
{
    public string answer_type;
    public int answer_idx;
    public int[] answers;
    public string[] contents;
    public float demerit_score;
}

[Serializable]
public class STPacketAnswer
{
    public int row_no;
    public string regr_id;
    public string modr_id;
    public string reg_dtm;
    public string mod_dtm;
    public string order_column;
    public string order_asc;
    public int page_size;
    public int page_number;
    public int page_offset;
    public string search_type;
    public int answer_seq_idx;
    public int applier_idx;
    public int part_idx;
    public int question_idx;
    public int answer_idx;
    public string answer_type;
    public int objective_answer;
    public string subjective_answer;
    public int[] answers;
    public string[] contents;
}

// Test Invest ---------------------------------
public class STPacketTestInvest
{
    public int code;
    public string message;
    public STPacketTestInvestBody body;
}

[Serializable]
public class STPacketTestInvestBody
{
    public STPacketTestInvestBodyApplier applier;
}

[Serializable]
public class STPacketTestInvestBodyApplier
{
    public int row_no;
    public int idx;
    public string applier_id;
    public string applier_no;
    public int exam_idx;
    public string username;
    public string password;
    public string status;
    public string email;
    public string phone_no;

    public int apply_count;
    public int login_count;
}

// Exam Info ----------------------------------

public class STPacketExamInfo
{
    public int code;
    public string message;
    public STPacketExamInfoBody[] body;
}

[Serializable]
public class STPacketExamInfoBody
{
    public int idx;
    public int applierIdx;
    public int examIdx;
    public int qstSetIdx;
    public int progressingTime;
    public int examTime;
    public int lastQstIdx;
    public int lastPageNo;
    public int sortSeq;
    public string status;
    public int iatSetIdx;
    public string qstTpCd;
    public string setDirTpCd;
    public string setCnnt;
    public string setCnntImg;
}
// -------------------------------------------------

// TODO --------------------------------------------
public class STPacketInfoMission
{
    public int code;
    public string message;
    public STPacketInfoMissionBody[] body;
}

[Serializable]
public class STPacketInfoMissionBody
{
    public int idx;
    public string title;
    public string content;
    public int sortSeq;
}
// -------------------------------------------------

// Guide Info --------------------------------------
public class STGuides
{
    public int code;
    public string message;

    public STGuidesBody body;
}

[Serializable]
public class STGuidesBody
{
    public int idx;
    public string title;
    public string code;
    public string lang;

    public STGuidesBodyContents[] contents;
}

[Serializable]
public class STGuidesBodyContents
{
    public int idx;
    public int depth;
    public string title;
    public string image_path;
    public string security_grade;
    public string reg_date;

    public STGuidesBodyContentsChildren[] children;
}

[Serializable]
public class STGuidesBodyContentsChildren
{
    public int idx;
    public int parent_idx;
    public int depth;
    public string title;
    public string image_path;
    public string security_grade;
    public string reg_date;
}

// Answer Dictionaries ---------------------------
public class STPacketAnswerDictionaries
{
    public int code;
    public string message;
    public STPacketAnswerDictionariesBody[] body;
}

[Serializable]
public class STPacketAnswerDictionariesBody
{
    public int dic_word_no;
    public string browse_code;
    public string word_name;
    public float score;
    public int parent_dic_word_no;
    public int dic_cate_no;
}

// QuestionsStatus --------------------------------
public class STPacketQuestionStatus
{
    public int code;
    public string message;
    public STPacketQuestionStatusBody body;

}

[Serializable]
public class STPacketQuestionStatusBody
{
    public string status;
    public string video_part_yn;
    public int next_part_idx;
    public string scale_type;
}
// -------------------------------------------------

// TODO 활동로그 남기기
// Action Log ---------------------------------------
[Serializable]
public class STPacketActionLog
{
    public string strCategory;
    public string strActionPage;
    public string strActionUrl;
    public int nActionTime;
}
// --------------------------------------------------


// -------------------------------------------------

public class STPacketBasic
{
    public int code;
    public string message;
}

public class STPacketActionExit
{
    public int code;
    public string message;
    public int body;
}


public class CQuizData : MonoBehaviour
{
    #region SingleTon
    public static CQuizData _instance = null;

    public static CQuizData Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CQuizData install null");

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

    //[Serializable]
    //public class Answers
    //{
    //    public int anwr_idx;
    //    public string anwr_brws_cd;
    //    public string anwr_cnnt;
    //}

    //[Serializable]
    //public class Questions
    //{
    //    public int test_qst_idx;
    //    public int[] test_anwr_idx;
    //    public int[] test_anwr_sbct;
    //    public int test_prg_time;
    //    public int qst_idx;
    //    public string qst_exos_cd;
    //    public string qst_sove_cd;
    //    public int qst_ans_cnt;
    //    public int[] qst_dics;
    //    public string qst_brws_cnnt;
    //    public string qst_brws_cd;
    //    public string qst_cnnt;
    //    public Answers[] answers;
    //}

    //[Serializable]
    //public class Sets
    //{
    //    public string dir_tp_cd;
    //    public string dir_cnnt;
    //    public Questions[] questions;
    //}


    //[Serializable]
    //public class Quiz
    //{
    //    public int part_idx;    // 문항유형번호
    //    public int exm_time;    // 문항유형 검사시간
    //    public int progress_time;   // 문항유형 검사 남은 시간
    //    public string prg_st_cd;
    //    public string set_dir_tp_cd;
    //    public string set_dir_cnnt;
    //    public int[] set_gudes;
    //    public string qst_tp_cd;
    //    public int last_qst_idx;
    //    public int last_page_no;
    //    public Sets[] sets;
    //}

    //public class PacketQuiz
    //{
    //    public int code;
    //    public string message;
    //    public Quiz[] body;
    //}

    public PacketQuiz m_packetQuiz;

    public PacketQuiz m_packetRQTTutorial;
    public PacketQuizPart m_packetRQT;
    public PacketQuizPart m_packetAPTD1;
    public PacketQuizPart m_packetAPTD2;
    public PacketQuizPart m_packetCST;
    public PacketQuizPart m_packetHPTS;
    public PacketQuizPart m_packetLGTK;
    public PacketQuizPart m_packetPCTR;
    public PacketQuizPart m_packetRAT;

    private STTestCheck m_stTestCheck;

    public STPacketExamInfo m_packetExamInfo;

    public STPacketInfoMission m_packetInfoMission;

    public STGuides m_stGuides;

    public STPacketQuestionStatus m_packetQuestionStatus;

    public STPacketAnswerDictionaries m_packetAnswerDictionaries;

    public List<STPacketAnswerDictionaries> m_listAnswerDictionaries;

    private string m_strUserName;
    private int m_nExitCnt;

    private int m_nMaxExitCnt = 10;
    // Start is called before the first frame update
    void Start()
    {
        m_stTestCheck = new STTestCheck();
        m_packetRQT = new PacketQuizPart();

        m_packetQuiz = new PacketQuiz();

        TextAsset textAsset = Resources.Load<TextAsset>("Scripts/dummy_api");
        m_packetQuiz = JsonUtility.FromJson<PacketQuiz>(textAsset.text);

        TextAsset textAssetRQTTutorial = Resources.Load<TextAsset>("Scripts/rqt_tutorial");
        m_packetRQTTutorial = JsonUtility.FromJson<PacketQuiz>(textAssetRQTTutorial.text);

        //Debug.Log(JsonUtility.ToJson(m_packetRQTTutorial));

        m_strUserName = "TestUser";

        m_listAnswerDictionaries = new List<STPacketAnswerDictionaries>();

        //GetQuiz("RQT");
        //Debug.Log(GetQuiz("RQT").qst_tp_cd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Quiz GetQuiz(string strTPCD, bool bTutoral = false)
    {
        //Debug.Log(m_packetQuiz.code);
        if(bTutoral)
        {
            if( strTPCD == "RQT")
            {
                return m_packetRQTTutorial.body[0];
            }
        }

        //if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        //{
        //    for (int i = 0; i < m_packetQuiz.body.Length; i++)
        //    {
        //        if (m_packetQuiz.body[i].qst_tp_cd.Equals(strTPCD))
        //            return m_packetQuiz.body[i];
        //    }
        //} else
        //{
        if (strTPCD.Equals("RQT"))
            return GetRQT().body;
        else if (strTPCD.Equals("CST"))
        {
            return GetCST().body;
        }
        else if (strTPCD.Equals("RAT"))
            return GetRAT().body;
        else if (strTPCD.Equals("LGTK"))
            return GetLGTK().body;
        else if (strTPCD.Equals("APTD1"))
            return GetAPTD1().body;
        else if (strTPCD.Equals("APTD2"))
            return GetAPTD2().body;
        else if (strTPCD.Equals("HPTS"))
            return GetHPTS().body;
        //}

        return null;
    }

    public int GetQuizTotalCount(string strTPCD, bool bTutoral = false)
    {
        Quiz quiz = GetQuiz(strTPCD, bTutoral);
        return quiz.sets.Length;
    }

    public void SetTestCheck(STTestCheck stTestCheck)
    {
        m_stTestCheck = stTestCheck;
    }

    public STTestCheck GetTestCheck()
    {
        return m_stTestCheck;
    }

    public void SetRQT(PacketQuizPart packetQuiz)
    {
        m_packetRQT = packetQuiz;
    }

    public PacketQuizPart GetRQT()
    {
        return m_packetRQT;
    }

    public void SetUserName(string strUserName)
    {
        m_strUserName = strUserName;
    }

    public string GetUserName()
    {
        return m_strUserName;
    }

    public void SetExitCount(int nCount)
    {
        m_nExitCnt = nCount;
    }

    public int GetExitCount()
    {
        return m_nExitCnt;
    }

    public int GetEnableExitCount()
    {
        return GetMaxExitCount() - GetExitCount();
    }

    public void SetMaxExitCount(int nMaxCount)
    {
        m_nMaxExitCnt = nMaxCount;
    }

    public int GetMaxExitCount()
    {
        return m_nMaxExitCnt;
    }

    //public PacketQuizPart m_packetAPTD1;
    //public PacketQuizPart m_packetAPTD2;
    //public PacketQuizPart m_packetCST;
    //public PacketQuizPart m_packetHPTS;
    //public PacketQuizPart m_packetLGTK;
    //public PacketQuizPart m_packetPCTR;
    //public PacketQuizPart m_packetRAT;

    public void SetAPTD1(PacketQuizPart packetQuiz) { m_packetAPTD1 = packetQuiz; }
    public PacketQuizPart GetAPTD1() { return m_packetAPTD1; }

    public void SetAPTD2(PacketQuizPart packetQuiz) { m_packetAPTD2 = packetQuiz; }
    public PacketQuizPart GetAPTD2() { return m_packetAPTD2; }
    public void SetCST(PacketQuizPart packetQuiz) { m_packetCST = packetQuiz; }
    public PacketQuizPart GetCST() { return m_packetCST; }
    public void SetHPTS(PacketQuizPart packetQuiz) { m_packetHPTS = packetQuiz; }
    public PacketQuizPart GetHPTS() { return m_packetHPTS; }
    public void SetLGTK(PacketQuizPart packetQuiz) { m_packetLGTK = packetQuiz; }
    public PacketQuizPart GetLGTK() { return m_packetLGTK; }
    public void SetPCTR(PacketQuizPart packetQuiz) { m_packetPCTR = packetQuiz; }
    public PacketQuizPart GetPCTR() { return m_packetPCTR; }
    public void SetRAT(PacketQuizPart packetQuiz) { m_packetRAT = packetQuiz; }
    public PacketQuizPart GetRAT() { return m_packetRAT; }

    public void SetExamInfo(STPacketExamInfo packetExamInfo)
    {
        m_packetExamInfo = packetExamInfo;
    }

    public STPacketExamInfo GetExamInfo()
    {
        return m_packetExamInfo;
    }

    public STPacketExamInfoBody GetExamInfoDetail(string strTpCd)
    {
        for(int i = 0; i < m_packetExamInfo.body.Length; i++)
        {
            if(m_packetExamInfo.body[i].qstTpCd.Equals(strTpCd))
            {
                return m_packetExamInfo.body[i];
            }
        }

        return null;
    }

    public void SetInfoMission(STPacketInfoMission packetInfoMission)
    {
        m_packetInfoMission = packetInfoMission;
    }

    public STPacketInfoMission GetInfoMission()
    {
        return m_packetInfoMission;
    }

    public void SetGuides(STGuides stGuides)
    {
        m_stGuides = stGuides;
    }

    public STGuides GetGuides()
    {
        return m_stGuides;
    }

    public void SetQuestionStatus(STPacketQuestionStatus stPacketQuestionStatus)
    {
        m_packetQuestionStatus = stPacketQuestionStatus;
    }

    public STPacketQuestionStatus GetQuestionStatus()
    {
        return m_packetQuestionStatus;
    }

    public void SetAnswerDictionaries(STPacketAnswerDictionaries packetAnswerDictionaries)
    {
        m_packetAnswerDictionaries = packetAnswerDictionaries;
    }

    public STPacketAnswerDictionaries GetAnswerDictionaries()
    {
        return m_packetAnswerDictionaries;
    }

    public void AddAnswerDictionaries(STPacketAnswerDictionaries packetAnswerDictionaries)
    {
        m_listAnswerDictionaries.Add(packetAnswerDictionaries);
    }

    public STPacketAnswerDictionaries GetAnswerDictionaries(int nDicCateNo)
    {
        for(int i = 0; i < m_listAnswerDictionaries.Count; i++)
        {
            if( m_listAnswerDictionaries[i].body[0].dic_cate_no == nDicCateNo)
            {
                return m_listAnswerDictionaries[i];
            }
        }

        return null;
    }
   
}

//public class CQuizQuestion
//{
//    public int nTestQstIdx;
//    public int[] listTestAnwrIdx;
//    public int[] listTestAnwrSbct;
//    public int nTestPrgTime;
//    public int nQstIdx;
//    public string strQstExosCD;
//    public string strSoveCD;
//    public int nQstAnsCnt;
//    public string[] listQstDics;
//    public string strQstBrwsCnnt;
//    public string strQstBrwsCD;
//    public string strQstCnnt;
//    public CQuizAnswer[] listAnswer;
//}

//public class CQuizAnswer
//{
//    public int nAnwrIdx;
//    public string strAnwrBrwsCD;
//    public string strAnwrCnnt;
//}

//public class CQuizSet
//{
//    public string strDirTpCD;
//    public string strDirCnnt;
//    public CQuizQuestion[] listQuestion;

//}

//public class CQuiz
//{
//    public int nPartIndex;
//    public int nExmTime;
//    public int nProgesssTime;
//    public string strPrgStCD;
//    public string strSetDirTpCD;
//    public string strSetDirCnnt;
//    public string[] listSetGudes;
//    public string strQstTpCD;
//    public int nLastQstIdx;
//    public int nLastPageNo;
//    public CQuizSet[] listSets;
//}
