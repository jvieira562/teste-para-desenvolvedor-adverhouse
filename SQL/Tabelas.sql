CREATE TABLE Usuarios (
  usuario_id INT PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL,
  senha VARCHAR(25) NOT NULL,
  tipo INT NOT NULL
);

CREATE TABLE Projetos (
  usuario_id INT NOT NULL,
  projeto_id INT AUTO_INCREMENT,
  nome VARCHAR(100) NOT NULL,
  descricao VARCHAR(255) NOT NULL,
  PRIMARY KEY (projeto_id),
  FOREIGN KEY (usuario_id) REFERENCES Usuarios(usuario_id)
);
CREATE INDEX idx_projetos_id ON Projetos (usuario_id, projeto_id);


CREATE TABLE Jobs (
  usuario_id INT NOT NULL,
  projeto_id INT NOT NULL,
  job_id INT AUTO_INCREMENT,
  nome VARCHAR(100)  NOT NULL,
  descricao VARCHAR(255) NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES Usuarios(usuario_id),
  FOREIGN KEY (projeto_id) REFERENCES Projetos(projeto_id),
  PRIMARY KEY (job_id)
);
CREATE INDEX idx_jobs_id ON Jobs (usuario_id, projeto_id, job_id);

CREATE TABLE LancamentosTimesheet (
  usuario_id INT NOT NULL,
  projeto_id INT NOT NULL,
  job_id INT NOT NULL,
  timesheet_id INT AUTO_INCREMENT,
  descricao VARCHAR(255) NOT NULL,
  data DATE  NOT NULL,
  hora TIME  NOT NULL,
  status INT  NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES Usuarios(usuario_id),
  FOREIGN KEY (projeto_id) REFERENCES Projetos(projeto_id),
  FOREIGN KEY (job_id) REFERENCES Jobs(job_id),
  PRIMARY KEY (timesheet_id)
);
CREATE INDEX idx_lancamentostimesheet_id ON LancamentosTimesheet (usuario_id, projeto_id, job_id, timesheet_id);

CREATE TABLE Aprovadores (
  usuario_id INT NOT NULL,
  timesheet_id INT NOT NULL,
  status INT NOT NULL,
  FOREIGN KEY (timesheet_id) REFERENCES LancamentosTimesheet(timesheet_id),
  FOREIGN KEY (usuario_id) REFERENCES Usuarios(usuario_id)
);
CREATE INDEX idx_aprovadores_id ON Aprovadores (usuario_id, timesheet_id);


CREATE TABLE Status (
  status_id INT PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(255),
  cor VARCHAR(255)
);

INSERT INTO Status (nome, cor) VALUES
  ('Reprovado', 'Vermelho'),
  ('Aguardando Aprovação', 'Laranja'),
  ('Aprovado', 'Verde'),
  ('Não Validado', 'Branco');


DELIMITER //
CREATE TRIGGER tr_atualiza_status_lancamento_timesheet
AFTER UPDATE ON Aprovadores
FOR EACH ROW
BEGIN
    IF NEW.status = 1 THEN
        UPDATE LancamentosTimesheet
        SET status = 1
        WHERE timesheet_id = NEW.timesheet_id;
    END IF;
END //
DELIMITER ;

DELIMITER //
CREATE TRIGGER tr_atualiza_status_lancamento_timesheet_se_aprovadores_igual_3
AFTER UPDATE ON Aprovadores
FOR EACH ROW
BEGIN
    DECLARE total_aprovadores INT;
    DECLARE total_aprovadores_status_3 INT;

    SELECT COUNT(*) INTO total_aprovadores FROM Aprovadores WHERE timesheet_id = NEW.timesheet_id;
    SELECT COUNT(*) INTO total_aprovadores_status_3 FROM Aprovadores WHERE timesheet_id = NEW.timesheet_id AND status = 3;

    IF total_aprovadores > 0 AND total_aprovadores = total_aprovadores_status_3 THEN
        UPDATE LancamentosTimesheet SET status = 3 WHERE timesheet_id = NEW.timesheet_id;
    END IF;
END //
DELIMITER ;


DELIMITER //

CREATE TRIGGER tr_atualiza_status_lancamento_timesheet_se_aguardando_aprovacao
AFTER INSERT ON Aprovadores
FOR EACH ROW
BEGIN
    DECLARE total_aprovadores INT;
    DECLARE total_aprovadores_status_2 INT;

    SELECT COUNT(*) INTO total_aprovadores FROM Aprovadores WHERE timesheet_id = NEW.timesheet_id;
    SELECT COUNT(*) INTO total_aprovadores_status_2 FROM Aprovadores WHERE timesheet_id = NEW.timesheet_id AND status = 2;

    IF total_aprovadores > 0 AND total_aprovadores > total_aprovadores_status_2 THEN
        UPDATE LancamentosTimesheet SET status = 2 WHERE timesheet_id = NEW.timesheet_id;
    END IF;
END //

DELIMITER ;



DELIMITER //
CREATE TRIGGER tr_atualiza_status_lancamento_timesheet_se_nao_validado
AFTER INSERT ON Aprovadores
FOR EACH ROW
BEGIN
    IF NEW.status = 2 THEN
        UPDATE LancamentosTimesheet
        SET status = 2
        WHERE timesheet_id = NEW.timesheet_id;
    END IF;
END //
DELIMITER ;