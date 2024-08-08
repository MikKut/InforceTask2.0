using PhotoGallery.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.UnitTests
{
    public static class TestData
    {
        public static string UserToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwiZXhwIjoxNjk3NjIxNzczLCJpc3MiOiJNeVBob3RvR2FsbGVyeUFwcCIsImF1ZCI6IlBob3RvR2FsbGVyeVVzZXJzIn0.JBc_wUpw9JUStUPWB3mpt_PUMABIDN2FF4d5Kx1Uo48";
        public static string AdminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiYWRtaW4iLCJ1c2VyIl0sImV4cCI6MTY5NzYyMTk4MiwiaXNzIjoiTXlQaG90b0dhbGxlcnlBcHAiLCJhdWQiOiJQaG90b0dhbGxlcnlVc2VycyJ9.56eM2sfHGiTbGFJDMZMENOTmQ0s-JEbWktWDMmhEl2c";
        public static List<User> GetUsers()
        {
            return new List<User>
        {
            new User
            {
                UserId = 1,
                Email = "user@gmail.com",
                UserName = "user1",
                Password = "password1",
                IsAdmin = false,
                Albums = new List<Album>()
            },
            new User
            {
                UserId = 2,
                Email = "admin@gmail.com",
                UserName = "user2",
                Password = "password2",
                IsAdmin = true,
                Albums = new List<Album>()
            }
        };
        }

        public static List<Album> GetAlbums()
        {
            return new List<Album>
        {
            new Album
            {
                AlbumId = 1,
                Title = "Vacation 2023",
                Description = "A wonderful vacation",
                UserId = 1, // Belongs to user 1
                User = null, // Will be populated during mapping
                Images = new List<Image>()
            },
            new Album
            {
                AlbumId = 2,
                Title = "Family Photos",
                Description = "Memories with family",
                UserId = 2, // Belongs to user 2
                User = null, // Will be populated during mapping
                Images = new List<Image>()
            }
        };
        }

        public static List<Image> GetImages()
        {
            return new List<Image>
        {
            new Image
            {
                ImageId = 1,
                Extension = ".jpg",
                Title = "Beach Sunset",
                Description = "Beautiful sunset at the beach",
                Likes = 100,
                Dislikes = 10,
                AlbumId = 1, // Belongs to Album 1
                Album = null, // Will be populated during mapping
            },
            new Image
            {
                ImageId = 2,
                Extension = ".png",
                Title = "Mountain Hike",
                Description = "Scenic mountain hike",
                Likes = 50,
                Dislikes = 5,
                AlbumId = 1, // Belongs to Album 1
                Album = null, // Will be populated during mapping
            },
            new Image
            {
                ImageId = 3,
                Extension = ".jpg",
                Title = "Family Reunion",
                Description = "Joyful family reunion",
                Likes = 75,
                Dislikes = 8,
                AlbumId = 2, // Belongs to Album 2
                Album = null, // Will be populated during mapping
            }
        };
        }
    }

}
