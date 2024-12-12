CREATE TABLE "Project" (
  "Id" uuid PRIMARY KEY,
  "Name" text,
  "ManagerId" uuid
);

CREATE TABLE "TimeTraker" (
  "Id" uuid PRIMARY KEY,
  "Data" date,
  "UserId" uuid,
  "ProjectId" uuid,
  "CountTime" integer
);

CREATE TABLE "User" (
  "Id" uuid PRIMARY KEY,
  "Name" text
);

CREATE TABLE "UserInProject" (
  "Id" uuid PRIMARY KEY,
  "UserId" uuid,
  "ProjectId" uuid
);

ALTER TABLE "UserInProject" ADD FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE "UserInProject" ADD FOREIGN KEY ("ProjectId") REFERENCES "Project" ("Id");

ALTER TABLE "User" ADD FOREIGN KEY ("Id") REFERENCES "TimeTraker" ("UserId");

ALTER TABLE "TimeTraker" ADD FOREIGN KEY ("ProjectId") REFERENCES "Project" ("Id");
