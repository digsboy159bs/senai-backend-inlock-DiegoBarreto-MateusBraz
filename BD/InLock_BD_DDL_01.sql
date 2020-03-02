CREATE DATABASE InLock_Games_Tarde;

USE InLock_Games_Tarde;

CREATE TABLE Estudios (
IdEstudio		INT PRIMARY KEY IDENTITY,
NomeEstudio		VARCHAR (255),
);

CREATE TABLE Jogos(
IdJogo			INT PRIMARY KEY IDENTITY,
NomeJogo		VARCHAR(255),
Descricao		VARCHAR(255),
DataLancamento	DATETIME,
Valor			INT,
IdEstudio		INT FOREIGN KEY REFERENCES Estudios(IdEstudio)
);

CREATE TABLE TipoUsuario(
IdTipoUsuario		INT PRIMARY KEY IDENTITY,
Titulo				VARCHAR(255)
);

CREATE TABLE Usuarios(
IdUsuario			INT PRIMARY KEY IDENTITY,
Email				VARCHAR (255),
Senha				VARCHAR(255),
IdTipoUsuario		INT FOREIGN KEY REFERENCES TipoUsuario(IdTipoUsuario)
);
