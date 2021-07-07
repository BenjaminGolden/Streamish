using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT * From UserProfile;
            ";

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserProfile>();
                    while (reader.Read())
                    {
                        users.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")

                        });
                    }

                    reader.Close();

                    return users;
                }
            }
        }

        //public List<UserProfile> GetAllWithComments()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
        //               v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,

        //               up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
        //               up.ImageUrl AS UserProfileImageUrl,

        //               c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
        //          FROM Video v 
        //               JOIN UserProfile up ON v.UserProfileId = up.Id
        //               LEFT JOIN Comment c on c.VideoId = v.id
        //     ORDER BY  v.DateCreated
        //    ";

        //            var reader = cmd.ExecuteReader();

        //            var videos = new List<Video>();
        //            while (reader.Read())
        //            {
        //                var videoId = DbUtils.GetInt(reader, "VideoId");

        //                var existingVideo = videos.FirstOrDefault(p => p.Id == videoId);
        //                if (existingVideo == null)
        //                {
        //                    existingVideo = new Video()
        //                    {
        //                        Id = videoId,
        //                        Title = DbUtils.GetString(reader, "Title"),
        //                        Description = DbUtils.GetString(reader, "Description"),
        //                        DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
        //                        Url = DbUtils.GetString(reader, "Url"),
        //                        UserProfileId = DbUtils.GetInt(reader, "VideoUserProfileId"),
        //                        UserProfile = new UserProfile()
        //                        {
        //                            Id = DbUtils.GetInt(reader, "VideoUserProfileId"),
        //                            Name = DbUtils.GetString(reader, "Name"),
        //                            Email = DbUtils.GetString(reader, "Email"),
        //                            DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
        //                            ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
        //                        },
        //                        Comments = new List<Comment>()
        //                    };

        //                    videos.Add(existingVideo);
        //                }

        //                if (DbUtils.IsNotDbNull(reader, "CommentId"))
        //                {
        //                    existingVideo.Comments.Add(new Comment()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "CommentId"),
        //                        Message = DbUtils.GetString(reader, "Message"),
        //                        VideoId = videoId,
        //                        UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
        //                    });
        //                }
        //            }

        //            reader.Close();

        //            return videos;
        //        }
        //    }
        //}

        public UserProfile GetByIdWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.Name, up.Email, up.ImageUrl, up.DateCreated, 
                        v.Id as VideoId, v.Title, v.Description, v.DateCreated as VideoDateCreated, V.Url
                      
                        FROM UserProfile up
                        Left Join Video v ON up.Id = v.UserProfileId
                        WHERE up.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile user = null;
                    while (reader.Read())
                    {
                        if (user == null)
                        {
                            user = new UserProfile()
                            {

                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Videos = new List<Video>()
                            };
                            if (DbUtils.IsNotDbNull(reader, "VideoId"))
                            {
                                user.Videos.Add(new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    DateCreated = DbUtils.GetDateTime(reader,
                                    "VideoDateCreated"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                });
                            }
                        }
                    }

                    reader.Close();

                    return user;
                }
            }
        }
        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                              SELECT Id, Name, Email, ImageUrl, DateCreated 
                      
                        FROM UserProfile 
                       
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile user = null;
                    if (reader.Read())
                    {
                        user = new UserProfile()
                        {

                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),

                        };
                    }

                    reader.Close();

                    return user;
                }
            }
        }

        //public Video GetByIdWithComments(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //               SELECT v.Id AS VideoId, v.Title, v.Description, v.Url, 
        //               v.DateCreated AS VideoDateCreated, v.UserProfileId As VideoUserProfileId,

        //               up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
        //               up.ImageUrl AS UserProfileImageUrl,

        //               c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
        //          FROM Video v 
        //               JOIN UserProfile up ON v.UserProfileId = up.Id
        //               LEFT JOIN Comment c on c.VideoId = v.id
        //                   WHERE v.Id = @Id";

        //            DbUtils.AddParameter(cmd, "@Id", id);

        //            var reader = cmd.ExecuteReader();

        //            Video video = null;
        //            while (reader.Read())
        //            {
        //                if (video == null)
        //                {
        //                    video = new Video()
        //                    {
        //                        Id = id,
        //                        Title = DbUtils.GetString(reader, "Title"),
        //                        Description = DbUtils.GetString(reader, "Description"),
        //                        DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
        //                        Url = DbUtils.GetString(reader, "Url"),
        //                        UserProfileId = DbUtils.GetInt(reader, "VideoUserProfileId"),
        //                        UserProfile = new UserProfile()
        //                        {
        //                            Id = DbUtils.GetInt(reader, "VideoUserProfileId"),
        //                            Name = DbUtils.GetString(reader, "Name"),
        //                            Email = DbUtils.GetString(reader, "Email"),
        //                            DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
        //                            ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
        //                        },
        //                        Comments = new List<Comment>()
        //                    };
        //                }
        //                if (DbUtils.IsNotDbNull(reader, "CommentId"))
        //                {
        //                    video.Comments.Add(new Comment()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "CommentId"),
        //                        Message = DbUtils.GetString(reader, "Message"),
        //                        VideoId = id,
        //                        UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
        //                    });
        //                }
        //            }

        //            reader.Close();

        //            return video;
        //        }
        //    }
        //}

        public void Add(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @ImageUrl, @DateCreated)";

                    DbUtils.AddParameter(cmd, "@Title", user.Name);
                    DbUtils.AddParameter(cmd, "@Description", user.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", user.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Url", user.DateCreated);


                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               ImageUrl = @ImageUrl,
                               DateCreated = @DateCreated,
                               
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", user.Name);
                    DbUtils.AddParameter(cmd, "@Description", user.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", user.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Url", user.DateCreated);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
