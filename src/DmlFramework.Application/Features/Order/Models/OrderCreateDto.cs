using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.Order.Models
{
    public class OrderCreateDto
    {
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
