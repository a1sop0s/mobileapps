using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Android.App;
using Android.Views;
using Android.Widget;
using CarBrands.Models;

namespace CarBrands
{
    public class CarAdapter : BaseAdapter<Car>
    {
        private List<Car> _items;
        private Activity _context;

        public CarAdapter(Activity context, List<Car> items)
        {
            _items = items;
            _context = context;
        }

        public override Car this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            
            if (view != null) return view;
            if (_context.LayoutInflater != null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.car_row_layout, null);
            
            view.FindViewById<TextView>(Resource.Id.manufacturerTextView).Text = _items[position].Manufacturer;
            view.FindViewById<TextView>(Resource.Id.modelTextView).Text = _items[position].Model;
            view.FindViewById<TextView>(Resource.Id.kWTextView).Text = _items[position].kW.ToString();
            
            return view;
        }
    }
}