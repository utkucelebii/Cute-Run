using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeItem : MonoBehaviour
{
    public enum type
    {
        none, tile, visual, block, reward
    }
    public enum tile
    {
        none, straight, left, right, leftRight,
    }
    public enum visual
    {
        none, tree, mountain,
    }
    public enum block
    {
        none, block, longblock, jump, slide, bigblock
    }
    public enum reward
    {
        none, coin, candy
    }

    public ModeData.SubMode subMode;
    public type itemType;
    public tile tileType;
    public visual visualType;
    public block blockType;
    public reward rewardType;
}
