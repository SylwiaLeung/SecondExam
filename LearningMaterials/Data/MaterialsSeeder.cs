using LearningMaterials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningMaterials.Data
{
    public class MaterialsSeeder
    {
        private readonly MaterialsDbContext _context;

        public MaterialsSeeder(MaterialsDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                //if (!_context.Roles.Any())
                //{
                //    _context.Roles.AddRange(GetRoles());
                //    _context.SaveChanges();
                //}
                if (!_context.Authors.Any())
                {
                    _context.Authors.AddRange(GetAuthors());
                    _context.SaveChanges();
                }
                if (!_context.MaterialTypes.Any())
                {
                    _context.MaterialTypes.AddRange(GetMaterialTypes());
                    _context.SaveChanges();
                }
                //if (!_context.Materials.Any())
                //{
                //    _context.Materials.AddRange(GetMaterials());
                //    _context.SaveChanges();
                //}
                //if (!_context.Reviews.Any())
                //{
                //    _context.Reviews.AddRange(GetReviews());
                //    _context.SaveChanges();
                //}
            }
        }

        private IEnumerable<Author> GetAuthors()
        {
            List<Author> authors = new()
            {
                new Author()
                {
                    Name = "Dominik Starzyk",
                    Description = "Codecool / Mototola Academy mentorski"
                },
                new Author()
                {
                    Name = "Jacek",
                    Description = "Motorola God"
                }
            };
            return authors;
        }

        private IEnumerable<MaterialType> GetMaterialTypes()
        {
            List<MaterialType> materialTypes = new()
            {
                new MaterialType()
                {
                    Name = "Youtube Video",
                    Definition = "An amatour instructional video made most likely by an Indian person"
                },
                new MaterialType()
                {
                    Name = "Microsoft Documentation",
                    Definition = "Official, quite detailed documentation by Microsoft"
                },
                new MaterialType()
                {
                    Name = "Blog Post",
                    Definition = "A written explanation of a given topic, not too detailed, rather opinionated"
                },
                new MaterialType()
                {
                    Name = "Powerpoint presentation",
                    Definition = "Serves as a general orientation point or an introduction to a given topic"
                }
            };
            return materialTypes;
        }

        //private IEnumerable<Role> GetRoles()
        //{
        //    List<Role> roles = new()
        //    {
        //        new Role()
        //        {
        //            Name = "User"
        //        },
        //        new Role()
        //        {
        //            Name = "Admin"
        //        }
        //    };
        //    return roles;
        //}
    }
}
