create table users(
	uid varchar(28) PRIMARY KEY NOT NULL,
    name varchar(60) NOT NULL,
    email varchar(60) NOT NULL,
    password varchar(60) NOT NULL,
    profilePhoto mediumtext NULL,
    partnerUID varchar(28) NULL,
    partnerName varchar(60) NULL,
    havePartner boolean NOT NULL
);

create table invites(
	id INT AUTO_INCREMENT PRIMARY KEY,
    inviteAccepted boolean NOT NULL,
    inviteDate datetime NOT NULL,
    uidInvited varchar(28) NOT NULL,
    uidInviter varchar(28) NOT NULL,
    CONSTRAINT FK_User_Invited FOREIGN KEY (uidInvited) REFERENCES users(UID),
    CONSTRAINT FK_User_Inviter FOREIGN KEY (uidInviter) REFERENCES users(UID)
);

create table relationship(
	partnerUid1 varchar(28) NOT NULL,
    partnerUid2 varchar(28) NOT NULL,
    status varchar(1) NOT NULL,
    startDate datetime NOT NULL,
    endDate datetime NULL,
    CONSTRAINT FK_partner1 FOREIGN KEY (partnerUid1) REFERENCES users(UID),
    CONSTRAINT FK_partner2 FOREIGN KEY (partnerUid2) REFERENCES users(UID)
);