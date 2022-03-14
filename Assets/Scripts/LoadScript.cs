using UnityEngine;
using UnityEngine.UI;

public class LoadScript : MonoBehaviour
{
    public GameObject gameBoard;
    public Button loadButton;
    void Start()
    {
        Button btn = loadButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        if(gameBoard.GetComponent<GameScript>().isGameOver)
        {
            gameBoard.GetComponent<GameScript>().DoNotLoad();
            
        }
        else
        {
            loadButton.gameObject.SetActive(false);
            Debug.Log("You have clicked the Load button!");
            gameBoard.GetComponent<GameScript>().Resume();
        }
        
    }
}