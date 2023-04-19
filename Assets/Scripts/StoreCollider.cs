using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCollider : MonoBehaviour
{
    public GameObject storePanel;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            storePanel.SetActive(true);
        }
    }
}
