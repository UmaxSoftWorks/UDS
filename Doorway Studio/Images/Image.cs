namespace Doorway_Studio.Images
{
    class Image
    {
        public Image(string Path)
        {
            this.Path = Path;
            Used = false;
        }

        public string Path { get; set; }
        public bool Used { get; set; }
    }
}
