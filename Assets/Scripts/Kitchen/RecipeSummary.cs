using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSummary : MonoBehaviour
{
    [SerializeField] private GameObject summary;
    [SerializeField] private Image recipeImage;
    [SerializeField] private int animationTime;

    public void OpenSummary(RecipeSO recipe)
    {
        StartCoroutine(OpenSummaryCO(recipe));
    }

    IEnumerator OpenSummaryCO(RecipeSO recipe)
    {
        summary.SetActive(true);
        recipeImage.sprite = recipe.recipeSprite;
        yield return new WaitForSeconds(animationTime);
        summary.SetActive(false);
    }
}
