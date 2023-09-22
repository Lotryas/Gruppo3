create database Progetto_Libreria
use Progetto_Libreria

Create table Utenti (
    id INT PRIMARY KEY IDENTITY(1,1),
    nome VARCHAR(50),
    pass VARCHAR(64),
    ruolo VARCHAR(150)
);

Create table Libri (
    id INT PRIMARY KEY IDENTITY(1,1),
    titolo VARCHAR(255),
    autore VARCHAR(200),
    genere VARCHAR(100),
    quantita INT,
    formato BIT,
    nomeFile VARCHAR(255)
);

Create table Utenti_Libri (
    id INT PRIMARY KEY IDENTITY(1,1),
    idUtente INT,
    idLibro INT,

    FOREIGN KEY (idUtente) REFERENCES Utenti(id) ON UPDATE SET NULL ON DELETE SET NULL,
    FOREIGN KEY (idLibro) REFERENCES Libri(id) ON UPDATE SET NULL ON DELETE SET NULL
);

INSERT INTO Utenti (nome, pass, ruolo)
VALUES
    ('Alice', HASHBYTES('SHA2_512', N'hashed_password_1'), 'Amministratore'),
    ('Bob',HASHBYTES('SHA2_512', N'hashed_password_2'), 'Dipendente'),
    ('Charlie', HASHBYTES('SHA2_512', N'hashed_password_3'), 'Dipendente');

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

select * from Utenti
select * from Libri
select * from Utenti_Libri

drop table Utenti
drop table Libri
drop table Utenti_Libri
