-- Cria o banco de dados, se não existir
CREATE DATABASE IF NOT EXISTS lovelettersdb;

-- Usa o banco de dados criado
USE lovelettersdb;

-- Cria a tabela users se não existir
CREATE TABLE IF NOT EXISTS users (
    guid VARCHAR(28) PRIMARY KEY NOT NULL,
    name VARCHAR(60) NOT NULL,
    email VARCHAR(60) NOT NULL,
    password VARCHAR(60) NOT NULL,
    profilePhoto MEDIUMTEXT NULL,
    partnerGuidpartnerUIDusersusers VARCHAR(28) NULL,
    partnerName VARCHAR(60) NULL,
    havePartner BOOLEAN NOT NULL
);

-- Cria a tabela invites se não existir
CREATE TABLE IF NOT EXISTS invites (
    id INT AUTO_INCREMENT PRIMARY KEY,
    inviteAccepted BOOLEAN NOT NULL,
    inviteDate DATETIME NOT NULL,
    guidInvited VARCHAR(28) NOT NULL,
    guidInviter VARCHAR(28) NOT NULL,
    CONSTRAINT FK_User_Invited FOREIGN KEY (guidInvited) REFERENCES users(guid),
    CONSTRAINT FK_User_Inviter FOREIGN KEY (guidInviter) REFERENCES users(guid)
);

-- Cria a tabela relationship se não existir
CREATE TABLE IF NOT EXISTS relationship (
    partnerGuid1 VARCHAR(28) NOT NULL,
    partnerGuid2 VARCHAR(28) NOT NULL,
    status VARCHAR(1) NOT NULL,
    startDate DATETIME NOT NULL,
    endDate DATETIME NULL,
    CONSTRAINT FK_partner1 FOREIGN KEY (partnerGuid1) REFERENCES users(guid),
    CONSTRAINT FK_partner2 FOREIGN KEY (partnerGuid2) REFERENCES users(guid)
);


