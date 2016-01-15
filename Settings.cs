﻿using System;

namespace CaptIt
{
    public enum ImageSaveFormat
    {
        PNG, JPG, BMP
    }

    public class Settings
    {
        public INISave iniSave;

        #region ShowEditor
        public bool isShowEditorAfterCaptureSShot;
        #endregion

        #region AutoSave
        public bool isSaveAutoAfterCaptureSShot;
        public string AutoSavePath;
        public int AutoSaveFileNumber;
        public string AutoSaveFileName;
        public ImageSaveFormat ImageSaveFormat;
        #endregion

        public bool isShowCheckCapturedForm;

        public Settings()
        {
            #region Initialize
            if (Properties.Settings.Default.AutoSavePath == "NON_SET" || Properties.Settings.Default.AutoSavePath == @"C:\Users\arss3\OneDrive\Documents") // 초기 자동저장 패스를 내 문서로
            {
                Properties.Settings.Default.AutoSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Properties.Settings.Default.Save();
            }
            #endregion

            LoadSettings();

            iniSave = new INISave(Environment.CurrentDirectory + "\\settings.ini");
        }

        public void LoadSettings()
        {
            this.isShowEditorAfterCaptureSShot = Properties.Settings.Default.isShowEditorAfterCaptureSShot;
            this.isSaveAutoAfterCaptureSShot = Properties.Settings.Default.isSaveAutoAfterCaptureSShot;
            this.AutoSavePath = Properties.Settings.Default.AutoSavePath;
            this.AutoSaveFileNumber = Properties.Settings.Default.AutoSaveFileNumber;
            this.AutoSaveFileName = Properties.Settings.Default.AutoSaveFileName;
            this.isShowCheckCapturedForm = Properties.Settings.Default.isShowCheckCapturedForm;
            this.ImageSaveFormat = (ImageSaveFormat)Enum.Parse(typeof(ImageSaveFormat), Properties.Settings.Default.ImageSaveFormat);
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.isShowEditorAfterCaptureSShot = this.isShowEditorAfterCaptureSShot;
            Properties.Settings.Default.isSaveAutoAfterCaptureSShot = this.isSaveAutoAfterCaptureSShot;
            Properties.Settings.Default.AutoSavePath = this.AutoSavePath;
            Properties.Settings.Default.AutoSaveFileNumber = this.AutoSaveFileNumber;
            Properties.Settings.Default.AutoSaveFileName = this.AutoSaveFileName;
            Properties.Settings.Default.isShowCheckCapturedForm = this.isShowCheckCapturedForm;
            Properties.Settings.Default.ImageSaveFormat = this.ImageSaveFormat.ToString();
            Properties.Settings.Default.Save();
        }

        public int GETAutoSaveFileNumber()
        {
            for (int j = 1; j <= this.AutoSaveFileNumber; j++)
            {
                string path = AutoSavePath + "\\" + AutoSaveFileName + " " + j.ToString() + "." + ImageSaveFormat.ToString().ToLower();
                if (!System.IO.File.Exists(path))
                {
                    AutoSaveFileNumber = j;
                    break;
                }
            }

            while(System.IO.File.Exists(AutoSavePath + "\\" + AutoSaveFileName + " " + this.AutoSaveFileNumber.ToString() + "." + ImageSaveFormat.ToString()))
            {
                AutoSaveFileNumber++;
            }

            int i = this.AutoSaveFileNumber++;
            SaveSettings();
            return i;
        }
    }
}