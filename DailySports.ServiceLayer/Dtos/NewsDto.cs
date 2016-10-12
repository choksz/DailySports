using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public  class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBigraphy { get; set; }
        public string NewsImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public GameDto Game { get; set; }
        public List<PetOfTheWeekDto> PetOfTheDate { get; set; }
        public List<MatchDto> NextMatches { get; set; }
    }
}
