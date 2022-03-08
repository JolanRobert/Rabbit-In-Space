using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPanel : MonoBehaviour
{
    [SerializeField] private Transform panelGroup;
    [SerializeField] private GameObject recipePanelPrefab;
    private GameObject recipePanel;
    [SerializeField] private List<RecipeSO> recipes;
    void Start()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            recipePanel = Instantiate(recipePanelPrefab, Vector3.zero, Quaternion.identity, panelGroup);
            recipePanel.GetComponent<RecipePanel>().SetupPanel(recipes[i]);
        }
    }
}
