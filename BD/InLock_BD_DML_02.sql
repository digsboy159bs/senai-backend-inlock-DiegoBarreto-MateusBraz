USE InLock_Games_Tarde;

INSERT INTO Estudios(NomeEstudio)
VALUES ('Blizzard'), ('Rockstar Studios'), ('Square Enix');

INSERT INTO Jogos(NomeJogo, DataLancamento, Descricao, Valor, IdEstudio)
VALUES ('Diablo 3','15/05/2012','E um jogo que contem bastante acao e � viciante, seja voc� um novato ou um f�', 99,1), 
('Red Dead Redemption II','26/10/2018','Jogo eletr�nico de a�ao-aventura Western', 120 , 2);

INSERT INTO TipoUsuario( Titulo)
VALUES ('Administrador'), ('Cliente');

INSERT INTO Usuarios( Email,Senha,IdTipoUsuario)
VALUES ('admin@admin.com', 'admin', 1), ('cliente@cliente.com','cliente',2);



