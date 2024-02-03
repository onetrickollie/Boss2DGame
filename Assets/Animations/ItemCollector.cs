using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using box collider to detect if item is touched
public class ItemCollector : MonoBehaviour
{

    private int strawberries = 0;
    [SerializeField] private Text strawberriesText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            Destroy(collision.gameObject);
            strawberries++;
            strawberriesText.text = "Boss has " + strawberries + " strawberries!";
            if(strawberries == 3)
            {
                strawberriesText.text = "Boss got all his strawberries!!\nhe is now happy";
            }
            if(strawberries > 3)
            {
                strawberriesText.text = "Wait... there's extra??";
            }
        }
    }
}
