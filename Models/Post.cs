using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HaveFun.Models
{
    public class Post
    {
        public long Id { get; set; }

        public string Key
        {
            get
            {
                if (Title == null)
                    return null;

                var key = Regex.Replace(Title, @"[^a-zA-Z0-9\- ]", string.Empty);
                return key.Replace(" ", "-").ToLower();
            }
        }

        [Required]
        [StringLength(90, MinimumLength = 5,
                        ErrorMessage = "Title must be between 5 and 90 characters long")]
        public string Title { get; set; }

        public DateTime Time { get; set; }

        public string Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(8, ErrorMessage = "Blog post must be at least 8 characters long")]
        public string Body { get; set; }
    }
}
