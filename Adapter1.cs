using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Android.Resource;

namespace FamilyCalculator
{
    class Adapter1 : BaseAdapter<Item>
    {
        public ObservableCollection<Item> list;
        Context mContext;

        public Adapter1(Context context, ObservableCollection<Item> items)
        {
            mContext = context;
            list = items;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Item this[int position]
        {
            get { return list[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }

            TextView category = row.FindViewById<TextView>(Resource.Id.textCategory);
            category.Text = list[position].Category;

            TextView name = row.FindViewById<TextView>(Resource.Id.textName);
            name.Text = list[position].Name;

            TextView price = row.FindViewById<TextView>(Resource.Id.textPrice);
            price.Text = (list[position].Price).ToString() + " грн";


            return row;
        }

        
        public override int Count
        {
            get
            {
                return list.Count;
            }
        }
    }
}