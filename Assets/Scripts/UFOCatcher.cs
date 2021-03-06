﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCatcher : MonoBehaviour {
    public UFOController UFOController;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Target" && UFOController.isCatching)
        {
            GameManager.instance.IncrementPlayerScore();
            Destroy(other.gameObject);
        }
    }
}
