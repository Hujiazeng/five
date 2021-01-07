using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    // Start is called before the first frame update

    // 导入白棋和黑棋
    public GameObject WhitePrefab;
    public GameObject BlackPrefab;

    // 定义状态
    enum State{
        BlackGo,
        WhiteGo,
        Over,
    }

    State _state;
    Board _board;
    BoardModel _model;
    AI _ai;
    public Text Message;
    public const int WinChessCount = 5;
    public Result Result;
    // 创建二维数组


    

    bool PlaceChess(Cross cross,bool isblack)
{
    if (cross == null)
        return false;
    if (isblack){
        _state = State.WhiteGo;
    }else{
        _state = State.BlackGo;
    }
    var newChess = GameObject.Instantiate<GameObject>(isblack ? BlackPrefab:WhitePrefab);
    newChess.transform.SetParent(cross.gameObject.transform,false);
    var mtype=isblack? ChessType.Black:ChessType.White;
    _model.SetChess(cross.GridX,cross.GridY,mtype);
    var linkCount = _model.CheckLink(cross.GridX,cross.GridY,mtype);
    if (linkCount >= BoardModel.WinChessCount){
            Debug.Log(mtype+"胜利");
            if (mtype==ChessType.Black){
                Message.text = string.Format("你被电脑击败了");
            }else{
                Message.text = string.Format("电脑被你击败了");
            }
            
            Result.gameObject.SetActive(true);
        
            Restart();
            _board.gameObject.SetActive(false);
        };
    return false;
}
    public void Restart(){
        _model = new BoardModel();
        _ai = new AI();
        _state = State.WhiteGo;
        _board.Reset();
        _board.gameObject.SetActive(true);
    }
    bool CanPlace( int gridX, int gridY )
    {
        // 如果这个地方可以下棋子
        return _model.GetChess(gridX, gridY) == ChessType.None;        
    }
    

    int _lastPlayerX, _lastPlayerY;
    public void OnClickss(Cross cross){
        // 在玩家的回合才可以点击
        if (_state!=State.WhiteGo){
            return;
        }
        //  点击时候会调用的方法
        if (CanPlace(cross.GridX, cross.GridY)==false){
            return;
        }
        _lastPlayerX = cross.GridX;
        _lastPlayerY = cross.GridY;
        var linkCount = PlaceChess(cross, false); 
    }

    void Start()
    {
        _model = new BoardModel();
        _board = GetComponent<Board>();
        _ai = new AI();
        _state = State.WhiteGo;
    }

    // Update is called once per frame
    void Update()
    {
        // 实时判断状态
        switch(_state){
            
            // 电脑玩家下棋
            case  State.BlackGo:{
                int cx, cy;
                _ai.ComputerDo(_lastPlayerX,_lastPlayerY,out cx,out cy);
                if (PlaceChess(_board.GetCross(cx, cy),true)==false){
                    _state = State.WhiteGo;
                };
                }
            
        break;}
        
    }
}
