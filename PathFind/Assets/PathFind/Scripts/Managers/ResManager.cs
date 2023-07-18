using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : GSingleton<ResManager>
{
    //리소스의 프리팹들을 다 가져오는 스크립트
    private const string TERRAIN_PREF_PATH = "Prefabs";
    //폴더가 수정되게 된다면 바꿔줘야한다.

    public Dictionary<string, GameObject> terrainPrefabs = default;

    protected override void Init()
    {
        base.Init();

        terrainPrefabs = new Dictionary<string, GameObject>();

        terrainPrefabs.AddObjs(Resources.LoadAll<GameObject>(TERRAIN_PREF_PATH));
        //LoadAll로 다 가져온다.
    }
}
