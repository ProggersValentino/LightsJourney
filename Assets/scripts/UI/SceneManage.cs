using UnityEngine.SceneManagement;
using UnityEngine;
 
public class SceneManage : MonoBehaviour
{
        void OnTriggerEnter(Collider other)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // calculation for next scene
            SceneManager.LoadScene(sceneIndex);
            // SceneManager.LoadScene("Level1");
        }


        public void playWithDark()
        {
            SceneManager.LoadScene("Level1");
        }

        public void rageQuit()
        {
            
        }

        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);

        }

}