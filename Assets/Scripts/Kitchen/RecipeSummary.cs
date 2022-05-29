using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSummary : MonoBehaviour
{
    [SerializeField] private GameObject summary;
    [SerializeField] private Image recipeImage, congratsImage;
    [SerializeField] private float presenceTime, fadeTime;

    public void OpenSummary(RecipeSO recipe)
    {
        StartCoroutine(OpenSummaryCO(recipe));
    }

    IEnumerator OpenSummaryCO(RecipeSO recipe)
    {
        recipeImage.color = Color.white;
        congratsImage.color = Color.white;
        summary.SetActive(true);
        recipeImage.sprite = recipe.recipeSprite;
        yield return new WaitForSeconds(presenceTime);
        recipeImage.DOFade(0, fadeTime);
        congratsImage.DOFade(0, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        summary.SetActive(false);
    }
}
