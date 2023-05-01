using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

    #region Singleton

    private static CustomerManager instance;
    public static CustomerManager Instance { get { return instance; } }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    #endregion

    [Header("Settings")]
    public List<RecipeData> possibleRecipes;
    public int currentDifficultyLevel;

    private void Update() {
        
    }

} 