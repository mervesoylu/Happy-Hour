using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Project
{

    public class CountDownController : MonoBehaviour
    {
        public void CountDown()
        {
            StartCoroutine(nameof(DoCountDown));
        }

        IEnumerator DoCountDown()
        {
            _countDownTextUI.gameObject.SetActive(true);
            _countDownTextUI.text = "3";
            yield return new WaitForSeconds(1);
            _countDownTextUI.text = "2";
            yield return new WaitForSeconds(1);
            _countDownTextUI.text = "1";
            yield return new WaitForSeconds(1);
            _countDownTextUI.gameObject.SetActive(false);
        }
        [SerializeField] TextMeshProUGUI _countDownTextUI;
    }
}
