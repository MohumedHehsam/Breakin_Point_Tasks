---
title: Hospital System Class Diagram
---
classDiagram
    class Person {
        <<abstract>>
        +int id
        +string name
        +Person(int id, string name)
    }

    class Doctor {
        +DoctorSpecialization specialization
        +Doctor(int id, string name, DoctorSpecialization specialization)
    }

    class Patient {
        +int age
        +Patient(int id, string name, int age)
    }

    class Appointment {
        +int id
        +Patient patient
        +Doctor doctor
        +AppointmentStatus status
        +Appointment(int id, Patient patient, Doctor doctor)
        +UpdateStatus(newStatus: AppointmentStatus)
        +ToString() string
    }

    class Hospital {
        -int _nextPatientId
        -int _nextDoctorId
        -int _nextAppointmentId
        -Hospital hospital$
        +List~Doctor~ doctors
        +List~Appointment~ appointments
        +List~Patient~ patients
        +Hospital GetInstance$
        -Hospital()
        +AddPatient(string name, int age)
        +AddDoctor(string name, DoctorSpecialization specialization)
        +MakeAppointment(int doctorId, int patientId)
        +UpdateAppointmentState(int id, AppointmentStatus newStatus)
        +DeleteAppointment(int id)
        +SearchPatientName(string name) List~Patient~
    }

    class AppointmentStatus {
        <<enumeration>>
        Pending
        Confirmed
        Completed
    }

    class DoctorSpecialization {
        <<enumeration>>
        dermatology
        psychiatry
        children
        Neurosurgery
    }

    %% Inheritance
    Person <|-- Doctor
    Person <|-- Patient
    
    %% Composition 
    Hospital *-- Doctor : contains
    Hospital *-- Patient : contains
    Hospital *-- Appointment : manages
    
    %% Associations
    Appointment --> Doctor : assigned to
    Appointment --> Patient : involves
    
    %% Enums
    Doctor --> DoctorSpecialization : has
    Appointment --> AppointmentStatus : tracks