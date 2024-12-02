using OnlineStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.Dtos
{
    public class OrderAddDto
    {
        public int? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Fullname { get; set; }
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }
}
