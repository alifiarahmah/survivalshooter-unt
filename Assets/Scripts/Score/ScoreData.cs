using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData
{
    public List<Score> scoreData;
    // Start is called before the first frame update
    public ScoreData()
    {
        scoreData = new List<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
