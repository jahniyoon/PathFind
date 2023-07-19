using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMap : TileMapController //Ÿ�ϸ� ��Ʈ�ѷ� ���
{
    private const string TERRAIN_TILEMAP_OBJ_NAME = "TerrainTilemap";   // ���ӿ�����Ʈ��� �Ȱ��� �����

    private Vector2Int mapCellsize = default;
    private Vector2 mapCellGap = default;

    private List<TerrainController> allTerrains = default;

    //! Awake Ÿ�ӿ� �ʱ�ȭ�� ���� �������Ѵ�.

    public override void InitAwake(MapBoard mapController_)
    {
        tileMapObjName = TERRAIN_TILEMAP_OBJ_NAME;
        base.InitAwake(mapController_);

        allTerrains = new List<TerrainController>();

        // { Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.
        mapCellsize = Vector2Int.zero;
        float tempTileY = allTileObjs[0].transform.localPosition.y;
        for (int i = 0; i < allTileObjs.Count; i++)
        {
            if (tempTileY.IsEquals(allTileObjs[i].transform.localPosition.y) == false)
            {
                mapCellsize.x = i;
                break;
                        // if : ù���� Ÿ���� y ��ǥ�� �޶����� ���� �� ������ ���� ���� �� ũ���̴�.
            }
        }

        // ��ü Ÿ���� ���� ���� ���� �� ũ��� ���� ���� ���� ���� �� ũ���̴�.
        mapCellsize.y = Mathf.FloorToInt(allTileObjs.Count / mapCellsize.x);

        // } Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.

        // { x �� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�. 
        mapCellGap = Vector2.zero;
        mapCellGap.x = allTileObjs[1].transform.localPosition.x - 
            allTileObjs[0].transform.localPosition.x;
        mapCellGap.y = allTileObjs[mapCellsize.x].transform.localPosition.y -
            allTileObjs[0].transform.localPosition.y;
        // } x �� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�. 
    }   // InitAwake()


    private void Start()
    {
        // { Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����
        GameObject changeTilePrefab = ResManager.Instance.terrainPrefabs
            [RDefine.TERRAIN_PREF_OCEAN];

        // Ÿ�ϸ� �߿� ��� ������ �ٴٷ� ��ü�� ������ �����Ѵ�.
        const float CHANGE_PERCENTAGE = 25.0f;
        float correctChangePercentage = allTileObjs.Count * (CHANGE_PERCENTAGE / 100.0f);

        // �ٴٷ� ��ü�� Ÿ���� ������ ����Ʈ ���·� �����ؼ� ���´�.
        List<int> changedTileResult = GFunc.CreateList(allTileObjs.Count, 1);
        changedTileResult.Shuffle();

        GameObject tempChangeTile = default;
        for (int i = 0; i < allTileObjs.Count; i++)
        {
            if (correctChangePercentage <= changedTileResult[i]) { continue; }

            // �������� �ν��Ͻ�ȭ�ؼ� ��ü�� Ÿ���� Ʈ�������� �����Ѵ�.
            tempChangeTile = Instantiate(
                changeTilePrefab, tileMap.transform);
            tempChangeTile.name = changeTilePrefab.name;
            tempChangeTile.SetLocalScale(allTileObjs[i].transform.localScale);
            tempChangeTile.SetLocalPos(allTileObjs[i].transform.localPosition);

            allTileObjs.Swap(ref tempChangeTile, i);
            tempChangeTile.DestroyObj();
        }       // loop : ������ ������ ������ ���� Ÿ�ϸʿ� �ٴٸ� �����ϴ� ����
        // } Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����

        // { ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����
        TerrainController tempTerrain = default;
        TerrainType terrainType = TerrainType.NONE;

        int loopCnt = 0;
        foreach (GameObject tile_ in allTileObjs)
        {
            tempTerrain = tile_.GetComponentMust<TerrainController>();
            switch (tempTerrain.name)
            {
                case RDefine.TERRAIN_PREF_PLAIN:
                    terrainType = TerrainType.PLAIN_PASS;
                    break;
                case RDefine.TERRAIN_PREF_OCEAN:
                    terrainType = TerrainType.OCEAN_N_PASS;
                    break;
                default:
                    terrainType = TerrainType.NONE;
                    break;
            }                    // switch: �������� �ٸ� ������ �Ѵ�.

            // TODO : tempTerrain Setup �Լ� �ʿ���.
            tempTerrain.SetupTerrain(mapController, terrainType, loopCnt);
            tempTerrain.transform.SetAsFirstSibling();  // �ش� ������Ʈ�� ������ ó������ ���� (���� ó�� ��µǹǷ� ���Ġ��� ��� �������ϴ�.)
            allTerrains.Add(tempTerrain);
            loopCnt += 1;
            // loop : Ÿ���� �̸��� ������ ������� �����ϴ� ����

            //// { Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �� Ÿ�Ϸ� ��ü�ϴ� ���� ========================
            //GameObject changeTilePrefab2 = ResManager.Instance.terrainPrefabs
            // [RDefine.TERRAIN_PREF_FOREST];

            //// ������ ��ü�� Ÿ���� ������ ����Ʈ ���·� �����ؼ� ���´�.
            //List<int> changedTileResult2 = GFunc.CreateList(allTileObjs.Count, 1);
            //changedTileResult2.Shuffle();

            //GameObject tempChangeTile2 = default;
            //for (int i = 0; i < allTileObjs.Count; i++)
            //{
            //    if (correctChangePercentage <= changedTileResult2[i]) { continue; }

            //    // �������� �ν��Ͻ�ȭ�ؼ� ��ü�� Ÿ���� Ʈ�������� �����Ѵ�.
            //    tempChangeTile2 = Instantiate(
            //        changeTilePrefab2, tileMap.transform);
            //    tempChangeTile2.name = changeTilePrefab2.name;
            //    tempChangeTile2.SetLocalScale(allTileObjs[i].transform.localScale);
            //    tempChangeTile2.SetLocalPos(allTileObjs[i].transform.localPosition);

            //    allTileObjs.Swap(ref tempChangeTile2, i);
            //    tempChangeTile2.DestroyObj();
            //}       // loop : ������ ������ ������ ���� Ÿ�ϸʿ� �ٴٸ� �����ϴ� ����
            //        // { Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ���� ========================
        }
        // } ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����
    }

    //! �ʱ�ȭ�� Ÿ���� ������ ������ ���� ����, ���� ũ�⸦ �����ϴ� �Լ�
    public Vector2Int GetCellSize() { return mapCellsize; }

    //! �ʱ�ȭ�� Ÿ���� ������ ������ Ÿ�� ������ ���� �����Ѵ�.
    public Vector2 GetCellGap() { return mapCellGap; }

    //! �ε����� �ش��ϴ� Ÿ���� �����ϴ� �Լ�
    public TerrainController GetTile(int tileIdx1D)
    {
        if (allTerrains.IsValid(tileIdx1D))
        {
            return allTerrains[tileIdx1D];
        }
        return default;
    }   // GetTile()

}
