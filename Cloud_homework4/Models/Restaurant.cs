using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cloud_homework4.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "餐廳名稱"), StringLength(60)]
        public string Name { get; set; }

        [Required, Display(Name = "餐廳種類"), StringLength(60)]
        public string Genre { get; set; }

        [Display(Name = "最低價錢"), DataType(DataType.Currency)]
        public decimal? MinPrice { get; set; }
        
        [Display(Name = "最高價錢"), DataType(DataType.Currency)]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "發布日期"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
    }
}
