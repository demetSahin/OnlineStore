using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserTypeEnum UserType { get; set; }
        public List<ProductLikeDto>? ProductLikeDtos { get; set; }
        public List<CommentDto>? CommentDtos { get; set; }
    }
}
