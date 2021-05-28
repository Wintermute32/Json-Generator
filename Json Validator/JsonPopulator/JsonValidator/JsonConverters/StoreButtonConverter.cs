﻿using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator.JsonConverters
{
    class StoreButtonConverter : JsonGeneration  // need to 
    {
        var popLists = flowBoxes.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
        List<string> _popIds = new List<string>();
            
       foreach (var x in popLists) //break me into a new class with store appearance
         {
                _popIds.Add(x.Text);
            }

    StoreButtonAppearance storeButtonAppearance = new StoreButtonAppearance()
    {
        style = comboBoxes.Find(x => x.Name == "styleCB").Text,
        ribbonLocalizationKey = comboBoxes.Find(x => x.Name == "ribbonLocKeyCB").Text,
        titleLocalizationKey = comboBoxes.Find(x => x.Name == "subLocKeyCB").Text,
        popIds = _popIds,
        order = Convert.ToInt32(comboBoxes.Find(x => x.Name == "orderCB").Text),
        discount = Convert.ToInt32(comboBoxes.Find(x => x.Name == "discountCB").Text)
    };
}