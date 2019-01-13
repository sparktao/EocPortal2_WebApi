using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hexagon.Data.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Hexagon.Data.EF.Context
{
    public class SqliteDbContextSeed
    {
        public static async Task SeedAsync(SqliteDbContext context,
                          ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                // myContext.Database.Migrate();

                if (!context.Organization_Employee.Any())
                {
                    context.Organization_Employee.AddRange(
                        new List<Organization_Employee>{
                            new Organization_Employee{
                                Employee_Id = 1,
                                Employee_Name = "name 1",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now

                            },
                            new Organization_Employee{
                                Employee_Id = 2,
                                Employee_Name = "name 2",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "11@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 3,
                                Employee_Name = "name 3",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 4,
                                Employee_Name = "name 4",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 5,
                                Employee_Name = "name 5",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 6,
                                Employee_Name = "name 6",
                                Gender = "1",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 7,
                                Employee_Name = "name 7",
                                Gender = "0",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            },
                            new Organization_Employee{
                                Employee_Id = 8,
                                Employee_Name = "name 8",
                                Gender = "0",
                                Birthday = DateTime.Now,
                                Contact_Phone = "13387631234",
                                Email = "1@11.com",
                                Created_Date = DateTime.Now
                            }
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<SqliteDbContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
