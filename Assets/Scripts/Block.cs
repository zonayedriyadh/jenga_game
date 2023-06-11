using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Glass,
    Wood,
    Stone,
}
public class Block : MonoBehaviour
{
    [SerializeField] Material glassMetarial;
    [SerializeField] Material woodMetarial;
    [SerializeField] Material stoneMetarial;

    //private BlockType blockType;
    private CourseItem courseItem;

    public CourseItem _CourseItem { get => courseItem; set => courseItem = value; }
    public Color orginalColor;
    public Vector3 initialPos;
    public Vector3 initialAngle;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialAngle = transform.eulerAngles;
        orginalColor = GetComponent<MeshRenderer>().material.color;
    }

    public void Initialize(CourseItem _courseItem)
    {
        courseItem = _courseItem;
        //Debug.Log("ourse id -> "+ _courseItem.id);
        if (_courseItem.mastery == 0)
        {
            GetComponent<MeshRenderer>().material = glassMetarial;
            //blockType = BlockType.Glass;
        }
        else if(_courseItem.mastery == 1)
        {
            GetComponent<MeshRenderer>().material = woodMetarial;
            //blockType = BlockType.Wood;
        }
        else
        {
            GetComponent<MeshRenderer>().material = stoneMetarial;
            //blockType = BlockType.Stone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
