﻿using Diary.Model.Domains;
using Diary.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Model.Converters;
using Diary.Model;

namespace Diary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students =  context.Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();

                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);

                return students
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();

            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            };
            
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentsPropertis(context, student);

                var studentsRatings = GetStudentRating(context, student);

                UpdateRate(student, ratings, context, studentsRatings, Subject.Math);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Technology);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Physics);
                UpdateRate(student, ratings, context, studentsRatings, Subject.PolishLang);
                UpdateRate(student, ratings, context, studentsRatings, Subject.EnglishLang);

                context.SaveChanges();
            }
        }

        private void UpdateStudentsPropertis(ApplicationDbContext context, Student student)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.Activities = student.Activities;
            studentToUpdate.GroupId = student.GroupId;
        }
        private static List<Rating> GetStudentRating(ApplicationDbContext context, Student student)
        {
           return  context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();
        }
        private static void UpdateRate(Student student, List<Rating> newRatings, ApplicationDbContext context, List<Rating> studentsRatings, Subject subject)
        {
            var subRating = studentsRatings
                    .Where(x => x.SubjectId == (int)subject)
                    .Select(x => x.Rate);
            var newSubRating = newRatings
                .Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);
            var subRatingsToDelete = subRating.Except(newSubRating).ToList();
            var subRatingsToAdd = newSubRating.Except(subRating).ToList();

            subRatingsToDelete.ForEach(x =>
            {
                var ratingsToDelete = context.Ratings
                .First(y => y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)subject);

                context.Ratings.Remove(ratingsToDelete);
            });

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject,
                };
                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });

                context.SaveChanges();
            }
        }
    }
}
