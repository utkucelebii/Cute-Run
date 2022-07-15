using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public Text currencyText;

    private void Update()
    {
        currencyText.text = GameManager.Instance.currency.ToString();
    }
}
