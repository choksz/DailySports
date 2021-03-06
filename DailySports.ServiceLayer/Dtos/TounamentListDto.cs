﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class TounamentListDto
    {
        public List<TournementsDto> AllTournaments { get; set; }
        public List<TournementsDto> LatestTournament { get; set; }
        public List<GameDto> AllGames { get; set; }
        public List<NewsDto> AllNews { get; set; }
    }
}
