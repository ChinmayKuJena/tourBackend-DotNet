namespace Service1.Models
{
    public class DetectedLabel
    {
        public string Name { get; set; }
        public float Percentage { get; set; }

        public DetectedLabel(string name, float percentage)
        {
            Name = name;
            Percentage = percentage;
        }
    }
}
