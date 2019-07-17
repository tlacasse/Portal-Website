﻿using Portal.App.Portal.Models;
using Portal.Data.Storage;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using System;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconUploadRequest : DependentBase, IRequest<Icon, Void> {

        public static readonly int MAX_ICON_MB = 10;
        public static readonly string SAVE_PATH_TEMPLATE = "App_Data/Icons/{0}.{1}";

        private IFileReceiver FileReceiver { get; }

        public IconUploadRequest(IWebsiteState WebsiteState, IDatabaseFactory DatabaseFactory,
                IFileReceiver FileReceiver) : base(WebsiteState, DatabaseFactory) {
            this.FileReceiver = FileReceiver;
        }

        public Void Process(Icon model) {
            model.ValidateData();

            // Force DB name to be correctly formatted
            model.Name = PortalUtility.UnUrlFormat(PortalUtility.UrlFormat(model.Name));

            IPostedFile file = GetAndCheckFile(model);

            using (IDatabase database = DatabaseFactory.Create()) {
                CheckAgainstExistingIcons(database, model);
                Icon newIcon = UpdateDatabase(database, model);
                SaveFile(file, newIcon);
            }

            return null;
        }

        private IPostedFile GetAndCheckFile(Icon model) {
            IPostedFile file = FileReceiver.GetPostedFiles().FirstOrDefault();
            if (model.IsNew && file == null) {
                throw new ArgumentNullException("Icon Image File");
            }
            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new ArgumentOutOfRangeException("Image Upload",
                    string.Format("Is too large (limit {0}MB)", MAX_ICON_MB));
            }
            return file;
        }

        private void CheckAgainstExistingIcons(IDatabase database, Icon model) {
            Icon existing = database.GetIconByName(model.Name);
            if (existing != null && existing.Id != model.Id) {
                throw new PortalException(string.Format("Icon Name '{0}' already exists", model.Name));
            }
            if (model.IsNew == false) {
                existing = database.GetIconById(model.Id);
                if (existing == null) {
                    throw new PortalException(string.Format("Icon with Id {0} does not exist", model.Id));
                }
                model.Image = model.Image ?? existing.Image; // make sure history has image
            }
        }

        private Icon UpdateDatabase(IDatabase database, Icon model) {
            model.DateChanged = DateTime.Now;
            if (model.IsNew) {
                model.DateCreated = DateTime.Now;
                database.Insert(model);
            } else {
                database.Update(model);
            }
            database.Commit();

            Icon newIcon = database.GetIconByName(model.Name);
            if (newIcon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found after added", model.Name));
            }

            IconHistory history = newIcon.ToHistory();
            database.Insert(history);

            return newIcon;
        }

        private void SaveFile(IPostedFile file, Icon newIcon) {
            if (file != null) {
                file.SaveAs(WebsiteState.GetPath(
                    string.Format(SAVE_PATH_TEMPLATE, newIcon.Id, newIcon.Image)
                ));
            }
        }

    }

}