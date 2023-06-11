using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade
{
    none,
    grade6,
    grade7,
    grade8,
}


enum Direction
{ 
    straight,
    nintyDegree,

}

public class JengaStackBuilder : MonoBehaviour
{
    private Grade gradeNumber;
    public GameObject blockPrefab;

    public Vector3 middlePoint;
    private int wholeBlockNumber;
    private Vector3 initialPos;
    public List<GameObject> listOfBlocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialize(Grade gradedNo)
    {
        initialPos = transform.position;
        gradeNumber = gradedNo;
        buildstack();
    }

    public void buildstack()
    {
        int currentBoxCreated = 0;
        float currentHeight = 0.05f;
        Direction currentDirection = Direction.straight;

        List<CourseItem> listCourseItem;
        if(gradeNumber == Grade.grade6)
        {
            listCourseItem = JSONReader.Instance.allCourseItem.listOfSixthGrade;
        }
        else if(gradeNumber == Grade.grade7)
        {
            listCourseItem = JSONReader.Instance.allCourseItem.listOfSeventhGrade;
        }
        else
        {
            listCourseItem = JSONReader.Instance.allCourseItem.listOfEighthGrade;
        }

        wholeBlockNumber = listCourseItem.Count;

        while (currentBoxCreated < wholeBlockNumber)
        {
            if (currentBoxCreated % 3 == 0 && currentBoxCreated != 0)
            {
                currentHeight += 0.1f;
                currentDirection = currentDirection == Direction.straight ? Direction.nintyDegree : Direction.straight;
            }

            float angle = currentDirection == Direction.straight ? 0 : 90;
            float xPos = currentDirection == Direction.straight ? 0 : 0.3f;
            float zPos = currentDirection == Direction.straight ? 0.3f : 0;

            if (currentBoxCreated % 3 == 0)
            {
                xPos *= -1;
                zPos *= -1;

            }
            else if(currentBoxCreated % 3 == 1)
            {
                xPos = 0;
                zPos = 0;
            }
            GameObject currentBox = Instantiate(blockPrefab, transform);
            currentBox.SetActive(true);
            currentBox.transform.localPosition = new Vector3(xPos,currentHeight,zPos);
            currentBox.transform.eulerAngles = new Vector3(0, angle, 0);

            Block block = currentBox.GetComponent<Block>();
            block.Initialize(listCourseItem[currentBoxCreated]);


            listOfBlocks.Add(currentBox);
            currentBoxCreated++;
        }

        middlePoint = new Vector3(transform.position.x,currentHeight / 2, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveGlassBlock()
    {
        foreach(GameObject block in listOfBlocks)
        {
            if(block.GetComponent<Block>()._CourseItem.mastery == 0)
            {
                block.gameObject.SetActive(false);
            }
            else
            {
                block.GetComponent<Rigidbody>().useGravity = true;
                block.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
