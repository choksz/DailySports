using DailySports.ServiceLayer.Dtos;
using System;

namespace DailySports.ServiceLayer.IServices
{
    public interface ICommentService : IDisposable
    {
        bool AddComment(CommentsDto Comment);
        bool DeleteComment(int Id);
      //  List<CommentsDto> AllNewsComments(int NewsId);
    }
}
