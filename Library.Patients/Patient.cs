using System;
using Library.Appointments;

namespace Library.Patients
{
    public class Patient
    {
        public string firstName {get; set;} = string.Empty;
        public string lastName {get; set;} = string.Empty;
        public string address {get; set;} = string.Empty;
        public string race {get; set;} = string.Empty;
        public string gender {get; set;} = string.Empty;
        public string diagnoses {get; set;} = string.Empty;
        public List<string> prescriptions {get; set;} = new List<string>();
        public List<List<int>> appointments {get; set;} = new List<List<int>>();
    }

}