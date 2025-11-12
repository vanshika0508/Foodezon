using System.Collections.Generic;
using System.ComponentModel;

namespace Foodezon.Core.Models
{
    public  class User : Base
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        

        public UserRole Role { get; set; } = UserRole.Customer;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual Cart Cart { get; set; } = new Cart();


    }
    public enum UserRole
    {
        Customer,

        Admin

    }
}