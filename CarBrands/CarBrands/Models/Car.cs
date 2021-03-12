namespace CarBrands.Models
{
    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        
        public int Year { get; set; }
        // ReSharper disable once InconsistentNaming
        public int kW { get; set; }
    }
}