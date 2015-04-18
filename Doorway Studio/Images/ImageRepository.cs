using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Doorway_Studio.Helpers;

namespace Doorway_Studio.Images
{
    class ImageRepository
    {
        public List<Image> Images { get; protected set; }

        protected ImageRepositoryContext Context { get; set; }

        public ImageRepository(ImageRepositoryContext Context)
        {
            this.Context = Context;

            Images = new List<Image>();
        }

        public string ReplaceTokens(string Content)
        {
            int startPosition = 0;

            // [IMAGE]
            if (MainSettings.Debug)
            {
                this.Context.Log.Append(DateTime.Now.ToString() + " Working on: [IMAGE]...\r\n");
            }

            if (Content.Contains("[IMAGE]"))
            {
                if (this.Images.Count == 0)
                {
                    Content = Content.Replace("[IMAGE]", string.Empty);
                }
                else
                {
                    List<string> imagesOnPage = new List<string>();
                    while (Content.Contains("[IMAGE]"))
                    {
                        startPosition = Content.IndexOf("[IMAGE]");
                        Content = Content.Remove(startPosition, 7);

                        string imageUrl = string.Empty;

                        while (string.IsNullOrEmpty(imageUrl))
                        {
                            var image = this.Images.FirstOrDefault(i => !i.Used);

                            imageUrl = image == null ? this.Images[Context.Random.Next(this.Images.Count)].Path : image.Path;

                            while (imagesOnPage.Count != this.Images.Count && imagesOnPage.Contains(imageUrl))
                            {
                                imageUrl = this.Images[Context.Random.Next(this.Images.Count)].Path;
                            }

                            if (!imagesOnPage.Contains(imageUrl))
                            {
                                imagesOnPage.Add(imageUrl);
                            }
                        }

                        this.Images.Where(i => i.Path == imageUrl).ToList().ForEach(i => i.Used = true);

                        if (!imageUrl.StartsWith("http"))
                        {
                            imageUrl = imageUrl.Replace("\\", "/");
                            if (!imageUrl.StartsWith("/"))
                            {
                                if (!this.Context.Settings.LinksRelativeURLs && this.Context.Settings.GeneralDoorwayUrls.Length > this.Context.SiteIndex)
                                {
                                    imageUrl = this.Context.Settings.GeneralDoorwayUrls[this.Context.SiteIndex] + imageUrl;
                                }
                                else
                                {
                                    imageUrl = imageUrl.MakeURLRelative();
                                }
                            }
                        }

                        Content = Content.Insert(startPosition, imageUrl);
                    }
                }
            }

            // [RIMAGE]
            if (MainSettings.Debug)
            {
                this.Context.Log.Append(DateTime.Now.ToString() + " Working on: [RIMAGE]...\r\n");
            }

            if (Content.Contains("[RIMAGE]"))
            {
                if (this.Images.Count == 0)
                {
                    Content = Content.Replace("[RIMAGE]", string.Empty);
                }
                else
                {
                    List<int> imagesOnPage = new List<int>();

                    while (Content.Contains("[RIMAGE]"))
                    {
                        startPosition = Content.IndexOf("[RIMAGE]");
                        Content = Content.Remove(startPosition, 8);

                        // Check to don't allow the same images on one page
                        int selectedImage = this.Context.Random.Next(this.Images.Count);
                        if (imagesOnPage.Count < this.Images.Count)
                        {
                            if (!imagesOnPage.Contains(selectedImage))
                            {
                                imagesOnPage.Add(selectedImage);
                            }
                            else
                            {
                                while (imagesOnPage.Contains(selectedImage))
                                {
                                    selectedImage = this.Context.Random.Next(this.Images.Count);
                                }

                                imagesOnPage.Add(selectedImage);
                            }
                        }

                        string imageUrl = this.Images[selectedImage].Path;
                        this.Images[selectedImage].Used = true;

                        if (!imageUrl.StartsWith("http"))
                        {
                            imageUrl = imageUrl.Replace("\\", "/");
                            if (!imageUrl.StartsWith("/"))
                            {
                                if (!this.Context.Settings.LinksRelativeURLs && this.Context.Settings.GeneralDoorwayUrls.Length > this.Context.SiteIndex)
                                {
                                    imageUrl = this.Context.Settings.GeneralDoorwayUrls[this.Context.SiteIndex] + imageUrl;
                                }
                                else
                                {
                                    imageUrl = imageUrl.MakeURLRelative();
                                }
                            }
                        }

                        Content = Content.Insert(startPosition, imageUrl);
                    }
                }
            }

            return Content;
        }

