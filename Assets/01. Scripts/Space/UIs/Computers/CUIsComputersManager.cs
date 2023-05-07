using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsComputersManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnClickLeftComputer()
    //{
    //    if (CSpaceAppEngine.Instance.IsFinishLeft01() && CSpaceAppEngine.Instance.IsFinishLeft02())
    //        return;

    //    CUIsSpaceManager.Instance.ScreenActive(true);

    //    if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
    //    {
    //        if (CQuizData.Instance.GetExamInfoDetail("RQT").status.Equals("WAITING"))
    //            Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RQT").idx);
    //        if (CQuizData.Instance.GetExamInfoDetail("CST").status.Equals("WAITING"))
    //            Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("CST").idx);
    //        if (CQuizData.Instance.GetExamInfoDetail("RAT").status.Equals("WAITING"))
    //            Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("RAT").idx);
    //        if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
    //            Server.Instance.RequestGetPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);

    //        Server.Instance.RequestGETInfoExams();
    //    }

    //    CUIsSpaceManager.Instance.ShowLeftPage();
    //}

    //public void OnClickCenterComputer()
    //{
    //    if (CSpaceAppEngine.Instance.IsFinishCenter())
    //        return;

    //    CUIsSpaceManager.Instance.ScreenActive(true);

    //    if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
    //    {
    //        //Server.Instance.RequestGETQuestions(0);
    //        CUIsSpaceManager.Instance.ShowCenterPage();
    //        return;
    //    }
    //    else
    //    {
    //        if (!CQuizData.Instance.GetExamInfoDetail("LGTK").status.Equals("WAITING"))
    //            Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
    //    }

    //    Server.Instance.RequestGETInfoExams();

    //    CUIsSpaceManager.Instance.ShowCenterPage();
    //}

    //public void OnClickRightComputer()
    //{
    //    if (CSpaceAppEngine.Instance.IsFinishRight())
    //        return;

    //    CUIsSpaceManager.Instance.ScreenActive(true);
    //    if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
    //    {
    //        Server.Instance.RequestGETQuestions(0);
    //        CUIsSpaceManager.Instance.ShowRightPage();
    //    }
    //    else
    //    {
    //        if (CQuizData.Instance.GetExamInfoDetail("APTD1").status.Equals("WAITING"))
    //        {
    //            Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
    //            Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
    //        }
    //        else
    //        {
    //            Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD1").idx);
    //            Server.Instance.RequestGETQuestions(CQuizData.Instance.GetExamInfoDetail("APTD2").idx);
    //        }

    //    }
    //}
}
