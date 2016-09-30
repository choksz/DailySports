using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.IServices
{
   public interface ICommentService : IDisposable
    {
        bool AddComment(CommentsDto Comment);
        bool DeleteComment(int Id);
      //  List<CommentsDto> AllNewsComments(int NewsId);
    }
}
