using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace StarwarsApp.Adapters
{
    public class PeopleAdapter : BaseAdapter<PeopleDetails>
    {
        private readonly List<PeopleDetails> _items;
        private readonly Activity _context;
        public PeopleAdapter(Activity context, List<PeopleDetails> items)
        {
            _items = items;
            _context = context;
        }

        public override PeopleDetails this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            
            // ReSharper disable once InvertIf
            if (view == null)
            {
                view = _context.LayoutInflater?.Inflate(Resource.Layout.people_layout, null);
                view?.FindViewById<TextView>(Resource.Id.nameTextView).Text = _items[position].Name;
                view?.FindViewById<TextView>(Resource.Id.ageTextView).Text = _items[position].BirthYear;
            }
            return view;
        }
    }
}