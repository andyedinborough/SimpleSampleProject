using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc3Razor.Models {
    public class ContactModel {

        [Required]
        [Key]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "User Name")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed")]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required()]
        public string Phone { get; set; }

    }

    public class ContactsDbContext : DbContext
    {
        public DbSet<ContactModel> Contacts { get; set; }

        //public Contacts()
        //{
        //    contactList.Add(new ContactModel
        //    {
        //        UserName = "BenM",
        //        FirstName = "Ben",
        //        LastName = "Miller",
        //        Phone = "555-555-5555"
        //    });
        //    contactList.Add(new ContactModel
        //    {
        //        UserName = "AnnB",
        //        FirstName = "Ann",
        //        LastName = "Beebe",
        //        Phone = "666-666-6666"
        //    });
        //}

        public List<ContactModel> contactList = new List<ContactModel>();

        public void Update(ContactModel contactModel) {

            foreach (ContactModel cm in Contacts) {
                if (cm.UserName == contactModel.UserName) {
                    Contacts.Remove(cm);
                    Contacts.Add(contactModel);
                    break;
                }
            }
        }

        public void Create(ContactModel contactModel) {
            foreach (ContactModel cm in Contacts)
            {
                if (cm.UserName == contactModel.UserName) {
                    throw new System.InvalidOperationException("Username already exists: " + cm.UserName);
                }
            }
            contactList.Add(contactModel);
        }

        public void Remove(string userName) {

            foreach (ContactModel cm in Contacts)
            {
                if (cm.UserName == userName)
                {
                    contactList.Remove(cm);
                    break;
                }
            }
        }

        public ContactModel GetContact(string userName) {
            ContactModel contactModel = null;

            foreach (ContactModel cm in Contacts)
                if (cm.UserName == userName)
                    contactModel = cm;

            return contactModel;
        }

    }
}