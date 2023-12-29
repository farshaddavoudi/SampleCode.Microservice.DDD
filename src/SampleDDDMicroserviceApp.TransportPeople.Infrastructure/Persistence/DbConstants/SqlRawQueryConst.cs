namespace SampleDDDMicroserviceApp.TransportPeople.Infrastructure.Persistence.DbConstants;

public class SqlRawQueryConst
{
    public const string CreateUserRolesView =
        $"""
         CREATE OR ALTER VIEW [dbo].[{ViewNameConst.UserRolesView}]
         As
         Select [Id], [UserId], [RoleName], [ApplicationName]
         FROM [OurUsers].[dbo].[UserRolePairsView]                                   
         """;

    public const string CreateCrewsView =
        $"""
         CREATE OR ALTER VIEW [dbo].[{ViewNameConst.CrewsView}]
         As
         SELECT 
             Convert(int, (select substring(EmployeeNo,patindex('%[0-9]%', EmployeeNo),len(EmployeeNo)))) As PersonnelCode,
             FName As ENFirstName,
             LName As ENLastName,
             CrewType As Pos,
             
         FROM [Crews].[dbo].Crew C 
         WHERE EmployeeNo IS NOT NULL 
         """;

    public const string CreateUsersView =
        $"""
        CREATE OR ALTER VIEW [dbo].[{ViewNameConst.UsersView}] 
        As
        SELECT A.[UserId]
        	  ,A.[PersonnelCode]
        	  ,A.[Username]
        	  ,A.[RahkaranId]
        	  ,A.[FirstName]
        	  ,A.[LastName]
        	  ,A.[FullName]
        	  ,A.[FatherName]
        FROM [Users].[dbo].[OurUsers] 
        """;
}