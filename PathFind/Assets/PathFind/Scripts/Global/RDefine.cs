using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RDefine //Resource Define
{
    public const string TERRAIN_PREF_OCEAN = "Tile_Ocean";
    public const string TERRAIN_PREF_PLAIN = "Tile_Plane";
    public const string TERRAIN_PREF_FOREST = "Tile_Forest";

    public const string OBSTACLE_PREF_PLAIN_CASTLE = "Obstacle_PlainCastle";

    public enum TileStatusColor
    {
        DEFAULT, SELECTED, SEARCH, INACTIVE
    }
}
