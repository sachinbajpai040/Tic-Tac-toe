using UnityEngine;
public class TileScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] tileImages;
    
    public GameObject gameboard1;
    
    public int tileNumber;
    public bool isPlayed;
    void Start()
    {
        spriteRenderer.sprite = null;
        isPlayed = false;
    }
    public void OnMouseDown()
    {
        bool isGameOver = gameboard1.GetComponent<GameScript>().isGameOver;
        bool isStarted = gameboard1.GetComponent<GameScript>().isStarted;
        
        if (!isPlayed && !isGameOver && isStarted)
        {
            int index = gameboard1.GetComponent<GameScript>().Playerturn();
            Debug.Log(index);
            spriteRenderer.sprite = tileImages[index];
            isPlayed = true;
            gameboard1.GetComponent<GameScript>().Played(tileNumber, index);
        }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent < SpriteRenderer>();
    }
}