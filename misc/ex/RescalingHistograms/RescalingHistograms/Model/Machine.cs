namespace RescalingHistograms.Model
{
    public class Machine
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public Machine()
        {

        }

        public Machine(string model, string manufacturer)
        {
            Model = model;
            Manufacturer = manufacturer;
        }
    }
}