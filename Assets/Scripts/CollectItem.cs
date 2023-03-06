using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    private int appleCount = 0;
    [SerializeField] private Text collectibleText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AppleCollectible"))
        {
            Destroy(collision.gameObject);
            appleCount++;
            collectibleText.text = "Collected Apples: " + appleCount;
        }
    }
}
