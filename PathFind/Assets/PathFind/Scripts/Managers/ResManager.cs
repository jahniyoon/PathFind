using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : GSingleton<ResManager>
{
    //���ҽ��� �����յ��� �� �������� ��ũ��Ʈ
    private const string TERRAIN_PREF_PATH = "Prefabs";
    //������ �����ǰ� �ȴٸ� �ٲ�����Ѵ�.

    public Dictionary<string, GameObject> terrainPrefabs = default;

    protected override void Init()
    {
        base.Init();

        terrainPrefabs = new Dictionary<string, GameObject>();

        terrainPrefabs.AddObjs(Resources.LoadAll<GameObject>(TERRAIN_PREF_PATH));
        //LoadAll�� �� �����´�.
    }
}
