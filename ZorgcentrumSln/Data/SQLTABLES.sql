DROP TABLE IF EXISTS Afdelingen;
DROP TABLE IF EXISTS Specialisaties;
DROP TABLE IF EXISTS PatientenAfdelingen;
DROP TABLE IF EXISTS ArtsenAfdelingen;
DROP TABLE IF EXISTS Consultaties;
DROP TABLE IF EXISTS Controles;
DROP TABLE IF EXISTS Operaties;
DROP TABLE IF EXISTS Afspraken;
DROP TABLE IF EXISTS Artsen;
DROP TABLE IF EXISTS Patienten;
DROP TABLE IF EXISTS Personen;

-- Make the tables

CREATE TABLE Afdelingen(
	Code VARCHAR(5) PRIMARY KEY NOT NULL,
	Naam VARCHAR(50) NOT NULL
);

CREATE TABLE Specialisaties(
	Code VARCHAR(5) PRIMARY KEY NOT NULL,
	Naam VARCHAR(50) NOT NULL
);

CREATE TABLE Personen(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,
	Naam VARCHAR(100) NOT NULL,
	Voornaam VARCHAR(100) NOT NULL
);

CREATE TABLE Artsen(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,
	RIZIV VARCHAR(50),
	SpecialisatieCode VARCHAR(5),

	FOREIGN KEY (RijksregisterNummer) REFERENCES Personen(RijksregisterNummer),
	FOREIGN KEY (SpecialisatieCode) REFERENCES Specialisaties(Code)
);

CREATE TABLE Patienten(
	RijksregisterNummer VARCHAR(20) PRIMARY KEY,

	FOREIGN KEY (RijksregisterNummer) REFERENCES Personen(RijksregisterNummer)
);

CREATE TABLE PatientenAfdelingen(
	RijksregisterNummer VARCHAR(20) NOT NULL,
	AfdelingCode VARCHAR(5) NOT NULL,

	PRIMARY KEY(RijksregisterNummer, AfdelingCode),

	FOREIGN KEY (RijksregisterNummer) REFERENCES Patienten(RijksregisterNummer),
	FOREIGN KEY (AfdelingCode) REFERENCES Afdelingen(Code),
);

CREATE TABLE ArtsenAfdelingen(
	RijksregisterNummer VARCHAR(20) NOT NULL,
	AfdelingCode VARCHAR(5) NOT NULL,

	PRIMARY KEY(RijksregisterNummer, AfdelingCode),

	FOREIGN KEY (RijksregisterNummer) REFERENCES Artsen(RijksregisterNummer),
	FOREIGN KEY (AfdelingCode) REFERENCES Afdelingen(Code),
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

-- Fill the Afdelingen en Specialisaties table

INSERT INTO Specialisaties (Code, Naam)
VALUES 
('ANES', 'Anesthesie‑reanimatie'),
('BIOL', 'Klinische biologie'),
('CARD', 'Cardiologie'),
('CHIR', 'Heelkunde'),
('DERM', 'Dermatologie'),
('ENDO', 'Endocrino‑diabetologie'),
('GAST', 'Gastro‑enterologie'),
('GERC', 'Geriatrie'),
('GYNA', 'Gynaecologie & verloskunde'),
('HEMA', 'Klinische hematologie'),
('NCHI', 'Neurochirurgie'),
('NEFR', 'Nefrologie'),
('NEUR', 'Neurologie'),
('NUCL', 'Nucleaire geneeskunde'),
('ONCO', 'Medische oncologie'),
('ORL', 'Oto-Rhino-Laryngologie'),
('OPHT', 'Oftalmologie'),
('ORTH', 'Orthopedische heelkunde'),
('PAAN', 'Pathologische anatomie'),
('PEDI', 'Kindergeneeskunde'),
('PNEU', 'Pneumologie'),
('PSYC', 'Psychiatrie'),
('RADO', 'Radiotherapie‑oncologie'),
('REUM', 'Reumatologie'),
('REVA', 'Fysische geneeskunde & revalidatie'),
('RXDI', 'Röntgendiagnose'),
('SLAP', 'Slaapgeneeskunde'),
('STOM', 'Stomatologie'),
('URGE', 'Urgentiegeneeskunde'),
('UROL', 'Urologie');

INSERT INTO Afdelingen (Code, Naam)
VALUES 
('PNEC', 'Pneumologie – Slaapcentrum'),
('CARD', 'Cardiologie‑afdeling'),
('GAST', 'Gastro‑enterologie afdeling'),
('NEUR', 'Neurologie afdeling'),
('ORTH', 'Orthopedie afdeling'),
('GERC', 'Geriatrie afdeling'),
('ONCO', 'Oncologie afdeling'),
('PAAN', 'Pathologische anatomie'),
('RADI', 'Radiologie / MRI / CT'),
('URGE', 'Spoed & urgentiegeneeskunde');