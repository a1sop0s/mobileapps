using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace StarwarsApp.Adapters
{
    public class PeopleAdapter : BaseAdapter<PeopleDetails>
    {
        List<PeopleDetails> _items;
        Activity _context;
        public PeopleAdapter(Activity context, List<PeopleDetails> items)
        {
            _items = items;
            _context = context;
        }

        public override PeopleDetails this[int position]
        {
            get { return _items[position]; }
        }
        public override int Count
        {
            get { return _items.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.people_row_layout, null);
                view.FindViewById<TextView>(Resource.Id.nameTextView).Text = _items[position].name;
                view.FindViewById<TextView>(Resource.Id.ageTextView).Text = _items[position].birth_year;
                return view;
            }
        }
    }
}