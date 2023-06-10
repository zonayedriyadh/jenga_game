using Modules;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : BasePanel
{
    private static HeadUpDisplay instance = null;
    public static HeadUpDisplay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HeadUpDisplay>();
            }
            return instance;
        }
    }

    [SerializeField]private TextMeshProUGUI text_GradeLevel;
    [SerializeField]private TextMeshProUGUI text_Cluster;
    [SerializeField] private TextMeshProUGUI text_StandardId;

    [SerializeField] private Button btn6thGrade;
    [SerializeField] private Button btn7thGrade;
    [SerializeField] private Button btn8thGrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInformation(CourseItem courseItem)
    {
        text_GradeLevel.text = "Grade Level: "+courseItem.domain;
        text_Cluster.text = "Cluster: " + courseItem.cluster;
        text_StandardId.text = "Standard: " + courseItem.standardid;
    }

    public void SetStackGrade(Grade grade)
    {
        switch(grade)
        {
            case Grade.grade6:

                break;
            case Grade.grade7:
                break;
            case Grade.grade8:
                break;
        }
    }
}
