namespace UserManager
{
    public struct OldestUserName
    {
        public string UserName { get; }
        public int Age { get;  }

        public OldestUserName(string userName,int age)
        {
            UserName = userName;
            Age = age;
        }
    }
}
