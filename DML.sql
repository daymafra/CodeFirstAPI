USE CodeFirstDesafio;
GO

  INSERT INTO TipoUsuario (Tipo)
	VALUES
		('Paciente'),
		('Medico')
GO
INSERT INTO Especialidade (Categoria)
	VALUES
		('Clínico Geral'),
		('Dentista'),
		('Fisioterapeuta')
GO

INSERT INTO Usuario(Nome, Email, IdTipoUsuario, Senha)
	VALUES	
		('Paulo', 'paulo@email.com', 1, '123456'),
		('João', 'joao@email.com', 1, '123456'),
		('Gabriel', 'gabriel@email.com', 1, '123456'),
		('Samuel', 'samule@email.com', 2, '123456'),
		('Renata', 'renata@email.com', 2, '123456'),
		('Carla', 'carla@email.com', 2, '123456')
		
GO

INSERT INTO Medico (CRM, IdEspecialidade, IdUsuario)
	VALUES
		('123456789', 1, 4),
		('987654321', 2, 5),
		('456123789', 3, 6)
GO

INSERT INTO Paciente (Carteirinha, DataNascimento, Ativo, IdUsuario)
	VALUES
		('741852963', '1964-07-12',1, 1),
		('369258147', '2011-08-10',1, 2),
		('258741369', '2021-09-17',1, 3)
GO

INSERT INTO Consulta (IdMedico, IdPaciente, DataHora)
	VALUES
		(4, 1, '2022-10-01'),
		(5, 2, '2022-10-02'),
		(6, 3, '2022-10-03')
GO