using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    [SerializeField] private LevelDefine levelDefine;
    public string s1;
    public string s2;
    public int i1;
  

    // Start is called before the first frame update
    void Start()
    {
        s1 = gameObject.name;
        s2 = s1.Substring(s1.Length - 1);
        i1 = int.Parse(s2);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        levelDefine.startLine[i1] = this;
    }
}
