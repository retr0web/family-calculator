using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyCalculator
{
    class Item
    {
        public string Category;
        public string Name;
        public double Price;

        public Item(string category, string name, double price)
        {
            Category = category;
            Name = name;
            Price = price;
        }
    }
}