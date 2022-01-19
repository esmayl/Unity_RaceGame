using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    float gameTime;

    void Update()
    {
        gameTime += Time.deltaTime;
        PlayerUIHandler.instance.UpdateTimeText(gameTime);
    }
}
