using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextAsset _apiText;
    public JengaStackBuilder _stackBuilder6th;
    public JengaStackBuilder _stackBuilder7th;
    public JengaStackBuilder _stackBuilder8th;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
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
}
