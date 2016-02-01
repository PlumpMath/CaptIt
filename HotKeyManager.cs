using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CaptIt
{
    public class HotKeyManager
    {
        public static CaptureLib ScreenCapture = new CaptureLib();
        public static List<SShot> Hotkeys = new List<SShot>();

        public HotKeyManager()
        {
            FullScreenShot FSS = new FullScreenShot();
            Hotkeys.Add(FSS);

            DragScreenShot DSS = new DragScreenShot();
            Hotkeys.Add(DSS);

            HotKeyScreenShot HKSS0 = new HotKeyScreenShot(0);
            Hotkeys.Add(HKSS0);

            HotKeyScreenShot HKSS1 = new HotKeyScreenShot(1, Keys.Alt | Keys.D2, Keys.D2 | Keys.Alt | Keys.Control);
            Hotkeys.Add(HKSS1);

            HotKeyScreenShot HKSS2 = new HotKeyScreenShot(2, Keys.Alt | Keys.D3, Keys.D3 | Keys.Alt | Keys.Control);
            Hotkeys.Add(HKSS2);

            HotKeyScreenShot HKSS3 = new HotKeyScreenShot(3, Keys.Alt | Keys.D4, Keys.D4 | Keys.Alt | Keys.Control);
            Hotkeys.Add(HKSS3);

            WindowScreenShot WSS = new WindowScreenShot();
            Hotkeys.Add(WSS);

            foreach (SShot s in Hotkeys)
            {
                if (s as HotKeyScreenShot != null)
                    SaveHotKeySet(s as HotKeyScreenShot);
                else
                    SaveKeySet(s);
            }
        }

        public bool isHotKeyed(Keys k)
        {
            foreach(SShot s in Hotkeys)
            {
                if (s.Key == k) return true;
            }
            return false;
        }

        public SShot CapturHotkeyed(Keys k)
        {
            foreach (SShot s in Hotkeys)
            {
                if (s.Key == k) return s;
            }

            throw new Exception("HotKeyManager CaptureHotKeyed Not Exist in HotKeys List");
        }
        
        public bool isHotKeyHotkeyed(Keys k)
        {
            foreach (SShot s in Hotkeys)
            {
                if (s.isHotKeyScreenShot) if (((HotKeyScreenShot)s).SetKey == k) return true;
            }
            return false;
        }
        
        public HotKeyScreenShot CaptureSetKeyed(Keys k)
        {
            foreach (SShot s in Hotkeys)
            {
                if (s.isHotKeyScreenShot) if (((HotKeyScreenShot)s).SetKey == k) return s as HotKeyScreenShot;
            }

            throw new Exception("HotKeyManager CaptureSetKeyed Not Exist in HotKeys List");
        }

        public static void SaveKeySet(SShot s)
        {
            MainForm.Setting.iniSave.WriteSetting("HotKey", s.shotType, s.Key.ToString());
        }

        public static void SaveHotKeySet(HotKeyScreenShot s)
        {
            MainForm.Setting.iniSave.WriteSetting("HotKey", s.Code.ToString(), s.Key.ToString() + "|" + s.SetKey.ToString());
        }

        public static Keys GetKeySet(SShot s)
        {
            return (Keys)Enum.Parse(typeof(Keys), MainForm.Setting.iniSave.GetSetting("HotKey", s.shotType));
        }

        public static Keys[] GetHotKeySet(HotKeyScreenShot s)
        {
            string[] str = MainForm.Setting.iniSave.GetSetting("HotKey", s.Code.ToString()).Split('|');
            Keys k = (Keys)Enum.Parse(typeof(Keys), str[0]);
            Keys setK = (Keys)Enum.Parse(typeof(Keys), str[1]);
            return new Keys[] { k, setK };
        }

        public static void SaveRect(HotKeyScreenShot s)
        {
            string str = s.Rect.X + "|" + s.Rect.Y + "|" + s.Rect.Width + "|" + s.Rect.Height;
            MainForm.Setting.iniSave.WriteSetting("HotKeyRect", s.Code.ToString(), str);
        }

        public static Rectangle GetRect(HotKeyScreenShot s)
        {
            string[] str = MainForm.Setting.iniSave.GetSetting("HotKeyRect", s.Code.ToString()).Split('|');
            return new Rectangle(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), Convert.ToInt32(str[2]), Convert.ToInt32(str[3]));
        }

        public static void ShowCheckCapturedForm(Image img, string path)
        {
            if (MainForm.Setting.isShowCheckCapturedForm)
            {
                bool autosave = false;
                if (MainForm.Setting.isSaveAutoAfterCaptureSShot)
                {
                    autosave = true;
                }
                SShotCaptured s = new SShotCaptured(img, autosave, path);
                s.Show();
            }
        }

        public static void SaveImage(Image img)
        {
            string path = string.Empty;
            try
            {
                path = MainForm.Setting.AutoSavePath + "\\" + MainForm.Setting.AutoSaveFileName + " " + MainForm.Setting.GETAutoSaveFileNumber();
                System.Drawing.Imaging.ImageFormat imgF;
                switch(MainForm.Setting.ImageSaveFormat)
                {
                    case ImageSaveFormat.PNG:
                        {
                            imgF = System.Drawing.Imaging.ImageFormat.Png;
                            path += ".png";
                            break;
                        }
                    case ImageSaveFormat.JPG:
                        {
                            imgF = System.Drawing.Imaging.ImageFormat.Jpeg;
                            path += ".jpg";
                            break;
                        }
                    case ImageSaveFormat.BMP:
                        {
                            imgF = System.Drawing.Imaging.ImageFormat.Bmp;
                            path += ".bmp";
                            break;
                        }
                }
                img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.TargetSite.ToString());
            }
            ShowCheckCapturedForm(img, path);
        }
    }

    public abstract class SShot
    {
        public abstract string shotType { get; }
        public abstract void CaptureSShot();
        public abstract Keys Key { get; set; }
        public bool isHotKeyScreenShot = false;
    }

    public class FullScreenShot : SShot
    {
        public override string shotType { get { return "FullSShot"; } }

        private Keys key = Keys.F12;
        public override Keys Key { get { return key; }
            set { key = value; } }

        public FullScreenShot()
        {
            try
            {
                this.key = HotKeyManager.GetKeySet(this);
            }
            catch { }
        }

        ~FullScreenShot() { HotKeyManager.SaveKeySet(this); }

        public override void CaptureSShot()
        {
            Image capturedSS = HotKeyManager.ScreenCapture.CaptureScreen();

            HotKeyManager.SaveImage(capturedSS);

            if (MainForm.Setting.isShowEditorAfterCaptureSShot)
            {
                ImageEdit editor = new ImageEdit(capturedSS);
                editor.ShowForm();
            }

            if (capturedSS != null) capturedSS.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public class DragScreenShot : SShot
    {
        public override string shotType { get { return "DragSShot"; } }
        private Keys key = Keys.F12 | Keys.Shift;
        public override Keys Key { get { return key; } set { key = value; } }
        bool isDragMode = false;

        public DragScreenShot()
        {
            try
            {
                this.key = HotKeyManager.GetKeySet(this);
            }
            catch { }
        }
        ~DragScreenShot()
        {
            HotKeyManager.SaveKeySet(this);
        }

        public override void CaptureSShot()
        {
            if (isDragMode) return;
            isDragMode = true;

            dragForm d = new dragForm();
            d.dragFinished += (rect, capturedSS) =>
            {
                HotKeyManager.SaveImage(capturedSS);
                if (MainForm.Setting.isShowEditorAfterCaptureSShot)
                {
                    ImageEdit editor = new ImageEdit(capturedSS);
                    editor.ShowForm();
                }

                isDragMode = false;

                if (capturedSS != null) capturedSS.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            };
            d.dragCanceled += () => { isDragMode = false; };
            d.ShowDialog();
        }
    }

    public class HotKeyScreenShot : SShot
    {
        public override string shotType { get { return "HotKeySShot"; } }
        
        public HotKeyScreenShot(int code)
        {
            this.isHotKeyScreenShot = true;
            try
            {
                Keys[] keys = HotKeyManager.GetHotKeySet(this);
                this.key = keys[0]; this.setKey = keys[1];
            }
            catch { }
            this.Rect = HotKeyManager.GetRect(this);
            this.Code = code;
        }

        public HotKeyScreenShot(int code, Keys key, Keys setKey)
        {
            this.isHotKeyScreenShot = true;
            try
            {
                Keys[] keys = HotKeyManager.GetHotKeySet(this);
                this.key = keys[0]; this.setKey = keys[1];
            }
            catch
            {
                this.key = key;
                this.setKey = setKey;
            }
            this.Rect = HotKeyManager.GetRect(this);
            this.Code = code;
        }

        ~HotKeyScreenShot()
        {
            HotKeyManager.SaveHotKeySet(this);
        }

        public int Code;

        private Keys key = Keys.D1 | Keys.Alt;
        public override Keys Key { get { return key; }
            set { key = value; } }
        public Rectangle Rect { get; set; }

        private Keys setKey = Keys.D1 | Keys.Alt | Keys.Control;
        public Keys SetKey { get { return setKey; } set { setKey = value; } }

        public override void CaptureSShot()
        {
            if (Rect == null)
            {
                MessageBox.Show("올바르지 않은 영역이 맵핑되어 있습니다!");
                return;
            }

            this.Rect = CaptureLib.InitializedSize(CaptureLib.fullScreensSize(), Rect);
            Image capturedSS = HotKeyManager.ScreenCapture.CaptureScreen(Rect);

            HotKeyManager.SaveImage(capturedSS);

            if (MainForm.Setting.isShowEditorAfterCaptureSShot)
            {
                ImageEdit editor = new ImageEdit(capturedSS);
                editor.ShowForm();
            }

            if (capturedSS != null) capturedSS.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        bool isDragMode = false;

        public void SetRect()
        {
            if (isDragMode) return;
            isDragMode = true;

            dragForm d = new dragForm();
            d.dragFinished += (rect, img) =>
            {
                this.Rect = rect;
                isDragMode = false;
                HotKeyManager.SaveRect(this);
                img.Dispose();
            };
            d.dragCanceled += () => { isDragMode = false; };
            d.ShowDialog();
        }
    }

    public class WindowScreenShot : SShot
    {
        public override string shotType { get { return "WindowScreenShot"; } }
        private Keys key = Keys.F12 | Keys.Alt | Keys.Control;
        public override Keys Key
        {
            get { return key; }
            set { key = value; }
        }
        //private Keys setKey = Keys.D1 | Keys.Alt | Keys.Control;
        //public Keys SetKey { get { return setKey; } set { setKey = value; } }

        public IntPtr windowHandle;

        public override void CaptureSShot()
        {
            SetWindowHandle();
            Image capturedSS = HotKeyManager.ScreenCapture.CaptureWindow(windowHandle);

            HotKeyManager.SaveImage(capturedSS);

            if (MainForm.Setting.isShowEditorAfterCaptureSShot)
            {
                ImageEdit editor = new ImageEdit(capturedSS);
                editor.ShowForm();
            }

            if (capturedSS != null) capturedSS.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void SetWindowHandle()
        {
            GetWindowProcess form = new GetWindowProcess();
            //form.GotHandle += (i) => { this.windowHandle = i; };
            form.ShowDialog();
        }
    }
}