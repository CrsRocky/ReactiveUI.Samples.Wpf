namespace ReactiveUI.Samples.Wpf.Dtos
{
    public class PeopleDto : ReactiveObject
    {
        private string name;
        private int age;
        private string sex;
        private string phone;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public int Age
        {
            get => age;
            set => this.RaiseAndSetIfChanged(ref age, value);
        }

        public string Sex
        {
            get => sex;
            set => this.RaiseAndSetIfChanged(ref sex, value);
        }

        public string Phone
        {
            get => phone;
            set => this.RaiseAndSetIfChanged(ref phone, value);
        }
    }
}