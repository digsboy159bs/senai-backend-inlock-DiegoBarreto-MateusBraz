USE InLock_Games_Tarde;

Select * FROM Estudios;
SELEcT * FROM Jogos;
SELEcT * FROM Usuarios;

SELECT Jogos.NomeJogo, Estudios.NomeEstudio FROM Jogos
INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio;