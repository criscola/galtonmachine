namespace RescalingHistograms.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public string Occupation { get; set; }

        public Employee()
        {

        }

        public Employee(string name, string occupation)
        {
            Name = name;
            Occupation = occupation;
        }
    }
}