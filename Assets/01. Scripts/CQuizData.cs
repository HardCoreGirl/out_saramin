using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class Quiz
{
    public int part_idx;    // 문항유형번호
    public int exm_time;    // 문항유형 검사시간
    public int progress_time;   // 문항유형 검사 남은 시간
    public string prg_st_cd;
    public string set_dir_tp_cd;
    public string set_dir_cnnt;
    public int[] set_gudes;
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

    // Start is called before the first frame update
    void Start()
    {
        m_packetQuiz = new PacketQuiz();

        TextAsset textAsset = Resources.Load<TextAsset>("Scripts/dummy_api");
        m_packetQuiz = JsonUtility.FromJson<PacketQuiz>(textAsset.text);

        TextAsset textAssetRQTTutorial = Resources.Load<TextAsset>("Scripts/rqt_tutorial");
        m_packetRQTTutorial = JsonUtility.FromJson<PacketQuiz>(textAssetRQTTutorial.text);

        Debug.Log(JsonUtility.ToJson(m_packetRQTTutorial));
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

        for (int i = 0; i < m_packetQuiz.body.Length; i++)
        {
            if (m_packetQuiz.body[i].qst_tp_cd.Equals(strTPCD))
                return m_packetQuiz.body[i];
        }

        return null;
    }

    public int GetQuizTotalCount(string strTPCD, bool bTutoral = false)
    {
        Quiz quiz = GetQuiz(strTPCD, bTutoral);
        return quiz.sets.Length;
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
