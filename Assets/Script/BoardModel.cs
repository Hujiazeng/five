
using System.Collections.Generic;
using UnityEngine;

// 创建枚举(棋子类型)
public enum ChessType{
    None = 0,
    Black = 1,
    White = 2,
}
public class BoardModel
{
    public const int WinChessCount = 5;
    // 创建二维数组
    ChessType[,] data = new ChessType[Board.CrossCount,Board.CrossCount];

    public ChessType GetChess(int x, int y){
        // 获取棋子
        // Debug.Log(Board.CrossCount);
        if ((x<0||x>=Board.CrossCount)||(y<0||y>=Board.CrossCount)){
            // 不在棋盘内
            return ChessType.None;
        }

        return data[x,y];
    }

    public bool SetChess(int x, int y, ChessType type){
        // 放置棋子
        if ((x<0||x>=Board.CrossCount)||(y<0||y>=Board.CrossCount)){
            // 不在棋盘内
            return false;
        }
        
        data[x,y] = type;
        return true;
    }

    // 判断胜利 (八个方向有五个字则为胜利)
    public bool CheckVictory(int px,int py, ChessType type){
        // 初始自己为一个
        int Count = 1;
        //正上
        for (int wy=py+1; wy <= Board.CrossCount; wy++){
            if (GetChess(px,wy) == type){
                Count ++;
                if (Count>=WinChessCount){
                return true;
            }
            }else{
                Count = 1;
                break;
            }}
         // 正下
        for (int sy=py-1; sy <= Board.CrossCount; sy--){
            if (GetChess(px,sy) == type){
                Count ++;
                if (Count>=WinChessCount){
                return true;
            }
            }else{
                Count = 1;
                break;
            }}
        // 正左
        for (int ax=px-1; ax <= Board.CrossCount; ax--){
            if (GetChess(ax,py) == type){
                Count ++;
                if (Count>=WinChessCount){
                return true;
            }
            }else{
                Count = 1;
                break;
            }}
        // 正右
        for (int dx=px+1; dx <= Board.CrossCount; dx++){
            if (GetChess(dx,py) == type){
                Count ++;
                if (Count>=WinChessCount){
                return true;
            }
            }else{
                Count = 1;
                break;
            }}

        
        return false;
        }

        int CheckVerticalLink(int px, int py, ChessType type)
    {
        // 算上自己
        int linkCount = 1;
                   
        // 朝上
        for (int y = py  + 1; y < Board.CrossCount; y++)
        {
            if (GetChess(px, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }


        // 朝下
        for (int y = py - 1; y >= 0; y--)
        {
            if (GetChess(px, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        return linkCount;
    }

    // 检查水平方向连接情况
    int CheckHorizentalLink(int px, int py, ChessType type)
    {
        int linkCount = 1;

        // 朝右+
        for (int x = px+1; x < Board.CrossCount; x++)
        {
            if (GetChess(x, py) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

       
        // 朝左
        for (int x = px-1; x >= 0; x--)
        {
            if (GetChess(x, py) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        return linkCount;
    }

    // 检查斜边情况
    int CheckBiasLink(int px, int py, ChessType type)
    {
        int ret = 0;
        int linkCount = 1;

        // 左下
        for (int x = px - 1, y = py - 1; x>=0 && y >=0 ; x--, y--)
        {
            if (GetChess(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        // 右上
        for (int x = px + 1, y = py + 1; x < Board.CrossCount && y < Board.CrossCount; x++, y++)
        {
            if (GetChess(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        ret = linkCount;
        linkCount = 1;
        // 左上
        for (int x = px -1 , y = py+ 1; x >=0 && y < Board.CrossCount; x--, y++)
        {
            if (GetChess(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        
        // 右下
        for (int x = px + 1, y = py - 1; x < Board.CrossCount && y >= 0; x++, y--)
        {
            if (GetChess(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        return Mathf.Max(ret, linkCount);
    }

    // 检查给定点周边的最大连接情况
    public int CheckLink(int px, int py, ChessType type )
    {
        int linkCount = 0;
       
        linkCount = Mathf.Max(CheckHorizentalLink(px, py, type), linkCount);
        linkCount = Mathf.Max(CheckVerticalLink(px, py, type), linkCount);
        linkCount = Mathf.Max(CheckBiasLink(px, py, type), linkCount);

        return linkCount;
    }

}
