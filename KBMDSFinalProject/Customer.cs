using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBMDSFinalProject
{
    public class Customer
    {
        //Defining all the Variables in the Customer Class.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        //Below is unused code as a shortcut altertive of calling all the variables seprately by calling one variable.
        public string EntireCustomerEntry => $"{FirstName?.Trim()} {LastName?.Trim()}'s wants to be Emailed to: {Email?.Trim()} #{Id}";

    }
}
