SHOW TABLES;
SELECT * FROM Usuarios;
SELECT * FROM Projetos;
SELECT * FROM Jobs WHERE projeto_id = 1;
SELECT * FROM LancamentosTimesheet WHERE projeto_id = 1;
SELECT * FROM Aprovadores;

UPDATE LancamentosTimesheet SET job_id = 1 WHERE job_id = 4;




SELECT u.usuario_id AS usuario_id,
u.nome AS usuario_nome,
u.email AS usuario_email,
u.tipo AS usuario_tipo,
p.projeto_id AS projeto_id, 
p.nome AS projeto_nome,
p.descricao AS projeto_descricao, 
j.job_id AS job_id, 
j.nome AS job_nome, 
j.descricao AS job_descricao, 
l.timesheet_id AS lancamento_id, 
l.descricao as lancamento_descricao, 
l.data AS lancamento_data, 
l.hora AS lancamento_hora, 
l.status AS lancamento_status, 
a.usuario_id AS aprovador_usuario_id, 
a.timesheet_id AS aprovador_lancamento_id, 
a.status AS aprovador_status
FROM Usuarios AS u
INNER JOIN Projetos AS p ON u.usuario_id = p.usuario_id
LEFT JOIN Jobs AS j ON u.usuario_id = j.usuario_id AND p.projeto_id = j.projeto_id
LEFT JOIN LancamentosTimesheet AS l ON u.usuario_id = l.usuario_id AND p.usuario_id = l.projeto_id AND j.job_id = l.job_id
LEFT JOIN Aprovadores AS a ON l.timesheet_id = a.timesheet_id
ORDER BY u.usuario_id;


SELECT DISTINCT u.usuario_id,
u.nome,
u.email,
u.tipo
FROM Usuarios AS u
LEFT JOIN Projetos AS p ON u.usuario_id = p.usuario_id
ORDER BY u.usuario_id;

SELECT p.projeto_id,
p.nome,
p.descricao
FROM Projetos AS p
WHERE p.usuario_id = 1;


SELECT j.job_id,
j.nome,
j.descricao
FROM Jobs AS j
WHERE j.usuario_id = 1 AND j.projeto_id = 1;



SELECT timesheet_id, descricao, data, hora, status FROM LancamentosTimesheet
WHERE usuario_id = @UsuarioId 
  AND projeto_id = @ProjetoId
  AND job_id = @JobId;

ALTER TABLE Jobs
ADD COLUMN descricao VARCHAR(255) NOT NULL;

SELECT




/* INSERT */
INSERT INTO Usuarios (nome, email, senha, tipo) 
VALUES ('Gabriel', 'gb@outlook.com', '0001234', 1),
        ('Juca', 'juca@hotmail.com', '0000011', 1);


INSERT INTO Projetos (usuario_id, nome, descricao)
VALUES (1, 'Barco de pesca', 'Comprar um barco de pesca.');

    INSERT INTO Jobs (usuario_id, projeto_id, nome, descricao)
    VALUES (1, 4, 'Ir até a cidade', 'Usar  bicicleta.');

INSERT INTO LancamentosTimesheet (usuario_id, projeto_id, job_id, descricao, data, hora, status)
VALUES (1, 1, 4, 'Teste', '2023-07-30', '08:30:00', 3),
        (1, 1, 4, 'Teste', '2023-07-30', '13:30:00', 1),
        (1, 1, 4, 'Teste', '2023-07-30', '20:00:00', 2);

INSERT INTO Aprovadores (usuario_id, timesheet_id, status)
VALUES (3, 1, 2);

SELECT * FROM Usuarios AS u 
INNER JOIN Aprovadores AS a 
WHERE u.usuario_id = a.usuario_id;


SELECT concat('KILL ', id, ';') INTO @kill_statement
FROM information_schema.processlist
WHERE user <> 'sysadmin';

PREPARE kill_statement FROM @kill_statement;
EXECUTE kill_statement;
DEALLOCATE PREPARE kill_statement;

SHOW PROCESSLIST;

KILL >336;