using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class JSONReader 
{
    private static JSONReader instance = null;
    public static JSONReader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = JSONReader.Create();
            }
            return instance;
        }
    }
    public AllCourseItem allCourseItem;

    public static JSONReader Create()
    {
        return new JSONReader();
    }

    public void Initialize(TextAsset _apiText)
    {
        //TextAsset _apiText = Resources.Load<TextAsset>("Api.json");

        AllCourseItem allCourseInJson = JsonUtility.FromJson<AllCourseItem>(_apiText.text);
        allCourseItem = allCourseInJson;
        //Debug.Log(allCourseInJson.listOfCourseItems.Count);        
        if (allCourseInJson.listOfCourseItems != null)
        {
            foreach (CourseItem item in allCourseInJson.listOfCourseItems)
            {
                if(item.grade == "6th Grade")
                {
                    allCourseInJson.listOfSixthGrade.Add(item);
                }
                else if(item.grade == "7th Grade")
                {
                    
                    allCourseInJson.listOfSeventhGrade.Add(item);
                }
                else if (item.grade == "8th Grade")
                {
                    allCourseInJson.listOfEighthGrade.Add(item);
                }
            }
            //Debug.Log(JSONReader.Instance.allCourseItem.listOfEighthGrade.Count);
        }
        sortList(allCourseInJson.listOfSixthGrade);
        sortList(allCourseInJson.listOfSeventhGrade);
        sortList(allCourseInJson.listOfEighthGrade);
    }

    private void sortList(List<CourseItem> listOfCourseItem)
    {
        listOfCourseItem = listOfCourseItem.OrderBy(d => d.domain).ToList();
        listOfCourseItem = listOfCourseItem.OrderBy(d => d.cluster).ToList();
        listOfCourseItem = listOfCourseItem.OrderBy(d => d.standardid).ToList();
    }
}

[System.Serializable]
public class AllCourseItem
{
    public List<CourseItem> listOfCourseItems = new List<CourseItem>();
    public List<CourseItem> listOfSixthGrade = new List<CourseItem>();
    public List<CourseItem> listOfSeventhGrade = new List<CourseItem>();
    public List<CourseItem> listOfEighthGrade = new List<CourseItem>();
}

[System.Serializable]
public class CourseItem
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}
