using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode // 오브젝트에 맵핑하지 않기 때문에 MonoBehaviour를 상속받지 않는다. 
{
    public TerrainController Terrain { get; private set; }
    public GameObject DestinationObj { get; private set; }

    // A Star algorithm
    public float AstarF { get; private set; } = float.MaxValue; // 가장 큰 값을 MaxValue로 설정
    public float AstarG { get; private set; } = float.MaxValue;
    public float AstarH { get; private set; } = float.MaxValue;

    public AstarNode AstarPrevNode { get; private set; } = default; // 이곳에 저장

    public AstarNode(TerrainController terrain_, GameObject destinationObj_)
    {
        Terrain = terrain_;
        DestinationObj = destinationObj_;
    }   // AstarNode()

    //! Astar 알고리즘에 사용할 비용을 설정한다.
    public void UpdateCost_Astar(float gCost, float heuristic, AstarNode prevNode)
    {
        float aStarF = gCost + heuristic;

        if (aStarF < AstarF)
        {
            AstarG = gCost;
            AstarH = heuristic;
            AstarF = aStarF;

            AstarPrevNode = prevNode;

        }   // if: 새로 계산한 비용이 더 작은 경우에만 업데이트 한다. 
        else { /* Do nothing */ }
    }   // UpdateCost_Astar()

    //! 설정한 비용을 출력한다.
    public void ShowCost_Astar()
    {
        GFunc.Log($"TileIdx1D: {Terrain.TileIdx1D}," +
            $"F: {AstarF}, G: {AstarG}, H: {AstarH}");      // $ 는 문자열 보간
        //1D는 순서대로 가서 확인한다. 2D는 {0,0} 방식으로 확인한다.
        //ShowCost_Astar()
    }



}
