namespace MyDIContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Kernel kernel = new Kernel();
            
            Durak durak = new Durak();
            PropertiesSetter propertiesChooser = new PropertiesSetter();
            Mate mate = new Mate("Igor");
            kernel.RegisterWithProperty<Durak>("dfsd", mate);

            propertiesChooser.SetProperties(typeof(Durak), durak);
        }
    }

    class Durak
    {
        [Inject]
        public Mate MyProperty { get; set; }

        [Inject]
        public int Prop2 { get; set; }
    }

    class Mate
    {
        public string name;

        public Mate(string n)
        {
            name = n;
        }
    }
}
