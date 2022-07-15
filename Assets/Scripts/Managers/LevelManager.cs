using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private GameManager gameManager;
    private ModeData.Mode currentMode;
    public ModeData modeData;
    public Vector3 currentDirection = Vector3.forward;
    public bool movingLane = true;



    private void Start()
    {
        currentMode = gameManager.currentMode;
        modeData = gameManager.datas.Find(x => x.modes == currentMode);
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    
}
