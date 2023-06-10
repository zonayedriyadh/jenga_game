using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextAsset _apiText;
    public JengaStackBuilder _stackBuilder6th;
    public JengaStackBuilder _stackBuilder7th;
    public JengaStackBuilder _stackBuilder8th;
    [SerializeField] Camera mainCamera;

    private Grade currentGradeSelected;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        SetGrade(Grade.grade8);
    }

    public void Initialize()
    {
        JSONReader.Instance.Initialize(_apiText);
        _stackBuilder6th.Initialize(Grade.grade6);
        _stackBuilder7th.Initialize(Grade.grade7);
        _stackBuilder8th.Initialize(Grade.grade8);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrade(Grade grade)
    {
        currentGradeSelected = grade;
        SetCamera();
    }

    private void SetCamera()
    {
        if(currentGradeSelected == Grade.grade6)
        {
            mainCamera.transform.position = _stackBuilder6th.transform.position + new Vector3(0,3f,-3f);
            mainCamera.transform.LookAt(_stackBuilder6th.middlePoint);
        }
        else if(currentGradeSelected == Grade.grade7)
        {
            mainCamera.transform.position = _stackBuilder7th.transform.position + new Vector3(0, 3f, -3f);
            mainCamera.transform.LookAt(_stackBuilder7th.middlePoint);
        }
        else if(currentGradeSelected == Grade.grade8)
        {
            mainCamera.transform.position = _stackBuilder8th.transform.position + new Vector3(0, 3f, -3f);
            mainCamera.transform.LookAt(_stackBuilder8th.middlePoint);
        }
    }

}
