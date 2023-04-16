using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static float time;
    Text text;

    void Awake ()
    {
        text = GetComponent <Text> ();
        time = 0;
    }


    void Update ()
    {
        time += Time.deltaTime;
        text.text = "Time: " + time.ToString("F2");
    }
}
