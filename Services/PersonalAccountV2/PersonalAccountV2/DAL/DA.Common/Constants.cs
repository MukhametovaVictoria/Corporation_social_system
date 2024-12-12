namespace DA.Common
{
    public static class Constants
    {
        public static string ProjectName = "PersonalAccount";
        public static string ModelsFolderName = "WebApi.Models";
        public static string CommonFolderName = "DA.Common";
        public static string RepositoriesPath = "DA.Repositories";

        public static Dictionary<string, string> TableAndRepositoryPath = new Dictionary<string, string>()
        {
            {"Accomplishment", $"{RepositoriesPath}.AccomplishmentRepository" },
            {"Communication", $"{RepositoriesPath}.CommunicationRepository" },
            {"Employee", $"{RepositoriesPath}.EmployeeRepository"  },
            {"Event", $"{RepositoriesPath}.EventRepository" },
            {"Experience", $"{RepositoriesPath}.ExperienceRepository"  },
            {"Skill", $"{RepositoriesPath}.SkillRepository"  }
        };

        public static Dictionary<string, string> ClassPathByName = new Dictionary<string, string>()
        {
            {"Accomplishment", $"{ModelsFolderName}.Accomplishment.AccomplishmentModel" },
            {"Communication", $"{ModelsFolderName}.Communication.CommunicationModel" },
            {"Employee", $"{ModelsFolderName}.Employee.EmployeeModel"  },
            {"Event", $"{ModelsFolderName}.Event.EventModel" },
            {"Experience", $".{ModelsFolderName}.Experience.ExperienceModel"  },
            {"SkillNews", $".{ModelsFolderName}.Skill.SkillModel"  },
            {"MappingQuery", $"{CommonFolderName}.SqlQuery.MappingQuery"  }
        };
    }
}
