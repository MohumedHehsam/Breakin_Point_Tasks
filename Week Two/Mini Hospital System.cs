static void Main(string[] args)
{
    var hospital = Hospital.GetInstance;

    // Seed some test data
    hospital.AddDoctor("Dr. Gregory House", DoctorSpecialization.psychiatry);
    hospital.AddPatient("John Doe", 35);

    Console.WriteLine("Hospital System Initialized.");

    bool isRunning = true;

    while (isRunning)
    {
        Console.WriteLine("\n=== Hospital Menu ===");
        Console.WriteLine("1. Add Patient");
        Console.WriteLine("2. Make Appointment");
        Console.WriteLine("3. Update Appointment Status");
        Console.WriteLine("4. View All Appointments");
        Console.WriteLine("5. View All Patients");
        Console.WriteLine("6. View All Doctors");
        Console.WriteLine("7. Exit");
        Console.Write("Select an option: ");

        string choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Enter patient name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter patient age: ");
                    int age = int.Parse(Console.ReadLine()); // Throws FormatException if not a number

                    hospital.AddPatient(name, age);
                    Console.WriteLine("SUCCESS: Patient added.");
                    break;

                case "2":
                    Console.Write("Enter Doctor ID: ");
                    int docId = int.Parse(Console.ReadLine());

                    Console.Write("Enter Patient ID: ");
                    int patId = int.Parse(Console.ReadLine());

                    hospital.MakeAppointment(docId, patId);
                    Console.WriteLine("SUCCESS: Appointment created.");
                    break;

                case "3":
                    Console.Write("Enter Appointment ID: ");
                    int appId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Select Status (0: Pending, 1: Confirmed, 2: Completed): ");
                    AppointmentStatus status = (AppointmentStatus)int.Parse(Console.ReadLine());

                    hospital.UpdateAppointmentState(appId, status);
                    Console.WriteLine("SUCCESS: Status updated.");
                    break;

                case "4":
                    Console.WriteLine("\n--- Current Appointments ---");
                    if (hospital.appointments.Any())
                    {
                        foreach (var app in hospital.appointments)
                            System.Console.WriteLine(app);
                    }
                    {
                        Console.WriteLine("No appointments found.");
                    }
                    break;
                case "5":
                    Console.WriteLine("\n--- Current Appointments ---");
                    if (hospital.patients.Any())
                    {
                        foreach (var patient in hospital.patients)
                            System.Console.WriteLine(patient);
                    }
                    {
                        Console.WriteLine("No patients found.");
                    }
                    break;
                case "6":
                    Console.WriteLine("\n--- Current Appointments ---");
                    if (hospital.doctors.Any())
                    {
                        foreach (var doc in hospital.doctors)
                            System.Console.WriteLine(doc);
                    }
                    {
                        Console.WriteLine("No doctors found.");
                    }
                    break;

                case "7":
                    isRunning = false;
                    Console.WriteLine("Shutting down...");
                    break;

                default:
                    Console.WriteLine("Invalid selection. Try again.");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("ERROR: Invalid input format. Please enter numbers where expected.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}










abstract class Person
{
    public int id { get; private set; }
    public string name { get; private set; }
    public Person(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
class Doctor : Person
{
    public DoctorSpecialization specialization { get; private set; }
    public Doctor(int id, string name, DoctorSpecialization specialization) : base(id, name)
    {
        this.specialization = specialization;
    }
}
class Patient : Person
{
    public int age { get; private set; }
    public Patient(int id, string name, int age) : base(id, name)
    {
        if (age <= 0)
            throw new ArgumentException("Age must be positive");

        this.age = age;
    }
}

class Appointment
{
    public int id { get; private set; }
    public Patient patient { get; private set; }
    public Doctor doctor { get; private set; }
    public AppointmentStatus status { get; private set; }
    public Appointment(int id, Patient patient, Doctor doctor)
    {
        if (patient is null || doctor is null)
            throw new ArgumentException("No Doctor or No Patient was provided");

        this.id = id;
        this.patient = patient;
        this.doctor = doctor;
        this.status = AppointmentStatus.Pending;
    }

    public void UpdateStatus(AppointmentStatus newStatus)
    {
        if (this.status > newStatus)
            throw new ArgumentException($"can not change status from {this.status.ToString()} to {newStatus.ToString()}");
        this.status = newStatus;
    }
    public override string ToString()
    {
        return $"Appt #{id} | Doctor: {doctor.name} | Patient: {patient.name} | Status: {status}";
    }
}

class Hospital
{
    private int _nextPatientId = 1;
    private int _nextDoctorId = 1;
    private int _nextAppointmentId = 1;
    public List<Doctor> doctors { get; private set; }
    public List<Appointment> appointments { get; private set; }
    public List<Patient> patients { get; private set; }

    static Hospital hospital { get; set; }

    private Hospital()
    {
        doctors = new List<Doctor>();
        appointments = new List<Appointment>();
        patients = new List<Patient>();
    }

    public static Hospital GetInstance
    {
        get
        {
            if (Hospital.hospital is null)
            {
                Hospital.hospital = new Hospital();
            }
            return Hospital.hospital;
        }
    }

    public void AddPatient(string name, int age)
    {
        var newPatientId = _nextPatientId + 1;
        var newPatient = new Patient(newPatientId, name, age);
        patients.Add(newPatient);
        _nextPatientId++;
    }
    public void AddDoctor(string name, DoctorSpecialization specialization)
    {
        var newDoctorId = _nextDoctorId + 1;
        var newDoctor = new Doctor(newDoctorId, name, specialization);
        doctors.Add(newDoctor);
        _nextDoctorId++;
    }

    public void MakeAppointment(int doctorId, int patientId)
    {
        var newAppointmentId = _nextAppointmentId + 1;
        var patient = patients.FirstOrDefault(x => x.id == patientId);
        var doctor = doctors.FirstOrDefault(x => x.id == doctorId);

        var newAppointment = new Appointment(newAppointmentId, patient, doctor);
        appointments.Add(newAppointment);
        _nextAppointmentId++;
    }

    public void UpdateAppointmentState(int id, AppointmentStatus newStatus)
    {
        var appointment = appointments.FirstOrDefault(x => x.id == id);
        if (appointment == default)
            throw new ArgumentException("Appointment with this id doesn't exist");

        appointment.UpdateStatus(newStatus);
    }

    public void DeleteAppointment(int id)
    {

        var appointment = appointments.FirstOrDefault(x => x.id == id);
        if (appointment == default)
            throw new ArgumentException("Appointment with this id doesn't exist");

        appointments.Remove(appointment);
    }
    public List<Patient> SearchPatientName(string name)
    {
        return patients.Where(x => x.name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

}


enum AppointmentStatus
{
    Pending,
    Confirmed,
    Completed
}

enum DoctorSpecialization
{
    dermatology,
    psychiatry,
    children,
    Neurosurgery
}