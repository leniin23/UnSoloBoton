using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Minigame;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class MachineManager : MonoBehaviour
    {
        public static MachineManager instance;
        [SerializeField] private List<Color> possibleColors;
        [SerializeField] private List<Transform> ingredientLights;
        private List<List<Color>> colorList;
        private List<Color> mainColors;
        private static Random _random = new Random();
        private Color[] lightbulbColors;


        public void ResetColors()
        {
            mainColors.Clear();
            CalculateRandomColors();
        }
        private void Start()
        {
            if (instance == null) instance = this;
            else Destroy(this);
            
            lightbulbColors = new Color[ingredientLights.Count];
            mainColors = new List<Color>();
            for (var i = 0; i < ingredientLights.Count; i++)
            {
                lightbulbColors[i] = Color.gray;
            }
            //CalculateRandomColors();
        }

        public bool LightMainBulb(Color color)
        {
            if (mainColors.Count >= 3)
            {
                return false;
            }
            mainColors.Add(color);
            if(mainColors.Count == 3)   CheckIngredient();
            ReLightMainBulbs();
            return true;
        }

        public void TurnOffMainBulb(Color color)
        {
            mainColors.Remove(color);
            ReLightMainBulbs();
        }

        private void ReLightMainBulbs()
        {
            var mainLights = transform.Find("main lights");
            for (int i = 0; i < 3; i++)
            {
                var nextColor = mainColors.Count > i ? mainColors[i] : Color.gray;
                mainLights.GetChild(i).GetComponent<MeshRenderer>().material.color = nextColor;
            }
        }
        
        private bool CompareColors(Color ca, Color cb)
        {
            bool Compare(float a, float b) => (Mathf.Abs(a - b) <= 0.01f);
            return Compare(ca.r, cb.r) && Compare(ca.g, cb.g) && Compare(ca.b, cb.b);
        }
        
        private void CheckIngredient()
        {
            //Debug.Log("Checking...");
            var ingredient = -1;
            for (int i = 0; i < colorList.Count; i++)
            {
                var isRecipe = true;
                for (int j = 0; j < colorList[0].Count; j++)
                {
                    var isContained = false;
                    foreach (var mainColor in mainColors)
                    {
                        isContained = CompareColors(mainColor, colorList[i][j]);
                        if (isContained)
                        {
                            break;
                        }
                        //Debug.Log(mainColor + " - " + colorList[i][j]);
                    }
                    //Debug.Log(isContained);
                    isRecipe &= isContained;
                    //isRecipe &= mainColors.Contains(colorList[i][j]);
                }

                if (isRecipe)
                {
                    ingredient = i;
                    //Debug.LogWarning("Has puesto un " + ingredient);
                    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!AQUI VA EL CODIGO ; 0 = ternera; 1 = vegetal; 2 = pollo
                    MinigameManager.instance.EndGame(ingredient);
                }
            }
        }
        
        private void CalculateRandomColors()
        {
            Debug.Log("Recoloring");
            List<int> numbers = new List<int>();
            for (int i = 0; i < possibleColors.Count; i++)
            {
                numbers.Add(i);
            }

            colorList = new List<List<Color>>();


            for (var i = 0; i < lightbulbColors.Length; i++)
            {
                var n = _random.Next(numbers.Count);
                colorList.Add(new List<Color>());
                colorList[i].Add(possibleColors[numbers[n]]);
                numbers.RemoveAt(n);
            }
            for (var i = 0; i < lightbulbColors.Length; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    Color nextColor;
                    do
                    {
                        var n = _random.Next(numbers.Count);
                        nextColor = possibleColors[numbers[n]];
                    } while (colorList[i].Contains(nextColor));
                    colorList[i].Add(nextColor);
                }
                colorList[i] = colorList[i].OrderBy(_=>_random.Next()).ToList();
            }

            for (var i = 0; i < ingredientLights.Count; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var lightbulb = ingredientLights[i].GetChild(j);
                    if(!lightbulb.CompareTag("Lightbulb"))
                    {
                        lightbulb = ingredientLights[i].GetChild(j+1);
                        if(!lightbulb.CompareTag("Lightbulb"))
                        {
                            Debug.Log(lightbulb.name);
                            continue;
                        }
                    }
                    lightbulb.GetComponent<MeshRenderer>().material.color = colorList[i][j];
                }
            }
        }
    }
}