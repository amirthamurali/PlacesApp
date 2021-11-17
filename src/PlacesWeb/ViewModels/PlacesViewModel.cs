using System.Collections.Generic;
using PlacesWeb.Models;

namespace PlacesWeb.ViewModels
{
    public class PlacesViewModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Cities { get; set; }

        public PlaceOnEarth placeOnEarth { get; set; }

        public PlacesViewModel()
        {
            Countries = new List<Country>();
        }
    }
}