        int currentImageName;
        string[] imageNames;
        private void GenerateImages(string TemplateFolder)
        {
            if (this.Context.Settings.GeneralGenerateImagesCount == 0)
            {
                return;
            }

            try
            {
                // Определение количество фаЙлов для генерирования
                int imagesCount = this.Context.Settings.GeneralGenerateImagesCount;

                this.Context.Log.AppendLine(DateTime.Now.ToString() + " Images to generate: " + imagesCount.ToString());

                // Создание подпапки
                if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count > 0)
                {
                    if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length).Contains("\\"))
                    {
                        string tempFolder = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length);
                        while (tempFolder.Contains("\\"))
                        {
                            string directory = this.Context.SiteDirectory + tempFolder.Substring(0, tempFolder.IndexOf("\\"));
                            Directory.CreateDirectory(directory);
                            File.SetAttributes(directory, FileAttributes.Normal);
                            IOHelper.SetFileDirectoryDate(directory, Context.Random, Context.Settings);
                            tempFolder = tempFolder.Substring(0 + tempFolder.IndexOf("\\") + 1);
                        }
                    }
                }

                ImageBuilder image = null;

                // Генерирование файлов
                for (int i = 0; i < imagesCount; i++)
                {
                    try
                    {
                        // Генерирование
                        if (Context.Random.Next(0, 100) > 50)
                        {
                            image = new CaptchaImageOne(StringHelper.MakeRandomString(3, 6, this.Context.Random),
                                Context.Random.Next(this.Context.Settings.GeneralImageSizeMinWidth, this.Context.Settings.GeneralImageSizeMaxWidth),
                                Context.Random.Next(this.Context.Settings.GeneralImageSizeMinHeight, this.Context.Settings.GeneralImageSizeMaxHeight));
                        }
                        else
                        {
                            image = new CaptchaImageTwo(StringHelper.MakeRandomString(3, 6, this.Context.Random),
                                Context.Random.Next(this.Context.Settings.GeneralImageSizeMinWidth, this.Context.Settings.GeneralImageSizeMaxWidth),
                                Context.Random.Next(this.Context.Settings.GeneralImageSizeMinHeight, this.Context.Settings.GeneralImageSizeMaxHeight));
                        }

                        // Созание имени для файла
                        string filename = this.Context.SiteDirectory;
                        if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count > 0)
                        {
                            if (!SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].StartsWith("http"))
                            {
                                filename += SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length);
                                filename = filename.Substring(0, filename.LastIndexOf("\\") + 1);
                            }
                        }

                        // Генерирование нового имени
                        filename += this.MakeImageName();

                        // Сохранение
                        try
                        {
                            this.Context.Log.AppendLine(DateTime.Now.ToString() + " Generating image: " + filename);
                            image.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                            IOHelper.SetFileDirectoryDate(filename, Context.Random, Context.Settings);
                        }
                        catch (Exception) { }

                        this.Images.Add(new Image(filename.Substring(this.Context.SiteDirectory.Length)));
                    }
                    catch (Exception)
                    {
                    }
                }

                this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000447") + imagesCount.ToString() + View.UILanguageResources.GetString("S0000446") + ".\r\n");
            }
            catch (Exception)
            {
            }
        }

        private string MakeImageName()
        {
            string imagefilename = string.Empty;
            if (this.Context.Settings.GeneralImageNamingType != 0)
            {
                if (imageNames == null)
                {
                    try
                    {
                        imageNames = File.ReadAllLines(this.Context.Settings.GeneralImageNamingFile, Encoding.Default);
                        if (imageNames.Length == 0)
                        {
                            this.Context.Settings.GeneralImageNamingType = 0;
                        }
                    }
                    catch (Exception)
                    {
                        this.Context.Settings.GeneralImageNamingType = 0;
                    }
                }
            }

            if (this.Context.Settings.GeneralImageNamingType == 0)
            {
                imagefilename = StringHelper.MakeRandomString(5, 14, Context.Random) + ".jpg";
            }
            else
            {
                imagefilename = (this.Context.Settings.GeneralImageNamingType == 1 ? imageNames[currentImageName] : imageNames[currentImageName].Translit()) + ".jpg";

                currentImageName++;
                if (currentImageName >= imageNames.Length)
                {
                    currentImageName = 0;
                }
            }

            //Checking for duplicate of filename
            while (CheckImageFileNameExist(imagefilename))
            {
                if (this.Context.Settings.GeneralImageNamingType == 0)
                {
                    imagefilename = StringHelper.MakeRandomString(5, 14, this.Context.Random) + ".jpg";
                }
                else
                {
                    imagefilename = (this.Context.Settings.GeneralImageNamingType == 1 ? imageNames[currentImageName] : imageNames[currentImageName].Translit()) + ".jpg";

                    currentImageName++;
                    if (currentImageName >= imageNames.Length)
                    {
                        currentImageName = 0;
                    }
                }
            }

            return imagefilename;
        }

        private bool CheckImageFileNameExist(string FileName)
        {
            bool exists = false;

            for (int i = 0; i < this.Images.Count; i++)
            {
                if (this.Images[i].Path.EndsWith(FileName))
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        private void ModifyImages(string TemplateFolder)
        {
            try
            {
                // Определение количество фаЙлов для генерирования
                int imagesCount = this.Context.Settings.GeneralGenerateImagesCount;

                this.Context.Log.AppendLine(DateTime.Now.ToString() + " Images to modify: " + imagesCount.ToString());
                // Создание подпапки
                if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count > 0)
                {
                    if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length).Contains("\\"))
                    {
                        string tempFolder = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length);
                        while (tempFolder.Contains("\\"))
                        {
                            string directory = this.Context.SiteDirectory + tempFolder.Substring(0, tempFolder.IndexOf("\\"));
                            Directory.CreateDirectory(directory);
                            File.SetAttributes(directory, FileAttributes.Normal);
                            IOHelper.SetFileDirectoryDate(directory, Context.Random, Context.Settings);
                            tempFolder = tempFolder.Substring(0 + tempFolder.IndexOf("\\") + 1);
                        }
                    }
                }
                ImageBuilder image;
                //Модифицирование файлов
                for (int i = 0; i < imagesCount; i++)
                {
                    try
                    {
                        //Модифицирование
                        image = new ImageModifier(StringHelper.MakeRandomString(3, 6, Context.Random),
                                                  SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[
                                                      Context.Random.Next(SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count)]);

                        //Создание имени для файла
                        string filename = this.Context.SiteDirectory;
                        if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count > 0)
                        {
                            if (!SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].StartsWith("http"))
                            {
                                filename += SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length);
                                filename = filename.Substring(0, filename.LastIndexOf("\\") + 1);
                            }
                        }

                        // Генерирование нового имени
                        string imagefilename = StringHelper.MakeRandomString(5, 14, Context.Random) + ".jpg";
                        //Checking for duplicate of filename
                        while (CheckImageFileNameExist(imagefilename))
                        {
                            imagefilename = StringHelper.MakeRandomString(5, 14, Context.Random) + ".jpg";
                        }

                        filename += imagefilename;

                        // Сохранение
                        this.Context.Log.AppendLine(DateTime.Now.ToString() + " Modifying image: " + SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[i]);
                        image.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        IOHelper.SetFileDirectoryDate(filename, Context.Random, Context.Settings);
                        this.Images.Add(new Image(filename.Substring(this.Context.SiteDirectory.Length)));
                    }
                    catch (Exception)
                    {
                    }
                }
                this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000448") + imagesCount.ToString() + View.UILanguageResources.GetString("S0000446") + ".\r\n");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Копирование/генерирование/спарсивание картинок
        /// </summary>
        public void MakeImages()
        {
            // Генерирование имени папки, где лежат файлы
            string templateFolder = string.Empty;
            string temp = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].ID.ToString();
            while (temp.Length < 7)
            {
                temp = "0" + temp;
            }

            templateFolder = System.Windows.Forms.Application.StartupPath + "\\Data\\" + temp + "\\Templates\\";
            temp = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].ID.ToString();
            while (temp.Length < 7)
            {
                temp = "0" + temp;
            }

            templateFolder += temp + "\\Files\\";
            temp = string.Empty;

            switch (this.Context.Settings.GeneralImageType)
            {
                // Копирование файлов
                case 0:
                case 1:
                    {
                        this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000441") + "\r\n");
                        this.CopyImages(templateFolder);
                        break;
                    }

                // Генерирование
                case 2:
                    {
                        this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000442") + "\r\n");
                        this.GenerateImages(templateFolder);
                        break;
                    }

                // Копирование и генерирование
                case 3:
                case 4:
                    {
                        this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000441") + "\r\n");
                        this.CopyImages(templateFolder);
                        this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000442") + "\r\n");
                        this.GenerateImages(templateFolder);
                        break;
                    }

                // Модифицирование шаблонных картинок
                case 5:
                    {
                        this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000442") + "\r\n");
                        if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count > 0)
                        {
                            this.ModifyImages(templateFolder);
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Копирование картинок
        /// </summary>
        private void CopyImages(string TemplateFolder)
        {
            int imagesCount = this.Context.Settings.GeneralGenerateImagesCount == 0
                              ? SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count
                              : this.Context.Settings.GeneralGenerateImagesCount;

            this.Context.Log.AppendLine(DateTime.Now.ToString() + " Images to copy: " + imagesCount.ToString());

            for (int i = 0; i < imagesCount; i++)
            {
                try
                {
                    int imageIndex = this.Context.Settings.GeneralGenerateImagesCount == 0
                                         ? i
                                         : this.Context.Random.Next(SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images.Count);

                    if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].StartsWith("http"))
                    {
                        this.Images.Add(new Image(SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex]));
                    }
                    else
                    {
                        // Создание папок
                        if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].Substring(TemplateFolder.Length).Contains("\\"))
                        {
                            string fileFolder = this.Context.SiteDirectory;
                            string tempFolder = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].Substring(TemplateFolder.Length);
                            while (tempFolder.Contains("\\"))
                            {
                                string directory = Path.Combine(fileFolder, tempFolder.Substring(0, tempFolder.IndexOf("\\")));
                                Directory.CreateDirectory(directory);
                                File.SetAttributes(directory, FileAttributes.Normal);
                                IOHelper.SetFileDirectoryDate(directory, Context.Random, Context.Settings);
                                fileFolder = Path.Combine(fileFolder, tempFolder.Substring(0, tempFolder.IndexOf("\\")));
                                tempFolder = tempFolder.Substring(0 + tempFolder.IndexOf("\\") + 1);
                            }
                        }

                        // Создание имени для файла
                        string filename = this.Context.SiteDirectory;
                        if (this.Context.Settings.GeneralImageType == 1)
                        {
                            if (SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].StartsWith("http"))
                            {
                                filename = SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex];
                            }
                            else
                            {
                                filename += SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[0].Substring(TemplateFolder.Length);
                                filename = filename.Substring(0, filename.LastIndexOf("\\") + 1);
                                filename += this.MakeImageName();
                            }
                        }
                        else
                        {
                            filename += SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].Substring(TemplateFolder.Length);
                        }

                        // Копирование файла
                        if (!SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex].StartsWith("http"))
                        {
                            this.Context.Log.AppendLine(DateTime.Now.ToString() + " Copying image: " + SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex]);
                            File.Copy(SharedData.WorkSpaces[this.Context.WorkSpaceIndex].Templates[this.Context.TemplateIndex].Images[imageIndex], filename);
                            IOHelper.SetFileDirectoryDate(filename, Context.Random, Context.Settings);
                        }

                        this.Images.Add(new Image(filename.Substring(this.Context.SiteDirectory.Length)));
                    }
                }
                catch (Exception) { }
            }

            this.Context.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000445") + this.Images.Count.ToString() + View.UILanguageResources.GetString("S0000446") + ".\r\n");
        }

        /// <summary>
        /// Delete unused images
        /// </summary>
        public void DeleteUnusedImages()
        {
            for (int i = 0; i < this.Images.Count; i++)
            {
                if (!this.Images[i].Used)
                {
                    try
                    {
                        this.Context.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000521") + Path.Combine(this.Context.SiteDirectory, this.Images[i].Path));
                        File.Delete(Path.Combine(this.Context.SiteDirectory, this.Images[i].Path));
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
