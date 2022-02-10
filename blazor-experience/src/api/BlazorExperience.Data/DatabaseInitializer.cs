using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorExperience.Data
{
    public static class DatabaseInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var _books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Managing Oneself",
                    Description =
                        "We live in an age of unprecedented opportunity: with ambition, drive, and talent, you can rise to the top of your chosen profession, regardless of where you started out...",
                    Author = "Peter Ducker"
                },
                new Book()
                {
                    Id = 2,
                    Title = "Evolutionary Psychology",
                    Description =
                        "Evolutionary Psychology: The New Science of the Mind, 5th edition provides students with the conceptual tools of evolutionary psychology, and applies them to empirical research on the human mind...",
                    Author = "David Buss"
                },
                new Book()
                {
                    Id = 3,
                    Title = "How to Win Friends & Influence People",
                    Description =
                        "Millions of people around the world have improved their lives based on the teachings of Dale Carnegie. In How to Win Friends and Influence People, he offers practical advice and techniques for how to get out of a mental rut and make life more rewarding...",
                    Author = "Dale Carnegie"
                },
                new Book()
                {
                    Id = 4,
                    Title = "The Selfish Gene",
                    Description =
                        "Professor Dawkins articulates a gene’s eye view of evolution. A view giving center stage to these persistent units of information, and in which organisms can be seen as vehicles for their replication. This imaginative, powerful, and stylistically brilliant work not only brought the insights of Neo-Darwinism to a wide audience. But galvanized the biology community, generating much debate and stimulating whole new areas of research...",
                    Author = "Richard Dawkins"
                },
                new Book()
                {
                    Id = 5,
                    Title = "The Lessons of History",
                    Description =
                        "Will and Ariel Durant have succeeded in distilling for the reader the accumulated store of knowledge and experience from their five decades of work on the eleven monumental volumes of The Story of Civilization...",
                    Author = "Will & Ariel Durant"
                }
            };

            modelBuilder.Entity<Book>().HasData(_books[0], _books[1], _books[2], _books[3], _books[4]);
        }
    }
}