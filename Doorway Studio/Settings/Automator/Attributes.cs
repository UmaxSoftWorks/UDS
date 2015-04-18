﻿using System;

namespace Settings.Automator
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GlobalizedPropertyAttribute : Attribute
    {
        private String resourceName = "";
        private String resourceDescription = "";
        private String resourceTable = "";
        private String resourceCategory = "";

        public GlobalizedPropertyAttribute(String name)
        {
            resourceName = name;
        }

        public String Name
        {
            get { return resourceName; }
            set { resourceName = value; }
        }

        public String Description
        {
            get { return resourceDescription; }
            set { resourceDescription = value; }
        }

        public String Table
        {
            get { return resourceTable; }
            set { resourceTable = value; }
        }

        public String Category
        {
            get { return resourceCategory; }
            set { resourceCategory = value; }
        }
    }
}
