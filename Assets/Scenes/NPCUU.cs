using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUU : MonoBehaviour
{
    public enum state
    {       
        start, notComplete, complete
    }

    public state _state;

    [Header("對話")]
    public string sayStart = "請前往地圖最右側拿取秘寶";
    public string sayNotComplete = "還沒去拿秘寶嗎?";
    public string sayComplete = "感謝";
    [Range(0.001f, 1.5f)]
    public float speed = 1.5f;
    public AudioClip soundSay;    
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    [Header("勝利")]
    public GameObject Win;

    
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.name == "Playr")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Playr")
            SayClose();
    }

    private void Say()
    {
        // 畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish) _state = state.complete;


        // 判斷式(狀態)
        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog(sayStart));           // 開始對話
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));     // 開始對話未完成
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));        // 開始對話完成
                Win.SetActive(true);
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                              // 清空文字

        for (int i = 0; i < say.Length; i++)            // 迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();          // 累加每個文字
            //aud.PlayOneShot(soundSay, 0.6f);            // 播放一次音效(音效片段，音量)
            yield return new WaitForSeconds(speed);     // 等待
        }
    }

    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    public void PlayerGet()
    {
        countPlayer++;
    }
        
}
