using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public TextAsset _apiText;
    public JengaStackBuilder _stackBuilder6th;
    public JengaStackBuilder _stackBuilder7th;
    public JengaStackBuilder _stackBuilder8th;
    [SerializeField] Camera gameCamera;
    [SerializeField] Material SelectMaterial;
    private Grade currentGradeSelected;

    private static GameManager instance = null;

    Vector2 currentMousePosition;
    Vector2 mouseDeltaPosition;
    Vector2 lastMousePosition;
    [SerializeField]float cameraRotateeSpeed;
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
        SetGrade(Grade.none);
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
        if(currentGradeSelected != Grade.none)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {

                currentMousePosition = Input.mousePosition;
                mouseDeltaPosition = (currentMousePosition - lastMousePosition) * -1;


                gameCamera.transform.RotateAround(getGrade(currentGradeSelected).gameObject.transform.position, new Vector3(0, -3.5f, 0), mouseDeltaPosition.x * cameraRotateeSpeed * Time.deltaTime);
                //Debug.Log("mouseDeltaPosition "+ mouseDeltaPosition.x);
                //gameCamera.transform.Rotate(0f, mouseDeltaPosition.x * 1, 0f);

                gameCamera.transform.LookAt(getGrade(currentGradeSelected).middlePoint);
                lastMousePosition = currentMousePosition;
            }

            
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {

                currentMousePosition = Input.mousePosition;
                mouseDeltaPosition = (currentMousePosition - lastMousePosition) * -1;


                gameCamera.transform.RotateAround(_stackBuilder7th.middlePoint, new Vector3(0, -7.5f, 0), mouseDeltaPosition.x * cameraRotateeSpeed * Time.deltaTime);
                //Debug.Log(" "+ mouseDeltaPosition.x);
                //Quaternion lookRoation = Quaternion.LookRotation(geNearest().middlePoint, gameCamera.transform.position);
                //gameCamera.transform.rotation = Quaternion.Slerp(gameCamera.transform.rotation, lookRoation, cameraRotateeSpeed * Time.deltaTime);
                gameCamera.transform.LookAt(_stackBuilder7th.middlePoint);

                /*Vector3 direction = geNearest().middlePoint - gameCamera.transform.position;
                Quaternion toRotation = Quaternion.FromToRotation(gameCamera.transform.forward, direction);
                gameCamera.transform.rotation = Quaternion.Lerp(gameCamera.transform.rotation, toRotation, 0.005f * Time.time);*/
                lastMousePosition = currentMousePosition;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("mouse hit position -> "+ Input.mousePosition);
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Debug.DrawRay (ray.origin, ray.direction * 100, Color.yellow);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.tag == "block")
                {
                    GameObject block = hit.collider.gameObject;

                    Grade blockGrade = Grade.grade8;
                    if (block.GetComponent<Block>()._CourseItem.grade == "6th Grade") blockGrade = Grade.grade6;
                    else if (block.GetComponent<Block>()._CourseItem.grade == "7th Grade") blockGrade = Grade.grade7;

                    SetSelectedColor(Grade.none);
                    SetSelectedColor(blockGrade, block);
                    HeadUpDisplay.Instance.ShowInformation(block.GetComponent<Block>()._CourseItem);

                }
            }
        }
    }

    

    public void SetSelectedColor(Grade grade,GameObject currentBlock = null)
    {
        Debug.Log("materials ");
        switch (grade)
        {
            case Grade.grade6:
                SetColor(_stackBuilder6th, currentBlock);
                break;
            case Grade.grade7:
                SetColor(_stackBuilder7th, currentBlock);
                break;
            case Grade.grade8:
                SetColor(_stackBuilder8th, currentBlock);
                break;
            case Grade.none:
                DeselectAllBlock();
                break;

        }
    }

    public void SetColor(JengaStackBuilder stack,GameObject currentBlock)
    {
        foreach (GameObject block in stack.listOfBlocks)
        {
            if (block == currentBlock)
            {
                block.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                block.GetComponent<MeshRenderer>().material.color = block.GetComponent<Block>().orginalColor;
            }
        }
    }

    public void SetGrade(Grade grade)
    {
        currentGradeSelected = grade;
        SetCamera();
    }

    public JengaStackBuilder geNearest()
    {
        float distanceSix = Vector3.Distance(gameCamera.transform.position, _stackBuilder6th.transform.position);
        float distanceSeven = Vector3.Distance(gameCamera.transform.position, _stackBuilder7th.transform.position);
        float distanceEight = Vector3.Distance(gameCamera.transform.position, _stackBuilder8th.transform.position);

        if(distanceSix< distanceSeven && distanceSix < distanceEight)
        {
            return _stackBuilder6th;
        }
        else if(distanceSeven < distanceSix && distanceSeven < distanceEight)
        {
            return _stackBuilder7th;
        }
        else { return _stackBuilder8th; }
    }

    public JengaStackBuilder getGrade(Grade grade)
    {
        if (grade == Grade.grade6)
            return _stackBuilder6th;
        else if (grade == Grade.grade7) 
            return _stackBuilder7th;
        else return _stackBuilder8th;
    }

    public void DeselectAllBlock()
    {
        SetColor(_stackBuilder6th, null);
        SetColor(_stackBuilder7th, null);
        SetColor(_stackBuilder8th, null);
    }

    private void SetCamera()
    {
        Vector3 cameraPos = new Vector3(0, 1.5f, -3.5f);
        if (currentGradeSelected == Grade.grade6)
        {
            gameCamera.transform.position = _stackBuilder6th.transform.position + cameraPos /*+ new Vector3(0,0,-.5f)*/;
            gameCamera.transform.LookAt(_stackBuilder6th.middlePoint);

            _stackBuilder6th.gameObject.SetActive(true);
            _stackBuilder8th.gameObject.SetActive(false);
            _stackBuilder7th.gameObject.SetActive(false);
        }
        else if(currentGradeSelected == Grade.grade7)
        {
            gameCamera.transform.position = _stackBuilder7th.transform.position + cameraPos;
            gameCamera.transform.LookAt(_stackBuilder7th.middlePoint);

            _stackBuilder6th.gameObject.SetActive(false);
            _stackBuilder7th.gameObject.SetActive(true);
            _stackBuilder8th.gameObject.SetActive(false);
        }
        else if(currentGradeSelected == Grade.grade8)
        {
            gameCamera.transform.position = _stackBuilder8th.transform.position + cameraPos;
            gameCamera.transform.LookAt(_stackBuilder8th.middlePoint);

            _stackBuilder6th.gameObject.SetActive(false);
            _stackBuilder7th.gameObject.SetActive(false);
            _stackBuilder8th.gameObject.SetActive(true);
        }
        else
        {
            gameCamera.transform.position = _stackBuilder7th.transform.position + new Vector3(0, 1.5f, -4.5f);
            gameCamera.transform.LookAt(_stackBuilder7th.middlePoint);

            _stackBuilder6th.gameObject.SetActive(true);
            _stackBuilder7th.gameObject.SetActive(true);
            _stackBuilder8th.gameObject.SetActive(true);
        }
    }

    public void RemoveGlasssBlock()
    {
        if(currentGradeSelected == Grade.none)
        {
            getGrade(Grade.grade6).RemoveGlassBlock();
            getGrade(Grade.grade7).RemoveGlassBlock();
            getGrade(Grade.grade8).RemoveGlassBlock();
        }
        else
            getGrade(currentGradeSelected).RemoveGlassBlock();
    }

    public void RebuildStacks()
    {
        _stackBuilder6th.Rebuild();
        _stackBuilder7th.Rebuild();
        _stackBuilder8th.Rebuild();
    }

}
