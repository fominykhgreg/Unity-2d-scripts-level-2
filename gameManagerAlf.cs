using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameManagerAlf : MonoBehaviour
{

    public AudioSource myFx;
    public AudioClip najim;
    public void Sound()
    {
        myFx.PlayOneShot(najim);
    }
   


    public void gameOver()
    {
        Sound();
        print("GAME OVER.");
        Invoke("reloadScene", 3);
        Invoke("sbros", 3);
       
    }
    public void gameWin()
    {
        //Invoke("konec", 3);
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void sbros()
    {
        lifeScriptAlf.lifeValue = 100;
        
        scoreScriptAlf.scoreValue = 0; 
       
       
    }
    void endgame()
    {
        lifeScript.lifeValue = 1;
    }

    void konec()
    {
        //Application.LoadLevel("mult3");
    }

}
