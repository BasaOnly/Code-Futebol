using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltaInicio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ClicouInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void ClicouInicio2()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
