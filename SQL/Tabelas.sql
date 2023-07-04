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


DROP TABLE jobs, Projetos, Usuarios;