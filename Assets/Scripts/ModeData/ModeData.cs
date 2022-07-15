using UnityEngine;


[CreateAssetMenu(menuName = "ModeData")]
public class ModeData : ScriptableObject
{
    public enum Mode 
    { 
        none,
        Default
    }
    public enum SubMode
    {
        winter,
        spring,
        summer,
        autumn,
        street
    }
    public Mode modes;
    public SubMode[] subModes;
    public ModeItem[] items;
    public ModeData[] subModeData;
}