CREATE DATABASE bd_matriculas;
GO

USE bd_matriculas;
GO


CREATE TABLE estudiante (
    id_estudiante INT IDENTITY(1,1) PRIMARY KEY,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    dni VARCHAR(15) UNIQUE NOT NULL,
    fecha_nacimiento DATE
);
GO

CREATE TABLE curso (
    id_curso INT IDENTITY(1,1) PRIMARY KEY,
    nombre_curso VARCHAR(100) NOT NULL,
    descripcion VARCHAR(MAX)
);
GO


CREATE TABLE matricula (
    id_matricula INT IDENTITY(1,1) PRIMARY KEY,
    id_estudiante INT NOT NULL,
    id_curso INT NOT NULL,
    fecha_matricula DATE NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Activa',
    CONSTRAINT fk_matricula_estudiante FOREIGN KEY (id_estudiante) REFERENCES estudiante(id_estudiante),
    CONSTRAINT fk_matricula_curso FOREIGN KEY (id_curso) REFERENCES curso(id_curso),
    CONSTRAINT uq_estudiante_curso UNIQUE (id_estudiante, id_curso),
    CONSTRAINT chk_estado_matricula CHECK (estado IN ('Activa', 'Cancelada', 'Finalizada'))
);
GO

INSERT INTO estudiante (nombres, apellidos, dni, fecha_nacimiento) VALUES
('Juan', 'Pérez', '12345678', '2000-05-15'),
('María', 'Gómez', '23456789', '1999-08-21'),
('Luis', 'Ramírez', '34567890', '2001-02-10');


INSERT INTO curso (nombre_curso, descripcion) VALUES
('Matemáticas', 'Curso de álgebra, aritmética y geometría'),
('Lenguaje', 'Curso de comprensión y redacción de textos'),
('Historia', 'Curso sobre historia nacional y mundial');

INSERT INTO matricula (id_estudiante, id_curso, fecha_matricula, estado) VALUES
(1, 1, '2025-03-01', 'Activa'),        
(1, 2, '2025-03-01', 'Finalizada'),    
(2, 1, '2025-03-02', 'Cancelada'),     
(3, 3, '2025-03-03', 'Activa');     


CREATE PROCEDURE sp_listar_matriculas_por_curso
    @idCurso INT
AS
BEGIN
    SELECT 
        m.id_matricula,
        m.id_estudiante,
        e.nombres + ' ' + e.apellidos AS nombreEstudiante,
        m.id_curso,
        c.nombre_curso AS nombreCurso,
        m.fecha_matricula,
        m.estado
    FROM matricula m
    INNER JOIN estudiante e ON m.id_estudiante = e.id_estudiante
    INNER JOIN curso c ON m.id_curso = c.id_curso
    WHERE m.id_curso = @idCurso;
END;

CREATE PROCEDURE sp_listar_matriculas_por_estudiante
    @idEstudiante INT
AS
BEGIN
    SELECT 
        m.id_matricula AS IdMatricula,
        m.id_estudiante AS IdEstudiante,
        e.nombres + ' ' + e.apellidos AS NombreEstudiante,
        m.id_curso AS IdCurso,
        c.nombre_curso AS NombreCurso,
        m.fecha_matricula AS FechaMatricula,
        m.estado AS Estado
    FROM matricula m
    INNER JOIN estudiante e ON m.id_estudiante = e.id_estudiante
    INNER JOIN curso c ON m.id_curso = c.id_curso
    WHERE m.id_estudiante = @idEstudiante;
END;

CREATE PROCEDURE sp_listar_matriculas_por_estado
    @estado VARCHAR(20)
AS
BEGIN
    SELECT 
        m.id_matricula AS IdMatricula,
        m.id_estudiante AS IdEstudiante,
        e.nombres + ' ' + e.apellidos AS NombreEstudiante,
        m.id_curso AS IdCurso,
        c.nombre_curso AS NombreCurso,
        m.fecha_matricula AS FechaMatricula,
        m.estado AS Estado
    FROM matricula m
    INNER JOIN estudiante e ON m.id_estudiante = e.id_estudiante
    INNER JOIN curso c ON m.id_curso = c.id_curso
    WHERE m.estado = @estado;
END;

CREATE PROCEDURE sp_listar_matriculas_por_estudiante
    @idEstudiante INT
AS
BEGIN
    SELECT 
        m.id_matricula AS IdMatricula,
        m.id_estudiante AS IdEstudiante,
        e.nombres + ' ' + e.apellidos AS NombreEstudiante,
        m.id_curso AS IdCurso,
        c.nombre_curso AS NombreCurso,
        m.fecha_matricula AS FechaMatricula,
        m.estado AS Estado
    FROM matricula m
    INNER JOIN estudiante e ON m.id_estudiante = e.id_estudiante
    INNER JOIN curso c ON m.id_curso = c.id_curso
    WHERE m.id_estudiante = @idEstudiante;
END;


select * from estudiante;

select * from curso;

select * from matricula;

go

exec sp_listar_matriculas_por_estudiante @idEstudiante = 1;
exec sp_listar_matriculas_por_estado @estado = 'Activa';
exec sp_listar_matriculas_por_curso @idCurso = 1;