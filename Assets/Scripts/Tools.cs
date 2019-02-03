using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace VolkTools
{


    public static class Tools
    {
        /// <summary>
        /// clear all finded T items in transform conteiner 
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="T"></typeparam>
        public static void ClearItems<T>(Transform container, bool withDisabled = false) where T : MonoBehaviour
        {
            List<T> allChildren = container.GetComponentsInChildren<T>(withDisabled).ToList<T>();
            allChildren.Remove(container.GetComponent<T>());// remove container from list  if it have T item
            allChildren.ForEach(x => GameObject.Destroy(x.gameObject));
            allChildren.Clear();
        }

        public static T ReturnRandomByWeight<T>(T[] objectsToRandom, float[] objectsWeight)
        {
            // ReturnRandomByWeight(new uint[4]{1,2,3,5},new float[4]{ 50,10,20,5})  - ������
            if (objectsToRandom.Length < 1 || objectsWeight.Length < 1)
            {
                Debug.LogError("Lenght one of array has 0");
                return default(T);
            }
            if (objectsWeight.Length != objectsToRandom.Length)
            {
                Debug.LogError("arrays are not equal");
                return default(T);
            }
            Dictionary<T, float> weightDictionary = new Dictionary<T, float>();
            float maxRandom = 0;

            for (int i = 0; i < objectsToRandom.Length; i++)
            {
                weightDictionary.Add(objectsToRandom[i], objectsWeight[i]);
                maxRandom += objectsWeight[i];
            }
            float dice = Random.Range(0, maxRandom);
            float currentWalue = 0;
            foreach (T thisObject in objectsToRandom)
            {
                currentWalue += weightDictionary[thisObject];
                if (dice > currentWalue)
                {
                    continue;
                }
                return thisObject;
            }
            Debug.Log("unrecheble place reached");
            return default(T);
        }
    }
}
