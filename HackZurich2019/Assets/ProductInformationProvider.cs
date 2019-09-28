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

using System;
using System.Collections.Generic;
using Random = System.Random;

namespace MagicLeap
{
    public class ProductInformationProvider : MonoBehaviour
    {

        [System.Serializable]
        private class CO2CostReceivingEvent : UnityEvent<double>{};
        [SerializeField, Space]
        private CO2CostReceivingEvent OnCostReceivedEvent = null;

        private void OnCostReceived(double usage)
        {
            OnCostReceivedEvent.Invoke(usage);
        }


        #region Public Variables
        public Text co2ConsumptionText, healthText, productText, originText;
        #endregion

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

        #region Event Handlers
        /// <summary>
        /// Updates preview object with new captured image
        /// </summary>
        /// <param name="texture">The new image that got captured.</param>
        public void OnImageCaptured(Texture2D texture)
        {
            co2value += co2sampler.Next(0, 10);
            healthScore += healthSampler.Next(0, 10);

            co2ConsumptionText.text = "going up to " + co2value.ToString();
            healthText.text = "going down to " + healthScore.ToString();

            currentCountry = countrySampler.Next(0, nCountries - 1);
            currentProduct = productSampler.Next(0, nProducts - 1);

            originText.text = Countries[currentCountry];
            productText.text = Products[currentProduct];

            
        }
        #endregion
    }
}
