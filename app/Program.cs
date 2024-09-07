using Library.Patients;
using Library.Doctors;
using Library.Appointments;
using Library.Practice;
using System;

namespace app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Office office = new Office();
            office.runConsole();
        }
    }
}