using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class MachineManager : MonoBehaviour
    {
        [SerializeField] private List<Color> possibleColors;
        [SerializeField] private List<Transform> ingredientLights;
        private List<List<Color>> colorList;
        private List<Color> mainColors;
        public int NOnBulbs { get; private set; }

        private Color[] lightbulbColors;

        private void Start()
        {
            lightbulbColors = new Color[ingredientLights.Count];
            mainColors = new List<Color>();
            for (var i = 0; i < ingredientLights.Count; i++)
            {
                lightbulbColors[i] = Color.gray;
            }
            CalculateRandomColors();
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

        private void CheckIngredient()
        {
            Debug.Log("Checking...");
            var ingredient = -1;
            for (int i = 0; i < colorList.Count; i++)
            {
                var isRecipe = true;
                for (int j = 0; j < colorList[0].Count; j++)
                {
                    isRecipe &= !mainColors.TrueForAll(col =>!(Mathf.Approximately(col.r, colorList[i][j].r) && Mathf.Approximately(col.g, colorList[i][j].g) && Mathf.Approximately(col.b, colorList[i][j].b)));
                    //isRecipe &= mainColors.Contains(colorList[i][j]);
                    if (!isRecipe)
                    {
                        Debug.Log("No esta " + colorList[i][j]);
                    }
                }

                if (isRecipe)
                {
                    ingredient = i;
                    Debug.Log("Has puesto un " + ingredient);
                }
            }
        }
        
        private void CalculateRandomColors()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < possibleColors.Count; i++)
            {
                numbers.Add(i);
            }

            var random = new Random();
            colorList = new List<List<Color>>();


            for (var i = 0; i < lightbulbColors.Length; i++)
            {
                var n = random.Next(numbers.Count);
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
                        var n = random.Next(numbers.Count);
                        nextColor = possibleColors[numbers[n]];
                    } while (colorList[i].Contains(nextColor));
                    colorList[i].Add(nextColor);
                }
                colorList[i] = colorList[i].OrderBy(_=>random.Next()).ToList();
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