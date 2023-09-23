CREATE DATABASE Progetto_Libreria;

USE Progetto_Libreria;

CREATE TABLE Utenti (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(50),
	email VARCHAR(255),
	pass VARCHAR(64),
	ruolo VARCHAR(150)
);

CREATE TABLE Libri (
	id INT PRIMARY KEY IDENTITY(1,1),
	titolo VARCHAR(255),
	autore VARCHAR(200),
	genere VARCHAR(100),
	quantita INT,
	formato BIT,
	nomeFile VARCHAR(255)
);

CREATE TABLE Utenti_Libri (
	id INT PRIMARY KEY IDENTITY(1,1),
	idUtente INT,
	idLibro INT,

	FOREIGN KEY (idUtente) REFERENCES Utenti(id) ON UPDATE SET NULL ON DELETE SET NULL,
	FOREIGN KEY (idLibro) REFERENCES Libri(id) ON UPDATE SET NULL ON DELETE SET NULL
);

INSERT INTO Utenti (nome, email, pass, ruolo)
VALUES
('Alice', 'alice.benedetti@gmail.com', HASHBYTES('Sha2_512', N'hashed_password_1'), 'Dipendente'),
('Bob', 'bob.marley@hotmail.com', HASHBYTES('Sha2_512', N'hashed_password_2'), 'Amministratore'),
('Charlie', 'charlieedison@gmail.com', HASHBYTES('Sha2_512', N'hashed_password_3'), 'Dipendente');

INSERT INTO Libri (titolo, autore, genere, quantita, formato, nomeFile)
VALUES
('Il Signore degli Anelli', 'J.R.R. Tolkien', 'Fantasy', 10, 0, 'signore_degli_anelli.pdf'),
('1984', 'George Orwell', 'Distopia', 5, 1, '1984.pdf'),
('Harry Potter e la Pietra Filosofale', 'J.K. Rowling', 'Fantasy', 7, 0, 'harry_potter.pdf');

INSERT INTO Utenti_Libri (idUtente, idLibro)
VALUES
(1, 1),
(1, 2),
(2, 1),
(3, 3);

DROP DATABASE Progetto_Libreria;

SELECT * FROM Utenti;
SELECT * FROM Libri;
SELECT * FROM Utenti_Libri;

DECLARE @titolo VARCHAR(255) = 'signore';
SELECT * FROM Libri WHERE titolo LIKE '%' + @titolo + '%';


DECLARE @email NVARCHAR(15) = 'ddma@outlook.it';
DECLARE @pass NVARCHAR(4000) ='Ciao1234';
SELECT TOP 1 * FROM Utenti
WHERE email = @email
AND pass = HASHBYTES('SHA2_512', @pass);
