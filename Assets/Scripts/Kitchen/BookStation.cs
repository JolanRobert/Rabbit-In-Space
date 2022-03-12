using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStation : InteractableElement
{
    public override void Interact() {
        UIManager.Instance.OpenMinigame(interactPanel); //Faudra faire une nouvelle fonction, openminigame ça correspond pas
    }
}
