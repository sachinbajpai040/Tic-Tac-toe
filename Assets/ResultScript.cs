using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ResultScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Result ResultData;
    public Text player1;
    public Text player2;
    public Text playerWon;
    public Text matchNumber;
    void Start()
    {
        string savestring = File.ReadAllText(Application.dataPath + "/result.json");
        ResultData = JsonUtility.FromJson<Result>(savestring);
        Debug.Log("Result Called" + ResultData.NoOfMatches);
        int count = 0;
        for (int i = ResultData.NoOfMatches-1; i >=0; i--)
        {
            matchNumber.text += (count++ + 1)+"\n";
            player1.text += ResultData.player1name[i]+"\n";
            player2.text += ResultData.player2name[i]+"\n";
            playerWon.text += ResultData.playerWon[i]+"\n";
            Debug.Log(ResultData.playerWon[i]);
        }
    }
    public class Result
    {
        public string[] player1name = new string[20]; 
        public string[] player2name = new string[20];
        public string[] playerWon = new string[20];
        // public string[] result = new string[5];
        public int NoOfMatches = 0;
    }
}
