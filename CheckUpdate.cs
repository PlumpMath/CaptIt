using System.Net;

namespace CaptIt
{
    public class UpdateChecker
    {
        private const string link = "https://www.dropbox.com/s/0vub4qbzazzxjwr/version.txt?dl=1";
        private string AppName = string.Empty;

        public UpdateChecker(string name)
        {
            this.AppName = name;
        }

        public VERSION Get()
        {
            WebClient w = new WebClient();
            w.Encoding = System.Text.Encoding.Default;
            string result = w.DownloadString(new System.Uri(link));
            string[] apps = result.Split('$');

            VERSION v_ = new VERSION() { SUCCEED = false };
            v_.AppName = this.AppName;

            int temp = 0;
            foreach (string app in apps)
            {
                string[] lines = app.Split(new string[] { "\r\n"}, System.StringSplitOptions.None);
                if (lines[0] != AppName) continue;
                foreach (string line in lines)
                {
                    switch(temp)
                    {
                        case 0:
                            break;
                        case 1:
                            v_.Version = (float)System.Convert.ToDouble(line);
                            break;
                        case 2:
                            v_.DownloadLink = line;
                            break;
                        default:
                            v_.Description += line + System.Environment.NewLine;
                            break;
                    }

                    temp++;
                }
                v_.SUCCEED = true;
            }
            return v_;
        }
    }

    public struct VERSION
    {
        public bool SUCCEED;
        public string AppName;
        public float Version;
        public string DownloadLink;
        public string Description;
    }
}
