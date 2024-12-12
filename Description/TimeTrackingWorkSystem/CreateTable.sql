CREATE TABLE "Users"
(
    "UserId"   uuid NOT NULL DEFAULT gen_random_uuid(),
    "UserName" varchar(100) NOT NULL DEFAULT ''::character varying,
    "Email"    varchar(300) NOT NULL DEFAULT ''::character varying,
    "RegistrationDate" date NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT "PKUser" PRIMARY KEY ("UserId")
);


CREATE TABLE "Project"
(
    "ProjectId"   uuid NOT NULL DEFAULT gen_random_uuid(),
    "UserId"      uuid NOT NULL,
    "ProjectCode" varchar(100) NOT NULL DEFAULT ''::character varying,
    CONSTRAINT "PKProject" PRIMARY KEY ("ProjectId"),
    CONSTRAINT "FKUser" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId")
);


CREATE TABLE "ProjectTimeDetails"
(
    "ProjectTimeDetailId" uuid NOT NULL DEFAULT gen_random_uuid(),
    "ProjectId"           uuid NOT NULL,
    "UserId"              uuid NOT NULL,
    "CurrentTimeDate"     date NOT NULL DEFAULT CURRENT_DATE,
    "QuantityTime"        INTEGER,
    CONSTRAINT "PKProjectTimeDetail" PRIMARY KEY ("ProjectTimeDetailId"),
    CONSTRAINT "FKProject" FOREIGN KEY ("ProjectId") REFERENCES public."Project"("ProjectId"),
    CONSTRAINT "FKUser" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId") 
);