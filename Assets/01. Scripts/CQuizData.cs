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
public class Questions
{
    public int test_qst_idx;
    public int[] test_anwr_idx;
    public int[] test_anwr_sbct;
    public int test_prg_time;
    public int qst_idx;
    public string qst_exos_cd;
    public string qst_sove_cd;
    public string qst_ans_cd;
    public int qst_ans_cnt;
    public QSTDics[] qst_dics;
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
    public string[] contents;
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

//{
//    "row_no": 0,
//  "regr_id": "string",
//  "modr_id": "string",
//  "reg_dtm": "2023-02-08T04:18:41.528Z",
//  "mod_dtm": "2023-02-08T04:18:41.528Z",
//  "order_column": "string",
//  "order_asc": "string",
//  "page_size": 0,
//  "page_number": 0,
//  "page_offset": 0,
//  "search_type": "string",
//  "answer_seq_idx": 0,
//  "applier_idx": 0,
//  "part_idx": 0,
//  "question_idx": 0,
//  "answer_idx": 0,
//  "answer_type": "QuestionAnswerType.OBJ(code=OBJ, desc=객관식)",
//  "objective_answer": 0,
//  "subjective_answer": "string",
//  "answers": [
//    0
//  ],
//  "contents": [
//    "string"
//  ]
//}

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

    private string m_strUserName;

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

        Debug.Log(JsonUtility.ToJson(m_packetRQTTutorial));

        m_strUserName = "TestUser";
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

        //Debug.Log(strTPCD);

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
            return GetCST().body;
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
