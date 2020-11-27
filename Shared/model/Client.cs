using System;

namespace Shared.model
{
    public class Client
    {
        public int Id { get; set; }
        public string ForName { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Nbr { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string IdCarNbr { get; set; }
        public DateTime BirthDate { get; set; }
        public string DrivingLicence { get; set; }
    }
}
