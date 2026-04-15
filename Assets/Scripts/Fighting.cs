using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    
    [SerializeField] private TMP_Text countDownText;

    [SerializeField] private Ease Ease;

    [SerializeField] private float shrinkAmount;
    
    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {        
        for (int i = 5; i > 0; i--)
        {
            countDownText.text = i.ToString();
            countDownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            transform.DOScaleX(transform.localScale.x - transform.localScale.x / shrinkAmount, 1).SetEase(Ease);
            transform.DOScaleZ(transform.localScale.x - transform.localScale.x / shrinkAmount, 1).SetEase(Ease);
        }
        if (player1.transform.localScale.x > 3)
            GameManager.instance.NextRound(false);
        else
            GameManager.instance.NextRound(true);
        
    }
}
