IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [EmployeeCompanies] (
    [EmployeeCompanyId] bigint NOT NULL IDENTITY,
    [CompanyId] bigint NOT NULL,
    [EmployeeId] bigint NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_EmployeeCompanies] PRIMARY KEY ([EmployeeCompanyId])
);

GO

CREATE TABLE [JuridicalPeople] (
    [JuridicalPersonId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [PublicName] nvarchar(max) NULL,
    [Cnpj] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Active] bit NULL,
    CONSTRAINT [PK_JuridicalPeople] PRIMARY KEY ([JuridicalPersonId])
);

GO

CREATE TABLE [People] (
    [PersonId] bigint NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Sex] int NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NULL,
    [Admission] datetime2 NULL,
    [Position] nvarchar(max) NULL,
    CONSTRAINT [PK_People] PRIMARY KEY ([PersonId])
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeCompanyId', N'Active', N'CompanyId', N'EmployeeId') AND [object_id] = OBJECT_ID(N'[EmployeeCompanies]'))
    SET IDENTITY_INSERT [EmployeeCompanies] ON;
INSERT INTO [EmployeeCompanies] ([EmployeeCompanyId], [Active], [CompanyId], [EmployeeId])
VALUES (CAST(1 AS bigint), CAST(1 AS bit), CAST(1 AS bigint), CAST(1 AS bigint)),
(CAST(2 AS bigint), CAST(1 AS bit), CAST(1 AS bigint), CAST(2 AS bigint));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeCompanyId', N'Active', N'CompanyId', N'EmployeeId') AND [object_id] = OBJECT_ID(N'[EmployeeCompanies]'))
    SET IDENTITY_INSERT [EmployeeCompanies] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'JuridicalPersonId', N'Cnpj', N'Discriminator', N'Email', N'Name', N'PhoneNumber', N'PublicName', N'Active') AND [object_id] = OBJECT_ID(N'[JuridicalPeople]'))
    SET IDENTITY_INSERT [JuridicalPeople] ON;
INSERT INTO [JuridicalPeople] ([JuridicalPersonId], [Cnpj], [Discriminator], [Email], [Name], [PhoneNumber], [PublicName], [Active])
VALUES (CAST(1 AS bigint), N'51028989000182', N'Company', N'company@email.com', N'Generic Company', N'71 999095155', N'Generic', CAST(1 AS bit)),
(CAST(2 AS bigint), N'92794727000106', N'Company', N'companys@email.com', N'Generic Company', N'71 999095155', N'Generic', CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'JuridicalPersonId', N'Cnpj', N'Discriminator', N'Email', N'Name', N'PhoneNumber', N'PublicName', N'Active') AND [object_id] = OBJECT_ID(N'[JuridicalPeople]'))
    SET IDENTITY_INSERT [JuridicalPeople] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PersonId', N'Cpf', N'DateOfBirth', N'Discriminator', N'Email', N'FirstName', N'LastName', N'PhoneNumber', N'Sex', N'Admission', N'Code', N'Position') AND [object_id] = OBJECT_ID(N'[People]'))
    SET IDENTITY_INSERT [People] ON;
INSERT INTO [People] ([PersonId], [Cpf], [DateOfBirth], [Discriminator], [Email], [FirstName], [LastName], [PhoneNumber], [Sex], [Admission], [Code], [Position])
VALUES (CAST(1 AS bigint), N'52383119075', '1990-08-15T00:00:00.0000000', N'Employee', N'antonioazevedo.123@gmail.com', N'Antonio', N'Azevedo', N'71 999095155', 77, '2021-02-13T09:40:05.0759293-03:00', N'123456A', N'Programmer'),
(CAST(2 AS bigint), N'63488338010', '1990-08-17T00:00:00.0000000', N'Employee', N'antonioazevedo@brq.com', N'Marcelo', N'Hage', N'71 999085155', 77, '2021-02-13T09:40:05.0788008-03:00', N'123456B', N'Sales');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PersonId', N'Cpf', N'DateOfBirth', N'Discriminator', N'Email', N'FirstName', N'LastName', N'PhoneNumber', N'Sex', N'Admission', N'Code', N'Position') AND [object_id] = OBJECT_ID(N'[People]'))
    SET IDENTITY_INSERT [People] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210213124005_GenericApp.Infra.Data.Context.GenericAppContext', N'3.1.12');

GO

