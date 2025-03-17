using Autodesk.Forge.Oss.DesignAutomation.Attributes;

namespace ricaun.RevitTest.DA.Console
{
    public class ParameterOptions
    {
        [ParameterInput("input.zip")]
        public string Input { get; set; }

        [ParameterOutput("output.json")]
        public OutputModel Output { get; set; }

        [ParameterOutput("output.zip", DownloadFile = false)]
        public string OutputZip { get; set; }

        [ParameterActivityLanguage]
        public string Language { get; set; }

        [ParameterWorkItem3LeggedToken]
        internal string AccessToken { get; set; }
    }
}