using System;
using Library.Appointments;

namespace Library.Doctors
{
    public class Doctor
    {
        public string firstName {get; set;} = string.Empty;
        public string lastName {get; set;} = string.Empty;
        public int licenseNumber {get; set;}
        public DateTime graduationDate {get; set;} = DateTime.Now;
        public List<string> specializations = new List<string>();
        public List<List<int>> appointments {get; set;} = new List<List<int>>();

    }

}

