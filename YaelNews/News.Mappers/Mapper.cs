using News.DbModel;

namespace News.Mappers
{
    public class Mapper
    {
        public static DbModel.Item ItemFromModel(News.Model.Item item)
        {
            return new DbModel.Item()
            {
                Content   = item.Content  ,
                CreatedOn = item.CreatedOn,
                Id        = item.ID       ,
                Image     = item.Image    ,
                IsDeleted = item.IsDeleted,
                SourceId  = item.SourceID ,
                SubjectId = item.SubjectId,
                Title     = item.Title    ,
                Writer    = item.Writer   ,
                Link      = item.Link
            };
        }

        public static Model.Item ItemToModel(News.DbModel.Item item)
        {
            return new Model.Item()
            {
                Content = item.Content,
                CreatedOn = item.CreatedOn,
                ID = item.Id,
                Image = item.Image,
                IsDeleted = item.IsDeleted,
                SourceID = item.SourceId,
                SubjectId = item.SubjectId,
                Title = item.Title,
                Writer = item.Writer,
                SubjectName = item.Subject?.Name,
                SourceName = item.Source.Name,
                Link = item.Link
            };
        }

        public static Model.Subject SubjectToModel(Subject x)
        {
            return new Model.Subject()
            {
                ID = x.Id,
                Name = x.Name,
                ShowInMenu = x.ShowInMenu,
                ShowInNewItem = x.ShowInNewItem,
                Sort = x.Sort
            };
        }

        public static Model.Source SourceToModel(Source x)
        {
            return new Model.Source()
            {
                Id      = x.Id     ,
                Name    = x.Name   ,
                BaseUrl = x.BaseUrl,
                IconUrl = x.IconUrl
            };
        }

        public static DbModel.Source SourceFromModel(Model.Source x)
        {
            return new DbModel.Source()
            {
                Id      = x.Id     ,
                Name    = x.Name   ,
                BaseUrl = x.BaseUrl,
                IconUrl = x.IconUrl
            };
        }

        public static DbModel.Subject SubjectFromModel(News.Model.Subject x)
        {
            return new DbModel.Subject()
            {
                 Id             = x.ID             ,
                 Name           = x.Name           ,
                 ShowInMenu     = x.ShowInMenu     ,
                 ShowInNewItem  = x.ShowInNewItem  ,
                 Sort           = x.Sort           
            };
        }
    }
}