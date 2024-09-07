using System;
using System.Globalization;
using Library.Doctors;
using Library.Patients;
using Library.Appointments;

namespace Library.Practice{

    public class Office{
        private List<Doctor> doctors = new List<Doctor>();
        private List<Patient> patients = new List<Patient>();

        public void runConsole(){
            
            while(true){
                int choice;
                Console.Write("[1] Create Patient\n[2] Create Doctor\n[3] Create Appointment\n[4] See all patients\n[5] See all doctors\n[6] Exit Program\n");
                choice = Convert.ToInt32(Console.ReadLine());

                switch(choice){
                    case 1:
                        createPatient();
                        break;
                    case 2:
                        createDoctor();
                        break;
                    case 3:
                        createAppointment();
                        break;
                    case 4:
                        listPatients();
                        break;
                    case 5:
                        listDoctors();
                        break;
                    case 6:
                        Console.Write("Goodbye!\n");
                        return;
                    default:
                        break;                        

                }
            }

        }
        private void createDoctor(){
            
            try{ 

                Doctor newDoc = new Doctor();
                Console.Write("Enter doctor's first name:\n");
                newDoc.firstName = Console.ReadLine();
                Console.Write("Enter doctor's last name:\n");
                newDoc.lastName = Console.ReadLine();
                Console.Write("Enter doctor's license number:\n");
                newDoc.licenseNumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter doctor's graduation date in the format 'yyyy-MM-dd HH:mm':\n");
                
                string dateString = string.Empty;
                dateString = Console.ReadLine();

                newDoc.graduationDate = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                Console.Write("How many specializations does this doctor have?\n");
                int numSpecializations = Convert.ToInt32(Console.ReadLine());
                
                while(numSpecializations-- > 0){
                    Console.Write("Input specialization below:\n");
                    newDoc.specializations.Add(Console.ReadLine());
                }
                doctors.Add(newDoc);
                Console.Write("Doctor creation completed!\n");
            

            } catch(Exception e){
            Console.Write("Oops - you've inputted something wrong. Try again!\n");
            }

        }

        private void createPatient(){
            try{
                Patient newPatient = new Patient();
                Console.Write("Enter patient's first name:\n");
                newPatient.firstName = Console.ReadLine();
                Console.Write("Enter patients's last name:\n");
                newPatient.lastName = Console.ReadLine();
                Console.Write("Enter patients's address:\n");
                newPatient.address = Console.ReadLine();
                Console.Write("Enter patients's race:\n");
                newPatient.race = Console.ReadLine();
                Console.Write("Enter patients's gender:\n");
                newPatient.gender = Console.ReadLine();
                Console.Write("Enter patients's diagnoses:\n");
                newPatient.diagnoses = Console.ReadLine();

                Console.Write("How many prescriptions does this patient have?\n");
                int numPrescriptions = Convert.ToInt32(Console.ReadLine());
                
                while(numPrescriptions-- > 0){
                    Console.Write("Input prescription below:\n");
                    newPatient.prescriptions.Add(Console.ReadLine());
                }

                patients.Add(newPatient);
                Console.Write("Patient creation completed!\n");

            } catch(Exception e) {
                Console.Write("Oops - you've inputted something wrong. Try again!\n");
            }
            
        }

        private void createAppointment(){

            int docIndex=0;
            int patientIndex=0;

            Console.Write("Choose a doctor to create an appointment:\n");
            listDoctors();
            docIndex = Convert.ToInt32(Console.ReadLine());
            Console.Write("Now choose a patient:\n");
            listPatients();
            patientIndex = Convert.ToInt32(Console.ReadLine());
            
            Doctor tempDoc = new Doctor();
            Patient tempPatient = new Patient();

            try {
                docIndex--;
                patientIndex--;
                tempDoc = doctors[docIndex];
                tempPatient = patients[patientIndex];
            } catch(Exception e){
                Console.Write("You picked an invalid doctor or patient number!\n");
                return;
            }

            int startDate;
            int endDate;

            Console.Write("Now select a start and end date for your appointment. Enter a 5-digit integer such that:\n- The first digit represents the weekday (1=Monday, 2=Tuesday...)\n- The next 2 digits represent the hour in military time (20=8PM)\n- The last 2 digits represent minutes\n");
            do{
                Console.Write("\nEnter a start date:\n");
                startDate = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter an end date:\n");
                endDate = Convert.ToInt32(Console.ReadLine());

                if(!AppointmentProxy.isValid(startDate, endDate)){
                    Console.Write("One or more of the dates you've inputted is invalid!\n");
                }
            }
            while(!AppointmentProxy.isValid(startDate, endDate));

            if(!AppointmentProxy.isOverlap(tempDoc.appointments, startDate, endDate)){
                //Inserting new appointment at right index
                int insertIndex = 0;
                for(int i =0; i < tempDoc.appointments.Count(); i++){
                    if(tempDoc.appointments[insertIndex][0] > startDate){
                        insertIndex = insertIndex;
                        break;
                    }
                }
                doctors[docIndex].appointments.Insert(insertIndex, [startDate, endDate]);
                patients[patientIndex].appointments.Insert(insertIndex, [startDate, endDate]);
                
            } else {
                Console.Write("*** This appointment overlaps with another appointment! ***\n");
            }
        }

        private void listPatients(){
            int it = 1;
            foreach(var patient in patients){
                Console.WriteLine($"[{it++}] - {patient.firstName} {patient.lastName}\n\tAddress: {patient.address}\n\tRace: {patient.race}\n\t");
                Console.WriteLine($"\tGender: {patient.gender}\n\tDiagnoses: {patient.diagnoses}\n\tPrescriptions:");
                foreach(var pres in patient.prescriptions){
                    Console.Write($"\t\t{pres}\n");
                }
                Console.Write("\tAppointments:\n");
                foreach(var appt in patient.appointments){
                    Console.Write($"\t\t{appt[0]} - {appt[1]}, ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private void listDoctors(){
            int it = 1;
            foreach(var doctor in doctors){
                Console.WriteLine($"[{it++}] - {doctor.firstName} {doctor.lastName}\n\tLicense number: {doctor.licenseNumber}\n\tGraduation date: {doctor.graduationDate}\n\tSpecializations:");
                foreach(var spec in doctor.specializations){
                    Console.Write($"\t\t{spec}\n");
                }
                Console.Write("\tAppointments:\n");
                foreach(var appt in doctor.appointments){
                    Console.Write($"\t\t{appt[0]} - {appt[1]}, ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }


    }
}