﻿using MyProject.Core.Entities.Content;
using MyProject.Core.Helper;

namespace MyProject.Web.Client.Modules.Articles.Models
{
    public class Article : Node
    {
        public Article() : base()
        {
            Module = Constants.ArticlesModule;
            Type = Constants.ArticleType;
            CustomFields = new Core.Entities.Common.EntityCustomFields();
        }

        public static Article Create(Node node)
        {
            return node.ConvertTo<Article>();
        }

        public string Summary
        {
            get => CustomFields.CustomField1;
            set => CustomFields.CustomField1 = value;
        }
    }
}
