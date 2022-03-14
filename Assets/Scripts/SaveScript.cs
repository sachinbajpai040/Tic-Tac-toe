using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public GameObject gameBoard;
    public Button saveButton;
    void Start()
    {
        Button btn = saveButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        //Debug.Log("You have clicked the Save button!");
        gameBoard.GetComponent<GameScript>().Save();
    }
}