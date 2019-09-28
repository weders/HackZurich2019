// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using Random = System.Random;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MagicLeap
{
    public class ProductInformationProvider : MonoBehaviour
    {

        private ProgressBar progressBar;
        private CaloriesProgressBar caloriesProgressBar;

        ///[System.Serializable]
        ///private class CO2CostReceivingEvent : UnityEvent<double>{};
        //[SerializeField, Space]
        //private CO2CostReceivingEvent OnCostReceivedEvent = null;

        #region Public Variables
        public Text productText, originText;
        public static double co2ConsumptionState;
        #endregion

        public string URL = "http://api-beta.bite.ai/vision";

        IEnumerator PostCall(string queryImage)
        {
            WWWForm form = new WWWForm();
            form.headers["Authorization"] = "bearer 5ced68e643ecc02d1bc07b557c941be2aa90976b";
            form.headers["Content-Type"] = "application/json";
            form.AddField("base64", queryImage);

            Debug.Log("start request");
            UnityWebRequest www = UnityWebRequest.Post(URL, form);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log("error from API");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("got results from API");
            }

            Debug.Log("finished API call");
        }

        void Awake()
        {
            progressBar = GameObject.FindObjectOfType<ProgressBar>();
            caloriesProgressBar = GameObject.FindObjectOfType<CaloriesProgressBar>();
        }

        int co2value = 0;
        int healthScore = 0;

        Random co2sampler = new Random();
        Random healthSampler = new Random();
        Random countrySampler = new Random();
        Random productSampler = new Random();

        public static List<string> Countries = new List<string>() {"spain", "chile", "colombia", "switzerland", "france", "germany", "italy", "china"};
        public static List<string> Products = new List<string>() {"bananas", "avocados", "beef", "potatoes", "apples", "pork", "eggs", "chicken", "beer"};

        int nProducts = Products.Count;
        int nCountries = Countries.Count;

        int currentCountry;
        int currentProduct;

        double totalCO2 = 100;
        double totalCalories = 2500;
        double currentUsage;
        double currentCalories;

        private byte[] image;
        string base64_image;

        #region Event Handlers
        /// <summary>
        /// Updates preview object with new captured image
        /// </summary>
        /// <param name="texture">The new image that got captured.</param>
        public void OnImageCaptured(byte[] texture)
        {
            if (texture != null)
            {
                Debug.Log("starting to convert");
                base64_image = Convert.ToBase64String(texture);
                Debug.Log("finished conversion");
                PostCall(base64_image);

            }
            else
            {
                Debug.Log("Texture is null, API call not possible");
            }

            co2value += co2sampler.Next(0, 20);
            healthScore += healthSampler.Next(100, 500);

            currentCountry = countrySampler.Next(0, nCountries - 1);
            currentProduct = productSampler.Next(0, nProducts - 1);

            originText.text = Countries[currentCountry];
            productText.text = Products[currentProduct];

            currentUsage = Convert.ToDouble(co2value) / totalCO2;
            Debug.Log(currentUsage);
            Debug.Log(totalCO2);

            co2ConsumptionState = currentUsage;
            currentCalories = healthScore / totalCalories;

            Awake();
            progressBar.UpdateProgressBar(co2ConsumptionState);
            caloriesProgressBar.UpdateProgressBar(currentCalories);

        }
        #endregion
    }
}
