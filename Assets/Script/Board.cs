using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    // 动态把cross排到棋盘上

    // 1.声明Prefab模型
    public GameObject CrossPrefab;
    const float CrossSize = 40; // 设置常量cross大小

    public const int CrossCount = 15; // 设置cross的数量

    public const int Size = 560; // 设置棋盘的大小
    public const int HalfSize = Size/2; // 棋局一半的大小
public enum ChessType{
    None = 0,
    Black = 1,
    White = 2,
}

    // 字典存储坐标点
    Dictionary<int,Cross> _crossMap = new Dictionary<int, Cross>();//声明字典
    // 生成key
    static int MakeKey(int x, int y){
        return x*10000+y;
    }

    public  void Reset(){
        
        // 初始化删除全部子节点
        foreach(Transform child in gameObject.transform){
            GameObject.Destroy(child.gameObject);
        }
        _crossMap.Clear();
        // 一定要记得添加组件才能获取到
        var mainloop = GetComponent<MainLoop>();
        // 生成棋盘
        for (int x=0; x<CrossCount;x++){ // 遍历15次
            for (int y=0; y<CrossCount;y++){ // 遍历y轴15次
                var crossObject = GameObject.Instantiate<GameObject>(CrossPrefab);
                crossObject.transform.SetParent(gameObject.transform);
                // 缩放值复位, 控制放入的比例大小
                crossObject.transform.localScale = Vector3.one;

                // 获取位置
                var pos = crossObject.transform.localPosition;
                // 设置位置
                pos.x = -HalfSize + x*CrossSize;
                pos.y = -HalfSize + y*CrossSize;
                pos.z = 1;
                crossObject.transform.localPosition = pos;

                // 设置cross脚本中的交叉点坐标
                var cross  = crossObject.GetComponent<Cross>();//获取脚本cross
                cross.GridX = x;
                cross.GridY = y;
                cross.myloop = mainloop;
                _crossMap.Add(MakeKey(x,y),cross);

            }
        }

    }


    // 为cross添加方法?
    public Cross GetCross(int GridX, int GridY){
        Cross cross;
        if (_crossMap.TryGetValue(MakeKey(GridX,GridY),out cross)){
            return cross;
        }
        return null;
    }
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
