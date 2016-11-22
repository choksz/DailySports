using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class LanguageDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public LanguageDto() { }
        public LanguageDto(Language lang)
        {
            Code = lang.Code;
            Name = lang.Name;
            Flag = lang.Flag;
        }
    }
}
