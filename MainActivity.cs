using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.ObjectModel;

namespace FamilyCalculator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ObservableCollection<Item> listE = new ObservableCollection<Item>();
        ListView mainList;
        string categorySelect;
        double total;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.activity_main);
            
            EditText price = FindViewById<EditText>(Resource.Id.Price);
            EditText name = FindViewById<EditText>(Resource.Id.Name);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            Button addButton = FindViewById<Button>(Resource.Id.add);
            Button clearButton = FindViewById<Button>(Resource.Id.clearButton);
            TextView sum = FindViewById<TextView>(Resource.Id.Sum);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(this, Resource.Array.category_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter2;
            
            mainList = (ListView)FindViewById<ListView>(Resource.Id.mainlistview);
            Adapter1 adapter = new Adapter1(this, listE);
            mainList.Adapter = adapter;
            Button removeButton = FindViewById<Button>(Resource.Id.removeButton);

            listE.Add(new Item("Їжа","McDonalds", 500));
            listE.Add(new Item("Їжа", "KFC", 700));
            listE.Add(new Item("Їжа", "McDonalds", 500));

            int index = listE.Count - 1;
            
            foreach (Item i in listE)
            {
                total += Convert.ToInt32(i.Price);
            }
            sum.Text = (total).ToString() + "грн";
            
            CategoryCalc("add");

            mainList.ItemClick += (s, e) => {
                var t = listE[e.Position].Name;
                Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Long).Show();
            };

            addButton.Click += (sender, e) =>
            {
                if (String.IsNullOrEmpty(name.Text))
                {
                    Toast.MakeText(this, "Введіть назву!", ToastLength.Long).Show();
                }
                else if (String.IsNullOrEmpty(price.Text))
                {
                    Toast.MakeText(this, "Введіть ціну!", ToastLength.Long).Show();
                }
                else
                {
                    listE.Add(new Item(categorySelect, name.Text, Convert.ToDouble(price.Text)));
                    adapter.NotifyDataSetChanged();
                    index = listE.Count - 1;
                    total += listE[index].Price;
                    sum.Text = (total).ToString() + "грн";
                    CategoryCalc("add");

                    if (listE.Count != 0)
                    {
                        clearButton.Enabled = true;
                        removeButton.Enabled = true;
                    }
                }
            };
            
            removeButton.Click += (s, e) =>
            {
               total -= listE[index].Price;
               listE.RemoveAt(index);
               adapter.NotifyDataSetChanged();
               index = listE.Count - 1;
               sum.Text = (total).ToString() + "грн";
               CategoryCalc("remove");

               if (listE.Count == 0)
               {
                   removeButton.Enabled = false;
                   clearButton.Enabled = false;
                   sum.Text = "";
                   total = 0;
                   CategoryCalc("clear");
               }
            };
            
            clearButton.Click += (sender, e) =>
            {
                listE.Clear();
                adapter.NotifyDataSetChanged();
                CategoryCalc("clear");
                sum.Text = "";
                total = 0;
                index = listE.Count - 1;
                removeButton.Enabled = false;
                clearButton.Enabled = false;
            };
        }
        
        private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            EditText price = FindViewById<EditText>(Resource.Id.Price);
            EditText name = FindViewById<EditText>(Resource.Id.Name);
            
            Spinner spinner = (Spinner)sender;
            
            price.Text = "";
            name.Text = "";
            
            categorySelect = spinner.GetItemAtPosition (e.Position).ToString();
        }

        public void CategoryCalc(string operation)
        {
            TextView foodCell = FindViewById<TextView>(Resource.Id.FoodCell);
            TextView paymentsCell = FindViewById<TextView>(Resource.Id.PaymentsCell);
            TextView clothCell = FindViewById<TextView>(Resource.Id.ClothCell);
            TextView carCell = FindViewById<TextView>(Resource.Id.CarCell);
            TextView houseCell = FindViewById<TextView>(Resource.Id.HouseCell);
            TextView transportCell = FindViewById<TextView>(Resource.Id.TransportCell);
            TextView techCell= FindViewById<TextView>(Resource.Id.TechCell);
            TextView otherCell = FindViewById<TextView>(Resource.Id.OtherCell);

            double food = 0;
            double payments = 0;
            double cloth = 0;
            double car = 0;
            double house = 0;
            double transport = 0;
            double tech = 0;
            double other = 0;
            
            if (operation == "add" || operation == "remove")
            {
                foreach (var item in listE)
                {
                    if (item.Category == "Їжа")
                    {
                        food += item.Price;
                    }
                    else if (item.Category == "Платежі")
                    {
                        payments += item.Price;
                    }
                    else if (item.Category == "Одяг")
                    {
                        cloth += item.Price;
                    }
                    else if (item.Category == "Авто")
                    {
                        car += item.Price;
                    }
                    else if (item.Category == "Побут")
                    {
                        house += item.Price;
                    }
                    else if (item.Category == "Транспорт")
                    {
                        transport += item.Price;
                    }
                    else if (item.Category == "Техніка")
                    {
                        tech += item.Price;
                    }
                    else if (item.Category == "Інше")
                    {
                        other += item.Price;
                    }

                    foodCell.Text = $"Їжа: {food} грн.";
                    paymentsCell.Text = $"Платежі: {payments} грн.";
                    clothCell.Text = $"Одяг: {cloth} грн.";
                    carCell.Text = $"Авто: {car} грн.";
                    houseCell.Text = $"Побут: {house} грн.";
                    transportCell.Text = $"Транспорт: {transport} грн.";
                    techCell.Text = $"Техніка: {tech} грн.";
                    otherCell.Text = $"Інше: {other} грн.";
                }
            }

            else
            {
                foodCell.Text = $"Їжа: 0 грн.";
                paymentsCell.Text = $"Платежі: 0 грн.";
                clothCell.Text = $"Одяг: 0 грн.";
                carCell.Text = $"Авто: 0 грн.";
                houseCell.Text = $"Побут: грн.";
                transportCell.Text = $"Транспорт: грн.";
                techCell.Text = $"Техніка: 0 грн.";
                otherCell.Text = $"Інше: 0 грн.";
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}