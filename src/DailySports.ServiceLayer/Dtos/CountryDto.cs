using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class CountryDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public CountryDto() { }
        public CountryDto(Country ct)
        {
            Code = ct.Code;
            Name = ct.Name;
            Flag = ct.Flag;
        }
    }
}
