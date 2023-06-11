using Modules;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
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

    [SerializeField] private TextMeshProUGUI text_GradeLevel;
    [SerializeField] private TextMeshProUGUI text_Cluster;
    [SerializeField] private TextMeshProUGUI text_StandardId;

    [Header ("Left Button")]
    [SerializeField] private Button btn6thGrade;
    [SerializeField] private Button btn7thGrade;
    [SerializeField] private Button btn8thGrade;
    [SerializeField] private Button mainCamera;

    [Header("Right Button")]
    [SerializeField] private Button btnTestMyScore;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AddListener Called");
        btn6thGrade.onClick.AddListener(()=>OnCickSetGrade(Grade.grade6));
        btn7thGrade.onClick.AddListener(() => OnCickSetGrade(Grade.grade7));
        btn8thGrade.onClick.AddListener(() => OnCickSetGrade(Grade.grade8));
        mainCamera.onClick.AddListener(() => OnCickSetGrade(Grade.none));

        btnTestMyScore.onClick.AddListener(OnCLickTestMyScore);
        btnTestMyScore.interactable = false;
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

    public void OnCickSetGrade(Grade grade)
    {
        if (grade == Grade.none)
            btnTestMyScore.interactable = false;
        else
            btnTestMyScore.interactable = true; 

        GameManager.Instance.DeselectAllBlock();
        GameManager.Instance.SetGrade(grade);
    }

    public void OnCLickTestMyScore()
    {
        GameManager.Instance.RemoveGlasssBlock();
    }

    public void OnCLickRebuild()
    {
        GameManager.Instance.DeselectAllBlock();
        GameManager.Instance.RebuildStacks();
    }
}
