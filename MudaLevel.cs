using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudaLevel : MonoBehaviour
{
    public void GameLevel2()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase + 2);
    }

    public void GameLevel1()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase - 2);
    }
}
