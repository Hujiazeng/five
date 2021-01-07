using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cross : MonoBehaviour
{
    // Start is called before the first frame update
    // 添加全局变量交叉点坐标X,交叉点坐标Y
    public int GridX;
    public int GridY;

    public MainLoop myloop;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>{
            // 点击时 传入的是Cross脚本
        myloop.OnClickss(this);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
