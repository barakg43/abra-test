namespace UserManager
{
    public  class UserData
    {

        public int Id { public set; public get; }
        public string Name { public set; public get; }
        public string Email { public set; public get; }
        public string Gender { public set; public get; }
        public string Country { public set; public get; }
        public string Phone { public set; public get; }

        public UserData(int i_Id, string i_Name, string i_Email, string i_Gender, string i_Country, string i_Phone)
        {
            Id = i_Id;
            Name = i_Name;
            Email = i_Email;
            Gender = i_Gender;
            Country = i_Country;
            Phone = i_Phone;
        }
    }

}