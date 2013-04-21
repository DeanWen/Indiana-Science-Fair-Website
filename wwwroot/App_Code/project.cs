using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for project
/// </summary>
public class project
{
    private string projId;
    private string projName;
    private string catId;
    private string firstName;
    private string gradeId;
    private string division;
    private int times;
    private List<string> availList;
    private int weight;

	public project()
	{
        weight = 100;
	}

    public project(string pid, string pname, string cid, string fname, string gid, string div, int time, List<string> projAvail)
    {
        this.projId = pid;
        this.projName = pname;
        this.catId = cid;
        this.firstName = fname;
        this.gradeId = gid;
        this.division = div;
        this.times = time;
        this.availList = projAvail;
        weight = 10;
    }

    public void calculateWeight(string catA, string catB, string catC, string catD, List<string> judgeAvail)
    {
        if (weight != 0)
        {
            calByAvailability(judgeAvail);
            if (weight != 0)
            {
                calByCategory(catA, catB, catC, catD);
                if (weight != 0)
                {
                    calByTimes(times);
                }
            }
        }
    }

    private void calByAvailability(List<string> judgeAvail)
    {
        int count = 0;
        foreach (string availId in judgeAvail)
        {
            if (availList.Contains(availId))
                count += 1;                
        }
        if (count == 0)
            weight = 0;
        //string sql = "select PeriodId from ProjectAvailability where PID = " + projId;      
    }

    private void calByCategory(string catA, string catB, string catC, string catD)
    {
        if (catId == catA)
            weight += 5;
        else if (catId == catB)
            weight += 4;
        else if (catId == catC)
            weight += 3;
        else if (catId == catD)
            weight += 2;
        else
            weight = 0;
    }

    private void calByTimes(int times)
    {
        if (times < 5)
        {
            switch (times)
            {
                case 0:
                    weight += 5;
                    break;
                case 1:
                    weight += 4;
                    break;
                case 2:
                    weight += 3;
                    break;
                case 3:
                    weight += 2;
                    break;
                default:
                    weight += 1;
                    break;
            }
        }
        else     // A project can't be judged more than 5 times
            weight = 0;
    }

    public string PID
    {
        get { return projId; }
        set { projId = value; }
    }

    public string PName
    {
        get { return projName; }
        set { projName = value; }
    }

    public string CID
    {
        get { return catId; }
        set { catId = value; }
    }

    public string FName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string GID
    {
        get { return gradeId; }
        set { gradeId = value; }
    }

    public string Division
    {
        get { return division; }
        set { division = value; }
    }

    public int Times
    {
        get { return times; }
        set { times = value; }
    }

    public int Weight
    {
        get { return weight; }
        set { weight = value; }
    }
}