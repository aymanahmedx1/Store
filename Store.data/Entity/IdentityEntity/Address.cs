﻿using System.ComponentModel.DataAnnotations;

namespace Store.data.Entity.IdentityEntity
{
    public class Address
    {
        public long     Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}