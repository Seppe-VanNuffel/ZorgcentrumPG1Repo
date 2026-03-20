DROP TABLE IF EXISTS Consultaties;
DROP TABLE IF EXISTS Controles;
DROP TABLE IF EXISTS Operaties;
DROP TABLE IF EXISTS Afspraken;
DROP TABLE IF EXISTS Artsen;
DROP TABLE IF EXISTS Patienten;
DROP TABLE IF EXISTS Personen;

CREATE TABLE Personen(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,
	Naam VARCHAR(100) NOT NULL,
	Voornaam VARCHAR(100) NOT NULL
);

CREATE TABLE Artsen(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,
	RIZIV VARCHAR(50),
	Specialisatie VARCHAR(100),
	FOREIGN KEY (RijksregisterNummer) REFERENCES Personen(RijksregisterNummer)
);

CREATE TABLE Patienten(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,
	FOREIGN KEY (RijksregisterNummer) REFERENCES Personen(RijksregisterNummer)
);


CREATE TABLE Afspraken(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Type VARCHAR(20) NOT NULL CHECK (Type IN ('Operatie', 'Controle', 'Consultatie')),
	AfdelingCode VARCHAR(50),
	DatumTijdStart DATETIME NOT NULL,
	Duurmin INT NOT NULL,
	RijksregisterNrArts VARCHAR(20) NOT NULL,
	RijksregisterNrPatient VARCHAR(20) NOT NULL,
	FOREIGN KEY (RijksregisterNrArts)	 REFERENCES Artsen(RijksregisterNummer),
	FOREIGN KEY (RijksregisterNrPatient) REFERENCES Patienten(RijksregisterNummer)
);


CREATE TABLE Operaties(
	Id INT PRIMARY KEY,
	AantalMinutenOpVoorhand INT,
	Beschrijving NVARCHAR(MAX),
	FOREIGN KEY (Id) REFERENCES Afspraken(Id) ON DELETE CASCADE
);


CREATE TABLE Controles(
	Id INT PRIMARY KEY,
	Referentie INT NULL,
	FOREIGN KEY (Id) REFERENCES Afspraken(Id) ON DELETE CASCADE,
	FOREIGN KEY (Referentie) REFERENCES Afspraken(Id) ON DELETE NO ACTION
);


CREATE TABLE Consultaties(
	Id INT PRIMARY KEY,
	FOREIGN KEY (Id) REFERENCES Afspraken(Id) ON DELETE CASCADE
);