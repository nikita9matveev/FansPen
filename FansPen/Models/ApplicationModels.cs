using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FansPen.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public string AboutMe { get; set; }
        public string Interests { get; set; }
        public string Lang { get; set; }
        public string Style { get; set; }
        public string AvatarUrl { get; set; }
        public string ProviderKey { get; set; }
        public ICollection<Fanfic> Fanfics { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ApplicationUserTopic> RatedTopics { get; set; }
        public ApplicationUser()
        {
            Fanfics = new List<Fanfic>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
            RatedTopics = new List<ApplicationUserTopic>();
        }

        internal object FindById(object p)
        {
            throw new NotImplementedException();
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Fanfic> Fanfics { get; set; }
        public Category()
        {
            Fanfics = new List<Fanfic>();
        }
    }

    public class Fanfic
    {
        public int Id { get; set; }
        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public virtual ICollection<FanficTag> FanficTags { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditingDate { get; set; }
        public float AverageRating { get; set; }
        public string Content { get; set; }
        public Fanfic()
        {
            Comments = new List<Comment>();
            Topics = new List<Topic>();
            FanficTags = new List<FanficTag>();
        }
    }

    public class Img
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public string ImgUrl { get; set; }
    }

    public class Rating
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public int Value { get; set; }
    }

    public class Topic
    {
        public int Id { get; set; }
        public int? FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ICollection<Img> Imgs { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<ApplicationUserTopic> WhoRated { get; set; }
        public float AverageRating { get; set; }
        public Topic()
        {
            Imgs = new List<Img>();
            Ratings = new List<Rating>();
            WhoRated = new List<ApplicationUserTopic>();
        }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int? FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Like> Likes { get; set; }
        public string Text { get; set; }
        public DateTime DataCreate { get; set; }
        public Comment()
        {
            Likes = new List<Like>();
        }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountOfFanfic { get; set; }
        public virtual ICollection<FanficTag> FanficTags { get; set; }
        public Tag()
        {
            FanficTags = new List<FanficTag>();
        }
    }

    public class Like
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

    public class ApplicationUserTopic
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }

    public class FanficTag
    {
        public int Id { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
