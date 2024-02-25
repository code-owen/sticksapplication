namespace SticksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BlogPostId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.BlogPostId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        PageTitle = c.String(),
                        Content = c.String(),
                        ShortDescription = c.String(),
                        FeaturedImageUrl = c.String(),
                        UrlHandle = c.String(),
                        PublishedDate = c.DateTime(nullable: false),
                        Author = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagBlogPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        BlogPost_BlogPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.BlogPost_BlogPostId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.BlogPost_BlogPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagBlogPosts", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.TagBlogPosts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.Comments", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.TagBlogPosts", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.TagBlogPosts", new[] { "Tag_TagId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comments", new[] { "BlogPostId" });
            DropTable("dbo.TagBlogPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.Comments");
        }
    }
}
