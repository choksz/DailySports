using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Comments> _commentRepository;
        private IGenericRepository<News> _newsRepository;
        private IGenericRepository<User> _userRepository;
        public CommentService(IUnitOfWork unitOfWork,IGenericRepository<Comments> commentRepository,IGenericRepository<News> newsRepository,IGenericRepository<User> userRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            
        }
        public bool AddComment(CommentsDto Comment)
        {
            try
            {
                Comments newComment = new Comments();
                newComment.Description = Comment.Description;
               
                newComment.UserId = Comment.UserId;
                newComment.user = _userRepository.FindBy(U => U.Id == Comment.UserId).FirstOrDefault();
                _commentRepository.Add(newComment);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //public List<CommentsDto> AllNewsComments(int NewsId)
        //{
        //   try
        //    {
        //        List<Comments> CommentsList = _commentRepository.FindBy(C => C.NewsId == NewsId).ToList();
        //        List<CommentsDto> CommentDtoList = new List<CommentsDto>();
        //        foreach(var comment in CommentsList)
        //        {
        //            CommentDtoList.Add(new CommentsDto
        //            {
        //                Id=comment.Id,
        //                Description=comment.Description
                      
        //            });
        //        }
        //        return CommentDtoList;
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public bool DeleteComment(int Id)
        {
           try
            {
                Comments comment = _commentRepository.FindBy(C => C.Id == Id).FirstOrDefault();
                _commentRepository.Delete(comment);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
