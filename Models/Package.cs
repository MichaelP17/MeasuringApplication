namespace MeasuringApplication.Models
{
    public class Package
    {
        public string Station { get; set; } = string.Empty;

        public List<MeasureValueSourceFlatModel> MeasureValueSources { get; set; } = new List<MeasureValueSourceFlatModel>();
    }
}
