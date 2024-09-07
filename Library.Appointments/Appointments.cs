using System;

namespace Library.Appointments
{
    public class AppointmentProxy
    {

        //static List<List<int>> globalAppointments = new List<List<int>>();

        //Returns true if time passed in overlaps with existing apppointment
        /*Each appointment in the list is structured as two 5-digit ints where the first digit (1-5) is the day, 
         the next 2 digits represent hours (military time) and the last 2 represent minutes.
         Array of pairs such that: [startTime, endTime]*/
        //Assumes appointments are sorted
        public static bool isOverlap(List<List<int>> appointments, int startTime, int endTime){
            foreach(var app in appointments){
                if( !( (startTime < app[1] && endTime < app[0]) || (app[0] < endTime && app[1] < startTime)
                    || (app[0] < endTime && app[1] < startTime) || (startTime < app[1] && endTime < app[0]) ) )
                return true;
            }
            return false;
        }

        //Checks whether appointment was scheduled on Mon-Fri, 8-5, on the same day
        public static bool isValid(int startTime, int endTime){
            if(startTime < 10000 || startTime >= 60000|| endTime < 10000 || endTime >= 60000)
                return false;
            if(startTime % 10000 < 800 || startTime % 10000 >= 1700 || endTime % 10000 < 800 || endTime % 10000 >= 1700)
                return false;
            if(Math.Abs(startTime - endTime) >= 10000)
                return false;
            if(endTime < startTime)
                return false;
            return true;
        }

        
    }

}