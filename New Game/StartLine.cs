using System.Collections;
using System.Collections.Generic;
using Pathfinding.Ionic.Zip;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    private LevelDefine define;
    public string s1;
    public string s2;
    public int gameNum;
    // Start is called before the first frame update
    void Start()
    {
        define = GameObject.FindGameObjectWithTag("leveldefine").GetComponent<LevelDefine>();
        s1 = this.gameObject.name;
        s2 = s1.Substring(s1.Length - 1);
        gameNum = int.Parse(s2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
