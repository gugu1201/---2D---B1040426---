using UnityEngine;
using UnityEngine.SceneManagement;  // 引用 場景管理 API

namespace GUGU
{
    public class GameManager : MonoBehaviour
    {
        public void Replay()
        {            
            SceneManager.LoadScene("DemoScene");   
        }

        public void Quit()
        {
            Application.Quit(); 
        }
    }
}