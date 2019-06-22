using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_homework4.Models
{
    public class Seed
    {
        public static void Initalize(Cloud_homework4Context context)
        {
            context.Database.EnsureCreated();

            // Checking database is empty
            if (context.Restaurant.Count() == 0)
            {
                var data = new Restaurant[]
                {
                    new Restaurant
                    {
                        Name="感恩麵店",
                        Genre="快餐",
                        MinPrice=90,
                        MaxPrice=100,
                        ReleaseDate=DateTime.Parse("2019/6/15").Date
                    },
                    new Restaurant
                    {
                        Name="鐵板便當",
                        Genre="便當",
                        MinPrice=55,
                        MaxPrice=100,
                        ReleaseDate=DateTime.Parse("2019/6/15").Date
                    },
                    new Restaurant
                    {
                        Name="慈善便當",
                        Genre="便當",
                        MinPrice=55,
                        MaxPrice=55,
                        ReleaseDate=DateTime.Parse("2019/6/15").Date
                    },
                    new Restaurant
                    {
                        Name="宵夜快餐",
                        Genre="快餐",
                        MinPrice=35,
                        MaxPrice=100,
                        ReleaseDate=DateTime.Parse("2019/6/15").Date
                    },
                    new Restaurant
                    {
                        Name="敦煌快餐",
                        Genre="快餐",
                        MinPrice=5,
                        MaxPrice=100,
                        ReleaseDate=DateTime.Parse("2019/6/15").Date
                    }
                };

                foreach (Restaurant restaurant in data)
                {
                    context.Restaurant.Add(restaurant);
                }

                context.SaveChanges();
            }
        }
    }
}
